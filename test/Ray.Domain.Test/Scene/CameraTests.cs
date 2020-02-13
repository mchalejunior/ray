using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Model;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Scene
{
    [FeatureFile("./features/scene/Camera.feature")]
    public sealed class CameraTests : Feature
    {
        private Camera _cameraInstance;
        private Model.Ray _rayInstance;
        private float _firstRotation;
        private Vector3 _translation;

        [Given(@"camera with Hsize (\d+) Vsize (\d+) and Field of pi over (\d+)")]
        public void InitializationValues_SetOnCameraInstance(int hsize, int vsize, int field)
        {
            _cameraInstance = new Camera(hsize, vsize, MathF.PI / field);
        }

        [And(@"firstRotation equals Pi over (\d)")]
        public void InitializationValues_SetOnFirstRotation(int fraction)
        {
            _firstRotation = MathF.PI / fraction;
        }

        [And(@"Translation equals tuple (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnTranslationInstance(float x, float y, float z)
        {
            _translation.X = x;
            _translation.Y = y;
            _translation.Z = z;
        }

        [And(@"camera transform equals first rotation on y and translation")]
        public void ViewTransformation_RotateAndTranslate_SetOnCamera()
        {
            // From text - never quite sure what order to apply composite transforms.
            // But just tried reversing order and got the right answer!

            var transformBuilder = new MatrixTransformationBuilder()
                    .Translate(_translation)
                    .RotateY(_firstRotation);

            _cameraInstance.SetViewTransformation(transformBuilder);
        }


        [When(@"calculate ray at (\d+) (\d+)")]
        public void CalculateRay_AtPixelLocation(int pixelX, int pixelY)
        {
            _rayInstance = _cameraInstance.GetRay(pixelX, pixelY);
        }

        [Then(@"camera PixelSize equals (\d+\.\d+)")]
        public void CameraInstance_VerifyPixels(float pixelSize)
        {
            var expectedAnswer = pixelSize;

            var actualAnswer = _cameraInstance.PixelSize;

            Assert.True(expectedAnswer.IsApproximately(actualAnswer));
        }

        [Then(@"ray origin equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_VerifyRayOrigin(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _rayInstance.Origin;

            Assert.True(expectedAnswer.IsApproximately(actualAnswer, 6));
        }

        [And(@"ray direction equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_VerifyRayDirection(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _rayInstance.Direction;

            Assert.True(expectedAnswer.IsApproximately(actualAnswer, 6));
        }

    }
}
