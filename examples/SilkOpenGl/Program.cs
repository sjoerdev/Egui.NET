#pragma warning disable

using System.Diagnostics;
using System.Drawing;
using System.Collections.Immutable;
using Egui;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;
using Silk.NET.Windowing;
using System.Security.Cryptography;
using Silk.NET.GLFW;

namespace MySilkProgram;

public unsafe class Program
{
    private static IWindow _window;

    private static Context _ctx;

    private static GL _gl;

    private static OpenGlPainter _glPainter;

    private static RawInput _input = new RawInput();

    private static Stopwatch _startTimer = new Stopwatch();

    public static void Main(string[] args)
    {
        _ctx = new Context();
        Console.WriteLine($"CReated a ctx! {_ctx.Os}");

        WindowOptions options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);
        options.Title = "My first Silk.NET program!";
        options.API = GraphicsAPI.Default;

        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        _window.Run();
    }

    private static void OnLoad()
    {
        Console.WriteLine("Load!");

        _gl = _window.CreateOpenGLES();

        _glPainter = new OpenGlPainter(_gl);
        _startTimer.Start();

        IInputContext input = _window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
            input.Keyboards[i].KeyDown += KeyDown;
    }

    // These two methods are unused for this tutorial, aside from the logging we added earlier.
    private static void OnUpdate(double deltaTime)
    {
    }

    private static void OnRender(double deltaTime)
    {
        _input.ViewportId = ViewportId.Root;
        _input.Time = _startTimer.Elapsed.TotalSeconds;
        _input.ScreenRect = Rect.FromMinSize(new Pos2(0, 0), new Vec2(_window.Size.X, _window.Size.Y));

        _input.Viewports = _input.Viewports.SetItem(_input.ViewportId, new ViewportInfo
        {
            Parent = null,
            Title = "egui test",
            Events = ImmutableList<ViewportEvent>.Empty,
            NativePixelsPerPoint = 1.0f,
            MonitorSize = null,
            InnerRect = _input.ScreenRect,
        });

        var output = _ctx.Run(_input, ctx =>
        {

        });

        DrawOutput(in output);

        //new CentralPanel().Show(_ctx, ui =>
        {
            //Ui ui = default;
            // todo _ctx.StyleUi(ui);

        }//);
    }

    private static void KeyDown(IKeyboard keyboard, Silk.NET.Input.Key key, int keyCode)
    {

    }

    private static void DrawOutput(in FullOutput output)
    {
        _gl.ClearColor(Color.CornflowerBlue);
        _gl.Clear(ClearBufferMask.ColorBufferBit);

        var clippedPrimitives = _ctx.Tessellate(output.Shapes.ToArray(), 1.0f);
        _glPainter.PaintAndUpdateTextures((uint)_window.Size.X, (uint)_window.Size.Y, 1.0f, clippedPrimitives, output.TexturesDelta);
    }

    private struct OpenGlPainter
    {
        private readonly GL _gl;

        private readonly uint _program;

        private readonly int _u_screen_size;
        private readonly int _u_sampler;

        private readonly int _a_pos_loc;
        private readonly int _a_tc_loc;
        private readonly int _a_srgba_loc;

        private readonly uint _eao;

        private readonly uint _vbo;

        private readonly uint _vao;

        private readonly Dictionary<TextureId, uint> _textures;


        public OpenGlPainter(GL gl)
        {
            _gl = gl;

            var frag = CompileShader(GLEnum.FragmentShader, File.ReadAllText("fragment.glsl"));
            var vert = CompileShader(GLEnum.VertexShader, File.ReadAllText("vertex.glsl"));
            _program = LinkProgram([vert, frag]);
            _u_screen_size = _gl.GetUniformLocation(_program, "u_screen_size");
            _u_sampler = _gl.GetUniformLocation(_program, "u_sampler");

            _vbo = _gl.GenBuffer();

            _a_pos_loc = _gl.GetAttribLocation(_program, "a_pos");
            _a_tc_loc = _gl.GetAttribLocation(_program, "a_tc");
            _a_srgba_loc = _gl.GetAttribLocation(_program, "a_srgba");

            _vao = _gl.GenVertexArray();
            _gl.BindVertexArray(_vao);

            _gl.EnableVertexAttribArray((uint)_a_pos_loc);
            _gl.VertexAttribPointer((uint)_a_pos_loc, 2, GLEnum.Float, false, (uint)sizeof(Vertex), 0);
            _gl.EnableVertexAttribArray((uint)_a_tc_loc);
            _gl.VertexAttribPointer((uint)_a_tc_loc, 2, GLEnum.Float, false, (uint)sizeof(Vertex), 2);
            _gl.EnableVertexAttribArray((uint)_a_srgba_loc);
            _gl.VertexAttribPointer((uint)_a_srgba_loc, 4, GLEnum.UnsignedByte, false, (uint)sizeof(Vertex), 4);

            _gl.BindBuffer(GLEnum.ArrayBuffer, _vbo);

            _eao = _gl.GenBuffer();

            _textures = new Dictionary<TextureId, uint>();
        }

        public readonly void PaintAndUpdateTextures(uint width, uint height, float pixelsPerPoint, ReadOnlyMemory<ClippedPrimitive> clippedPrimitives, TexturesDelta texturesDelta)
        {
            return;
            foreach (var (id, delta) in texturesDelta.Set)
            {
                SetTexture(id, delta);
            }

            PaintPrimitives(width, height, pixelsPerPoint, clippedPrimitives);

            foreach (var id in texturesDelta.Free)
            {
                FreeTexture(id);
            }
        }

        private readonly void PreparePainting(uint width, uint height, float pixelsPerPoint)
        {
            _gl.Enable(GLEnum.ScissorTest);
            _gl.Disable(GLEnum.CullFace);
            _gl.Disable(GLEnum.DepthTest);

            _gl.ColorMask(true, true, true, true);

            _gl.Enable(GLEnum.Blend);
            _gl.BlendEquationSeparate(GLEnum.FuncAdd, GLEnum.FuncAdd);
            _gl.BlendFuncSeparate(GLEnum.One, GLEnum.OneMinusSrcAlpha, GLEnum.OneMinusDstAlpha, GLEnum.One);

            var widthInPoints = width / pixelsPerPoint;
            var heightInPoints = height / pixelsPerPoint;

            _gl.Viewport(0, 0, width, height);
            _gl.UseProgram(_program);

            _gl.Uniform2(_u_screen_size, [widthInPoints, heightInPoints]);
            _gl.Uniform1(_u_sampler, 0);
            _gl.ActiveTexture(GLEnum.Texture0);

            _gl.BindVertexArray(_vao);
            _gl.BindBuffer(GLEnum.ElementArrayBuffer, _eao);
        }

        private readonly void PaintPrimitives(uint width, uint height, float pixelsPerPoint, ReadOnlyMemory<ClippedPrimitive> primitives)
        {
            PreparePainting(width, height, pixelsPerPoint);

            foreach (var primitive in primitives.Span)
            {
                // todo: set clip rect
            }
        }

        public readonly void FreeTexture(TextureId id)
        {
            _textures.Remove(id, out var handle);
            _gl.DeleteTexture(handle);
        }

        public readonly void SetTexture(TextureId id, ImageDelta delta)
        {
            if (!_textures.ContainsKey(id))
            {
                _textures[id] = _gl.GenTexture();
            }

            _gl.BindTexture(GLEnum.Texture2D, _textures[id]);

            ImageData.Color image = default;

            _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GlowCode(delta.Options.Magnification, null));
            _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GlowCode(delta.Options.Minification, delta.Options.MipmapMode));
            _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)GlowCode(delta.Options.WrapMode));
            _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)GlowCode(delta.Options.WrapMode));

            _gl.PixelStore(GLEnum.UnpackAlignment, 1);

            fixed (Color32* data = image.Value.Pixels.ToArray())
            {
                if (delta.Pos is null)
                {
                    _gl.TexImage2D(GLEnum.Texture2D, 0, (int)GLEnum.Rgba8, (uint)image.Value.Size[0], (uint)image.Value.Size[1], 0, GLEnum.Rgba, GLEnum.UnsignedByte,
                        data);
                }
                else
                {
                    _gl.TexSubImage2D(GLEnum.Texture2D, 0, (int)delta.Pos[0], (int)delta.Pos[1], (uint)image.Value.Size[0], (uint)image.Value.Size[1], GLEnum.Rgba, GLEnum.UnsignedByte,
                        data);
                }
            }

            if (delta.Options.MipmapMode.HasValue)
            {
                _gl.GenerateMipmap(GLEnum.Texture2D);
            }
        }

        private readonly uint CompileShader(GLEnum shaderType, string text)
        {
            var shader = _gl.CreateShader(shaderType);
            _gl.ShaderSource(shader, text);
            _gl.CompileShader(shader);

            _gl.GetShader(shader, ShaderParameterName.CompileStatus, out int status);

            if ((GLEnum)status != GLEnum.True)
            {
                throw new Exception("Shader failed to compile: " + _gl.GetShaderInfoLog(shader));
            }

            return shader;
        }

        private readonly uint LinkProgram(ReadOnlySpan<uint> shaders)
        {
            var result = _gl.CreateProgram();
            foreach (var shader in shaders)
            {
                _gl.AttachShader(result, shader);
            }

            _gl.LinkProgram(result);

            _gl.GetProgram(result, ProgramPropertyARB.LinkStatus, out int status);
            if ((GLEnum)status != GLEnum.True)
            {
                throw new Exception("Program failed to link: " + _gl.GetProgramInfoLog(result));
            }

            return result;
        }

        private static GLEnum GlowCode(Egui.TextureWrapMode mode) {
            if (mode == Egui.TextureWrapMode.ClampToEdge)
            {
                return GLEnum.ClampToEdge;
            }
            else if (mode == Egui.TextureWrapMode.MirroredRepeat)
            {
                return GLEnum.MirroredRepeat;
            }
            else
            {
                return GLEnum.Repeat;
            }
        }

        private static GLEnum GlowCode(TextureFilter filter, TextureFilter? mipmapMode)
        {
            if (mipmapMode.HasValue)
            {
                if (mipmapMode.Value == TextureFilter.Linear)
                {
                    if (filter == TextureFilter.Linear)
                    {
                        return GLEnum.LinearMipmapLinear;
                    }
                    else
                    {
                        return GLEnum.NearestMipmapLinear;
                    }
                }
                else
                {
                    if (filter == TextureFilter.Linear)
                    {
                        return GLEnum.LinearMipmapNearest;
                    }
                    else
                    {
                        return GLEnum.NearestMipmapNearest;
                    }
                }
            }
            else
            {
                if (filter == TextureFilter.Linear)
                {
                    return GLEnum.Linear;
                }
                else
                {
                    return GLEnum.Nearest;
                }
            }
        }
    }
}