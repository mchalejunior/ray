using System;

namespace Ray.Domain.Model
{
    /// <summary>
    /// Replacing System.Windows.Media.Color, as is the only dependency on Windows in Ray.Domain.
    /// </summary>
    public struct Color
    {
        public float ScA;
        public float ScB;
        public float ScG;
        public float ScR;

        public static Color FromScRgb(float a, float r, float g, float b)
        {
            return new Color
            {
                ScA = a,
                ScR = r,
                ScB = b,
                ScG = g
            };
        }


        #region Straight from System.Windows.Media.Color decompilation

        public byte A
        {
            get
            {
                var a = ScA;
                if (a < 0.0)
                    a = 0.0F;
                else if (a > 1.0) 
                    a = 1F;

                return (byte) (a * (double) byte.MaxValue + 0.5);
            }
        }

        public byte R => ScRgbTosRgb(ScR);
        public byte G => ScRgbTosRgb(ScR);
        public byte B => ScRgbTosRgb(ScR);


        private static byte ScRgbTosRgb(float val)
        {
            if (val <= 0.0)
                return 0;
            if (val <= 0.0031308)
                return (byte) (byte.MaxValue * (double) val * 12.9200000762939 + 0.5);
            return (double) val < 1.0
                ? (byte) (byte.MaxValue * (1.05499994754791 * Math.Pow(val, 5.0 / 12.0) - 0.0549999997019768) + 0.5)
                : byte.MaxValue;
        }

        public static Color operator *(Color color, float coefficient)
        {
            // TODO: Might be some rounding required here. Come back to.
            return Color.FromScRgb(color.ScA * coefficient, color.ScR * coefficient, color.ScG * coefficient, color.ScB * coefficient);
        }

        public static Color operator +(Color color1, Color color2)
        {
            // TODO: Might be some rounding required here. Come back to.
            return Color.FromScRgb(color1.ScA + color2.ScA, color1.ScR + color2.ScR, color1.ScG + color2.ScG, color1.ScB + color2.ScB);
        }

        public static Color operator -(Color color1, Color color2)
        {
            // TODO: Might be some rounding required here. Come back to.
            return Color.FromScRgb(color1.ScA - color2.ScA, color1.ScR - color2.ScR, color1.ScG - color2.ScG, color1.ScB - color2.ScB);
        }

        #endregion

    }
}
