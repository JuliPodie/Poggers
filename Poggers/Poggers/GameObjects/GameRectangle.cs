using OpenTK.Mathematics;
using Poggers.Collision;

namespace Poggers.GameObjects
{
    public abstract class GameRectangle : GameObject, ICollidableRectangle
    {
        public GameRectangle(Vector2 center, float width, float height)
            : base(center)
        {
            this.Width = width;
            this.Height = height;
        }

        float ICollidableRectangle.Width => this.Width;

        float ICollidableRectangle.Height => this.Height;

        Vector2 ICollidable.Center => this.Center;

        public float Width { get; set; }

        public float Height { get; set; }

        bool ICollidable.CollidesWith(ICollidable other)
        {
            return ColliderFunctions.Collides(this, other);
        }
    }
}
