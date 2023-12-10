using System;

namespace Mole.Halt.Utils
{
    public static class NumberExtensions
    {
        public static string ToPrecision(this float value, int decimals = 1)
        {
            double factor = Math.Pow(10, decimals);
            return (Math.Truncate(value * factor) / factor).ToString();
        }
    }
}