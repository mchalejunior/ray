using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Ray.Domain.Extensions;

namespace Ray.Domain.Maths.Factories
{
    class MatrixTransformationBuilder : IMatrixTransformationBuilder
    {
        private readonly Stack<Matrix4x4> _matrices = new Stack<Matrix4x4>();

        public Vector4 Execute(Vector4 tuple)
        {
            if (!_matrices.Any())
            {
                throw new InvalidOperationException("No transformations specified");
            }

            Matrix4x4 transformationChain = default;
            bool firstTransform = true;
            while (_matrices.TryPop(out var currentTransformation))
            {
                if (firstTransform)
                {
                    firstTransform = false;
                    transformationChain = currentTransformation.ToColumnMajorForm();
                    continue;
                }

                transformationChain *= currentTransformation.ToColumnMajorForm();
            }

            return transformationChain.Multiply(tuple, true);
        }

        public IMatrixTransformationBuilder RotateX(float radians)
        {
            _matrices.Push(Matrix4x4.CreateRotationX(radians));
            return this;
        }

        public IMatrixTransformationBuilder RotateY(float radians)
        {
            _matrices.Push(Matrix4x4.CreateRotationY(radians));
            return this;
        }

        public IMatrixTransformationBuilder RotateZ(float radians)
        {
            _matrices.Push(Matrix4x4.CreateRotationZ(radians));
            return this;
        }

        public IMatrixTransformationBuilder Scale(Vector3 proportions)
        {
            _matrices.Push(Matrix4x4.CreateScale(proportions));
            return this;
        }

        public IMatrixTransformationBuilder Translate(Vector3 vector)
        {
            _matrices.Push(Matrix4x4.CreateTranslation(vector));
            return this;
        }
    }
}
