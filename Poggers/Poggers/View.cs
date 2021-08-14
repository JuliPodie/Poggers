using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using Poggers.GameObjects.Weapons.Attacks;
using Poggers.Interfaces;

namespace Poggers
{
    public class View
    {
        private static float windowRatio;

        public static float WindowRatio { get => windowRatio; set => windowRatio = value; }

        public static void Draw(Model model)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); // clear the screen
            Vector2 offset = model.Player?.Center ?? (0, 0);

            foreach (var obj in model.GameObjects.ToArray())
            {
                obj.Draw(Vector2.Subtract(obj.Center, offset), windowRatio);
            }

            foreach (Attack attack in model.Attacks.ToArray())
            {
                foreach (IAttackComponent component in attack.Hitboxes.ToArray())
                {
                    component.Draw(Vector2.Subtract(component.Center, offset), windowRatio);
                }
            }

            foreach (IOverlay hud in model.Overlays.ToArray())
            {
                hud.Draw(windowRatio);
            }
        }

        public static void Resize(GameWindow window, Vector2i windowSize)
        {
            WindowRatio = (float)windowSize.Y / (float)windowSize.X;
            GL.Viewport(0, 0, windowSize.X, windowSize.Y);
            window.Size = windowSize;
        }
    }
}
