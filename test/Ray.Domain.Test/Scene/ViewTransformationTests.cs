using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Model;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Scene
{
    [FeatureFile("./features/scene/ViewTransformation.feature")]
    public sealed class ViewTransformationTests : Feature
    {
        private Vector4 _from, _to, _up;
        private Camera _cameraInstance = new Camera();

        [Given(@"From equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnFromInstance(float x, float y, float z, float w)
        {
            _from.X = x;
            _from.Y = y;
            _from.Z = z;
            _from.W = w;
        }

        [And(@"To equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnToInstance(float x, float y, float z, float w)
        {
            _to.X = x;
            _to.Y = y;
            _to.Z = z;
            _to.W = w;
        }

        [And(@"Up equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnUpInstance(float x, float y, float z, float w)
        {
            _up.X = x;
            _up.Y = y;
            _up.Z = z;
            _up.W = w;
        }

        [When(@"set view transformation on camera")]
        public void InitializationValues_UseFromToUp_SetOnCameraInstance()
        {
            _cameraInstance.SetViewTransformation(_from, _to, _up);
        }

        [Then(@"camera transform equals identity matrix")]
        public void ViewTransformation_IdentityMatrix_VerifyResult()
        {
            AssertExpectedAgainstViewTransformation(
                expectedResultInRowMajorForm: Matrix4x4.Identity);
        }

        [Then(@"camera transform equals scaling matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void ViewTransformation_ScalingMatrix_VerifyResult(float x, float y, float z)
        {
            AssertExpectedAgainstViewTransformation(
                expectedResultInRowMajorForm: Matrix4x4.CreateScale(x, y, z));
        }

        [Then(@"camera transform equals translation matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void ViewTransformation_TranslationMatrix_VerifyResult(float x, float y, float z)
        {
            AssertExpectedAgainstViewTransformation(
                expectedResultInRowMajorForm: Matrix4x4.CreateTranslation(x, y, z));
        }




        private void AssertExpectedAgainstViewTransformation(Matrix4x4 expectedResultInRowMajorForm)
        {
            // See Camera.Transformation.
            // Stored in CMF (texts standard).
            // Convert expected from RMF (Microsoft standard) before comparing.

            var expectedResultInColumnMajorForm = expectedResultInRowMajorForm.ToColumnMajorForm();

            var actualResultInColumnMajorForm = _cameraInstance.Transform;

            Assert.Equal(expectedResultInColumnMajorForm, actualResultInColumnMajorForm);
        }
    }
}
