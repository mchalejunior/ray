using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Simulations.Intersections;
using Ray.Domain.Model;
using Ray.Domain.Transportation;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Rays
{
    [FeatureFile("./features/rays/RaySphere.feature")]
    public sealed class RaySphereTests : Feature
    {
        private Vector4 _origin, _direction;
        private Model.Ray _rayInstance;
        private Sphere _sphereInstance = null;
        private float _distance;

        private SceneIntersectionCalculator _xs = null;
        

        [Given(@"origin equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnOriginInstance(float x, float y, float z, float w)
        {
            _origin.X = x;
            _origin.Y = y;
            _origin.Z = z;
            _origin.W = w;
        }

        [And(@"direction equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnDirectionInstance(float x, float y, float z, float w)
        {
            _direction.X = x;
            _direction.Y = y;
            _direction.Z = z;
            _direction.W = w;
        }

        [And(@"distance equals (-?\d+\.\d+)")]
        public void InitializationValues_SetOnDistanceInstance(float t)
        {
            _distance = t;
        }

        [When(@"initialize ray with origin and direction")]
        public void InitializationValues_SetOnRayInstance()
        {
            _rayInstance.Origin = _origin;
            _rayInstance.Direction = _direction;
        }
        
        [And(@"initialize sphere as a unit sphere at the origin")]
        public void InitializationValues_SetOnSphereInstance()
        {
            _sphereInstance = Sphere.CreateDefaultInstance(false);
        }

        [And(@"initialize xs as intersection calulator for ray, sphere")]
        public void InitializationValues_SetOnIntersectionCalculator()
        {
            _xs = new SceneIntersectionCalculator(new List<IBasicShape> {_sphereInstance});
        }

        [Then(@"ray origin equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void GivenExpectedAnswer_VerifyRayOrigin(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _rayInstance.Origin;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [Then(@"ray position equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_MoveAlongRay_VerifyPosition(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _rayInstance.GetPosition(_distance);

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"ray direction equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void GivenExpectedAnswer_VerifyRayDirection(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _rayInstance.Direction;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [Then(@"xs intersection count equals (\d)")]
        public void GivenExpectedAnswer_RunIntersectionSimulation_VerifyCount(int count)
        {
            var expectedAnswer = count;

            var actualAnswer = _xs.CalculateIntersections(_rayInstance).Count();

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs element (\d) has t equals (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyDistance(int index, float t)
        {
            var expectedAnswer = t;

            var intersections = _xs.CalculateIntersections(_rayInstance).ToList();
            var actualAnswer = intersections[index].DistanceT;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs element (\d) has shape equals sphere")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyShape(int index)
        {
            var expectedAnswer = _sphereInstance;

            var intersections = _xs.CalculateIntersections(_rayInstance).ToList();
            var actualAnswer = intersections[index].Shape;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs hit t equals (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryHit_VerifyDistance(float t)
        {
            var expectedAnswer = t;

            var actualAnswer = _xs.CalculateHit(_rayInstance).DistanceT;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs hit t equals null")]
        public void GivenExpectedAnswer_QueryHit_VerifyNull()
        {
            Assert.False(_xs.CalculateHit(_rayInstance).HasValue);
        }

        [And(@"xs calculates ray as originating inside shape")]
        public void GivenExpectedAnswer_QueryRayOrigin_VerifyInside()
        {
            var expectedAnswer = true;

            var intersections = _xs.CalculateIntersections(_rayInstance).ToList();
            var actualAnswer = intersections[0].RayOriginatesInsideShape;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs calculates shape being behind ray")]
        public void GivenExpectedAnswer_QueryRayOrigin_VerifyShapeBehind()
        {
            var expectedAnswer = IntersectionDto.RaysOrigin.ShapeBehindRay;

            var intersections = _xs.CalculateIntersections(_rayInstance).ToList();
            var actualAnswer = intersections[0].RayOrigin;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs calculates shape being in front of ray")]
        public void GivenExpectedAnswer_QueryRayOrigin_VerifyNormal()
        {
            var expectedAnswer = IntersectionDto.RaysOrigin.Normal;

            var intersections = _xs.CalculateIntersections(_rayInstance).ToList();
            var actualAnswer = intersections[0].RayOrigin;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs calculates intersection as tangential")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyTangential()
        {
            var expectedAnswer = true;

            var intersections = _xs.CalculateIntersections(_rayInstance).ToList();
            var actualAnswer = intersections[0].TangentialIntersection;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs calculates intersection as non tangential")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyNonTangential()
        {
            var expectedAnswer = false;

            var intersections = _xs.CalculateIntersections(_rayInstance).ToList();
            var actualAnswer = intersections[0].TangentialIntersection;

            Assert.Equal(expectedAnswer, actualAnswer);
        }
    }
}
