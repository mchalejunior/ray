using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Simulations.Intersections;
using Ray.Domain.Model;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Rays
{
    [FeatureFile("./features/rays/TransformRaySphere.feature")]
    public sealed class TransformRaySphereTests : Feature
    {
        private Vector4 _origin, _direction;
        private readonly Model.Ray _rayInstance = new Model.Ray(),
            _transformedRay = new Model.Ray();
        private IBasicShape _sphereInstance = null;
        private Matrix4x4 _firstMatrix;

        private SceneIntersectionCalculator _xs = null;
        //private IList<float> _orderedIntersectionDistances =>
        //    _xs.Intersections.Select(x => x.DistanceT).OrderBy(x => x).ToList();

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

        [Given(@"initialize sphere as a unit sphere at the origin")]
        public void InitializationValues_SetOnSphereInstance()
        {
            _sphereInstance = Sphere.CreateDefaultInstance(true);
        }

        [When(@"initialize ray with origin and direction")]
        public void InitializationValues_SetOnRayInstance()
        {
            _rayInstance.Origin = _origin;
            _rayInstance.Direction = _direction;
        }

        [When(@"set sphere transformation equals firstMatrix")]
        public void InitializationValues_Transform_SetOnSphereInstance()
        {
            _sphereInstance.Transformation = _firstMatrix;
        }

        [And(@"initialize ray with origin and direction")]
        public void InitializationValues_SetOnRayInstance_Overload()
        {
            InitializationValues_SetOnRayInstance();
        }

        [And(@"firstMatrix equals Identity Matrix")]
        public void InitializationValues_Identity_SetOnFirstMatrixInstance()
        {
            _firstMatrix = Matrix4x4.Identity;
        }

        [And(@"firstMatrix equals Translation Matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_Translation_SetOnFirstMatrixInstance(float x, float y, float z)
        {
            _firstMatrix = Matrix4x4.CreateTranslation(x, y, z);
        }

        [And(@"firstMatrix equals Scaling Matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_Scaling_SetOnFirstMatrixInstance(float x, float y, float z)
        {
            _firstMatrix = Matrix4x4.CreateScale(x, y, z);
        }

        [And(@"initialize sphere as a unit sphere at the origin")]
        public void InitializationValues_SetOnSphereInstance_Overload()
        {
            InitializationValues_SetOnSphereInstance();
        }

        [And(@"initialize xs as intersection calulator for ray, sphere")]
        public void InitializationValues_SetOnIntersectionCalculator()
        {

            _xs = new SceneIntersectionCalculator(_rayInstance, new List<IBasicShape> { _sphereInstance });



        }

        



        [And(@"transform ray with firstMatrix")]
        public void TransformRay_SetOnInitializedInstance()
        {
            _transformedRay.Origin = _firstMatrix.Multiply(_rayInstance.Origin);
            _transformedRay.Direction = _firstMatrix.Multiply(_rayInstance.Direction);
        }

        [Then(@"xs intersection count equals (\d)")]
        public void GivenExpectedAnswer_RunIntersectionSimulation_VerifyCount(int count)
        {
            var expectedAnswer = count;

            _xs.RunSimulation();
            var actualAnswer = _xs.Intersections.Count;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs element (\d) has t equals (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyDistance(int index, float t)
        {
            var expectedAnswer = t;

            var actualAnswer = _xs.Intersections[index].DistanceT;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [Then(@"transformRay origin equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryTransformedRayOrigin_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);

            var actualResult = _transformedRay.Origin;

            Assert.True(expectedResult.IsApproximately(actualResult));
        }

        [And(@"transformRay direction equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryTransformedRayDirection_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);

            var actualResult = _transformedRay.Direction;

            Assert.True(expectedResult.IsApproximately(actualResult));
        }

        

        [Then(@"sphere transform equals firstMatrix")]
        public void GivenExpectedAnswer_QuerySphereTransform_VerifyResult()
        {
            var expectedAnswer = _firstMatrix;

            var actualAnswer = _sphereInstance.Transformation;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

    }
}
