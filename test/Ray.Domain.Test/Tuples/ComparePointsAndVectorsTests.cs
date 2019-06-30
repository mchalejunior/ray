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

        [Given(@"a <- tuple (\d\.\d) (-\d\.\d) (\d\.\d) (\d\.\d)")]
        public void InitializationValues_SetOnTupleInstance(float x, float y, float z, float w)
        {
            _tuple.X = x;
            _tuple.Y = y;
            _tuple.Z = z;
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

        [And(@"a.w = (\d\.\d)")]
        public void ReadW_VerifyValue(float w)
        {
            Assert.Equal(w, _tuple.W);
        }

        [And(@"a is a point")]
        public void ReadTuple_VerifyIsPoint()
        {
            Assert.True(_tuple.IsPoint());
        }

        [And(@"a is not a vector")]
        public void ReadTuple_VerifyIsNotVector()
        {
            Assert.False(_tuple.IsVector());
        }
    }
}
