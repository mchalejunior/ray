using System;

namespace Ray.Domain.Extensions
{
    public static class FloatExtensionMethods
    {
        public static bool IsApproximately(this float left, float right, int? decimalPlaces = 7)
        {
            if (decimalPlaces == null)
            {
                // Use Epsilon (minimum possible difference
                return Math.Abs(left - right) <= Single.Epsilon;
            }

            // Use a more tolerant and likely difference.
            // TODO: could cache result of this parse in a dictionary
            float tolerance =(float) double.Parse("1E-" + decimalPlaces);
            float difference = Math.Abs(left - right);

            bool result = difference <= tolerance;
            return result;
        }
    }
}
