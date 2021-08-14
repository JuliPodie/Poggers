using System;
using System.Timers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Directions;
using Poggers.Textures;

namespace Poggers.GameObjects.Weapons.Attacks
{
    public class Projectile : GameCircle, IAttackComponent
    {
        private readonly Vector2 direction;
        private Timer updateTimer;
        private Vector2 centerAnim;
        private Vector2 offTemp;
        private int tex;
        private bool anim = false;
        private int cnt = 0;
        private int length;
        private Color4 spriteColor;

        public Projectile(Vector2 center, float radius, Vector2 direction, float increment, int tex)
            : base(center, radius)
        {
            this.direction = Vector2.Multiply(direction, increment / direction.Length);
            this.tex = tex;
        }

        public Projectile(Vector2 center, float radius, Direction direction, float increment, int tex, Color4 spriteColor, int length)
            : this(center, radius, direction.GetDirectionVector(), increment, tex)
        {
            this.anim = true;
            this.spriteColor = spriteColor;
            this.length = length / 3;

            switch (direction)
            {
                case Direction.A:
                    this.tex = 25;
                    break;
                case Direction.S:
                    this.tex = 25;
                    break;
                case Direction.D:
                    this.tex = 25;
                    break;
                case Direction.W:
                    this.tex = 25;
                    break;
                case Direction.WA:
                    this.tex = 25;
                    break;
                case Direction.WD:
                    this.tex = 25;
                    break;
                case Direction.SA:
                    this.tex = 25;
                    break;
                case Direction.SD:
                    this.tex = 25;
                    break;
            }

            this.centerAnim = this.Center;
            this.updateTimer = new Timer();
            this.updateTimer.Elapsed += this.UpdateT;
            this.updateTimer.Interval = this.length;
            this.updateTimer.Start();
        }

        public void UpdateT(object source, ElapsedEventArgs e)
        {
            this.updateTimer.Stop();
            if (this.cnt < 3)
            {
                this.centerAnim = this.offTemp;
                this.updateTimer.Interval = this.length;
                this.tex++;
                this.cnt++;
                this.updateTimer.Start();
            }
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            this.offTemp = offset;
            GL.Color4(Color4.White);
            if (this.anim)
            {
                GL.Color4(this.spriteColor);
                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(this.tex));

                /*
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 1);
                GL.Vertex2((this.centerAnim.X - (0.5 * this.Radius)) * windowRatio, this.centerAnim.Y - (0.5 * this.Radius)); // draw first quad corner
                GL.TexCoord2(1, 1);
                GL.Vertex2((this.centerAnim.X + (0.5 * this.Radius)) * windowRatio, this.centerAnim.Y - (0.5 * this.Radius));
                GL.TexCoord2(1, 0);
                GL.Vertex2((this.centerAnim.X + (0.5 * this.Radius)) * windowRatio, this.centerAnim.Y + (0.5 * this.Radius));
                GL.TexCoord2(0, 0);
                GL.Vertex2((this.centerAnim.X - (0.5 * this.Radius)) * windowRatio, this.centerAnim.Y + (0.5 * this.Radius));
                GL.End();
                */

                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 1);
                GL.Vertex2((offset.X - (1 * this.Radius)) * windowRatio, offset.Y - (0.5 * this.Radius)); // draw first quad corner
                GL.TexCoord2(1, 1);
                GL.Vertex2((offset.X + (1 * this.Radius)) * windowRatio, offset.Y - (0.5 * this.Radius));
                GL.TexCoord2(1, 0);
                GL.Vertex2((offset.X + (1 * this.Radius)) * windowRatio, offset.Y + (0.5 * this.Radius));
                GL.TexCoord2(0, 0);
                GL.Vertex2((offset.X - (1 * this.Radius)) * windowRatio, offset.Y + (0.5 * this.Radius));
                GL.End();
            }
            else
            {
                GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(this.tex));
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
        }

        public void Update()
        {
            this.Center = Vector2.Add(this.Center, this.direction);
        }
    }
}
