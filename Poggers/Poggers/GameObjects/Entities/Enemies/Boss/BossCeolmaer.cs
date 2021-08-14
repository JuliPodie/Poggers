using System.Timers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Poggers.Directions;
using Poggers.EntityStates;
using Poggers.GameObjects.Weapons;
using Poggers.Interfaces;
using Poggers.Pathfinding;
using Poggers.Randomize;
using Poggers.Textures;

namespace Poggers.GameObjects.Entities.Enemies
{
    public class BossCeolmaer : BasicEntity, IArmedEntity
    {
        private const float WIDTH = 1.44f;
        private const float HEIGHT = 0.6f;

        private const float HANDWIDTH = 0.263f;
        private const float HANDHEIGHT = 0.7f;

        private const int INVICIBLETIME = 3000;
        private const int ATTACKTIME = 2500;
        private const int ATTACKCD = 1200;
        private const int ATTACKCDRND = 80;

        private Vector2 handCenterORG = (0.27f, 0.1f);

        private IWeapon weapon;

        private Timer attackTimer;
        private Timer attackCDTimer;
        private Timer animTimer;

        private bool isAttacking;
        private bool hasAttackCD;
        private int animTime = 80;
        private int animCount = 0;

        private Vector2 handCenter;

        public BossCeolmaer(Vector2 center, IModel model)
            : base(center, WIDTH, HEIGHT, model)
        {
            this.Health = new ValueWithBounds(10, 1);
            this.Endurance = new ValueWithBoundsAndTimer(500);

            this.Weapon = new SageStaff(model, this);

            this.InvincibleTime = INVICIBLETIME;
            this.IsAttacking = false;
            this.hasAttackCD = false;
            this.handCenter = this.handCenterORG;

            this.attackTimer = new Timer();
            this.attackTimer.Elapsed += this.EndAttack;

            this.animTimer = new Timer();

            this.attackCDTimer = new Timer();
            this.attackCDTimer.Elapsed += this.EndAttackCD;
        }

        public IWeapon Weapon { get => this.weapon; set => this.weapon = value; }

        public bool IsAttacking { get => this.isAttacking; set => this.isAttacking = value; }

        public override void Die()
        {
            this.Weapon.StopAttack();
            this.Model.FinishedGame();
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            GL.Color4(this.SpriteColor);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(18));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2((offset.X - ((this.Width / 0.9) / 2)) * windowRatio, offset.Y - (this.Height / 2)); // draw first quad corner
            GL.TexCoord2(1, 1);
            GL.Vertex2((offset.X + ((this.Width / 0.9) / 2)) * windowRatio, offset.Y - (this.Height / 2));
            GL.TexCoord2(1, 0);
            GL.Vertex2((offset.X + ((this.Width / 0.9) / 2)) * windowRatio, offset.Y + this.Height);
            GL.TexCoord2(0, 0);
            GL.Vertex2((offset.X - ((this.Width / 0.9) / 2)) * windowRatio, offset.Y + this.Height);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTexture(19));
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(((offset.X + this.handCenter.X) - (0.5 * HANDWIDTH)) * windowRatio, (offset.Y + this.handCenter.Y) - (0.5 * HANDHEIGHT)); // draw first quad corner
            GL.TexCoord2(1, 1);
            GL.Vertex2(((offset.X + this.handCenter.X) + (0.5 * HANDWIDTH)) * windowRatio, (offset.Y + this.handCenter.Y) - (0.5 * HANDHEIGHT));
            GL.TexCoord2(1, 0);
            GL.Vertex2(((offset.X + this.handCenter.X) + (0.5 * HANDWIDTH)) * windowRatio, (offset.Y + this.handCenter.Y) + (0.5 * HANDHEIGHT));
            GL.TexCoord2(0, 0);
            GL.Vertex2(((offset.X + this.handCenter.X) - (0.5 * HANDWIDTH)) * windowRatio, (offset.Y + this.handCenter.Y) + (0.5 * HANDHEIGHT));
            GL.End();
        }

        public override void Move()
        {
            Vector2 direction = Pathprovider.Instance.GetDirectionVector(this.Center, this.Model.Player.Center);
            if (direction != (0, 0))
            {
                this.Direction = DirectionExtension.GetDirectionFromVector2(direction);
            }
        }

        public void StartAttacking()
        {
            this.Attack();
        }

        public void Attack()
        {
            if (!this.IsAttacking && !this.hasAttackCD && !this.State.IsDead())
            {
                float distance = Vector2.Subtract(this.Center, this.Model.Player?.Center ?? (0, 0)).Length;
                if (distance < (this.Weapon?.Range ?? 0))
                {
                    this.IsAttacking = true;
                    this.attackTimer.Interval = ATTACKTIME;
                    this.attackTimer.Start();

                    switch (Randomizer.GetInt(3))
                    {
                        case 0:
                            this.Attack1();
                            break;
                        case 1:
                            this.Attack2();
                            break;
                        case 2:
                            this.Attack3();
                            break;
                    }
                }
            }
        }

        private void Attack1() // Bullet
        {
            this.animTimer.Interval = this.animTime;
            this.animTimer.Elapsed += this.EndAnimA1;
            this.animTimer.Start();
            this.Weapon.StartAttack(0);
        }

        private void Attack2() // Fire
        {
            this.animTimer.Interval = this.animTime;
            this.animTimer.Elapsed += this.EndAnimA2;
            this.animTimer.Start();
            this.Weapon.StartAttack(1);
        }

        private void Attack3() // Swing
        {
            this.animTimer.Interval = this.animTime;
            this.animTimer.Elapsed += this.EndAnimA3;
            this.animTimer.Start();
            this.Weapon.StartAttack(2);
        }

        private void EndAttack(object source, ElapsedEventArgs e)
        {
            this.attackTimer.Stop();

            this.hasAttackCD = true;
            this.IsAttacking = false;

            int rndcd = Randomizer.GetInt(10);

            this.attackCDTimer.Interval = ATTACKCD + (ATTACKCDRND * rndcd);
            this.attackCDTimer.Start();
        }

        private void EndAttackCD(object source, ElapsedEventArgs e)
        {
            this.attackCDTimer.Stop();

            this.hasAttackCD = false;
        }

        private void EndAnimA1(object source, ElapsedEventArgs e)
        {
            this.animTimer.Stop();

            if (this.animCount < 5)
            {
                this.handCenter += new Vector2(-0.05f, 0.05f);
                this.animCount++;
                this.animTimer.Start();
            }
            else if (this.animCount < 7)
            {
                this.animCount++;
                this.animTimer.Start();
            }
            else if (this.animCount < 9)
            {
                this.handCenter += new Vector2(0.1f, -0.1f);
                this.animCount++;
                this.animTimer.Start();
            }
            else
            {
                this.handCenter = this.handCenterORG;
                this.animCount = 0;
                this.animTimer.Elapsed -= this.EndAnimA1;
            }
        }

        private void EndAnimA2(object source, ElapsedEventArgs e)
        {
            this.animTimer.Stop();

            if (this.animCount < 4)
            {
                this.handCenter += new Vector2(0, 0.02f);
                this.animCount++;
                this.animTimer.Start();
            }
            else if (this.animCount < 6)
            {
                this.handCenter += new Vector2(0f, -0.05f);
                this.animCount++;
                this.animTimer.Start();
            }
            else if (this.animCount < 10)
            {
                this.animCount++;
                this.animTimer.Start();
            }
            else
            {
                this.handCenter = this.handCenterORG;
                this.animCount = 0;
                this.animTimer.Elapsed -= this.EndAnimA2;
            }
        }

        private void EndAnimA3(object source, ElapsedEventArgs e)
        {
            this.animTimer.Stop();

            if (this.animCount < 6)
            {
                this.handCenter += new Vector2(+0.025f, 0f);
                this.animCount++;
                this.animTimer.Start();
            }
            else if (this.animCount < 15)
            {
                this.animCount++;
                this.animTimer.Start();
            }
            else
            {
                this.handCenter = this.handCenterORG;
                this.animCount = 0;
                this.animTimer.Elapsed -= this.EndAnimA3;
            }
        }
    }
}
