using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Tuples
{
    [FeatureFile("./features/tuples/ConvertTupleToPointOrVector.feature")]
    public sealed class ConvertTupleToPointOrVectorTests : Feature
    {
        private Vector3 _tuple = new Vector3();
        private Vector4 _pointOrVector;

        [Given(@"a <- tuple (\d) (-\d) (\d)")]
        public void InitializationValues_SetOnTupleInstance(float x, float y, float z)
        {
            _tuple.X = x;
            _tuple.Y = y;
            _tuple.Z = z;
        }

        [Then(@"a.AsPoint converts to Point")]
        public void ConvertTuple_ToPoint_SetMemberField()
        {
            _pointOrVector = _tuple.AsPoint();
        }

        [Then(@"a.AsVector converts to Vector")]
        public void ConvertTuple_ToVector_SetMemberField()
        {
            _pointOrVector = _tuple.AsVector();
        }

        [And(@"a.x = (\d)")]
        public void ReadX_VerifyValue(float x)
        {
            Assert.Equal(x, _pointOrVector.X);
        }

        [And(@"a.y = (-\d)")]
        public void ReadY_VerifyValue(float y)
        {
            Assert.Equal(y, _pointOrVector.Y);
        }

        [And(@"a.z = (\d)")]
        public void ReadZ_VerifyValue(float z)
        {
            Assert.Equal(z, _pointOrVector.Z);
        }

        [And(@"a.w = (\d)")]
        public void ReadW_VerifyValue(float w)
        {
            Assert.Equal(w, _pointOrVector.W);
        }

        [And(@"a.IsPoint = (.+)")]
        public void ReadTuple_CheckForPoint(string boolToParse)
        {
            bool expected = bool.Parse(boolToParse);
            Assert.Equal(expected, _pointOrVector.IsPoint());
        }

        [And(@"a.IsVector = (.+)")]
        public void ReadTuple_CheckForVector(string boolToParse)
        {
            bool expected = bool.Parse(boolToParse);
            Assert.Equal(expected, _pointOrVector.IsVector());
        }
    }
}
