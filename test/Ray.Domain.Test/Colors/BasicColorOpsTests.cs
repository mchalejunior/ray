using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Windows.Media;
using System.Text;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Colors
{
    [FeatureFile("./features/colors/BasicColorOps.feature")]
    public sealed class BasicColorOpsTests : Feature
    {
        private Color _firstColor, _secondColor;

        [Given(@"c1 equals color (\d+) (\d+) (\d+)")]
        public void InitializationValues_SetOnTupleInstance(int r, int g, int b)
        {
            _firstColor = Color.FromArgb(r, g, b);
        }

        [And(@"c2 equals color (\d+) (\d+) (\d+)")]
        public void InitializationValues_SetOtherTupleInstance(int r, int g, int b)
        {
            _secondColor = Color.FromArgb(r, g, b);
        }

        [Then(@"c1.red equals (\d+)")]
        public void ReadRed_VerifyValue(int r)
        {
            Assert.Equal(r, _firstColor.R);
        }

        [And(@"c1.green equals (\d+)")]
        public void ReadGreen_VerifyValue(int g)
        {
            Assert.Equal(g, _firstColor.G);
        }

        [And(@"c1.blue equals (\d+)")]
        public void ReadBlue_VerifyValue(int b)
        {
            Assert.Equal(b, _firstColor.B);
        }

        [Then(@"c1 plus c2 equals color (\d+) (\d+) (\d+)")]
        public void GivenExpectedAnswer_PerformSubtraction_VerifyResult(int r, int g, int b)
        {
            var expectedResult = Color.FromArgb(r, g, b);
            //var actualResult = _firstColor + _secondColor;
        }
    }
}
