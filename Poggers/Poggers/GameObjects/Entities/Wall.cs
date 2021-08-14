using OpenTK.Mathematics;

namespace Poggers.GameObjects.Entities
{
    public class Wall : GridBox
    {
        private bool destroyAble;
        private bool destroyed;

        public Wall(Vector2 center, float width, float height, bool destroyAble, uint typ)
            : base(center, width, height, typ)
        {
            this.DestroyAble = destroyAble;
        }

        public bool DestroyAble { get => this.destroyAble; set => this.destroyAble = value; }

        public bool Destroyed { get => this.destroyed; set => this.destroyed = value; }
    }
}
