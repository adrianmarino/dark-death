using System;

namespace Util
{
    public class RandomUtils
    {
        private static readonly Random random = new Random();

        public static int GetNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        private RandomUtils()
        {
        }
    }
}