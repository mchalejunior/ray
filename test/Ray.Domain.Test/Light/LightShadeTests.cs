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
        private IBasicShape _sphereInstance = null;
        private readonly IMatrixTransformationBuilder _transformMatrix = new MatrixTransformationBuilder();
        private Vector4 _t1, _t2, _resultantT;
        private Model.Light _lightInstance;
        private Material _materialInstance;

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

        //[Given(@"light intensity equals tuple (-?\d+) (-?\d+) (-?\d+)")]
        //public void InitializationValues_Intensity_SetOnLightInstance(float r, float g, float b)
        //{
        //    _lightInstance.Intensity = Color.FromScRgb(1.0F, r, g, b);
        //}

        //[And(@"light intensity equals tuple (-?\d+) (-?\d+) (-?\d+)")]
        //public void InitializationValues_Intensity_SetOnLightInstance_Overload(float r, float g, float b)
        //{
        //    InitializationValues_Intensity_SetOnLightInstance(r, g, b);
        //}

        //[Given(@"light position equals tuple (-?\d+) (-?\d+) (-?\d+)")]
        //public void InitializationValues_Position_SetOnLightInstance(float x, float y, float z)
        //{
        //    _lightInstance.Position = new Vector3(x, y, z).AsPoint();
        //}

        //[And(@"light position equals tuple (-?\d+) (-?\d+) (-?\d+)")]
        //public void InitializationValues_Position_SetOnLightInstance_Overload(float x, float y, float z)
        //{
        //    InitializationValues_Intensity_SetOnLightInstance(x, y, z);
        //}

        // Use star notation in feature file: * Material with default values
        [Given(@"Material with default values")]
        public void InitializationValues_Defaults_SetOnMaterialInstance()
        {
            _materialInstance = Material.CreateDefaultInstance();
        }

        [Given(@"Material with non default values")]
        public void InitializationValues_NonDefault_SetOnMaterialInstance()
        {
            _materialInstance = new Material();
            _materialInstance.Ambient = 27F;
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

        [When(@"set sphere material equals material")]
        public void InitializationValues_Material_SetOnSphereInstance()
        {
            _sphereInstance.Material = _materialInstance;
        }

        [When(@"calculate normal for sphere at t1")]
        public void Calculate_NormalAt_SetOnResultVector()
        {
            _resultantT = _sphereInstance.GetNormal(_t1);
        }

        [And(@"calculate normal for sphere at t1")]
        public void Calculate_NormalAt_SetOnResultVector_AndOverload()
        {
            Calculate_NormalAt_SetOnResultVector();
        }

        [When(@"calculate reflection of t1 given normal t2")]
        public void Calculate_Reflection_SetOnResultVector()
        {
            _resultantT = _t1.Reflect(_t2);
        }

        [When(@"light intensity and position initialized from t1 and t2")]
        public void InitializationValues_SetOnLightInstance()
        {
            _lightInstance = new Model.Light
            {
                Intensity = Color.FromScRgb(1.0F, _t1.X, _t1.Y, _t1.Z),
                Position = _t2
            };
        }

        [Then(@"resultantT equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_CompareToCalculatedInstance_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);

            var actualResult = _resultantT;

            Assert.True(expectedResult.IsApproximately(actualResult, 5));
        }

        [Then(@"normal equals normalized normal")]
        public void GivenExpectedAnswer_NormalizeNormal_VerifyResult()
        {
            var expectedResult = Vector4.Normalize(_resultantT);

            var actualResult = _resultantT;

            Assert.True(expectedResult.IsApproximately(actualResult));
        }

        [Then(@"light intensity equals t1")]
        public void GivenExpectedAnswer_LightIntensity_VerifyResult()
        {
            var expectedResult = Color.FromScRgb(1.0F, _t1.X, _t1.Y, _t1.Z);

            var actualResult = _lightInstance.Intensity;

            Assert.Equal(expectedResult, actualResult);
        }

        [Then(@"light position equals t2")]
        public void GivenExpectedAnswer_LightPosition_VerifyResult()
        {
            var expectedResult = _t2;

            var actualResult = _lightInstance.Position;

            Assert.Equal(expectedResult, actualResult);
        }

        [And(@"light position equals t2")]
        public void GivenExpectedAnswer_LightPosition_VerifyResult_Overload()
        {
            GivenExpectedAnswer_LightPosition_VerifyResult();
        }

        [Then(@"material color equals t1")]
        public void GivenExpectedAnswer_MaterialColor_VerifyResult()
        {
            var expectedResult = Color.FromScRgb(1.0F, _t1.X, _t1.Y, _t1.Z);

            var actualResult = _materialInstance.Color;

            Assert.Equal(expectedResult, actualResult);
        }

        [And(@"material defaults are set")]
        public void GivenDefaultInitialization_MaterialInstance_VerifyResult()
        {
            Assert.Equal(_materialInstance.Ambient, Material.DefaultAmbient);
            Assert.Equal(_materialInstance.Specular, Material.DefaultSpecular);
            Assert.Equal(_materialInstance.Shininess, Material.DefaultShininess);
        }

        [Then(@"sphere material equals material")]
        public void GivenExpectedAnswer_SphereMaterial_VerifyResult()
        {
            var expectedResult = _materialInstance;

            var actualResult = _sphereInstance.Material;

            Assert.Equal(expectedResult, actualResult);
        }

    }
}
