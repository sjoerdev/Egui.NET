using Egui;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace MySilkProgram;

public class Program
{
    private static IWindow _window;

    public static void Main(string[] args)
    {
        var ctx = new Context();
        ctx.StyleMut((ref Style x) => x.AnimationTime = 0.329f);

        Console.WriteLine($"CReated a ctx! {ctx.Os}");

        WindowOptions options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);
        options.Title = "My first Silk.NET program!";

        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        _window.Run();
    }

    private static void OnLoad()
    {
        Console.WriteLine("Load!");

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
    }

    private static void KeyDown(IKeyboard keyboard, Silk.NET.Input.Key key, int keyCode)
    {
        if (key == Silk.NET.Input.Key.Escape)
            _window.Close();
    }
}