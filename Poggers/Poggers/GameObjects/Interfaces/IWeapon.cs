namespace Poggers.GameObjects
{
    public interface IWeapon
    {
        public float EnduranceCost { get; set; }

        public float Range { get; }

        public bool IsUsable { get; }

        public IArmedEntity Owner { get; }

        public void StartAttack(int attack);

        public void StopAttack();
    }
}
