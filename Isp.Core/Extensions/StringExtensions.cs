namespace Isp.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Convert given string to numerical of the long (int64) type
        /// </summary>
        /// <param name="str">String to be parsed as long</param>
        /// <returns>Numerical value or null</returns>
        public static long? TryToInt64(this string str)
        {
            long result;
            if (long.TryParse(str, out result))
            {
                return result;
            }

            return null;
        }
    }
}