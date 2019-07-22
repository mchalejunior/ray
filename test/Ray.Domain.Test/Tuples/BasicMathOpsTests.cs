using System.Numerics;
using Ray.Domain.Extensions;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Tuples
{
    [FeatureFile("./features/tuples/BasicMathOps.feature")]
    public sealed class BasicMathOpsTests : Feature
    {
        private Vector4 _firstTuple = new Vector4(),
            _secondTuple = new Vector4();

        [Given(@"a1 = tuple (-?\d) (-?\d) (-?\d) (-?\d)")]
        public void InitializationValues_SetOnFirstTuple(float x, float y, float z, float w)
        {
            _firstTuple.X = x;
            _firstTuple.Y = y;
            _firstTuple.Z = z;
            _firstTuple.W = w;
        }

        [And(@"a2 = tuple (-?\d) (-?\d) (-?\d) (-?\d)")]
        public void InitializationValues_SetOnSecondTuple(float x, float y, float z, float w)
        {
            _secondTuple.X = x;
            _secondTuple.Y = y;
            _secondTuple.Z = z;
            _secondTuple.W = w;
        }

        [Then(@"a1 plus a2 = tuple (-?\d) (-?\d) (-?\d) (-?\d)")]
        public void GivenExpectedAnswer_PerformAddition_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);
            var actualResult = _firstTuple + _secondTuple; //Vector4.Add(_firstTuple, _secondTuple);
            Assert.True(expectedResult.Equals(actualResult));
        }

        [Then(@"a1 minus a2 = tuple (-?\d) (-?\d) (-?\d) (-?\d)")]
        public void GivenExpectedAnswer_PerformSubtraction_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);
            var actualResult = _firstTuple - _secondTuple; //Vector4.Subtract(_firstTuple, _secondTuple);
            Assert.True(expectedResult.Equals(actualResult));
        }

        [Then(@"-a1 = tuple (-?\d) (-?\d) (-?\d) (-?\d)")]
        public void GivenExpectedAnswer_PerformNegation_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);
            var actualResult = -_firstTuple; //Vector4.Negate(_firstTuple);
            Assert.True(expectedResult.Equals(actualResult));
        }

        [Then(@"a1 multiplied by (\d\.\d) = tuple (-?\d+\.\d) (-?\d+\.\d) (-?\d+\.\d) (-?\d+\.\d)")]
        public void GivenExpectedAnswer_PerformScaling_ByMultiplication_VerifyResult(float scale, float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);
            var actualResult = _firstTuple * scale; //Vector4.Multiply(_firstTuple, scale);
            Assert.True(expectedResult.Equals(actualResult));
        }

        [Then(@"a1 divided by (\d\.\d) = tuple (-?\d+\.\d) (-?\d+\.\d) (-?\d+\.\d) (-?\d+\.\d)")]
        public void GivenExpectedAnswer_PerformScaling_ByDivision_VerifyResult(float scale, float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);
            var actualResult = _firstTuple / scale; //Vector4.Divide(_firstTuple, scale);
            Assert.True(expectedResult.Equals(actualResult));
        }

        [Then(@"magnitude of a1 equals (\d\.\d+)")]
        public void GivenExpectedAnswer_ComputeMagnitude_VerifyResult(float magnitude)
        {
            var actualResult = _firstTuple.Length();
            Assert.True(magnitude.Equals(actualResult));
        }


        #region Overloads as helpers / give more natural language to the BDD spec

        // Tuple as Vector

        [Given(@"a1 = vector (-?\d) (-?\d) (-?\d)")]
        public void InitializationValues_SetOnFirstVector(float x, float y, float z)
        {
            InitializationValues_SetOnFirstTuple(x, y, z, 0.0F);
        }

        [And(@"a2 = vector (-?\d) (-?\d) (-?\d)")]
        public void InitializationValues_SetOnSecondVector(float x, float y, float z)
        {
            InitializationValues_SetOnSecondTuple(x, y, z, 0.0F);
        }

        [Then(@"a1 minus a2 = vector (-?\d) (-?\d) (-?\d)")]
        public void GivenExpectedAnswer_PerformSubtraction_VerifyVectorResult(float x, float y, float z)
        {
            GivenExpectedAnswer_PerformSubtraction_VerifyResult(x, y, z, 0.0F);
        }

        #endregion

    }
}
