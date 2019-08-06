using System.Windows.Media;
using Ray.Domain.Extensions;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Colors
{
    [FeatureFile("./features/colors/BasicColorOps.feature")]
    public sealed class BasicColorOpsTests : Feature
    {
        private Color _firstColor, _secondColor;

        [Given(@"c1 equals color (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_SetOnTupleInstance(float r, float g, float b)
        {
            _firstColor = new Color
            {
                ScR = r,
                ScG = g,
                ScB = b
            };
        }

        [And(@"c2 equals color (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_SetOtherTupleInstance(float r, float g, float b)
        {
            _secondColor = new Color
            {
                ScR = r,
                ScG = g,
                ScB = b
            };
        }

        [Then(@"c1.red equals (-?\d+\.\d+)")]
        public void ReadRed_VerifyValue(float r)
        {
            Assert.Equal(r, _firstColor.ScR);
        }

        [And(@"c1.green equals (-?\d+\.\d+)")]
        public void ReadGreen_VerifyValue(float g)
        {
            Assert.Equal(g, _firstColor.ScG);
        }

        [And(@"c1.blue equals (-?\d+\.\d+)")]
        public void ReadBlue_VerifyValue(float b)
        {
            Assert.Equal(b, _firstColor.ScB);
        }

        [Then(@"c1 plus c2 equals color (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_PerformAddition_VerifyResult(float r, float g, float b)
        {
            var expectedResult = new Color
            {
                ScR = r,
                ScG = g,
                ScB = b
            };
            var actualResult = _firstColor + _secondColor;
            Assert.True(Color.AreClose(expectedResult, actualResult));
        }

        [Then(@"c1 minus c2 equals color (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_PerformSubtraction_VerifyResult(float r, float g, float b)
        {
            var expectedResult = new Color
            {
                ScR = r,
                ScG = g,
                ScB = b
            };
            var actualResult = _firstColor - _secondColor;
            Assert.True(Color.AreClose(expectedResult, actualResult));
        }

        [Then(@"c1 multiplied by scalar (\d) equals color (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_MultiplyByScalar_VerifyResult(int scalar, float r, float g, float b)
        {
            var expectedResult = new Color
            {
                ScR = r,
                ScG = g,
                ScB = b
            };
            var actualResult = _firstColor * scalar;
            Assert.True(Color.AreClose(expectedResult, actualResult));
        }

        [Then(@"c1 multiplied by c2 equals color (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_PerformMultiplication_VerifyResult(float r, float g, float b)
        {
            var expectedResult = new Color
            {
                ScR = r,
                ScG = g,
                ScB = b
            };
            var actualResult = _firstColor.Multiply(_secondColor);
            Assert.True(Color.AreClose(expectedResult, actualResult));
        }

    }
}
