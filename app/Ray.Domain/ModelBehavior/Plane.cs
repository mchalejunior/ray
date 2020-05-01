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

        public override Vector4 GetNormal(Vector4 point, bool applyLocalTransformation = true)
        {
            // Modeled as xz Plane (y=0) passing through the origin.

            // Can't hand straight off to the base. Calculation for Sphere and Plane follows
            // identical routine, when applying transforms, but differs if none.
            // We'll need a third shape for further comparison, but for now the simplest
            // approach is to have the Plane return it's known constant if no transforms to apply.

            bool isTransformSpecified = Transformation.GetCompositeTransformation() != Matrix4x4.Identity;
            bool shouldApplyLocalTransformation = applyLocalTransformation && isTransformSpecified;

            if (shouldApplyLocalTransformation)
            {
                return base.GetNormal(point, true);
            }

            return ConstantNormal;
        }

        protected override Vector4 GetLocalNormal(Vector4 point)
        {
            return point - ConstantNormal;
        }
    }
}
