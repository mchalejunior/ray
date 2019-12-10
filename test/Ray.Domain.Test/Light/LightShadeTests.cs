using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Maths.Simulations.Intersections;
using Ray.Domain.Model;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Light
{
    [FeatureFile("./features/light/LightShade.feature")]
    public sealed class LightShadeTests : Feature
    {
        private Sphere _sphereInstance = null;
        private readonly IMatrixTransformationBuilder _transformMatrix = new MatrixTransformationBuilder();
        private Vector4 _t1, _normal;

        [Given(@"initialize sphere as a unit sphere at the origin")]
        public void InitializationValues_SetOnSphereInstance()
        {
            _sphereInstance = Sphere.CreateDefaultInstance(true);
        }

        [And(@"initialize sphere as a unit sphere at the origin")]
        public void InitializationValues_SetOnSphereInstance_AndOverload()
        {
            InitializationValues_SetOnSphereInstance();
        }

        [And(@"t1 equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_SetOnTupleInstance(float x, float y, float z, float w)
        {
            _t1.X = x;
            _t1.Y = y;
            _t1.Z = z;
            _t1.W = w;
        }

        [And(@"transformMatrix equals Identity Matrix")]
        public void InitializationValues_Identity_SetOnTransformMatrixInstance()
        {
            // This is the default value when no transforms applied to the builder
        }

        [And(@"transformMatrix includes Translation Matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_Translation_SetOnTransformMatrixInstance(float x, float y, float z)
        {
            _transformMatrix.Translate(new Vector3(x, y, z));
        }

        [And(@"transformMatrix includes Scaling Matrix (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_Scaling_SetOnTransformMatrixInstance(float x, float y, float z)
        {
            _transformMatrix.Scale(new Vector3(x, y, z));
        }

        [And(@"transformMatrix includes Z Rotation Matrix pi over (-?\d+)")]
        public void InitializationValues_ZRotation_SetOnTransformMatrixInstance(int piOverDenominator)
        {
            _transformMatrix.RotateZ(MathF.PI / piOverDenominator);
        }

        [When(@"set sphere transformation equals transformMatrix")]
        public void InitializationValues_Transform_SetOnSphereInstance()
        {
            _sphereInstance.Transformation = _transformMatrix;
        }

        [When(@"calculate normal for sphere at t1")]
        public void Calculate_NormalAt_SetOnResultVector()
        {
            _normal = _sphereInstance.GetNormal(_t1);
        }

        [And(@"calculate normal for sphere at t1")]
        public void Calculate_NormalAt_SetOnResultVector_AndOverload()
        {
            Calculate_NormalAt_SetOnResultVector();
        }

        [Then(@"normal equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryTransformedRayOrigin_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);

            var actualResult = _normal;

            Assert.True(expectedResult.IsApproximately(actualResult, 5));
        }

        [Then(@"normal equals normalized normal")]
        public void GivenExpectedAnswer_NormalizeNormal_VerifyResult()
        {
            var expectedResult = Vector4.Normalize(_normal);

            var actualResult = _normal;

            Assert.True(expectedResult.IsApproximately(actualResult));
        }

    }
}
