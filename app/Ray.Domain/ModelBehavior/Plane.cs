using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Transportation;

namespace Ray.Domain.Model
{
    public partial class Plane
    {
        public IEnumerable<IntersectionDto> GetIntersections(Ray ray, bool applyLocalTransformation = true)
        {
            // Modeled as xz Plane (y=0) passing through the origin.

            Ray localRay = GetTransformedRay(ray, applyLocalTransformation);

            if (MathF.Abs(localRay.Direction.Y).IsApproximately(0F))
            {
                // A Ray with y~0 is parallel (never intersects).
                // Infinite intersections ("inside" plane) also caught here:
                //  reporting no intersections is visually the same and also avoids stack overflow!

                return new List<IntersectionDto>();
            }

            // As per text. Formula (relevant only for xz Plane).
            // t = -originY / directionY
            var t = -localRay.Origin.Y / localRay.Direction.Y;
            return new List<IntersectionDto>
            {
                new IntersectionDto
                {
                    Ray = ray, // NOTE: return original ray (and shape with transformation).
                    Shape = this,
                    DistanceT = t
                }
            };
        }

        public Vector4 GetNormal(Vector4 point, bool applyLocalTransformation = true)
        {
            // Modeled as xz Plane (y=0) passing through the origin.

            if (applyLocalTransformation)
            {
                // Normal for a given point on any shape follows same principles.
                // Potential refactor to base class here. However, something to be
                // said for making it easier to understand by leaving it within each shape.
                // E.g. here the local normal is always constant as we assume an xz plane
                // passing through the origin.

                var object_point = Transformation.Execute(point, true);
                var object_normal = new Vector4(0F, 1F, 0F, 0F);
                var world_normal = Transformation.Execute(object_normal, true, true);
                world_normal.W = 0F;
                return Vector4.Normalize(world_normal);
            }
            else
            {
                return new Vector4(0F, 1F, 0F, 0F);
            }
        }
    }
}
