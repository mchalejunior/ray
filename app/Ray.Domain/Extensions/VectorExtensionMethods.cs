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

        public static Vector4 Cross(this Vector4 left, Vector4 right)
        {
            // Cross Product only with Vector3.
            // Apparently it's possible with 4D vectors, but significantly more complex.
            // And not required by the 3D Ray Tracer.

            if (left.IsPoint() || right.IsPoint())
            {
                throw new ApplicationException(
                    "Cross Product can only be calculated on two Vectors (w=0).");
            }

            return Vector3.Cross(
                new Vector3(left.X, left.Y, left.Z),
                new Vector3(right.X, right.Y, right.Z)
            ).AsVector();
        }

        public static bool IsUnitVector(this Vector4 tuple, bool isUnitTest = false)
        {
            if (!isUnitTest)
            {
                throw new ApplicationException(
                    @"Wouldn't recommend calling in Production. 
                    Instead can assume v.Normalize works as expected.");
            }

            if (!tuple.IsVector())
            {
                return false;
            }

            // Slightly convoluted assertion.
            // Basically the normalization process gives a Vector with magnitude 1.
            // But it's not accurate to Single.Epsilon.
            // So here we normalize another vector and use it for comparison.

            var unitVector = Vector4.Normalize(new Vector4(5.0F, -5.0F, 4.0F, 0.0F));
            return unitVector.Length().IsApproximately(tuple.Length());
        }
    }
}
