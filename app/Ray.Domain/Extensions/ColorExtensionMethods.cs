using System.Windows.Media;

namespace Ray.Domain.Extensions
{
    public static class ColorExtensionMethods
    {
        public static Color Multiply(this Color left, Color right)
        {
            // TODO: What to do with Alpha value?
            // TODO: What about Alpha defaults in general?

            return new Color
            {
                ScR = left.ScR * right.ScR,
                ScG = left.ScG * right.ScG,
                ScB = left.ScB * right.ScB
            };
        }
    }
}
