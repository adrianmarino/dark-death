namespace Util
{
    public class Random
    {
        private static readonly System.Random random = new System.Random();

        public static int GetNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        private Random()
        {
        }
    }
}