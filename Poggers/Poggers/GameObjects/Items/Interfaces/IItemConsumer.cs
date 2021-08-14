namespace Poggers.GameObjects.Items
{
    public interface IItemConsumer
    {
        public float Health { get; set; }

        public float Endurance { get; set; }

        public float Speed { get;  set; }

        public IItem[] Items { get; }

        public void AddItem(IItem item);
    }
}
