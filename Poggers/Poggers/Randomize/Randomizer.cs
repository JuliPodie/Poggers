using System;

namespace Poggers.Randomize
{
    public static class Randomizer
    {
        private static readonly Random Random = new Random();

        public static int GetInt(int max, int min = 0)
        {
            return Random.Next(min, max);
        }
    }
}
