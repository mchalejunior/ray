using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Transportation;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Planes
{
    [FeatureFile("./features/plane/Planes.feature")]
    public sealed class PlanesFeatureTests : Feature
    {
        private Vector4 _t1, _t2;
        private Model.Ray _rayInstance;
        private readonly Model.Plane _planeInstance = new Model.Plane();
        private List<IntersectionDto> _xs;
        private IntersectionDto _hit;

        [Given(@"t1 equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_SetOnTupleInstance(float x, float y, float z, float w)
        {
            _t1.X = x;
            _t1.Y = y;
            _t1.Z = z;
            _t1.W = w;
        }

        [And(@"t1 equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_SetOnTupleInstance_Overload(float x, float y, float z, float w)
        {
            InitializationValues_SetOnTupleInstance(x, y, z, w);
        }

        [And(@"t2 equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_SetOnSecondTupleInstance(float x, float y, float z, float w)
        {
            _t2.X = x;
            _t2.Y = y;
            _t2.Z = z;
            _t2.W = w;
        }

        [When(@"initialize ray with origin t1 and direction t2")]
        public void InitializationValues_SetOnRayInstance()
        {
            _rayInstance.Origin = _t1;
            _rayInstance.Direction = _t2;
        }

        [And(@"xs equals plane intersections given ray")]
        public void CalculateIntersections_SetResultList()
        {
            _xs = _planeInstance.GetIntersections(_rayInstance).OrderBy(x => x.DistanceT).ToList();
        }

        [And(@"hit equals plane intersection hit given ray")]
        public void CalculateHit_SetOnHitInstance()
        {
            _hit = _planeInstance.GetIntersections(_rayInstance).GetHit();
        }

        [Then(@"plane normal at t1 equals t2")]
        public void GivenExpectedAnswer_CalculateNormal_VerifyResult()
        {
            var expectedResult = _t2;

            var actualResult = _planeInstance.GetNormal(_t1);

            Assert.Equal(expectedResult, actualResult);
        }

        [Then(@"xs intersection count equals (\d)")]
        public void GivenExpectedAnswer_RunIntersectionSimulation_VerifyCount(int count)
        {
            var expectedAnswer = count;

            var actualAnswer = _xs.Count;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs intersection hit equals plane instance")]
        public void CalculateHit_CompareToPlaneInstance_VerifyResult()
        {
            var expectedResult = _planeInstance;

            var actualResult = _planeInstance.GetIntersections(_rayInstance).GetHit().Shape;

            Assert.Equal(expectedResult, actualResult);
        }

    }
}
