using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Media;
using Ray.Domain.Extensions;
using Ray.Domain.Maths;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Model;
using Ray.Domain.Transportation;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Scene
{
    [FeatureFile("./features/scene/World.feature")]
    public sealed class WorldTests : Feature
    {
        private World _world;
        private Sphere _outerSphere, _innerSphere;
        private Vector4 _origin, _direction;
        private Model.Ray _rayInstance;
        private List<IntersectionDto> _xs;
        private IntersectionDto _hit;


        [Given("world equals test default setup")]
        public void InitializationValues_SetupDefaultWorld()
        {
            InitializationValues_SetupDefaultWorld_Overload();
        }

        private void InitializationValues_SetupDefaultWorld_Overload(float? fixedAmbient = null)
        {
            _outerSphere = Sphere.CreateDefaultInstance();
            _innerSphere = Sphere.CreateDefaultInstance();

            // Encapsulate further, like SetColor?
            var innerMaterial = _innerSphere.Material;
            var outerNonDefaultMaterial = Material.CreateDefaultInstance();
            outerNonDefaultMaterial.Color = Color.FromScRgb(Material.DefaultColorA, 0.8F, 1.0F, 0.6F);
            outerNonDefaultMaterial.Diffuse = 0.7F;
            outerNonDefaultMaterial.Specular = 0.2F;

            if (fixedAmbient.HasValue)
            {
                innerMaterial.Ambient = fixedAmbient.Value;
                outerNonDefaultMaterial.Ambient = fixedAmbient.Value;
            }

            _outerSphere.Material = outerNonDefaultMaterial;
            _innerSphere.Material = innerMaterial;
            // As feature file: concentric circles with inner sphere scaled down.
            _innerSphere.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(0.5F, 0.5F, 0.5F));

            _world = new World(
                new List<IBasicShape> { _outerSphere, _innerSphere },
                new Model.Light
                {
                    Position = new Vector4(-10.0F, 10.0F, -10.0F, 1.0F),
                    Intensity = System.Windows.Media.Colors.White
                }
            );
        }

        [And(@"light source is inside both spheres")]
        public void InitializationValues_LightSource_InsideBothSpheres()
        {
            _world.LightSource.Position = new Vector4(0.0F, 0.25F, 0.0F, 1.0F);
        }

        [And(@"light source is inside outer sphere")]
        public void InitializationValues_LightSource_InsideOuterSphere()
        {
            _world.LightSource.Position = new Vector4(0.0F, 0.0F, 0.75F, 1.0F);
        }

        [And(@"material ambient equals (\d+\.\d+)")]
        public void InitializationValues_Material_SetAmbient(float ambient)
        {
            // Use case: Set ambient to 1 so can assert on known color.
            InitializationValues_SetupDefaultWorld_Overload(ambient);
        }


        [And(@"origin equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnOriginInstance(float x, float y, float z, float w)
        {
            _origin.X = x;
            _origin.Y = y;
            _origin.Z = z;
            _origin.W = w;
        }

        [And(@"origin equals tuple (-?\d+.\d+) (-?\d+.\d+) (-?\d+.\d+) (-?\d+.\d+)")]
        public void InitializationValues_SetOnOriginInstance_Overload(float x, float y, float z, float w)
        {
            InitializationValues_SetOnOriginInstance(x, y, z, w);
        }

        [And(@"direction equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnDirectionInstance(float x, float y, float z, float w)
        {
            _direction.X = x;
            _direction.Y = y;
            _direction.Z = z;
            _direction.W = w;
        }

        [When(@"initialize ray with origin and direction")]
        public void InitializationValues_SetOnRayInstance()
        {
            _rayInstance.Origin = _origin;
            _rayInstance.Direction = _direction;
        }

        [And(@"xs equals world intersections given ray")]
        public void CalculateIntersections_SetResultList()
        {
            _xs = _world.CalculateIntersections(_rayInstance).OrderBy(x => x.DistanceT).ToList();
        }

        [And(@"hit equals world intersection hit given ray")]
        public void CalculateHit_SetOnHitInstance()
        {
            _hit = _world.CalculateHit(_rayInstance);
        }


        [Then("hit shape equals default world outer sphere")]
        public void GivenExpectedAnswer_VerifyHitShape()
        {
            var expectedAnswer = _outerSphere;

            var actualAnswer = _hit.Shape;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"hit position equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_VerifyPosition(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _hit.Position;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"hit eyev equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_VerifyEyeV(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _hit.EyeV;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"hit normalv equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_VerifyNormalV(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _hit.NormalV;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"hit inside shape equals (\w+)")]
        public void GivenExpectedAnswer_VerifyRayOrigin(bool isInside)
        {
            var expectedAnswer = isInside;

            var actualAnswer = _hit.RayOriginatesInsideShape;

            Assert.Equal(expectedAnswer, actualAnswer);
        }


        [Then(@"xs intersection count equals (\d)")]
        public void GivenExpectedAnswer_RunIntersectionSimulation_VerifyCount(int count)
        {
            var expectedAnswer = count;

            var actualAnswer = _xs.Count;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs element (\d) has t equals (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyDistance(int index, float t)
        {
            var expectedAnswer = t;

            var actualAnswer = _xs[index].DistanceT;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        //[And(@"xs element (\d) has shape equals sphere")]
        //public void GivenExpectedAnswer_QueryIntersection_VerifyShape(int index)
        //{
        //    var expectedAnswer = _sphereInstance;

        //    var actualAnswer = _xs[index].Shape;

        //    Assert.Equal(expectedAnswer, actualAnswer);
        //}

        [And(@"xs hit t equals (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryHit_VerifyDistance(float t)
        {
            var expectedAnswer = t;

            var actualAnswer = _hit.DistanceT;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs hit t equals null")]
        public void GivenExpectedAnswer_QueryHit_VerifyNull()
        {
            Assert.False(_hit.HasValue);
        }

        [And(@"xs calculates ray as originating inside shape")]
        public void GivenExpectedAnswer_QueryRayOrigin_VerifyInside()
        {
            var expectedAnswer = true;

            var actualAnswer = _xs[0].RayOriginatesInsideShape;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs calculates shape being behind ray")]
        public void GivenExpectedAnswer_QueryRayOrigin_VerifyShapeBehind()
        {
            var expectedAnswer = IntersectionDto.RaysOrigin.ShapeBehindRay;

            var actualAnswer = _xs[0].RayOrigin;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs calculates shape being in front of ray")]
        public void GivenExpectedAnswer_QueryRayOrigin_VerifyNormal()
        {
            var expectedAnswer = IntersectionDto.RaysOrigin.Normal;

            var actualAnswer = _xs[0].RayOrigin;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs calculates intersection as tangential")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyTangential()
        {
            var expectedAnswer = true;

            var actualAnswer = _xs[0].TangentialIntersection;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs calculates intersection as non tangential")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyNonTangential()
        {
            var expectedAnswer = false;

            var actualAnswer = _xs[0].TangentialIntersection;

            Assert.Equal(expectedAnswer, actualAnswer);
        }


        [Then(@"world color for ray equals (\d+\.\d+) (\d+\.\d+) (\d+\.\d+)")]
        public void GivenExpectedAnswer_VerifyColor(float r, float g, float b)
        {
            var expectedAnswer = Color.FromScRgb(Material.DefaultColorA, r, g, b);

            var actualAnswer = Lighting.CalculateColorWithPhongReflection(_world, _rayInstance);

            Assert.True(expectedAnswer.AreClose(actualAnswer, true));

        }

        [Then(@"world color for ray equals inner material color")]
        public void GivenExpectedAnswer_CompareColor()
        {
            GivenExpectedAnswer_VerifyColor(
                _innerSphere.Material.Color.ScR,
                _innerSphere.Material.Color.ScG,
                _innerSphere.Material.Color.ScB
            );
        }

    }
}
