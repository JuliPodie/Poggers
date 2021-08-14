using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Poggers.Input;
using Poggers.Interfaces;
using Poggers.Textures;

namespace Poggers.Overlays
{
    public class InfoWindow : IOverlayWithInput
    {
        private readonly IModel model;
        private int type; // type0 = loadscreen, Type1 = deathscreen, Type2 = Endscreen
        private int sprite;
        private Color4 color;

        // DEATH
        private Vector2 infoBounds = new Vector2(-1f, -1f);
        private float infoHeight = 2f;
        private float infoWidth = 2f;

        private Vector2 infoLblBounds = new Vector2(-0.5f, -0.7f);
        private float infoLblHeight = 0.2f;
        private float infoLblWidth = 1f;

        public InfoWindow(IModel model, int type)
        {
            this.model = model;
            this.type = type;

            switch (this.type)
            {
                case 0:
                    this.color = Color4.Black;
                    this.sprite = 18;
                    break;
                case 1:
                    this.color = Color4.White;
                    this.sprite = 18;
                    break;
                case 2:
                    this.color = Color4.White;
                    this.sprite = 17;
                    break;
            }

            /*
            this.color = this.type switch
            {
                0 => Color4.Black,
                1 => Color4.White,
                _ => throw new ArgumentException(),
            }; */
        }

        public void UpdateInfoWindow(GameWindow window, KeyboardKeyEventArgs args)
        {
            if (window.KeyboardState.IsKeyDown(Keys.Enter) && this.type == 1)
            {
                this.model.LoadMainMenu();
            }
        }

        public void Hide()
        {
            InputProvider.Instance.UnsubscribeDown(this.UpdateInfoWindow);
        }

        public void Show()
        {
            InputProvider.Instance.SubscribeDown(this.UpdateInfoWindow);
        }

        public void Draw(float windowRatio)
        {
            GL.Color4(this.color);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetMenuTexture(this.sprite));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(this.infoBounds.X, this.infoBounds.Y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(this.infoBounds.X + this.infoWidth, this.infoBounds.Y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(this.infoBounds.X + this.infoWidth, this.infoBounds.Y + this.infoHeight);
            GL.TexCoord2(0, 0);
            GL.Vertex2(this.infoBounds.X, this.infoBounds.Y + this.infoHeight);
            GL.End();

            if (this.type != 2)
            {
                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetMenuTexture(15));
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 1);
                GL.Vertex2(this.infoLblBounds.X * windowRatio, this.infoLblBounds.Y);
                GL.TexCoord2(1, 1);
                GL.Vertex2((this.infoLblBounds.X + this.infoLblWidth) * windowRatio, this.infoLblBounds.Y);
                GL.TexCoord2(1, 0);
                GL.Vertex2((this.infoLblBounds.X + this.infoLblWidth) * windowRatio, this.infoLblBounds.Y + this.infoLblHeight);
                GL.TexCoord2(0, 0);
                GL.Vertex2(this.infoLblBounds.X * windowRatio, this.infoLblBounds.Y + this.infoLblHeight);
                GL.End();
            }
        }
    }
}
