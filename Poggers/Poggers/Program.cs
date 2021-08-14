using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Poggers.Input;
using Poggers.Textures;

namespace Poggers
{
    public class Program
    {
        public static void Main()
        {
            GameWindow window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability });
            View.Resize(window, (1000, 600));
            InputProvider.Instance = new InputProvider(window);
            Model model = new Model(window); // model does not know about the view

            window.Load += TextureLoader.LoadTex;

            window.Title = "Grals Journey";

            window.UpdateFrequency = 60;

            window.Resize += args => View.Resize(window, args.Size);
            window.RenderFrame += _ => View.Draw(model); // first draw the model
            window.RenderFrame += _ => window.SwapBuffers(); // then wait for next frame and buffer swap
            window.Run(); // start the game loop with 60Hz
        }
    }
}