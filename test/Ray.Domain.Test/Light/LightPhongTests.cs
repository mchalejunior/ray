using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit.Gherkin.Quick;
using System.Windows.Media;
using Ray.Domain.Extensions;
using Ray.Domain.Model;
using Xunit;

namespace Ray.Domain.Test.Light
{
    [FeatureFile("./features/light/LightPhong.feature")]
    public sealed class LightPhongTests : Feature
    {
        private Model.Light _lightInstance;
        private Vector4 _t1, _t2, _eye, _surfaceNormal, _pointPosition;
        private Color _resultantColor;
        private Material _materialInstance;


        [Given(@"position at the origin")]
        public void InitializationValues_SetPositionAtOrigin()
        {
            _pointPosition = new Vector3(0F, 0F, 0F).AsPoint();
        }

        // Use star notation in feature file: * Material with default values
        [And(@"Material with default values")]
        public void InitializationValues_Defaults_SetOnMaterialInstance()
        {
            _materialInstance = Material.CreateDefaultInstance();
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



        [And(@"light intensity and position initialized from t1 and t2")]
        public void InitializationValues_SetOnLightInstance()
        {
            _lightInstance = new Model.Light
            {
                Intensity = Color.FromScRgb(1.0F, _t1.X, _t1.Y, _t1.Z),
                Position = _t2
            };
        }


        [When("calculate resultantColor lighting for material light position eye normal")]
        public void Calculate_Lighting_SetOnResultColor()
        {
            // TODO: Just sticking "phong reflection" calculation here.
            // This needs to move e.g. under Domain.Maths.

            Color ambient, diffuse, specular;

            // combine the surface color with the lights color/intensity
            Color effective_color = _materialInstance.Color.Multiply(_lightInstance.Intensity);

            // find the direction to the light source
            var lightv = Vector4.Normalize(_lightInstance.Position - _pointPosition);

            // compute the ambient contribution
            ambient = effective_color * _materialInstance.Ambient;

            // light_dot_normal represents the cosine of the angle between the
            // light vector and the normal vector. A negative number means the
            // light is on the other side of the surface.
            var light_dot_normal = Vector4.Dot(lightv, _surfaceNormal);
            if (light_dot_normal < 0F)
            {
                diffuse = System.Windows.Media.Colors.Black;
                specular = System.Windows.Media.Colors.Black;
            }
            else
            {
                // compute the diffuse contribution
                diffuse = effective_color * _materialInstance.Diffuse * light_dot_normal;

                // reflect_dot_eye represents the cosine of the angle between the
                // reflection vector and the eye vector. A negative number means the
                // light reflects away from the eye.
                var reflectv = (-lightv).Reflect(_surfaceNormal);
                var reflect_dot_eye = Vector4.Dot(reflectv, _eye);

                if (reflect_dot_eye <= 0F)
                {
                    specular = System.Windows.Media.Colors.Black;
                }
                else
                {
                    // compute the specular contribution
                    var factor = MathF.Pow(reflect_dot_eye, _materialInstance.Shininess);
                    specular = _lightInstance.Intensity * _materialInstance.Specular * factor;
                }
            }

            // add the three contributions together to get the final shading
            _resultantColor = ambient + diffuse + specular;
        }


        [Then(@"resultantColor equals tuple (\d+\.\d+) (\d+\.\d+) (\d+\.\d+)")]
        public void GivenExpectedAnswer_CompareToCalculatedInstance_VerifyResult(float r, float g, float b)
        {
            var expectedResult = Color.FromScRgb(1.0F, r, g, b);

            var actualResult = _resultantColor;

            Assert.True(expectedResult.AreClose(actualResult, true));
        }


    }
}
