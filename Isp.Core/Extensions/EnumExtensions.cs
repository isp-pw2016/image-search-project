using System;

namespace Isp.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the enum's value stored as integer
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Integer part of enum value</returns>
        public static long ToLong(this Enum value)
        {
            return Convert.ToInt64(value);
        }
    }
}