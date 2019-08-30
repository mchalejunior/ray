using System.Numerics;

namespace Ray.Domain.Extensions
{
    public static class MatrixExtensionMethods
    {
        public static Vector4 Multiply(this Matrix4x4 left, Vector4 right)
        {
            // TODO: some useful prose to explain (skinny matrix!).
            // TODO: Not sure if the "Tuple" must be a "Vector" (W = 1.0F).
            //    Figure out and then do check like VectorExtensionMethods.Cross.

            var skinnyMatrix = new Matrix4x4 {M11 = right.X, M21 = right.Y, M31 = right.Z, M41 = right.W};
            var matrixCalculation = left * skinnyMatrix;

            return new Vector4(
                matrixCalculation.M11,
                matrixCalculation.M21,
                matrixCalculation.M31,
                matrixCalculation.M41
            );
        }
    }
}
