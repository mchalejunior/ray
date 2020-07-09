using Ray.Domain.Model;
using System.Numerics;

namespace Ray.Domain.Maths.Factories
{
    public interface IMatrixTransformationBuilder
    {
        /// <summary>
        /// Apply (multiply) all added matrix transformations
        /// as a chain or composite transformation.
        /// </summary>
        /// <remarks>
        /// When chaining transformations you must apply in reverse.
        /// So note that the order the transformations were added using
        /// <see cref="RotateX"/>, <see cref="Scale"/> etc. will be
        /// chained in reverse order and the result multiplied by the input tuple.
        /// </remarks>
        /// <param name="tuple">
        /// The <see cref="Vector4"/> (normally a point) to apply the transform(s) to.
        /// </param>
        /// <param name="invert">
        /// E.g. apply the transform to the <see cref="Model.Ray"/> rather than the
        /// <see cref="IBasicShape"/>. So invert the result to invert the perspective.
        /// </param>
        /// <param name="transpose">
        /// Same idea as <paramref name="invert"/> but for calculating normals.
        /// Normal calculation actually inverts and then transposes.
        /// </param>
        Vector4 Execute(Vector4 tuple, bool invert = false, bool transpose = false);
        /// <summary>
        /// Get the chained / composite matrix.
        /// </summary>
        /// <remarks>
        /// Favor using <see cref="Execute"/> instead, but you can
        /// retrieve the matrix result with this method, if required.
        /// </remarks>
        /// <see cref="Execute"/>.
        Matrix4x4 GetCompositeTransformation();

        IMatrixTransformationBuilder RotateX(float radians);
        IMatrixTransformationBuilder RotateY(float radians);
        IMatrixTransformationBuilder RotateZ(float radians);
        IMatrixTransformationBuilder Scale(Vector3 proportions);
        IMatrixTransformationBuilder Translate(Vector3 vector);
        IMatrixTransformationBuilder Shear(float x2y, float x2z, float y2x, float y2z, float z2x, float z2y);
    }
}