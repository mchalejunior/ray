using System.Numerics;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Tuples
{
    [FeatureFile("./features/tuples/BasicOperations.feature")]
    public sealed class BasicOperationsTests : Feature
    {
        private Vector4 _firstTuple = new Vector4(),
            _secondTuple = new Vector4();

        [Given(@"a1 = tuple (\d) (-\d) (\d) (\d)")]
        public void One(float x, float y, float z, float w)
        {
            _firstTuple.X = x;
            _firstTuple.Y = y;
            _firstTuple.Z = z;
            _firstTuple.W = w;
        }

        [And(@"a2 = tuple (-\d) (\d) (\d) (\d)")]
        public void Two(float x, float y, float z, float w)
        {
            _secondTuple.X = x;
            _secondTuple.Y = y;
            _secondTuple.Z = z;
            _secondTuple.W = w;
        }

        [Then(@"a1 plus a2 = tuple (\d) (\d) (\d) (\d)")]
        public void Three(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);
            var actualResult = Vector4.Add(_firstTuple, _secondTuple);
            Assert.True(expectedResult.Equals(actualResult));
        }

    }
}
