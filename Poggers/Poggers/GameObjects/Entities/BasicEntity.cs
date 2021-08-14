using System.Timers;
using OpenTK.Mathematics;
using Poggers.Collision;
using Poggers.Directions;
using Poggers.EntityStates;
using Poggers.Interfaces;

namespace Poggers.GameObjects.Entities
{
    public abstract class BasicEntity : GameRectangle, IBasicEntity
    {
        public const float MAXSPEED = 0.02f;
        public const int INVINCIBLETIME = 800;
        private const int HITTIME = 200;
        private const float REVERTOFFSET = 0.001f;

        private readonly IModel model;
        private ValueWithBounds health;
        private ValueWithBoundsAndTimer endurance;
        private float speed;
        private int invincibleTime;
        private int hitTime;
        private EntityState state;
        private Direction direction;
        private Timer invincibleTimer;
        private Timer hitTimer;
        private float tmpX;
        private float tmpY;

        public BasicEntity(Vector2 center, float width, float height, IModel model)
            : base(center, width, height)
        {
            this.Speed = MAXSPEED;
            this.InvincibleTime = INVINCIBLETIME;
            this.HitTime = HITTIME;

            this.direction = Direction.S;
            this.State = EntityState.Idle;

            this.invincibleTimer = new Timer();
            this.invincibleTimer.Elapsed += (_, __) => this.invincibleTimer.Stop();

            this.hitTimer = new Timer();
            this.hitTimer.Elapsed += (_, __) => this.hitTimer.Stop();
            this.model = model;
        }

        public ValueWithBounds Health { get => this.health; set => this.health = value; }

        public ValueWithBoundsAndTimer Endurance { get => this.endurance; set => this.endurance = value; }

        public float Speed { get => this.speed; set => this.speed = value; }

        public int InvincibleTime { get => this.invincibleTime; set => this.invincibleTime = value; }

        public int HitTime { get => this.hitTime; set => this.hitTime = value; }

        public Direction Direction { get => this.direction; set => this.direction = value; }

        public float TmpX { get => this.tmpX; set => this.tmpX = value; }

        public float TmpY { get => this.tmpY; set => this.tmpY = value; }

        public Timer InvincibleTimer { get => this.invincibleTimer; set => this.invincibleTimer = value; }

        float IBasicEntity.Width => this.Width;

        float IBasicEntity.Height => this.Height;

        public EntityState State { get => this.state; set => this.state = value; }

        public bool IsInvincible { get => this.invincibleTimer.Enabled; }

        public bool IsHit { get => this.hitTimer.Enabled; }

        public Color4 SpriteColor
        {
            get
            {
                float alpha = this.IsInvincible ? 0.5f : 1f;
                Color4 tmp = this.IsHit ? Color4.Red : Color4.White;
                return new Color4(tmp.R, tmp.G, tmp.G, alpha);
            }
        }

        public IModel Model => this.model;

        public abstract void Move();

        public abstract void Die();

        public void TakeDamage(float damage)
        {
            if (!this.IsInvincible)
            {
                this.StartHitTimer();
                this.StartInvincibleTimer(this.InvincibleTime);
                if (!this.Health.Subtract(damage, true))
                {
                    this.State = EntityState.Dead;
                    this.Die();
                }
            }
        }

        public void StartInvincibleTimer(int dur)
        {
            this.invincibleTimer.Interval = dur;
            this.invincibleTimer.Start();
        }

        public void StartHitTimer()
        {
            this.hitTimer.Interval = this.HitTime;
            this.hitTimer.Start();
        }

        public void MoveX()
        {
            this.Center = (this.Center.X + this.TmpX, this.Center.Y);
        }

        public void MoveY()
        {
            this.Center = (this.Center.X, this.Center.Y + this.TmpY);
        }

        public void RevertMovementX(ICollidable collidable)
        {
            float difference = (this.Width / 2) + REVERTOFFSET;

            if (collidable is ICollidableCircle circle)
            {
                difference += circle.Radius;
            }
            else if (collidable is ICollidableRectangle rectangle)
            {
                difference += rectangle.Width / 2;
            }

            if (this.Center.X > collidable.Center.X)
            {
                this.Center = (collidable.Center.X + difference, this.Center.Y);
            }
            else
            {
                this.Center = (collidable.Center.X - difference, this.Center.Y);
            }
        }

        public void RevertMovementY(ICollidable collidable)
        {
            float difference = (this.Height / 2) + REVERTOFFSET;

            if (collidable is ICollidableCircle circle)
            {
                difference += circle.Radius;
            }
            else if (collidable is ICollidableRectangle rectangle)
            {
                difference += rectangle.Height / 2;
            }

            if (this.Center.Y > collidable.Center.Y)
            {
                this.Center = (this.Center.X, collidable.Center.Y + difference);
            }
            else
            {
                this.Center = (this.Center.X, collidable.Center.Y - difference);
            }
        }

        public void FinishedMovement()
        {
            if (!this.State.IsDodging())
            {
                this.TmpX = 0;
                this.TmpY = 0;
            }
        }
    }
}
