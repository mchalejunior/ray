using System;
using System.Collections.Generic;
using System.Numerics;
using Ray.Domain.Extensions;
using Xunit.Gherkin.Quick;
using Ray.Domain.Maths;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Model;
using Xunit;

namespace Ray.Domain.Test.Shadow
{
    [FeatureFile("./features/shadow/Shadows.feature")]
    public sealed class ShadowsFeatureTests : Feature
    {
        private static Color WHITE = Color.FromScRgb(1F, 1F, 1F, 1F);
        private World _world;
        private Sphere _outerSphere, _innerSphere;
        private Model.Light _lightInstance;
        private Vector4 _eye, _surfaceNormal, _t1, _t2, _pointPosition;
        private bool _isInShadow;
        private Color _resultantColor;
        private Material _materialInstance;


        [Given(@"position at the origin")]
        public void InitializationValues_SetPositionAtOrigin()
        {
            _pointPosition = new Vector3(0F, 0F, 0F).AsPoint();
        }

        [And(@"Material with default values")]
        public void InitializationValues_Defaults_SetOnMaterialInstance()
        {
            _materialInstance = Material.CreateDefaultInstance();
        }

        // Use star notation in feature file: * World equals test default setup
        [Given("World equals test default setup")]
        public void InitializationValues_SetupDefaultWorld()
        {
            InitializationValues_SetupDefaultWorld_Overload();
        }

        private void InitializationValues_SetupDefaultWorld_Overload(float? fixedAmbient = null)
        {
            _outerSphere = Sphere.CreateDefaultInstance();
            _innerSphere = Sphere.CreateDefaultInstance();
            _outerSphere
                .UpdateColor(Color.FromScRgb(Material.DefaultColorA, 0.8F, 1.0F, 0.6F))
                .UpdateDiffuse(0.7F)
                .UpdateSpecular(0.2F)
                .UpdateAmbient(fixedAmbient ?? _outerSphere.Material.Ambient);
            _innerSphere
                .UpdateAmbient(fixedAmbient ?? _innerSphere.Material.Ambient);

            // As feature file: concentric circles with inner sphere scaled down.
            _innerSphere.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(0.5F, 0.5F, 0.5F));

            _world = new World(
                new List<IBasicShape> { _outerSphere, _innerSphere },
                new Model.Light
                {
                    Position = new Vector4(-10.0F, 10.0F, -10.0F, 1.0F),
                    Intensity = WHITE
                }
            );
        }



        [Given(@"eye equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_SetOnEyeInstance(float x, float y, float z, float w)
        {
            _eye.X = x;
            _eye.Y = y;
            _eye.Z = z;
            _eye.W = w;
        }

        [And(@"normal equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void InitializationValues_SetOnNormalInstance(float x, float y, float z, float w)
        {
            _surfaceNormal.X = x;
            _surfaceNormal.Y = y;
            _surfaceNormal.Z = z;
            _surfaceNormal.W = w;
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

        [And(@"light intensity and position initialized from t1 and t2")]
        public void InitializationValues_SetOnLightInstance()
        {
            _lightInstance = new Model.Light
            {
                Intensity = Color.FromScRgb(1.0F, _t1.X, _t1.Y, _t1.Z),
                Position = _t2
            };
        }

        [And(@"is in shadow equals (.+)")]
        public void InitializationValues_SetOnShadowInfo(string isInShadow)
        {
            _isInShadow = bool.Parse(isInShadow);
        }

        [When("calculate resultantColor lighting for material light position eye normal isInShadow")]
        public void Calculate_Lighting_SetOnResultColor()
        {
            _resultantColor = Lighting.CalculateColorWithPhongReflection(
                _materialInstance, _lightInstance, _pointPosition, _eye, _surfaceNormal, _isInShadow
            );
        }

        [Then(@"resultantColor equals tuple (\d+\.\d+) (\d+\.\d+) (\d+\.\d+)")]
        public void GivenExpectedAnswer_CompareToCalculatedInstance_VerifyResult(float r, float g, float b)
        {
            var expectedResult = Color.FromScRgb(1.0F, r, g, b);

            var actualResult = _resultantColor;

            Assert.True(expectedResult.AreClose(actualResult, true));
        }

        [Then(@"t1 placed in default world in_shadow equals (.+)")]
        public void GivenExpectedAnswer_CalculateT1Shadowed_VerifyResult(string in_shadow)
        {
            // For Ray from point (_t1), back to the light source:
            //  * Any collisions?
            //  * If so, is hit between the point position and the light? i.e. casts _t1 in shadow
            var v = _world.LightSource.Position - _t1;
            var direction = Vector4.Normalize(v);
            var lightingResult = Lighting.CalculateColorWithPhongReflection(
                _world, new Model.Ray(_t1, direction));

            var expectedAnswer = bool.Parse(in_shadow);
            var actualAnswer = lightingResult.IsInShadow;

            Assert.Equal(expectedAnswer, actualAnswer);
        }


    }
}
