using OpenTK.Mathematics;
using Poggers.Directions;
using Poggers.EntityStates;

namespace Poggers.GameObjects
{
    public interface IBasicEntity
    {
        public Direction Direction { get; }

        public Vector2 Center { get; }

        public float Width { get; }

        public float Height { get; }

        public EntityState State { get; set; }
    }
}
