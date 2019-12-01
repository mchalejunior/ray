using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;
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
        private Model.Ray _rayInstance, _transformedRay;
        private IBasicShape _sphereInstance = null;
        private readonly IMatrixTransformationBuilder _transformMatrix = new MatrixTransformationBuilder();
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

        [When(@"set sphere transformation equals transformMatrix")]
        public void InitializationValues_Transform_SetOnSphereInstance()
        {
            _sphereInstance.Transformation = _transformMatrix;
        }

        [And(@"initialize ray with origin and direction")]
        public void InitializationValues_SetOnRayInstance_Overload()
        {
            InitializationValues_SetOnRayInstance();
        }

        [And(@"transformMatrix equals Identity Matrix")]
        public void InitializationValues_Identity_SetOnTransformMatrixInstance()
        {
            // This is the default value when no transforms applied to the builder
        }

        [And(@"transformMatrix equals Translation Matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_Translation_SetOnTransformMatrixInstance(float x, float y, float z)
        {
            _transformMatrix.Translate(new Vector3(x, y, z));
        }

        [And(@"transformMatrix equals Scaling Matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_Scaling_SetOnTransformMatrixInstance(float x, float y, float z)
        {
            _transformMatrix.Scale(new Vector3(x, y, z));
        }

        [And(@"initialize sphere as a unit sphere at the origin")]
        public void InitializationValues_SetOnSphereInstance_Overload()
        {
            InitializationValues_SetOnSphereInstance();
        }

        [And(@"initialize xs as intersection calulator for ray, sphere")]
        public void InitializationValues_SetOnIntersectionCalculator()
        {

            _xs = new SceneIntersectionCalculator(new List<IBasicShape> { _sphereInstance });
        }

        [And(@"transform ray with transformMatrix")]
        public void TransformRay_SetOnInitializedInstance()
        {
            _transformedRay.Origin = _transformMatrix.Execute(_rayInstance.Origin);
            _transformedRay.Direction = _transformMatrix.Execute(_rayInstance.Direction);
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

        [Then(@"sphere transform equals transformMatrix")]
        public void GivenExpectedAnswer_QuerySphereTransform_VerifyResult()
        {
            var expectedAnswer = _transformMatrix.GetCompositeTransformation();

            var actualAnswer = _sphereInstance.Transformation.GetCompositeTransformation();

            Assert.Equal(expectedAnswer, actualAnswer);
        }

    }
}
