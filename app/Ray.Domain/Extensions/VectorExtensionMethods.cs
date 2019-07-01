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

        public static Vector4 AsPoint(this Vector3 tuple)
        {
            return new Vector4(tuple, 1.0F);
        }

        public static Vector4 AsVector(this Vector3 tuple)
        {
            return new Vector4(tuple, 0.0F);
        }
    }
}
