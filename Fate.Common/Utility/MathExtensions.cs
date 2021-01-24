namespace Fate.Common.Utility
{
    public static class MathExtensions
    {
        public static decimal SafeDivision(this decimal numerator, decimal denominator)
        {
            return (denominator == 0) ? 0 : numerator / denominator;
        }

        public static decimal SafeDivision(this int numerator, int denominator)
        {
            return (denominator == 0) ? 0 : numerator / denominator;
        }

        public static double SafeDivision(this double numerator, double denominator)
        {
            return (denominator == 0) ? 0 : numerator / denominator;
        }
    }
}
