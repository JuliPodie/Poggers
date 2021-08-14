using System;

namespace Poggers.GameObjects
{
    public class ValueWithBounds
    {
        private readonly float upperBounds;
        private readonly float lowerBounds;
        private float value;

        public ValueWithBounds(float value, float upperBounds, float lowerBounds)
        {
            this.value = value;
            this.upperBounds = upperBounds;
            this.lowerBounds = lowerBounds;
        }

        public ValueWithBounds(float upperBounds, float lowerBounds)
           : this(upperBounds, upperBounds, lowerBounds)
        {
        }

        public ValueWithBounds(float upperBounds)
            : this(upperBounds, upperBounds, 0)
        {
        }

        public float Value
        {
            get => this.value; set
            {
                if (value > this.upperBounds || value < this.lowerBounds)
                {
                    throw new ArgumentException();
                }

                this.value = value;
            }
        }

        /// <summary>
        /// Adds the parsed value to the current <see cref="value"/>.
        /// </summary>
        /// <param name="value">The Value to add.</param>
        /// <returns><c>true</c>, if the resulting value is less than <see cref="upperBounds"/>. <c>false</c>, if not.</returns>
        public bool Add(float value)
        {
            if (this.value + value <= this.upperBounds)
            {
                this.value += value;
                return true;
            }

            this.value = this.upperBounds;
            return false;
        }

        /// <summary>
        /// Subtracts the parsed value from the current value, if the resulting <see cref="value"/> is not greater than <see cref="lowerBounds"/>. The value is not subtracted.
        /// </summary>
        /// <param name="value">The Value to subtract.</param>
        /// <param name="subtractAnyways">If set to true, the subtraction in performed regardless of the result.</param>
        /// <returns><c>true</c>, if the value is greater than <see cref="lowerBounds"/>. <c>false</c>, if not.</returns>
        public bool Subtract(float value, bool subtractAnyways = false)
        {
            if (this.value - value >= this.lowerBounds)
            {
                this.value -= value;
                return true;
            }

            if (subtractAnyways)
            {
                this.value = Math.Max(this.value - value, 0);
            }

            return false;
        }
    }
}
