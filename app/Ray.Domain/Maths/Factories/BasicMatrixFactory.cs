using System.Numerics;

namespace Ray.Domain.Maths.Factories
{
    public static class BasicMatrixFactory
    {
        /// <summary>
        /// Create a Shearing matrix with the specified axis-ratio locks.
        /// </summary>
        /// <param name="x2y">X in proportion to Y</param>
        /// <param name="x2z">X in proportion to Z</param>
        /// <param name="y2x">Y in proportion to X</param>
        /// <param name="y2z">Y in proportion to Z</param>
        /// <param name="z2x">Z in proportion to X</param>
        /// <param name="z2y">Z in proportion to Y</param>
        /// <returns>
        /// A Shearing matrix with encoded axis-ratio locks.
        /// Encoded in RMF (Row Major Form) - Microsoft standard. This is consistent
        /// with all other transformations. See test suite to understand when the
        /// conversion to CMF occurs - the Ray Tracer Challenge text standard and expectation.
        /// </returns>
        /// <remarks>
        /// A points movement on an axis are proportional to another axis movements.
        /// </remarks>
        public static Matrix4x4 CreateShearingMatrix(float x2y, float x2z, float y2x, float y2z, float z2x, float z2y)
        {
            var matrix = Matrix4x4.Identity;
            matrix.M21 = x2y;
            matrix.M31 = x2z;
            matrix.M12 = y2x;
            matrix.M32 = y2z;
            matrix.M13 = z2x;
            matrix.M23 = z2y;

            return matrix;
        }
    }
}
