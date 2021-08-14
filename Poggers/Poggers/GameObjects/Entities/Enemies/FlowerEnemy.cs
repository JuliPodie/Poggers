using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Interfaces;
using Poggers.Textures;

namespace Poggers.GameObjects.Entities.Enemies
{
    public class FlowerEnemy : BasicEnemy
    {
        private const float WIDTH = 0.17f;
        private const float HEIGHT = 0.10f;
        private Animator ani = new Animator();

        public FlowerEnemy(Vector2 center, IModel model)
            : base(center, WIDTH, HEIGHT, model)
        {
            this.Health = new ValueWithBounds(1, 1);
            this.Speed = MAXSPEED / 5;
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            GL.Color4(this.SpriteColor);
            GL.BindTexture(TextureTarget.Texture2D, this.ani.GetEnemy2Frame(this.State));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2((offset.X - (this.Width / 2)) * windowRatio, offset.Y - (this.Height / 2)); // draw first quad corner
            GL.TexCoord2(1, 1);
            GL.Vertex2((offset.X + (this.Width / 2)) * windowRatio, offset.Y - (this.Height / 2));
            GL.TexCoord2(1, 0);
            GL.Vertex2((offset.X + (this.Width / 2)) * windowRatio, offset.Y + this.Height);
            GL.TexCoord2(0, 0);
            GL.Vertex2((offset.X - (this.Width / 2)) * windowRatio, offset.Y + this.Height);
            GL.End();
        }
    }
}
