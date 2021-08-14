using System.Timers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Poggers.Directions;
using Poggers.EntityStates;
using Poggers.GameObjects.Items;
using Poggers.Input;
using Poggers.Interfaces;
using Poggers.Textures;

namespace Poggers.GameObjects.Entities
{
    public class Player : BasicEntity, IItemConsumer, IArmedEntity
    {
        private const float WIDTH = 0.1f;
        private const float HEIGHT = 0.15f;

        private const float DODGECOST = 15f;
        private const int DODGETIME = 500;
        private const int DODGECD = 200;

        private readonly float sprintSpeed;
        private bool sprint = false;

        private bool attacking;
        private int selectedItem;
        private Animator ani = new Animator();
        private Timer dodgeTimer;
        private IItem[] items = new IItem[3];
        private IWeapon weapon;

        public Player(Vector2 center, IModel model)
            : base(center, WIDTH, HEIGHT, model)
        {
            this.Health = new ValueWithBounds(4, 6, 1);
            this.Endurance = new ValueWithBoundsAndTimer(100);
            this.sprintSpeed = this.Speed * 1.5f;

            this.dodgeTimer = new Timer();
            this.dodgeTimer.Elapsed += this.EndDodge;

            InputProvider.Instance.SubscribeDown(this.OnKeyPressed);
            InputProvider.Instance.SubscribeUp(this.OnKeyPressed);
        }

        public int SelectedItem { get => this.selectedItem; set => this.selectedItem = value; }

        public IItem[] Items { get => this.items; set => this.items = value; }

        float IItemConsumer.Health { get => this.Health.Value; set => this.Health.Add(value - this.Health.Value); }

        float IItemConsumer.Endurance { get => this.Endurance.Value; set => this.Endurance.Add(value - this.Endurance.Value); }

        public bool HasDogeCD { get => this.dodgeTimer.Enabled; }

        public IWeapon Weapon { get => this.weapon; set => this.weapon = value; }

        public void OnKeyPressed(GameWindow window, KeyboardKeyEventArgs args)
        {
            if (window.KeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Model.OverlayController.ToggleMenu();
            }

            if (this.Model.OverlayController.MenuIsOpen)
            {
                return;
            }

            if (!this.State.IsRooted())
            {
                this.sprint = window.KeyboardState.IsKeyDown(Keys.LeftShift);

                this.State = EntityState.Moving;
                if (window.KeyboardState.IsKeyDown(Keys.W) && window.KeyboardState.IsKeyDown(Keys.D))
                {
                    this.Direction = Direction.WD;
                }
                else if (window.KeyboardState.IsKeyDown(Keys.W) && window.KeyboardState.IsKeyDown(Keys.A))
                {
                    this.Direction = Direction.WA;
                }
                else if (window.KeyboardState.IsKeyDown(Keys.S) && window.KeyboardState.IsKeyDown(Keys.D))
                {
                    this.Direction = Direction.SD;
                }
                else if (window.KeyboardState.IsKeyDown(Keys.S) && window.KeyboardState.IsKeyDown(Keys.A))
                {
                    this.Direction = Direction.SA;
                }
                else if (window.KeyboardState.IsKeyDown(Keys.D))
                {
                    this.Direction = Direction.D;
                }
                else if (window.KeyboardState.IsKeyDown(Keys.A))
                {
                    this.Direction = Direction.A;
                }
                else if (window.KeyboardState.IsKeyDown(Keys.S))
                {
                    this.Direction = Direction.S;
                }
                else if (window.KeyboardState.IsKeyDown(Keys.W))
                {
                    this.Direction = Direction.W;
                }
                else
                {
                    this.State = EntityState.Idle;
                }

                if (window.KeyboardState.IsKeyDown(Keys.Space) && !this.HasDogeCD)
                {
                    this.State = EntityState.Dodging;
                }

                if (window.KeyboardState.IsKeyDown(Keys.Left))
                {
                    this.UseItem(0);
                }

                if (window.KeyboardState.IsKeyDown(Keys.Up))
                {
                    this.UseItem(1);
                }

                if (window.KeyboardState.IsKeyDown(Keys.Right))
                {
                    this.UseItem(2);
                }

                this.attacking = window.KeyboardState.IsKeyDown(Keys.Down);
            }
        }

        public override void Move()
        {
            if (this.State.IsDodging() && !this.HasDogeCD)
            {
                this.Dodge();
            }
            else
            {
                this.FinishedMovement();
                if (this.State.IsMoving())
                {
                    Vector2 directionVector = this.Direction.GetDirectionWithLength(this.sprint ? this.sprintSpeed : this.Speed);

                    this.TmpX = directionVector.X;
                    this.TmpY = directionVector.Y;
                }
            }
        }

        public void Dodge()
        {
            if (this.Endurance.Subtract(DODGECOST))
            {
                this.StartInvincibleTimer(DODGETIME);
                this.dodgeTimer.Interval = DODGETIME;
                this.dodgeTimer.Start();
                this.Direction.GetDirectionWithLength((0.5f / 60f) / (DODGETIME / 1000f)).Deconstruct(out float tmpX, out float tmpY);
                this.TmpX = tmpX;
                this.TmpY = tmpY;
            }
            else
            {
                this.State = EntityState.Idle;
            }
        }

        public void EndDodge(object source, ElapsedEventArgs e)
        {
            if (this.State.IsDodging())
            {
                this.State = EntityState.Idle;
                this.dodgeTimer.Interval = DODGECD;
                this.FinishedMovement();
            }
            else
            {
                this.dodgeTimer.Stop();
            }
        }

        public void Attack()
        {
            if (this.attacking && !this.State.IsRooted() && this.Weapon.IsUsable && this.Endurance.Subtract(this.Weapon.EnduranceCost))
            {
                this.Weapon.StartAttack(0);
                this.attacking = false;
            }
        }

        public void Unsubscribe()
        {
            InputProvider.Instance.UnsubscribeDown(this.OnKeyPressed);
            InputProvider.Instance.UnsubscribeUp(this.OnKeyPressed);
        }

        public override void Draw(Vector2 offset, float windowRatio)
        {
            GL.BindTexture(TextureTarget.Texture2D, this.ani.GetPlayerFrame(this.State, this.Direction));
            GL.Color4(this.SpriteColor);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0f, 1f);
            GL.Vertex2((0 - (0.5 * this.Width)) * windowRatio, (this.Height / 2) - this.Height); // draw first quad corner
            GL.TexCoord2(1f, 1f);
            GL.Vertex2((0 + (0.5 * this.Width)) * windowRatio, (this.Height / 2) - this.Height);
            GL.TexCoord2(1f, 0f);
            GL.Vertex2((0 + (0.5 * this.Width)) * windowRatio, (this.Height / 2) + this.Height);
            GL.TexCoord2(0f, 0f);
            GL.Vertex2((0 - (0.5 * this.Width)) * windowRatio, (this.Height / 2) + this.Height);
            GL.End();
        }

        public override void Die()
        {
            this.Model.OverlayController.ShowDeathScreen();
        }

        public void AddItem(IItem item)
        {
            for (int i = 0; i < this.items.Length; i++)
            {
                if (this.Items[i] is null)
                {
                    this.Items[i] = item;
                    return;
                }
            }

            item.ApplyEffects(this);
        }

        private void UseItem(int index)
        {
            this.items[index]?.ApplyEffects(this);
            this.items[index] = null;
        }
    }
}
