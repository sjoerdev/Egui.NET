#pragma warning disable

using System.Diagnostics;
using System.Drawing;
using System.Collections.Immutable;

using Egui;
using Egui.Containers;
using Egui.Epaint;
using Egui.Viewport;
using Egui.Widgets;

using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;
using Silk.NET.Windowing;
using System.Security.Cryptography;
using Silk.NET.GLFW;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Numerics;
using Window = Egui.Containers.Window;
using Microsoft.VisualBasic;

namespace MySilkProgram;

public unsafe class Program
{
    private static IWindow _window;

    private static Context _ctx;

    private static GL _gl;

    private static OpenGlPainter _glPainter;

    private static RawInput _input = new RawInput();

    private static Stopwatch _startTimer = new Stopwatch();

    private static bool shift = false;
    private static bool alt = false;
    private static bool ctrl = false;
    private static bool focus = false;

    private static WidgetGallery _widgetGallery = new WidgetGallery();

    public static void Main(string[] args)
    {
        _ctx = new Context();
        
        WindowOptions options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);
        options.Title = "My first Silk.NET program!";
        options.API = GraphicsAPI.Default;

        _window = Silk.NET.Windowing.Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
        _window.FocusChanged += x => focus = x;

        _window.Run();
    }

    private static void OnLoad()
    {
        _gl = _window.CreateOpenGLES();

        _glPainter = new OpenGlPainter(_gl);
        _startTimer.Start();

        IInputContext input = _window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
        {
            input.Keyboards[i].KeyDown += KeyDown;
            input.Keyboards[i].KeyChar += KeyChar;
            input.Keyboards[i].KeyUp += KeyUp;
        }
        for (int i = 0; i < input.Mice.Count; i++)
        {
            input.Mice[i].MouseMove += MouseMove;
            input.Mice[i].MouseDown += MouseDown;
            input.Mice[i].MouseUp += MouseUp;
        }

    }

    // These two methods are unused for this tutorial, aside from the logging we added earlier.
    private static void OnUpdate(double deltaTime)
    {
    }

    private static bool openIt = true;

    private static void OnRender(double deltaTime)
    {
        var scaleFactor = (float)_window.FramebufferSize.X / _window.Size.X;

        _input.ViewportId = ViewportId.Root;
        _input.Focused = focus;
        _input.Time = _startTimer.Elapsed.TotalSeconds;
        _input.ScreenRect = Rect.FromMinSize(new Pos2(0, 0), new Vec2(_window.Size.X, _window.Size.Y));

        _input.SystemTheme = Theme.Light;
        _input.Viewports = _input.Viewports.SetItem(_input.ViewportId, new ViewportInfo
        {
            Parent = null,
            Title = "egui test",
            Events = ImmutableList<ViewportEvent>.Empty,
            NativePixelsPerPoint = scaleFactor,
            MonitorSize = null,
            Focused = focus,
            InnerRect = _input.ScreenRect
        });

        var output = _ctx.Run(_input, ctx =>
        {
            new Window("bababooey")
                .Show(ctx, ui =>
            {
                if (ui.Label("welcome to the winder").Clicked) { openIt = true; }
            });

            new Window("🗄 Widget Gallery")
                .Open(ref openIt)
                .Resizable(new Vec2b(true, false))
                .DefaultWidth(280)
                .Show(ctx, ui =>
            {
                _widgetGallery.Show(ui);
            });
        });

        DrawOutput(in output);

        //new CentralPanel().Show(_ctx, ui =>
        {
            //Ui ui = default;
            // todo _ctx.StyleUi(ui);

        }//);

        _input = new RawInput();
    }

    private class WidgetGallery
    {
        private bool _enabled = true;
        private bool _visible = true;
        private bool _boolean = false;
        private float _opacity = 1.0f;
        private Enum _radio = Enum.First;
        private float _scalar = 42.0f;
        private string _string = "";
        private Color32 _color = Color32.LightBlue.LinearMultiply(0.5f);
        private bool _animateProgressBar = false;

        public void Show(Ui ui)
        {
            UiBuilder uiBuilder = new UiBuilder();
            if (!_enabled)
            {
                uiBuilder = uiBuilder.WithDisabled();
            }

            if (!_visible)
            {
                uiBuilder = uiBuilder.WithInvisible();
            }

            ui.ScopeBuilder(uiBuilder, ui =>
            {
                ui.MultiplyOpacity(_opacity);
                new Grid("my_grid")
                    .NumColumns(2)
                    .Spacing(new Vec2(40, 4))
                    .Striped(true)
                    .Show(ui, GalleryGridContents);
            });

            ui.Separator();

            ui.Horizontal(ui =>
            {
                ui.Checkbox(ref _visible, "Visible")
                    .OnHoverText("Uncheck to hide all the widgets.");

                if (_visible)
                {
                    ui.Checkbox(ref _enabled, "Interactive")
                        .OnHoverText("Uncheck to inspect how the widgets look when disabled.");
                    ui.Add(new DragValue<float>(ref _opacity)
                        .Speed(0.01f)
                        .Range(0.0f, 1.0f))// | ui.Label("Opacity")
                        .OnHoverText("Reduce this value to make widgets semi-transparent");
                }
            });

            ui.Separator();

            ui.VerticalCentered(ui =>
            {
                ui.Hyperlink("https://docs.rs/egui/")
                    .OnHoverText("The full egui documentation.\nYou can also click the different widgets names in the left column.");
                ui.HyperlinkTo(new RichText("Source code of the widget gallery").Small(), "https://github.com/emilk/egui/blob/master/crates/egui_demo_lib/src/demo/widget_gallery.rs");
            });
        }

        private void GalleryGridContents(Ui ui)
        {
            ui.Add(DocLinkLabel("Label", "label"));
            ui.Label("Welcome to the widget gallery!");
            ui.EndRow();

            ui.Add(DocLinkLabel("Hyperlink", "Hyperlink"));
            ui.HyperlinkTo(" egui on GitHub", "https://github.com/emilk/egui");
            ui.EndRow();

            ui.Add(DocLinkLabel("TextEdit", "TextEdit"));
            ui.Add(TextEdit.Singleline(ref _string).HintText("Write something here"));
            ui.EndRow();

            ui.Add(DocLinkLabel("Button", "button"));
            _boolean ^= ui.Button("Click me!").Clicked;
            ui.EndRow();

            ui.Add(DocLinkLabel("Link", "link"));
            _boolean ^= ui.Link("Click me!").Clicked;
            ui.EndRow();

            ui.Add(DocLinkLabel("Checkbox", "checkbox"));
            ui.Checkbox(ref _boolean, "Checkbox");
            ui.EndRow();

            ui.Add(DocLinkLabel("RadioButton", "radio"));
            ui.Horizontal(ui =>
            {
                ui.RadioValue(ref _radio, Enum.First, "First");
                ui.RadioValue(ref _radio, Enum.Second, "Second");
                ui.RadioValue(ref _radio, Enum.Third, "Third");
            });
            ui.EndRow();

            ui.Add(DocLinkLabel("SelectableLabel", "SelectableLabel"));
            ui.Horizontal(ui =>
            {
                ui.SelectableValue(ref _radio, Enum.First, "First");
                ui.SelectableValue(ref _radio, Enum.Second, "Second");
                ui.SelectableValue(ref _radio, Enum.Third, "Third");
            });
            ui.EndRow();

            ui.Add(DocLinkLabel("ComboBox", "ComboBox"));
            ComboBox.FromLabel("Take your pick")
                .SelectedText($"{_radio}")
                .ShowUi(ui, ui =>
                {
                    ui.SelectableValue(ref _radio, Enum.First, "First");
                    ui.SelectableValue(ref _radio, Enum.Second, "Second");
                    ui.SelectableValue(ref _radio, Enum.Third, "Third");
                });
            ui.EndRow();

            ui.Add(DocLinkLabel("Slider", "Slider"));
            ui.Add(new Slider<float>(ref _scalar, 0.0f, 360.0f).Suffix("°"));
            ui.EndRow();

            ui.Add(DocLinkLabel("DragValue", "DragValue"));
            ui.Add(new DragValue<float>(ref _scalar).Speed(1));
            ui.EndRow();

            ui.Add(DocLinkLabel("ProgressBar", "ProgressBar"));
            var progress = _scalar / 360.0f;
            var progressBar = new ProgressBar(progress)
                .ShowPercentage()
                .Animate(_animateProgressBar);
            _animateProgressBar = ui.Add(progressBar)
                .OnHoverText("The progress bar can be animated!")
                .Hovered;
            ui.EndRow();

            ui.Add(DocLinkLabel("Color picker", "color_edit"));
            ui.ColorEditButtonSrgba(ref _color);
            ui.EndRow();

            ui.Add(DocLinkLabel("Separator", "separator"));
            ui.Separator();
            ui.EndRow();

            ui.Add(DocLinkLabel("CollapsingHeader", "collapsing"));
            ui.Collapsing("Click to see what is hidden!", ui =>
            {
                ui.HorizontalWrapped(ui =>
                {
                    // ui.spacing_mut().item_spacing.x = 0.0;
                    ui.Label("It's a ");
                    ui.Add(DocLinkLabel("Spinner", "spinner"));
                    ui.AddSpace(4.0f);
                    ui.Add(new Spinner());
                });
            });
            ui.EndRow();

            ui.Hyperlink("Custom widget");
            ui.Add(new Toggle(ref _boolean))
                .OnHoverText("It's easy to create your own widgets!\nThis toggle switch is just 15 lines of code.");
            ui.EndRow();
            /*

      ui.add(doc_link_label("Image", "Image"));
      let egui_icon = egui::include_image!("../../data/icon.png");
      ui.add(egui::Image::new(egui_icon.clone()));
      ui.end_row();

      ui.add(doc_link_label(
          "Button with image",
          "Button::image_and_text",
      ));
      if ui
          .add(egui::Button::image_and_text(egui_icon, "Click me!"))
          .clicked()
      {
          *boolean = !*boolean;
      }
      ui.end_row();
           */

        }

        private static DocLinkLabelWidget DocLinkLabel(string title, string searchTerm)
        {
            return new DocLinkLabelWidget
            {
                Title = title,
                SearchTerm = searchTerm
            };
        }

        private struct DocLinkLabelWidget : IWidget
        {
            public required string Title;
            public required string SearchTerm;

            Response IWidget.Ui(Ui ui)
            {
                var searchTerm = SearchTerm;
                return ui.HyperlinkTo(Title, $"https://docs.rs/egui?search={searchTerm}")
                .OnHoverUi(ui =>
                {
                    ui.HorizontalWrapped(ui =>
                    {
                        ui.Label("Search egui docs for");
                        ui.Code(searchTerm);
                    });
                });
            }
        }
    }

    /// <summary>
    /// iOS-style toggle switch.
    /// </summary>
    private ref struct Toggle : IWidget
    {
        /// <summary>
        /// The value to update.
        /// </summary>
        private ref bool _on;

        /// <summary>
        /// Creates a toggle.
        /// </summary>
        /// <param name="on">Whether the toggle should be on.</param>
        public Toggle(ref bool on)
        {
            _on = ref on;
        }

        Response IWidget.Ui(Ui ui)
        {
            // Widget code can be broken up in four steps:
            //  1. Decide a size for the widget
            //  2. Allocate space for it
            //  3. Handle interactions with the widget (if any)
            //  4. Paint the widget

            // 1. Deciding widget size:
            // You can query the `ui` how much space is available,
            // but in this example we have a fixed size widget based on the height of a standard button:
            //var desiredSize = ui.Spacing.InteractSize.Y * new Vec2(2.0f, 1.0f);
            var desiredSize = new Vec2(2.0f * ui.Spacing.InteractSize.Y, ui.Spacing.InteractSize.Y);

            // 2. Allocating space:
            // This is where we get a region of the screen assigned.
            // We also tell the Ui to sense clicks in the allocated region.
            var (rect, response) = ui.AllocateExactSize(desiredSize, Sense.Click);

            // 3. Interact: Time to check for clicks!
            if (response.Clicked) {
                _on = !_on;
                response.MarkChanged();
            }

            // Attach some meta-data to the response which can be used by screen readers:
            /*response.widget_info(|| {
                egui::WidgetInfo::selected(egui::WidgetType::Checkbox, ui.is_enabled(), *on, "")
            });*/

            // 4. Paint!
            // Make sure we need to paint:
            if (ui.IsRectVisible(rect)) {
                // Let's ask for a simple animation from egui.
                // egui keeps track of changes in the boolean associated with the id and
                // returns an animated value in the 0-1 range for how much "on" we are.
                var howOn = ui.Ctx.AnimateBoolResponsive(response.Id, _on);
                // We will follow the current style by asking
                // "how should something that is being interacted with be painted?".
                // This will, for instance, give us different colors when the widget is hovered or clicked.
                var visuals = ui.Style.InteractSelectable(response, _on);
                // All coordinates are in absolute screen coordinates so we use `rect` to place the elements.
                rect = rect.Expand(visuals.Expansion);
                var radius = 0.5f * rect.Height;
                ui.Painter.Rect(
                    rect,
                    //radius,
                    CornerRadius.Same((byte)MathF.Round(radius)),
                    visuals.BgFill,
                    visuals.BgStroke,
                    StrokeKind.Inside
                );
                // Paint the circle, animating it from left to right with `how_on`:
                //var circleX = EguiHelpers.Lerp(rect.Left + radius, rect.Right - radius, howOn);
                var circleX = (1.0f - howOn) * (rect.Left + radius) + howOn * (rect.Right - radius);
                var center = new Pos2(circleX, rect.Center.Y);
                ui.Painter
                    .Circle(center, 0.75f * radius, visuals.BgFill, visuals.FgStroke);
            }

            // All done! Return the interaction response so the user can check what happened
            // (hovered, clicked, ...) and maybe show a tooltip:
            return response;
        }
    }

    private enum Enum
    {
        First,
        Second,
        Third,
    }

    public static Egui.Key? MapKeys(Silk.NET.Input.Key key)
    {
        switch (key)
        {
            case Silk.NET.Input.Key.Number1: return Egui.Key.Num1;
            case Silk.NET.Input.Key.Number2: return Egui.Key.Num2;
            case Silk.NET.Input.Key.Number3: return Egui.Key.Num3;
            case Silk.NET.Input.Key.Number4: return Egui.Key.Num4;
            case Silk.NET.Input.Key.Number5: return Egui.Key.Num5;
            case Silk.NET.Input.Key.Number6: return Egui.Key.Num6;
            case Silk.NET.Input.Key.Number7: return Egui.Key.Num7;
            case Silk.NET.Input.Key.Number8: return Egui.Key.Num8;
            case Silk.NET.Input.Key.Number9: return Egui.Key.Num9;
            case Silk.NET.Input.Key.Number0: return Egui.Key.Num0;
            case Silk.NET.Input.Key.Minus: return Egui.Key.Minus;
            case Silk.NET.Input.Key.Equal: return Egui.Key.Equals;
            case Silk.NET.Input.Key.Backspace: return Egui.Key.Backspace;
            case Silk.NET.Input.Key.GraveAccent: return Egui.Key.Backtick;

            case Silk.NET.Input.Key.Tab: return Egui.Key.Tab;
            case Silk.NET.Input.Key.Q: return Egui.Key.Q;
            case Silk.NET.Input.Key.W: return Egui.Key.W;
            case Silk.NET.Input.Key.E: return Egui.Key.E;
            case Silk.NET.Input.Key.R: return Egui.Key.R;
            case Silk.NET.Input.Key.T: return Egui.Key.T;
            case Silk.NET.Input.Key.Y: return Egui.Key.Y;
            case Silk.NET.Input.Key.U: return Egui.Key.U;
            case Silk.NET.Input.Key.I: return Egui.Key.I;
            case Silk.NET.Input.Key.O: return Egui.Key.O;
            case Silk.NET.Input.Key.P: return Egui.Key.P;
            case Silk.NET.Input.Key.LeftBracket: return Egui.Key.OpenBracket;
            case Silk.NET.Input.Key.RightBracket: return Egui.Key.CloseBracket;
            case Silk.NET.Input.Key.BackSlash: return Egui.Key.Backslash;
            case Silk.NET.Input.Key.A: return Egui.Key.A;
            case Silk.NET.Input.Key.S: return Egui.Key.S;
            case Silk.NET.Input.Key.D: return Egui.Key.D;
            case Silk.NET.Input.Key.F: return Egui.Key.F;
            case Silk.NET.Input.Key.G: return Egui.Key.G;
            case Silk.NET.Input.Key.H: return Egui.Key.H;
            case Silk.NET.Input.Key.J: return Egui.Key.J;
            case Silk.NET.Input.Key.K: return Egui.Key.K;
            case Silk.NET.Input.Key.L: return Egui.Key.L;
            case Silk.NET.Input.Key.Semicolon: return Egui.Key.Semicolon;
            case Silk.NET.Input.Key.Apostrophe: return Egui.Key.Quote;
            case Silk.NET.Input.Key.Z: return Egui.Key.Z;
            case Silk.NET.Input.Key.X: return Egui.Key.X;
            case Silk.NET.Input.Key.C: return Egui.Key.C;
            case Silk.NET.Input.Key.V: return Egui.Key.V;
            case Silk.NET.Input.Key.B: return Egui.Key.B;
            case Silk.NET.Input.Key.N: return Egui.Key.N;
            case Silk.NET.Input.Key.M: return Egui.Key.M;
            case Silk.NET.Input.Key.Comma: return Egui.Key.Comma;
            case Silk.NET.Input.Key.Period: return Egui.Key.Period;
            case Silk.NET.Input.Key.Slash: return Egui.Key.Slash;
            case Silk.NET.Input.Key.Space: return Egui.Key.Space;
            case Silk.NET.Input.Key.Enter: return Egui.Key.Enter;
            case Silk.NET.Input.Key.Escape: return Egui.Key.Escape;
            case Silk.NET.Input.Key.F1: return Egui.Key.F1;
            case Silk.NET.Input.Key.F2: return Egui.Key.F2;
            case Silk.NET.Input.Key.F3: return Egui.Key.F3;
            case Silk.NET.Input.Key.F4: return Egui.Key.F4;
            case Silk.NET.Input.Key.F5: return Egui.Key.F5;
            case Silk.NET.Input.Key.F6: return Egui.Key.F6;
            case Silk.NET.Input.Key.F7: return Egui.Key.F7;
            case Silk.NET.Input.Key.F8: return Egui.Key.F8;
            case Silk.NET.Input.Key.F9: return Egui.Key.F9;
            case Silk.NET.Input.Key.F10: return Egui.Key.F10;
            case Silk.NET.Input.Key.F11: return Egui.Key.F11;
            case Silk.NET.Input.Key.F12: return Egui.Key.F12;
            case Silk.NET.Input.Key.End: return Egui.Key.End;
            case Silk.NET.Input.Key.Delete: return Egui.Key.Delete;
            case Silk.NET.Input.Key.Left: return Egui.Key.ArrowLeft;
            case Silk.NET.Input.Key.Right: return Egui.Key.ArrowRight;
            case Silk.NET.Input.Key.Up: return Egui.Key.ArrowUp;
            case Silk.NET.Input.Key.Down: return Egui.Key.ArrowDown;
        }

        return null;
    }

    private static void KeyDown(IKeyboard keyboard, Silk.NET.Input.Key key, int keyCode)
    {
        if (key == Silk.NET.Input.Key.ShiftLeft || key == Silk.NET.Input.Key.ShiftRight)
        {
            shift = true;
        }

        if (key == Silk.NET.Input.Key.AltLeft || key == Silk.NET.Input.Key.AltRight)
        {
            alt = true;
        }

        if (key == Silk.NET.Input.Key.ControlLeft || key == Silk.NET.Input.Key.ControlRight)
        {
            ctrl = true;
        }

        if (ctrl && key == Silk.NET.Input.Key.C)
        {
            _input.Events = _input.Events.Add(new Event.Copy());
            return;
        }
        if (ctrl && key == Silk.NET.Input.Key.V)
        {
            if (keyboard.ClipboardText.Length > 0)
            {
                _input.Events = _input.Events.Add(new Event.Paste() { Value = keyboard.ClipboardText });
            }
            return;
        }
        if (ctrl && key == Silk.NET.Input.Key.X)
        {
            _input.Events = _input.Events.Add(new Event.Cut());
            return;
        }

        var mapped = MapKeys(key);

        if (mapped.HasValue)
        {
            _input.Events = _input.Events.Add(new Event.Key
            {
                LogicalKey = mapped.Value,
                PhysicalKey = mapped.Value,
                Pressed = true,
                Modifiers = new() { 
                    Alt = alt,
                    Ctrl = ctrl,
                    Shift = shift
                }

            });
        }
    }
    
    private static void KeyChar(IKeyboard keyboard, char data)
    {
        _input.Events = _input.Events.Add(new Event.Text(data.ToString()));
    }

    private static void KeyUp(IKeyboard keyboard, Silk.NET.Input.Key key, int keyCode)
    {
        if (key == Silk.NET.Input.Key.ShiftLeft || key == Silk.NET.Input.Key.ShiftRight)
        {
            shift = false;
        }

        if (key == Silk.NET.Input.Key.AltLeft || key == Silk.NET.Input.Key.AltRight)
        {
            alt = false;
        }

        if (key == Silk.NET.Input.Key.ControlLeft || key == Silk.NET.Input.Key.ControlRight)
        {
            ctrl = false;
        }

        var mapped = MapKeys(key);

        if (mapped.HasValue)
        {
            _input.Events = _input.Events.Add(new Event.Key
            {
                LogicalKey = mapped.Value,
                PhysicalKey = mapped.Value,
                Pressed = false,
                Modifiers = new()
                {
                    Alt = alt,
                    Ctrl = ctrl,
                    Shift = shift
                }
            });
        }
    }

    private static void MouseMove(IMouse mouse, Vector2 vector)
    {
        _input.Events = _input.Events.Add(new Event.PointerMoved
        {
            Value = new(vector.X, vector.Y)
        });
    }

    private static void MouseDown(IMouse mouse, Silk.NET.Input.MouseButton button)
    {
        _input.Events = _input.Events.Add(new Event.PointerButton
        {
            Button = (Egui.PointerButton)button,
            Pressed = true,
            Pos = new Pos2(mouse.Position.X, mouse.Position.Y),
            Modifiers = new()
            {
                Alt = alt,
                Ctrl = ctrl,
                Shift = shift
            }
        });
    }

    private static void MouseUp(IMouse mouse, Silk.NET.Input.MouseButton button)
    {
        _input.Events = _input.Events.Add(new Event.PointerButton
        {
            Button = (Egui.PointerButton)button,
            Pressed = false,
            Pos = new Pos2(mouse.Position.X, mouse.Position.Y),
            Modifiers = new()
            {
                Alt = alt,
                Ctrl = ctrl,
                Shift = shift
            }
        });

    }

    private static void DrawOutput(in FullOutput output)
    {
        CheckGlErrors();
        _gl.Disable(GLEnum.ScissorTest);
        _gl.Viewport(0, 0, (uint)_window.FramebufferSize.X, (uint)_window.FramebufferSize.Y);
        _gl.ClearColor(Color.CornflowerBlue);
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        CheckGlErrors();

        var clippedPrimitives = _ctx.Tessellate(output.Shapes.ToArray(), output.PixelsPerPoint);
        _glPainter.PaintAndUpdateTextures((uint)_window.FramebufferSize.X, (uint)_window.FramebufferSize.Y, output.PixelsPerPoint, clippedPrimitives, output.TexturesDelta);
    }

    private static void CheckGlErrors()
    {
        var error = _gl.GetError();
        if (error != GLEnum.NoError)
        {
            throw new Exception($"GL error: {error}");
        }
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
            CheckGlErrors();
            _gl = gl;

            var frag = CompileShader(GLEnum.FragmentShader, File.ReadAllText("fragment.glsl"));
            var vert = CompileShader(GLEnum.VertexShader, File.ReadAllText("vertex.glsl"));
            _program = LinkProgram([vert, frag]);
            _u_screen_size = _gl.GetUniformLocation(_program, "u_screen_size");
            _u_sampler = _gl.GetUniformLocation(_program, "u_sampler");

            _vbo = _gl.GenBuffer();

            CheckGlErrors();
            
            _a_pos_loc = _gl.GetAttribLocation(_program, "a_pos");
            _a_tc_loc = _gl.GetAttribLocation(_program, "a_tc");
            _a_srgba_loc = _gl.GetAttribLocation(_program, "a_srgba");

            CheckGlErrors();
            _vao = _gl.GenVertexArray();
            CheckGlErrors();
            _gl.BindVertexArray(_vao);
            _gl.BindBuffer(GLEnum.ArrayBuffer, _vbo);
            CheckGlErrors();

            _gl.EnableVertexAttribArray((uint)_a_pos_loc);
            _gl.VertexAttribPointer((uint)_a_pos_loc, 2, GLEnum.Float, false, (uint)sizeof(Vertex), 0);
            CheckGlErrors();
            _gl.EnableVertexAttribArray((uint)_a_tc_loc);
            CheckGlErrors();
            _gl.VertexAttribPointer((uint)_a_tc_loc, 2, GLEnum.Float, false, (uint)sizeof(Vertex), 2 * 4);
            CheckGlErrors();
            _gl.EnableVertexAttribArray((uint)_a_srgba_loc);
            _gl.VertexAttribPointer((uint)_a_srgba_loc, 4, GLEnum.UnsignedByte, false, (uint)sizeof(Vertex), 4 * 4);

            CheckGlErrors();

            _eao = _gl.GenBuffer();

            _textures = new Dictionary<TextureId, uint>();
            CheckGlErrors();
        }

        public readonly void PaintAndUpdateTextures(uint width, uint height, float pixelsPerPoint, ReadOnlyMemory<ClippedPrimitive> clippedPrimitives, TexturesDelta texturesDelta)
        {
            CheckGlErrors();
            foreach (var (id, delta) in texturesDelta.Set)
            {
                SetTexture(id, delta);
            }

            CheckGlErrors();
            PaintPrimitives(width, height, pixelsPerPoint, clippedPrimitives);
            CheckGlErrors();

            foreach (var id in texturesDelta.Free)
            {
                FreeTexture(id);
            }
            CheckGlErrors();
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
                switch (primitive.Primitive.Inner)
                {
                    case Primitive.Mesh meshPrimitive:
                    {
                        Mesh mesh = meshPrimitive.Value;

                        SetClipRect(width, height, pixelsPerPoint, primitive.ClipRect);

                        var texture = _textures[mesh.TextureId];
                        _gl.BindBuffer(GLEnum.ArrayBuffer, _vbo);

                        fixed (Vertex* vertices = mesh.Vertices.ToArray())
                        {
                            _gl.BufferData(GLEnum.ArrayBuffer, (nuint)(mesh.Vertices.Count * sizeof(Vertex)), vertices, BufferUsageARB.StreamDraw);
                        }

                        _gl.BindBuffer(GLEnum.ElementArrayBuffer, _eao);

                        fixed (uint* indices = mesh.Indices.ToArray())
                        {
                            _gl.BufferData(GLEnum.ElementArrayBuffer, (nuint)(mesh.Indices.Count * sizeof(uint)), indices, BufferUsageARB.StreamDraw);
                        }

                        _gl.BindTexture(GLEnum.Texture2D, texture);
                        _gl.DrawElements(GLEnum.Triangles, (uint)mesh.Indices.Count, GLEnum.UnsignedInt, null);
                        break;
                    }
                }
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

            CheckGlErrors();
            _gl.BindTexture(GLEnum.Texture2D, _textures[id]);
            CheckGlErrors();

            switch (delta.Image.Inner)
            {
                case ImageData.Color image:
                {
                    _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GlowCode(delta.Options.Magnification, null));
                    _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GlowCode(delta.Options.Minification, delta.Options.MipmapMode));
                    _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)GlowCode(delta.Options.WrapMode));
                    _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)GlowCode(delta.Options.WrapMode));

                    CheckGlErrors();
                    _gl.PixelStore(GLEnum.UnpackAlignment, 1);
                    CheckGlErrors();

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
                    CheckGlErrors();

                    if (delta.Options.MipmapMode.HasValue)
                    {
                        _gl.GenerateMipmap(GLEnum.Texture2D);
                    }
                    CheckGlErrors();
                    break;
                }
            }
        }

        private readonly void SetClipRect(uint width, uint height, float pixelsPerPoint, Rect clipRect)
        {
            var clipMinXf = pixelsPerPoint * clipRect.Min.X;
            var clipMinYf = pixelsPerPoint * clipRect.Min.Y;
            var clipMaxXf = pixelsPerPoint * clipRect.Max.X;
            var clipMaxYf = pixelsPerPoint * clipRect.Max.Y;

            var clipMinX = (int)MathF.Round(clipMinXf);
            var clipMinY = (int)MathF.Round(clipMinYf);
            var clipMaxX = (int)MathF.Round(clipMaxXf);
            var clipMaxY = (int)MathF.Round(clipMaxYf);

            clipMinX = Math.Clamp(clipMinX, 0, (int)width);
            clipMinY = Math.Clamp(clipMinY, 0, (int)height);
            clipMaxX = Math.Clamp(clipMaxX, clipMinX, (int)width);
            clipMaxY = Math.Clamp(clipMaxY, clipMinY, (int)height);

            _gl.Scissor(clipMinX, (int)height - clipMaxY, (uint)(clipMaxX - clipMinX), (uint)(clipMaxY - clipMinY));
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

        private static GLEnum GlowCode(Egui.TextureWrapMode mode)
        {
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