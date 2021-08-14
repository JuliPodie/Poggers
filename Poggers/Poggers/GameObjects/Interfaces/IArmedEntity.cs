namespace Poggers.GameObjects
{
    public interface IArmedEntity : IBasicEntity
    {
        public IWeapon Weapon { get; }

        public ValueWithBoundsAndTimer Endurance { get; }

        public void Attack();
    }
}
