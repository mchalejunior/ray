using System.Numerics;
using Ray.Domain.Extensions;

namespace Ray.Domain.Model
{
    public partial class Camera
    {
        public void SetViewTransformation(Vector4 from, Vector4 to, Vector4 up)
        {
            // TODO: DBC - point, point, vector.

            var forward = Vector4.Normalize(to - from);
            var upn = Vector4.Normalize(up);
            var left = forward.Cross(upn);
            var true_up = left.Cross(forward);

            // Straight from text
            var columnMajorForm = new Matrix4x4
            {
                M11 = left.X, M12 = left.Y, M13 = left.Z, M14 = 0.0F,
                M21 = true_up.X, M22 = true_up.Y, M23 = true_up.Z, M24 = 0.0F,
                M31 = -forward.X, M32 = -forward.Y, M33 = -forward.Z, M34 = 0.0F,
                M41 = 0.0F, M42 = 0.0F, M43 = 0.0F, M44 = 1.0F
            };

            // As in MatrixTransformationBuilder: When chaining matrix transformations need
            // to standardize on CMF (textbook standard) to get the expected results!
            var orientation = columnMajorForm;
            var translation = Matrix4x4.CreateTranslation(-from.X, -from.Y, -from.Z)
                                       .ToColumnMajorForm();

            var view_transform = orientation * translation;
            Transform = view_transform;
        }
    }
}
