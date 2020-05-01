using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Ray.Domain.Extensions
{
    public static class ColorExtensionMethods
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

        /// <summary>
        /// Convert a WPF (PresentationCore) Color to a System.Drawing.Color.
        /// </summary>
        /// <remarks>
        /// The reason for this is as follows:
        /// 1. WPF colors provide maths operations that the ray tracer needs.
        /// 2. WPF Bitmaps are too complex.
        /// So we need one object from System.Windows.Media and the other from System.Drawing.
        /// Therefore we need to convert from WPF color to old-skool color to avail of the
        /// much simpler and fit-for-purpose Bitmap object.
        /// </remarks>
        public static System.Drawing.Color Simplify(this Color value, int? fixedAlpha = null)
        {
            return System.Drawing.Color.FromArgb(fixedAlpha ?? value.A, value.R, value.G, value.B);
        }

        /// <summary>
        /// Multiply colors i.e. blend colors - 
        /// aka the Hadamard Product (domain: linear algebra / matrices).
        /// </summary>
        /// <example>
        /// Calculate the visible color of a yellow-green surface when illuminated by a reddish-purple light.
        /// For this case the R component ends up with the largest value, so the color "appears red".
        /// </example>
        /// <returns>New color that's a blend of the two input colors</returns>
        public static Color Multiply(this Color left, Color right)
        {
            return new Color
            {
                ScA = left.ScA * right.ScA,
                ScR = left.ScR * right.ScR,
                ScG = left.ScG * right.ScG,
                ScB = left.ScB * right.ScB
            };
        }

        public static bool AreClose(this Color left, Color right, bool ignoreAlpha, int? decimalPlaces = 6)
        {
            float tolerance = decimalPlaces == null ? float.Epsilon : ToleranceCache[decimalPlaces.Value];

            if (MathF.Abs(left.ScR - right.ScR) > tolerance) return false;
            if (MathF.Abs(left.ScG - right.ScG) > tolerance) return false;
            if (MathF.Abs(left.ScB - right.ScB) > tolerance) return false;

            return ignoreAlpha || MathF.Abs(left.ScA - right.ScA) <= tolerance;
        }
    }
}
