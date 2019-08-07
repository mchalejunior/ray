using System.Windows.Media;

namespace Ray.Domain.Extensions
{
    public static class ColorExtensionMethods
    {
        /// <summary>
        /// Multiply colors i.e. mix two colors together - 
        /// aka the Hadamard Product (domain: linear algebra / matrices).
        /// </summary>
        /// <returns>New color that's the mix of the two input colors</returns>
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
    }
}
