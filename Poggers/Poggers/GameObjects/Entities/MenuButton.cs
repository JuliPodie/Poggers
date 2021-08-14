using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Textures;

namespace Poggers.GameObjects.Entities
{
    public class MenuButton : GameRectangle
    {
        private bool highlight;
        private int textureID;
        private bool gold = false;

        public MenuButton(Vector2 center, float width, float height, bool highlight, int textureID)
            : base(center, width, height)
        {
            this.highlight = highlight;
            this.textureID = textureID;
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            if (this.highlight)
            {
                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetMenuTexture(this.textureID + 1));

                if (this.gold)
                {
                    GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(this.textureID + 3));
                }
            }
            else
            {
                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetMenuTexture(this.textureID));

                if (this.gold)
                {
                    GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(this.textureID + 2));
                }
            }

            if (this.textureID == 0)
            {
                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetMenuTexture(this.textureID));
            }

            GL.Color4(Color4.White);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2((offset.X - (0.5 * this.Width)) * windowRatio, offset.Y - (0.5 * this.Height)); // draw first quad corner
            GL.TexCoord2(1, 1);
            GL.Vertex2((offset.X + (0.5 * this.Width)) * windowRatio, offset.Y - (0.5 * this.Height));
            GL.TexCoord2(1, 0);
            GL.Vertex2((offset.X + (0.5 * this.Width)) * windowRatio, offset.Y + (0.5 * this.Height));
            GL.TexCoord2(0, 0);
            GL.Vertex2((offset.X - (0.5 * this.Width)) * windowRatio, offset.Y + (0.5 * this.Height));
            GL.End();
        }

        public void Highlight()
        {
            this.highlight = true;
        }

        public void UnHighlight()
        {
            this.highlight = false;
        }

        public void Golding()
        {
            this.gold = true;
        }

        public void UnGolding()
        {
            this.gold = false;
        }
    }
}
