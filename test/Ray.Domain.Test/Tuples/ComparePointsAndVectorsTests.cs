using System.Numerics;
using Ray.Domain.Extensions;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Tuples
{
    [FeatureFile("./features/tuples/ComparePointsAndVectors.feature")]
    public sealed class ComparePointsAndVectorsTests : Feature
    {
        private Vector4 _tuple = new Vector4();

        [Given(@"a <- tuple (\d\.\d) (-\d\.\d) (\d\.\d)")]
        public void InitializationValues_SetOnTupleInstance(float x, float y, float z)
        {
            _tuple.X = x;
            _tuple.Y = y;
            _tuple.Z = z;
        }

        [And(@"a.w = (\d\.\d)")]
        public void InitializationValues_SetOnTupleInstance(float w)
        {
            _tuple.W = w;
        }



        [Then(@"a.x = (\d\.\d)")]
        public void ReadX_VerifyValue(float x)
        {
            Assert.Equal(x, _tuple.X);
        }

        [And(@"a.y = (-\d\.\d)")]
        public void ReadY_VerifyValue(float y)
        {
            Assert.Equal(y, _tuple.Y);
        }

        [And(@"a.z = (\d\.\d)")]
        public void ReadZ_VerifyValue(float z)
        {
            Assert.Equal(z, _tuple.Z);
        }

        [And(@"a.IsPoint = (.+)")]
        public void ReadTuple_CheckForPoint(string boolToParse)
        {
            bool expected = bool.Parse(boolToParse);
            Assert.Equal(expected, _tuple.IsPoint());
        }

        [And(@"a.IsVector = (.+)")]
        public void ReadTuple_CheckForVector(string boolToParse)
        {
            bool expected = bool.Parse(boolToParse);
            Assert.Equal(expected, _tuple.IsVector());
        }
    }
}
