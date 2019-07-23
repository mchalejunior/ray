using System;

namespace Ray.Domain.Extensions
{
    public static class FloatExtensionMethods
    {
        public static bool IsApproximately(this float left, float right)
        {
            return Math.Abs(left - right) <= Single.Epsilon;
        }
    }
}
