using OpenTK.Mathematics;
using Poggers.Interfaces;

namespace Poggers.GameObjects
{
    public abstract class GameObject : IGameObject
    {
        public GameObject(Vector2 center)
        {
            this.Center = center;
        }

        public Vector2 Center { get; set; }

        public abstract void Draw(Vector2 offset, float windowRatio);
    }
}
