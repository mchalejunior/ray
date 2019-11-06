using System.Numerics;

namespace Ray.Domain.Maths.Factories
{
    public interface IMatrixTransformationBuilder
    {
        Vector4 Execute(Vector4 tuple);

        IMatrixTransformationBuilder RotateX(float radians);
        IMatrixTransformationBuilder RotateY(float radians);
        IMatrixTransformationBuilder RotateZ(float radians);
        IMatrixTransformationBuilder Scale(Vector3 proportions);
        IMatrixTransformationBuilder Translate(Vector3 vector);
    }
}