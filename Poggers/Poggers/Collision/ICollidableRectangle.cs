namespace Poggers.Collision
{
    public interface ICollidableRectangle : ICollidable
    {
        public float Width { get; }

        public float Height { get; }
    }
}
