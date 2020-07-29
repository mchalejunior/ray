using System;
using System.Numerics;
using Ray.Domain.Maths.Factories;
using Ray.Serialize.Scene;

namespace Ray.Command.Scene.Factory
{
    class TransformationFactory
    {
        public static Action<IMatrixTransformationBuilder> MapApiTransformToDomainAction(TransformDto input)
        {
            switch (input.TransformType)
            {
                case TransformDto.TransformationType.RotateX:
                    return tx => tx.RotateX(input.RotationRadians);
                case TransformDto.TransformationType.RotateY:
                    return tx => tx.RotateY(input.RotationRadians);
                case TransformDto.TransformationType.RotateZ:
                    return tx => tx.RotateZ(input.RotationRadians);
                case TransformDto.TransformationType.Scale:
                    return tx => tx.Scale(new Vector3(input.VectorTransformation.X, input.VectorTransformation.Y, input.VectorTransformation.Z));
                case TransformDto.TransformationType.Translate:
                    return tx => tx.Translate(new Vector3(input.VectorTransformation.X, input.VectorTransformation.Y, input.VectorTransformation.Z));
                case TransformDto.TransformationType.Shear:
                    var sx = input.ShearTransformation;
                    return tx => tx.Shear(sx.X2Y, sx.X2Z, sx.Y2X, sx.Y2Z, sx.Z2X, sx.Z2Y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(input.TransformType), "Unrecognized transform type: " + input.TransformType);
            }
        }
    }
}
