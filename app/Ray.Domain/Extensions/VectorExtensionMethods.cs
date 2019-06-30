using System;
using System.Numerics;

namespace Ray.Domain.Extensions
{
    public static class VectorExtensionMethods
    {
        public static bool IsPoint(this Vector4 tuple)
        {
            return Math.Abs(tuple.W - 1.0) <= Single.Epsilon;
        }

        public static bool IsVector(this Vector4 tuple)
        {
            return Math.Abs(tuple.W) <= Single.Epsilon;
        }
    }
}
