using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Poggers.GameObjects.Entities;
using Poggers.Input;
using Poggers.Interfaces;

namespace Poggers.Overlays
{
    public class InGameMenu : IOverlayWithInput
    {
        private readonly IModel model;
        private readonly MenuButton[] options;
        private int selected = 0;

        private Vector2 menuBounds = new Vector2(-0.5f, -0.5f);
        private float menuHeight = 1f;
        private float menuWidth = 1f;

        private float menuBtnHeight = 0.18f;
        private float menuBtnWidth = 0.45f;
        private Vector2 menuBtn1Bounds = new Vector2(-0.225f, 0.06f);
        private Vector2 menuBtn2Bounds = new Vector2(-0.225f, -0.24f);

        public InGameMenu(IModel model)
        {
            this.model = model;

            Vector2 offset = (this.menuBtnWidth / 2, this.menuBtnHeight / 2);
            this.options = new MenuButton[2]
            {
                new MenuButton(Vector2.Add(this.menuBtn1Bounds, offset), this.menuBtnWidth, this.menuBtnHeight, true, 13),
                new MenuButton(Vector2.Add(this.menuBtn2Bounds, offset), this.menuBtnWidth, this.menuBtnHeight, false, 15),
            };
        }

        public void UpdateInGameMenu(GameWindow window, KeyboardKeyEventArgs args)
        {
            this.options[this.selected].UnHighlight();

            if (args.Key == Keys.Up)
            {
                this.selected--;
            }

            if (args.Key == Keys.Down)
            {
                this.selected++;
            }

            this.selected = ((this.selected % this.options.Length) + this.options.Length) % this.options.Length; // use twice to prevent negative numbers
            this.options[this.selected].Highlight();

            if (window.KeyboardState.IsKeyDown(Keys.Enter))
            {
                switch (this.selected)
                {
                    case 0:
                        this.model.OverlayController.ToggleMenu();
                        break;
                    case 1:
                        this.model.LoadMainMenu();
                        break;
                }
            }
        }

        public void Hide()
        {
            foreach (MenuButton btn in this.options)
            {
                btn.UnHighlight();
            }

            InputProvider.Instance.UnsubscribeDown(this.UpdateInGameMenu);
        }

        public void Show()
        {
            this.selected = 0;
            this.options[this.selected].Highlight();
            InputProvider.Instance.SubscribeDown(this.UpdateInGameMenu);
        }

        public void Draw(float windowRatio)
        {
            GL.Color4(Color4.White);

            // Menu
            GL.Disable(EnableCap.Texture2D);
            GL.Color4(Color4.Black);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(this.menuBounds.X * windowRatio, this.menuBounds.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2((this.menuBounds.X + this.menuWidth) * windowRatio, this.menuBounds.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2((this.menuBounds.X + this.menuWidth) * windowRatio, this.menuBounds.Y + this.menuHeight);
            GL.TexCoord2(0, 0);
            GL.Vertex2(this.menuBounds.X * windowRatio, this.menuBounds.Y + this.menuHeight);
            GL.End();
            GL.Enable(EnableCap.Texture2D);

            foreach (MenuButton button in this.options)
            {
                button.Draw(button.Center, windowRatio);
            }
        }
    }
}
