namespace Poggers.GameObjects.Items
{
    public interface IItem
    {
        public int Texture { get; }

        public void ApplyEffects(IItemConsumer consumer);
    }
}
