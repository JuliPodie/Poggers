using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Textures;

namespace Poggers.GameObjects.Entities
{
    public class Lightning : GameRectangle
    {
        public Lightning(Vector2 center, float width, float height)
            : base(center, width, height)
        {
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(3));
            GL.Color4(Color4.White);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2((0 - (0.5 * this.Width)) * windowRatio, 0 - (0.5 * this.Height)); // draw first quad corner
            GL.TexCoord2(1, 1);
            GL.Vertex2((0 + (0.5 * this.Width)) * windowRatio, 0 - (0.5 * this.Height));
            GL.TexCoord2(1, 0);
            GL.Vertex2((0 + (0.5 * this.Width)) * windowRatio, 0 + (0.5 * this.Height));
            GL.TexCoord2(0, 0);
            GL.Vertex2((0 - (0.5 * this.Width)) * windowRatio, 0 + (0.5 * this.Height));
            GL.End();

            GL.Disable(EnableCap.Blend);
            GL.Color4(Color4.Black);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-1, -1); // left side blacking
            GL.Vertex2((0 - (0.5 * this.Width)) * windowRatio, -1);
            GL.Vertex2((0 - (0.5 * this.Width)) * windowRatio, 1);
            GL.Vertex2(-1, 1);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2((0 + (0.5 * this.Width)) * windowRatio, -1); // right side
            GL.Vertex2(1, -1);
            GL.Vertex2(1, 1);
            GL.Vertex2((0 + (0.5 * this.Width)) * windowRatio, 1);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-1, (0 + (0.5 * this.Width)) * windowRatio); // top
            GL.Vertex2(1, (0 + (0.5 * this.Width)) * windowRatio);
            GL.Vertex2(1, 1);
            GL.Vertex2(-1, 1);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-1, -1); // bottom
            GL.Vertex2(1, -1);
            GL.Vertex2(1, (0 - (0.5 * this.Width)) * windowRatio);
            GL.Vertex2(-1, (0 - (0.5 * this.Width)) * windowRatio);
            GL.End();

            GL.Enable(EnableCap.Blend);
        }
    }
}
