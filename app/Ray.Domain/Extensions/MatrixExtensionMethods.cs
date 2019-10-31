using System.Numerics;

namespace Ray.Domain.Extensions
{
    public static class MatrixExtensionMethods
    {
        /// <summary>
        /// Convert from <see cref="System.Numerics"/> "Row Major Form" notation
        /// to the Ray Tracer Texts expected "Column Major Form".
        /// </summary>
        /// <remarks>
        /// This is a major difference between the text and the code.
        /// The text uses CMF and this is often the standard in mathematics literature.
        /// But Microsoft has been using RMF since the early days of computing.
        /// 
        /// It doesn't matter which you use, but calculations are very different and so consistency is important.
        /// The Ray Tracer Text is the requirements as far as this code project is concerned. However, we're
        /// choosing to use <see cref="System.Numerics"/> namespace to do all the heavy lifting for us.
        /// So in our case we'll do as many calculations/transformation etc. using the MSDN standard.
        /// Then at the last minute (e.g. before translation to canvas) we'll convert using this extension
        /// method and calculate / make the final interpretation in the Ray Tracer Text standard.
        ///
        /// Crucially you must see the unit test suite for specific details on what to do here.
        /// </remarks>
        public static Matrix4x4 ToColumnMajorForm(this Matrix4x4 rmf)
        {
            return Matrix4x4.Transpose(rmf);
        }

        /// <summary>
        /// Multiplying a matrix by a tuple (most often a point) is used to perform
        /// operations like rotation of the point around an axis.
        /// </summary>
        /// <remarks>
        /// In many cases the tuple will be a point e.g. rotation, reflection etc.
        /// But some operations work on vectors as well e.g. scaling.
        /// Matrices are used to encode multiple operations, which can then be
        /// applied as an atomic operation on a point.
        /// </remarks>
        public static Vector4 Multiply(this Matrix4x4 left, Vector4 right)
        {
            // skinnyMatrix
            // A lay person explanation: System.Numerics namespace offers the ability to multiply two matrices,
            // or two vectors. But not a matrix by a vector. The Ray Tracer Text describes how this vector is
            // really just a skinny 1x4 matrix. So that's what we create here and then just multiple two matrices.
            
            // You'll notice that we use CMF here. See further notes about this in ToColumnMajorForm. 
            // Might not have to worry about this at all, but worth understanding the difference between CMF
            // and RMF. And how the Ray Tracer Text and Microsoft model things differently.

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
