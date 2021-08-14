using OpenTK.Mathematics;
using Poggers.Collision;

namespace Poggers.GameObjects
{
    public abstract class GameCircle : GameObject, ICollidableCircle
    {
        public GameCircle(Vector2 center, float radius)
            : base(center)
        {
            this.Radius = radius;
        }

        float ICollidableCircle.Radius => this.Radius;

        Vector2 ICollidable.Center => this.Center;

        public float Radius { get; set; }

        bool ICollidable.CollidesWith(ICollidable other)
        {
            return ColliderFunctions.Collides(this, other);
        }
    }
}
