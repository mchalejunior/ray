using System.Numerics;
using Gherkin.Ast;
using Ray.Domain.Extensions;
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

    [FeatureFile("./features/matrices/BasicMathOps.feature")]
    public sealed class BasicMathOpsTests : Feature
    {
        private Matrix4x4 _firstMatrix, _secondMatrix, _thirdMatrix;
        private Vector4 _tupleInstance = new Vector4();

        [Given(@"firstMatrix equals the following 4x4 matrix:")]
        public void InitializationValues_SetOnFirstMatrixInstance(DataTable m)
        {
            _firstMatrix = NewMatrix(m);
        }

        [And(@"secondMatrix equals the following 4x4 matrix:")]
        public void InitializationValues_SetOnSecondMatrixInstance(DataTable m)
        {
            _secondMatrix = NewMatrix(m);
        }

        [And(@"thirdMatrix equals firstMatrix multiplied by secondMatrix")]
        public void InitializationValues_SetThirdMatrixInstance()
        {
            _thirdMatrix = _firstMatrix * _secondMatrix;
        }

        [And(@"t equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnTransformationTuple(float x, float y, float z, float w)
        {
            _tupleInstance.X = x;
            _tupleInstance.Y = y;
            _tupleInstance.Z = z;
            _tupleInstance.W = w;
        }

        [Then(@"firstMatrix multiplied by secondMatrix equals the following 4x4 matrix:")]
        public void GivenExpectedAnswer_PerformMultiplication_VerifyResult(DataTable m)
        {
            var expectedResult = NewMatrix(m);
            var actualResult = _firstMatrix * _secondMatrix;
            Assert.Equal(expectedResult, actualResult);
        }

        [Then(@"firstMatrix multiplied by t equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void GivenExpectedAnswer_MultiplyMatrixByTuple_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);
            var actualResult = _firstMatrix.Multiply(_tupleInstance);

            Assert.Equal(expectedResult, actualResult);
        }

        [Then(@"firstMatrix multiplied by Identity Matrix equals firstMatrix")]
        public void GivenInputMatrix_MultiplyByIdentityMatrix_VerifyResult()
        {
            var expectedResult = _firstMatrix;
            var actualResult = _firstMatrix * Matrix4x4.Identity;
            Assert.Equal(expectedResult, actualResult);
        }

        [Then(@"the Transpose of firstMatrix is the following 4x4 matrix:")]
        public void GivenExpectedAnswer_PerformTranspose_VerifyResult(DataTable m)
        {
            var expectedResult = NewMatrix(m);
            var actualResult = Matrix4x4.Transpose(_firstMatrix);
            Assert.Equal(expectedResult, actualResult);
        }

        [Then(@"the Transpose of firstMatrix is still the Identity Matrix")]
        public void GivenIdentityMatrix_PerformTranspose_VerifyResult()
        {
            var expectedResult = Matrix4x4.Identity;
            var actualResult = Matrix4x4.Transpose(_firstMatrix);
            Assert.Equal(expectedResult, actualResult);
        }

        [Then(@"the Determinant of firstMatrix is (-?\d+)")]
        public void GivenExpectedAnswer_CalculateDeterminant_VerifyResult(float determinant)
        {
            var expectedResult = determinant;
            var actualResult = _firstMatrix.GetDeterminant();

            Assert.Equal(expectedResult, actualResult);
        }

        [And(@"firstMatrix IsInvertible equals (.+)")]
        public void GivenExpectedAnswer_CheckIsInvertible_VerifyResult(string boolToParse)
        {
            var expectedResult = bool.Parse(boolToParse); ;
            var actualResult = Matrix4x4.Invert(_firstMatrix, out var invertedMatrix);

            Assert.Equal(expectedResult, actualResult);
        }

        [Then(@"the Inversion of firstMatrix is the following 4x4 matrix:")]
        public void GivenExpectedAnswer_PerformInversion_VerifyResult(DataTable m)
        {
            var expectedResult = NewMatrix(m);
            Matrix4x4.Invert(_firstMatrix, out var actualResult);
            Assert.Equal(expectedResult, actualResult);
        }


        [Then(@"thirdMatrix multiplied by Inverse of secondMatrix equals firstMatrix")]
        public void GivenInputMatrices_UsingInverseMatrix_ReverseOperation_VerifyResult()
        {
            var expectedResult = _firstMatrix;
            Matrix4x4.Invert(_secondMatrix, out var inverseSecond);
            var actualResult = _thirdMatrix * inverseSecond;
            
            Assert.Equal(expectedResult, actualResult);
        }



        #region Helper methods

        private Matrix4x4 NewMatrix(DataTable m)
        {
            return new Matrix4x4(
                m.ToFloat(1, 1), m.ToFloat(1, 2), m.ToFloat(1, 3), m.ToFloat(1, 4),
                m.ToFloat(2, 1), m.ToFloat(2, 2), m.ToFloat(2, 3), m.ToFloat(2, 4),
                m.ToFloat(3, 1), m.ToFloat(3, 2), m.ToFloat(3, 3), m.ToFloat(3, 4),
                m.ToFloat(4, 1), m.ToFloat(4, 2), m.ToFloat(4, 3), m.ToFloat(4, 4)
            );
        } 

        #endregion
    }
}
