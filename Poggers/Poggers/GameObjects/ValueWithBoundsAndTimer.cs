using System.Timers;

namespace Poggers.GameObjects
{
    public class ValueWithBoundsAndTimer : ValueWithBounds
    {
        private const float INCREMENT = 5f;
        private const int INCREMENTINTERVAL = 200;
        private const int CDINTERVAL = 1000;

        private readonly Timer incremetTimer;
        private readonly Timer incrementCDTimer;

        public ValueWithBoundsAndTimer(float value, float upperBounds, float lowerBounds)
            : base(value, upperBounds, lowerBounds)
        {
            this.incremetTimer = new Timer(INCREMENTINTERVAL);
            this.incremetTimer.Elapsed += (_, __) => this.incremetTimer.Enabled = this.Add(INCREMENT);

            this.incrementCDTimer = new Timer(CDINTERVAL);
            this.incrementCDTimer.Elapsed += (_, __) =>
            {
                this.incrementCDTimer.Stop();
                this.incremetTimer.Start();
            };
        }

        public ValueWithBoundsAndTimer(float upperBounds, float lowerBounds)
           : this(upperBounds, upperBounds, lowerBounds)
        {
        }

        public ValueWithBoundsAndTimer(float upperBounds)
            : this(upperBounds, upperBounds, 0)
        {
        }

        public new bool Subtract(float value, bool subtractAnyways = false)
        {
            bool result = base.Subtract(value, subtractAnyways);
            if (result || subtractAnyways)
            {
                this.incremetTimer.Stop();
                this.incrementCDTimer.Interval = CDINTERVAL;
                this.incrementCDTimer.Start();
            }

            return result;
        }
    }
}
