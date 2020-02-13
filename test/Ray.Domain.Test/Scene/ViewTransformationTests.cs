using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Model;
using Xunit;
using Xunit.Gherkin.Quick;
using Gherkin.Ast;
using Ray.Domain.Test.Extensions;
using Feature = Xunit.Gherkin.Quick.Feature;

namespace Ray.Domain.Test.Scene
{
    [FeatureFile("./features/scene/ViewTransformation.feature")]
    public sealed class ViewTransformationTests : Feature
    {
        private Vector4 _from, _to, _up;
        private readonly Camera _cameraInstance = new Camera(160, 120, MathF.PI / 2);

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

        [Then(@"camera transform equals the following 4x4 matrix:")]
        public void ViewTransformation_ArbitraryMatrix_VerifyResult(DataTable m)
        {
            // Straight from text - already in CMF.
            var expectedResult = new Matrix4x4(
                m.ToFloat(1, 1), m.ToFloat(1, 2), m.ToFloat(1, 3), m.ToFloat(1, 4),
                m.ToFloat(2, 1), m.ToFloat(2, 2), m.ToFloat(2, 3), m.ToFloat(2, 4),
                m.ToFloat(3, 1), m.ToFloat(3, 2), m.ToFloat(3, 3), m.ToFloat(3, 4),
                m.ToFloat(4, 1), m.ToFloat(4, 2), m.ToFloat(4, 3), m.ToFloat(4, 4)
            );

            var actualResult = _cameraInstance.Transform;

            Assert.Equal(expectedResult, actualResult);
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
