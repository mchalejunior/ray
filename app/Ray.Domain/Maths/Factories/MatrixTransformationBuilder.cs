﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Ray.Domain.Extensions;

namespace Ray.Domain.Maths.Factories
{
    class MatrixTransformationBuilder : IMatrixTransformationBuilder
    {
        private readonly Stack<Matrix4x4> _transformationsStack = new Stack<Matrix4x4>();

        public Vector4 Execute(Vector4 tuple, bool invert = false)
        {
            if (!_transformationsStack.Any())
            {
                return tuple;
            }

            var transformationChain = GetCompositeTransformation();
            if (invert)
            {
                Matrix4x4.Invert(transformationChain, out transformationChain);
            }
            return transformationChain.Multiply(tuple, true);
        }

        public Matrix4x4 GetCompositeTransformation()
        {
            if (!_transformationsStack.Any())
            {
                return Matrix4x4.Identity;
            }

            // Copy instance stack, so can re-execute as many times as need to.
            var matrices = new Stack<Matrix4x4>(_transformationsStack.Reverse());
            Matrix4x4 transformationChain = default;
            bool firstTransform = true;
            while (matrices.TryPop(out var currentTransformation))
            {
                if (firstTransform)
                {
                    firstTransform = false;
                    transformationChain = currentTransformation.ToColumnMajorForm();
                    continue;
                }

                transformationChain *= currentTransformation.ToColumnMajorForm();
            }

            return transformationChain;
        }

        public IMatrixTransformationBuilder RotateX(float radians)
        {
            _transformationsStack.Push(Matrix4x4.CreateRotationX(radians));
            return this;
        }

        public IMatrixTransformationBuilder RotateY(float radians)
        {
            _transformationsStack.Push(Matrix4x4.CreateRotationY(radians));
            return this;
        }

        public IMatrixTransformationBuilder RotateZ(float radians)
        {
            _transformationsStack.Push(Matrix4x4.CreateRotationZ(radians));
            return this;
        }

        public IMatrixTransformationBuilder Scale(Vector3 proportions)
        {
            _transformationsStack.Push(Matrix4x4.CreateScale(proportions));
            return this;
        }

        public IMatrixTransformationBuilder Translate(Vector3 vector)
        {
            _transformationsStack.Push(Matrix4x4.CreateTranslation(vector));
            return this;
        }
    }
}
