using System.Timers;
using Poggers.GameObjects.Entities;

namespace Poggers.Overlays
{
    public class FadeEvent : IOverlayEvent
    {
        private readonly Timer timer;
        private Lightning lightning;
        private int eventCounter;

        public FadeEvent()
        {
            this.timer = new Timer(100);
        }

        public void Draw(float windowRatio)
        {
            this.lightning?.Draw(this.lightning.Center, windowRatio);
        }

        public void Start(int eventIndex)
        {
            this.ClearListeners();
            this.eventCounter = 0;
            if (eventIndex == 0)
            {
                this.StartFadeIn();
            }
            else
            {
                this.StartFadeOut();
            }
        }

        public void StartFadeIn()
        {
            this.lightning = new Lightning((0, 0), 2f, 2f);
            this.timer.Elapsed += this.FadeInUpdate;
            this.timer.Start();
        }

        public void StartFadeOut()
        {
            this.lightning = new Lightning((0, 0), 5f, 5f);
            this.timer.Elapsed += this.FadeOutUpdate;
            this.timer.Start();
        }

        private void FadeInUpdate(object source, ElapsedEventArgs e)
        {
            this.timer.Stop();
            if (this.eventCounter <= 6)
            {
                this.eventCounter += 1;
                this.lightning.Width += 0.5f;
                this.lightning.Height += 0.5f;
                this.timer.Start();
            }
        }

        private void FadeOutUpdate(object source, ElapsedEventArgs e)
        {
            this.timer.Stop();
            if (this.eventCounter <= 6)
            {
                this.eventCounter += 1;
                this.lightning.Width -= 0.5f;
                this.lightning.Height -= 0.5f;
                this.timer.Start();
            }
        }

        private void ClearListeners()
        {
            this.timer.Elapsed -= this.FadeInUpdate;
            this.timer.Elapsed -= this.FadeOutUpdate;
        }
    }
}
