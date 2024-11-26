namespace GladiatorsFight.Model.Infrastructure
{
    public static class UserUtils
    {
        private static Random _generator = new Random();

        public static int GetRandomNumber(int minValue, int maxValue) 
        { 
            return _generator.Next(minValue, maxValue + 1);
        }

        public static float GetRandomSingle() 
        { 
            return _generator.NextSingle();
        }
    }
}
