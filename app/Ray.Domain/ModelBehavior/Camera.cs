using System.Numerics;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;

namespace Ray.Domain.Model
{
    public partial class Camera
    {

        public void SetViewTransformation(IMatrixTransformationBuilder transform)
        {
            Transform = transform.GetCompositeTransformation();
        }

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

        public Ray GetRay(int pixelX, int pixelY)
        {
            // The offset from the edge of the canvas to the pixel's center.
            float xoffset = (pixelX + 0.5F) * PixelSize;
            float yoffset = (pixelY + 0.5F) * PixelSize;

            // The untransformed coordinates of the pixel in world space.
            // Remember: The camera looks toward -z, so +x is to the left.
            float world_x = HalfWidth - xoffset;
            float world_y = HalfHeight - yoffset;

            // Using the camera matrix, transform the canvas point and the origin,
            // and then compute the ray's direction vector.
            // Remember: Canvas is at z=-1 (always 1 unit away - use transforms like in intersections).
            var pixel = Transform.Invert().Multiply(
                new Vector4(world_x, world_y, -1.0F, 1.0F), 
                true);
            var origin = Transform.Invert().Multiply(
                new Vector4(0.0F, 0.0F, 0.0F, 1.0F),
                true);
            var direction = Vector4.Normalize(pixel - origin);

            return new Ray(origin, direction);
        }

    }
}
