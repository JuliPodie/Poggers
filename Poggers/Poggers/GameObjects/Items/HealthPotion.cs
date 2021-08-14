using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Textures;

namespace Poggers.GameObjects.Items
{
    public class HealthPotion : GameCircle, IItem
    {
        public HealthPotion(Vector2 center, float radius = 0.05f)
            : base(center, radius)
        {
        }

        public int Texture => 12;

        public void ApplyEffects(IItemConsumer consumer)
        {
            consumer.Health += 1;
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(this.Texture));
            GL.Begin(PrimitiveType.Polygon);

            float angleoffset = (float)(180 * Math.PI / 180);
            for (int angleD = 0; angleD < 360; angleD++)
            {
                float angle = (float)(angleD * Math.PI / 180);
                double x = ((Math.Cos(angle) * this.Radius) + offset.X) * windowRatio;
                double y = (Math.Sin(angle) * this.Radius) + offset.Y;

                GL.TexCoord2((Math.Cos(angle + angleoffset) * 0.5) + 0.5, (Math.Sin(angle + angleoffset) * 0.5) + 0.5);
                GL.Vertex2(x, y);
            }

            GL.End();
        }
    }
}