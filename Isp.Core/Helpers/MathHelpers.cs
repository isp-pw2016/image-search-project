using System;
using System.Collections.Generic;
using System.Linq;

namespace Isp.Core.Helpers
{
    public static class MathHelpers
    {
        public static double Median(this IEnumerable<double> source)
        {
            var doubles = source?.ToList();
            if (doubles == null || doubles.Count == 0)
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            var midpoint = (doubles.Count - 1)/2;
            var sorted = doubles.OrderBy(n => n);
            var median = sorted.ElementAt(midpoint);

            if (doubles.Count%2 == 0)
            {
                median = (median + sorted.ElementAt(midpoint + 1))/2;
            }

            return median;
        }
    }
}