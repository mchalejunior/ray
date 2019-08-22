using System.Windows.Media;

namespace Ray.Domain.Extensions
{
    public static class ColorExtensionMethods
    {
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
        public static System.Drawing.Color Simplify(this Color value)
        {
            return System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
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

        public static bool AreClose(Color left, Color right, bool ignoreAlpha)
        {
            if (!ignoreAlpha)
            {
                return Color.AreClose(left, right);
            }

            // Slightly convoluted assertion.
            // Basically the individual RGB comparisons can be outside of Single.Epsilon,
            // but Color.AreClose would still be happy they're the same color.
            // So let's use Color.AreClose logic, but just eliminate the Alpha variation.
            return Color.AreClose(
                Color.FromScRgb(1.0F, left.ScR, left.ScG, left.ScB),
                Color.FromScRgb(1.0F, right.ScR, right.ScG, right.ScB)
            );
        }
    }
}
