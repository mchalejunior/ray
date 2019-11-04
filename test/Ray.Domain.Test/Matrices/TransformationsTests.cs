using System;
using System.Numerics;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Matrices
{
    [FeatureFile("./features/matrices/Transformations.feature")]
    public sealed class TransformationsTests : Feature
    {
        private Matrix4x4 _firstMatrix, _secondMatrix;
        private Vector4 _tupleInstance;
        private float _firstRotation, _secondRotation;

        [Given(@"firstMatrix equals Translation Matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_Translation_SetOnFirstMatrixInstance(float x, float y, float z)
        {
            _firstMatrix = Matrix4x4.CreateTranslation(x, y, z);
        }

        [Given(@"firstMatrix equals Scaling Matrix (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_Scaling_SetOnFirstMatrixInstance(float x, float y, float z)
        {
            _firstMatrix = Matrix4x4.CreateScale(x, y, z);
        }

        [Given(@"firstMatrix equals Shearing Matrix (-?\d+) (-?\d+) (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_Shearing_SetOnFirstMatrixInstance(float x2y, float x2z, float y2x, float y2z, float z2x, float z2y)
        {
            _firstMatrix = BasicMatrixFactory.CreateShearingMatrix(
                x2y, x2z, y2x, y2z, z2x, z2y);
        }

        [And(@"firstMatrix equals X Rotation Matrix for firstRotation")]
        public void InitializationValues_XRotation_SetOnFirstMatrix()
        {
            _firstMatrix = Matrix4x4.CreateRotationX(_firstRotation);
        }

        [And(@"secondMatrix equals X Rotation Matrix for secondRotation")]
        public void InitializationValues_XRotation_SetOnSecondMatrix()
        {
            _secondMatrix = Matrix4x4.CreateRotationX(_secondRotation);
        }

        [And(@"firstMatrix equals Y Rotation Matrix for firstRotation")]
        public void InitializationValues_YRotation_SetOnFirstMatrix()
        {
            _firstMatrix = Matrix4x4.CreateRotationY(_firstRotation);
        }

        [And(@"secondMatrix equals Y Rotation Matrix for secondRotation")]
        public void InitializationValues_YRotation_SetOnSecondMatrix()
        {
            _secondMatrix = Matrix4x4.CreateRotationY(_secondRotation);
        }

        [And(@"firstMatrix equals Z Rotation Matrix for firstRotation")]
        public void InitializationValues_ZRotation_SetOnFirstMatrix()
        {
            _firstMatrix = Matrix4x4.CreateRotationZ(_firstRotation);
        }

        [And(@"secondMatrix equals Z Rotation Matrix for secondRotation")]
        public void InitializationValues_ZRotation_SetOnSecondMatrix()
        {
            _secondMatrix = Matrix4x4.CreateRotationZ(_secondRotation);
        }

        [And(@"firstRotation equals Pi over (\d)")]
        public void InitializationValues_SetOnFirstRotation(int fraction)
        {
            _firstRotation = (float)(Math.PI / fraction);
        }


        [And(@"secondRotation equals Pi over (\d)")]
        public void InitializationValues_SetOnSecondRotation(int fraction)
        {
            _secondRotation = (float)(Math.PI / fraction);
        }


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

        [And(@"secondMatrix equals inverse of firstMatrix")]
        public void InitializationValues_Inversion_SetOnSecondMatrix()
        {
            Matrix4x4.Invert(_firstMatrix, out var firstMatrixInverted);
            _secondMatrix = firstMatrixInverted;
        }


        [Then(@"firstMatrix multiplied by t1 equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_MultiplyFirstMatrixByTuple_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);

            var actualResult = _firstMatrix.ToColumnMajorForm().Multiply(_tupleInstance);

            Assert.True(expectedResult.IsApproximately(actualResult));
        }

        [Then(@"secondMatrix multiplied by t1 equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_MultiplySecondMatrixByTuple_VerifyResult(float x, float y, float z, float w)
        {
            var expectedResult = new Vector4(x, y, z, w);

            var actualResult = _secondMatrix.ToColumnMajorForm().Multiply(_tupleInstance);

            Assert.True(expectedResult.IsApproximately(actualResult));
        }

        [And(@"secondMatrix multiplied by t1 equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_MultiplySecondMatrixByTuple_VerifyResult_Overload(float x, float y, float z, float w)
        {
            GivenExpectedAnswer_MultiplySecondMatrixByTuple_VerifyResult(x, y, z, w);
        }


    }
}
