using OpenTK.Mathematics;
using Poggers.Interfaces;

namespace Poggers.GameObjects.Weapons.Attacks
{
    public interface IAttackComponent : IGameObject
    {
        public void Update();

        public void Draw(Vector2 offset, float windowRatio);
    }
}
