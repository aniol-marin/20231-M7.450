namespace Mole.Halt.Utils
{
    public static class NumberExtensions
    {
        public static string ToPrecision(this float value, int decimals = 1)
        {
            string format = string.Empty;
            for (int i = 0; i < decimals; i++)
            {
                format += "0";
            }
            return string.Format($"{{0,0:#.{decimals}}}", value);
        }
    }
}