using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Textures;

namespace Poggers.GameObjects.Entities
{
    public class GridBox : GameRectangle
    {
        private float offsetX;
        private float offsetY;
        private uint typ;
        private Vector2 f;
        private Vector2 bounds = (255, 95);
        private float tileSize = 16;
        private bool isEnd = false;

        public GridBox(Vector2 center, float width, float height, uint typ)
            : base(center, width, height)
        {
            this.typ = typ;
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            this.f = ((this.typ % this.tileSize) - 1, ((int)this.typ / (int)this.tileSize) - 1);

            this.offsetX = (this.f.X * this.tileSize) - 1;
            this.offsetY = ((this.f.Y * this.tileSize) - 1) - ((5 * this.tileSize) - 1);

            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(0));
            GL.Color4(Color4.White);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(this.offsetX / this.bounds.X, (this.offsetY / this.bounds.Y) + (this.tileSize / this.bounds.Y));
            GL.Vertex2((offset.X - (0.5 * this.Width)) * windowRatio, offset.Y - (0.5 * this.Height)); // draw first quad corner
            GL.TexCoord2((this.offsetX / this.bounds.X) + (this.tileSize / this.bounds.X), (this.offsetY / this.bounds.Y) + (this.tileSize / this.bounds.Y));
            GL.Vertex2((offset.X + (0.5 * this.Width)) * windowRatio, offset.Y - (0.5 * this.Height));
            GL.TexCoord2((this.offsetX / this.bounds.X) + (this.tileSize / this.bounds.X), this.offsetY / this.bounds.Y);
            GL.Vertex2((offset.X + (0.5 * this.Width)) * windowRatio, offset.Y + (0.5 * this.Height));
            GL.TexCoord2(this.offsetX / this.bounds.X, this.offsetY / this.bounds.Y);
            GL.Vertex2((offset.X - (0.5 * this.Width)) * windowRatio, offset.Y + (0.5 * this.Height));
            GL.End();
        }

        public void SetEnd(bool value)
        {
            this.isEnd = value;
        }

        public bool GetEnd()
        {
            return this.isEnd;
        }
    }
}
