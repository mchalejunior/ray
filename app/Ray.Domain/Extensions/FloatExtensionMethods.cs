using System;
using System.Collections.Generic;

namespace Ray.Domain.Extensions
{
    public static class FloatExtensionMethods
    {
        private static readonly Dictionary<int, float> ToleranceCache = new Dictionary<int, float>
        {
            {1, (float) double.Parse("1E-" + 1)},
            {2, (float) double.Parse("1E-" + 2)},
            {3, (float) double.Parse("1E-" + 3)},
            {4, (float) double.Parse("1E-" + 4)},
            {5, (float) double.Parse("1E-" + 5)},
            {6, (float) double.Parse("1E-" + 6)},
            {7, (float) double.Parse("1E-" + 7)},
            {8, (float) double.Parse("1E-" + 8)},
            {9, (float) double.Parse("1E-" + 9)},
            {10,(float) double.Parse("1E-" + 10)}
        };

        public static bool IsApproximately(this float left, float right, int? decimalPlaces = 7)
        {
            if (decimalPlaces == null)
            {
                // Use Epsilon (minimum possible difference
                return Math.Abs(left - right) <= float.Epsilon;
            }

            // Use a more tolerant and likely difference.
            float tolerance = ToleranceCache[decimalPlaces.Value];
            float difference = Math.Abs(left - right);

            bool result = difference <= tolerance;
            return result;
        }
    }
}
