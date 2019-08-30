using System.Numerics;
using Gherkin.Ast;
using Ray.Domain.Test.Extensions;
using Xunit;
using Xunit.Gherkin.Quick;
using Feature = Xunit.Gherkin.Quick.Feature;

namespace Ray.Domain.Test.Matrices
{
    /* NOTE: System.Numerics Matrix4x4 struct has 1-based indexing.
     *    However, the Ray Tracer text is zero-based. This is quite confusing!!
     *    But it'd be more confusing if I tried to convert between the two.
     *    So I'm forced to choose the Matrix4x4 1-based indexing, as this is the
     *    struct that will be used in calculations.
     *    Therefore in my code ALL matrix indexing is 1-based!
     *
     * TEST: I achieve this in testing with the Gherkin.Ast DataTable class.
     *    The feature files will map to this automatically (multi-line, pipe-delimited).
     *    Then the trick is to have both column and row headers.
     *    Now our input data has 1-based indexing, the same as System.Numerics Matrix4x4.
     */

    [FeatureFile("./features/matrices/CompareMatrices.feature")]
    public sealed class CompareMatricesTests : Feature
    {
        private Matrix4x4 _firstMatrix, _secondMatrix;

        [Given(@"firstMatrix equals the following 4x4 matrix:")]
        public void InitializationValues_SetOnFirstMatrixInstance(DataTable m)
        {
            _firstMatrix = new Matrix4x4(
                m.ToFloat(1, 1), m.ToFloat(1, 2), m.ToFloat(1, 3), m.ToFloat(1, 4),
                m.ToFloat(2, 1), m.ToFloat(2, 2), m.ToFloat(2, 3), m.ToFloat(2, 4),
                m.ToFloat(3, 1), m.ToFloat(3, 2), m.ToFloat(3, 3), m.ToFloat(3, 4),
                m.ToFloat(4, 1), m.ToFloat(4, 2), m.ToFloat(4, 3), m.ToFloat(4, 4)
            );
        }

        [And(@"secondMatrix equals the following 4x4 matrix:")]
        public void InitializationValues_SetOnSecondMatrixInstance(DataTable m)
        {
            _secondMatrix = new Matrix4x4(
                m.ToFloat(1, 1), m.ToFloat(1, 2), m.ToFloat(1, 3), m.ToFloat(1, 4),
                m.ToFloat(2, 1), m.ToFloat(2, 2), m.ToFloat(2, 3), m.ToFloat(2, 4),
                m.ToFloat(3, 1), m.ToFloat(3, 2), m.ToFloat(3, 3), m.ToFloat(3, 4),
                m.ToFloat(4, 1), m.ToFloat(4, 2), m.ToFloat(4, 3), m.ToFloat(4, 4)
            );
        }

        [Then(@"firstMatrix.M11 equals (-?\d+\.\d+)")]
        public void ReadMatrix_M11_VerifyValue(float r)
        {
            Assert.Equal(r, _firstMatrix.M11);
        }

        [And(@"firstMatrix.M14 equals (-?\d+\.\d+)")]
        public void ReadMatrix_M14_VerifyValue(float r)
        {
            Assert.Equal(r, _firstMatrix.M14);
        }

        [And(@"firstMatrix.M21 equals (-?\d+\.\d+)")]
        public void ReadMatrix_M21_VerifyValue(float r)
        {
            Assert.Equal(r, _firstMatrix.M21);
        }

        [And(@"firstMatrix.M23 equals (-?\d+\.\d+)")]
        public void ReadMatrix_M23_VerifyValue(float r)
        {
            Assert.Equal(r, _firstMatrix.M23);
        }

        [And(@"firstMatrix.M33 equals (-?\d+\.\d+)")]
        public void ReadMatrix_M33_VerifyValue(float r)
        {
            Assert.Equal(r, _firstMatrix.M33);
        }

        [And(@"firstMatrix.M41 equals (-?\d+\.\d+)")]
        public void ReadMatrix_M41_VerifyValue(float r)
        {
            Assert.Equal(r, _firstMatrix.M41);
        }

        [And(@"firstMatrix.M43 equals (-?\d+\.\d+)")]
        public void ReadMatrix_M43_VerifyValue(float r)
        {
            Assert.Equal(r, _firstMatrix.M43);
        }

        [Then(@"firstMatrix equals secondMatrix")]
        public void CompareMatrices_AssertEquality()
        {
            Assert.Equal(_firstMatrix, _secondMatrix);
        }

        [Then(@"firstMatrix does NOT equal secondMatrix")]
        public void CompareMatrices_AssertInequality()
        {
            Assert.NotEqual(_firstMatrix, _secondMatrix);
        }
    }
}
