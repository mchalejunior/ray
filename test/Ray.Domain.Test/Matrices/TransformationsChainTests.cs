using System;
using System.Numerics;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Matrices
{
    // In Transformations.feature we showed that transformations could be applied
    // (individually) in sequence or could be chained together.
    // If chained together, the transformations have to be applied in reverse order
    // to correctly encode a matrix with the combination of all transforms.

    // In TransformationsChain.feature we're working a little with the client API to make it
    // more natural to use. It should be understood when and why ToColumnMajorForm() gets called,
    // but ideally the API would also hide this detail for us entirely!

    [FeatureFile("./features/matrices/TransformationsChain.feature")]
    public sealed class TransformationsChainTests : Feature
    {
        private Vector4 _tupleInstance;
        private Vector3 _translation, _scale;
        private float _firstRotation;

        [Given(@"t1 equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnTupleInstance_Overload(float x, float y, float z, float w)
        {
            InitializationValues_SetOnTupleInstance(x, y, z, w);
        }

        [And(@"t1 equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnTupleInstance(float x, float y, float z, float w)
        {
            _tupleInstance.X = x;
            _tupleInstance.Y = y;
            _tupleInstance.Z = z;
            _tupleInstance.W = w;
        }

        [And(@"Translation equals tuple (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnTranslationInstance(float x, float y, float z)
        {
            _translation.X = x;
            _translation.Y = y;
            _translation.Z = z;
        }

        [And(@"Scale equals tuple (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnScaleInstance(float x, float y, float z)
        {
            _scale.X = x;
            _scale.Y = y;
            _scale.Z = z;
        }

        [And(@"firstRotation equals Pi over (\d)")]
        public void InitializationValues_SetOnFirstRotation(int fraction)
        {
            _firstRotation = (float)(Math.PI / fraction);
        }

        [Then(@"x-rotate scale and translate on t1 equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_XRotateScaleAndTranslate_TupleInstance_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);

            var actualResult_Raw = GetActualResult_Raw();
            var actualResult_Natural = GetActualResult_Natural();

            Assert.True(expectedResult.IsApproximately(actualResult_Raw));
            Assert.True(expectedResult.IsApproximately(actualResult_Natural));
        }

        private Vector4 GetActualResult_Raw()
        {
            // No attempt to clean-up API.
            // Just to show the explicit calls being made.

            // Transformation sequence
            var rotate = Matrix4x4.CreateRotationX(_firstRotation);
            var scale = Matrix4x4.CreateScale(_scale);
            var translate = Matrix4x4.CreateTranslation(_translation);

            // Reverse sequence when chaining.
            // ToColumnMajorForm() - remember the Text uses CMF. So any inputs and outputs
            //  need to be in CMF in order to get the same results. The Text is the requirements
            //  and the test specs are our validation, therefore we need to use CMF.
            var chainedTransformation = translate.ToColumnMajorForm() * scale.ToColumnMajorForm() * rotate.ToColumnMajorForm();

            // Don't ToColumnMajorForm() here, because chainedTransformation already in CMF
            // and the Multiply extension method produces a CMF "skinny matrix" from the tuple.
            return chainedTransformation.Multiply(_tupleInstance, true);
        }

        private Vector4 GetActualResult_Natural()
        {
            // More natural way of chaining transformations.
            // The builder does the RMF/CMF conversion and
            // reverses the transformation order for final calculation.

            IMatrixTransformationBuilder builder = new MatrixTransformationBuilder();
            return builder
                .RotateX(_firstRotation)
                .Scale(_scale)
                .Translate(_translation)
                .Execute(_tupleInstance);
        }
    }
}
