using OpenTK.Mathematics;
using Poggers.Directions;
using Poggers.EntityStates;
using Poggers.GameObjects.Items;
using Poggers.Interfaces;
using Poggers.Pathfinding;
using Poggers.Randomize;

namespace Poggers.GameObjects.Entities.Enemies
{
    public abstract class BasicEnemy : BasicEntity
    {
        private const float TRIGGERDISTANCE = 1f;

        public BasicEnemy(Vector2 center, float width, float height, IModel model)
            : base(center, width, height, model)
        {
            this.Speed = MAXSPEED / 10;
            this.InvincibleTime = (int)(INVINCIBLETIME / 2.5);
        }

        public override void Die()
        {
            IItem item = Randomizer.GetInt(9) switch
            {
                0 => new SpeedPotion(this.Center),
                1 => new HealthPotion(this.Center),
                2 => new EndurancePotion(this.Center),
                _ => null,
            };

            if (item != null)
            {
                this.Model.AddItem(item);
            }
        }

        public override void Move()
        {
            if (this.State.IsIdle() && Vector2.Distance(this.Center, this.Model.Player.Center) < TRIGGERDISTANCE)
            {
                this.State = EntityState.Moving;
            }

            if (this.State.IsMoving() && Vector2.Distance(this.Center, this.Model.Player.Center) > TRIGGERDISTANCE * 1.5)
            {
                this.State = EntityState.Idle;
            }

            if (this.State.IsMoving())
            {
                Vector2 direction = Pathprovider.Instance.GetDirectionVector(this.Center, this.Model.Player.Center);
                if (direction != (0, 0))
                {
                    this.Direction = DirectionExtension.GetDirectionFromVector2(direction);
                    Vector2 velocity = this.Direction.GetDirectionWithLength(this.Speed);
                    this.TmpX = velocity.X;
                    this.TmpY = velocity.Y;
                }
            }
        }
    }
}