using System;

namespace Isp.Core.Extensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// Convert elapsed time to a string representation
        /// </summary>
        /// <param name="time">Double value representing elapsed time by Stopwatch class</param>
        /// <param name="precision">Round off precision</param>
        /// <returns>Double value parsed as a time string</returns>
        public static string ToTimeString(this double time, int precision = 3)
        {
            return ToTimeStringFormatter(time, precision);
        }

        /// <summary>
        /// Convert if given elapsed time to a string representation
        /// </summary>
        /// <param name="time">Nullable double value representing elapsed time by Stopwatch class</param>
        /// <param name="precision">Round off precision</param>
        /// <returns>Double value parsed as a time string</returns>
        public static string ToTimeString(this double? time, int precision = 3)
        {
            return !time.HasValue
                ? string.Empty
                : ToTimeStringFormatter(time.Value, precision);
        }

        private static string ToTimeStringFormatter(this double time, int precision = 3)
        {
            if (time > 1)
                return $"{Math.Round(time, precision)} s";
            if (time > 1e-3)
                return $"{Math.Round(1e3 * time, precision)} ms";
            if (time > 1e-6)
                return $"{Math.Round(1e6 * time, precision)} µs";
            if (time > 1e-9)
                return $"{Math.Round(1e9 * time, precision)} ns";

            return string.Empty;
        }
    }
}