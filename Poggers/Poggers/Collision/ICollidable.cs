using OpenTK.Mathematics;

namespace Poggers.Collision
{
    public interface ICollidable
    {
        public Vector2 Center { get; }

        public bool CollidesWith(ICollidable other);
    }
}
