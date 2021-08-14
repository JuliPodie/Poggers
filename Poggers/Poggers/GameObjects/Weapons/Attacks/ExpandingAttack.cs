using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Textures;

namespace Poggers.GameObjects.Weapons.Attacks
{
    public class ExpandingAttack : GameCircle, IAttackComponent
    {
        private readonly float increment;
        private List<int> tex = new List<int>();

        public ExpandingAttack(Vector2 center, float radius, float increment, List<int> tex)
            : base(center, radius)
        {
            this.increment = increment;
            this.tex = tex;
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(this.tex[2]));
            GL.Color4(Color4.White);
            GL.Begin(PrimitiveType.TriangleFan);

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

        public void Update()
        {
            this.Radius += this.increment;
        }
    }
}
