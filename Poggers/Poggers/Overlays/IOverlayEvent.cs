using Poggers.Interfaces;

namespace Poggers.Overlays
{
    public interface IOverlayEvent : IOverlay
    {
        public void Start(int eventIndex);
    }
}
