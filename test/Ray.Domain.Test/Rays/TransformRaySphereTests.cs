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
        private Sphere _sphereInstance = null;
        private Matrix4x4 _firstMatrix;

        private SceneIntersectionCalculator _xs = null;
        private IList<float> _orderedIntersectionDistances =>
            _xs.Intersections.Select(x => x.GetPreciseIntersectionPoint().Distance).OrderBy(x => x).ToList();

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
            _sphereInstance = new Sphere();
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
            // TODO: Temporary - need to think about applying directly to the sphere vs to the ray.
            // Would applying to the ray only work for single sphere and transform - in my case,
            // surely yes, but maybe if I'd modeled as the book, it wouldn't be such a problem!

            // TODO: the below works - so have proved the books algorithm.
            // Next try apply the scaling transform to the sphere. This obviously changes the radius.
            // I think I can multiply the radius float by the matrix as per previous chapter.
            // Verify that get same result that way too.

            // TODO: An ApplyTransform method on an IBasicShape instance that accepted a IMatrixTransformationBuilder?

            if (_sphereInstance.Transformation != Matrix4x4.Identity)
            {

                // 1. Books way - apply Sphere transform to Ray instead.
                //DoInitXs_BooksWay_ApplySphereTransformToRay();

                // 2. Transform the sphere - simple way.
                DoInitXs_MyWay_ApplyTransformToSphere_SimpleVersion();
            }
            else
            {
                _xs = new SceneIntersectionCalculator(_rayInstance, new List<IBasicShape> { _sphereInstance });
            }
        }

        
        // TODO: Temp code - decide best way forward.
        private void DoInitXs_BooksWay_ApplySphereTransformToRay()
        {
            Matrix4x4.Invert(_sphereInstance.Transformation, out var sphereTransformInverted);
            _transformedRay.Origin = sphereTransformInverted.Multiply(_rayInstance.Origin);
            _transformedRay.Direction = sphereTransformInverted.Multiply(_rayInstance.Direction);

            _xs = new SceneIntersectionCalculator(_transformedRay, new List<IBasicShape> { _sphereInstance });
        }

        private void DoInitXs_MyWay_ApplyTransformToSphere_SimpleVersion()
        {
            _sphereInstance.Origin = _sphereInstance.Transformation.Multiply(_sphereInstance.Origin);
            // Bit of a problem here. A matrix can encode many transforms, including scaling.
            // But for sphere we have Radius = float. Might need to change to vector and have a
            // convenience property for radius. For now, just see if it even works!

            _sphereInstance.Scale = _sphereInstance.Transformation.Multiply(_sphereInstance.Scale);

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
            var actualAnswer = _orderedIntersectionDistances.Count;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs element (\d) has t equals (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyDistance(int index, float t)
        {
            var expectedAnswer = t;

            var actualAnswer = _orderedIntersectionDistances[index];

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
