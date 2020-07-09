using Ray.Domain.Extensions;
using Ray.Domain.Transportation;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Ray.Domain.Model
{
    public partial class Sphere
    {

        public IEnumerable<IntersectionDto> GetIntersections(Ray ray, bool applyLocalTransformation = true)
        {
            // These calculation assume a unit sphere at the axis origin.
            // See Transformation property to understand how we scale and translate.
            // But for this method, we should validate the assumptions as a pre-condition.
            bool preconditionFailure = !IsPerfectSphere || !IsAtAxisOrigin;
            if (_enforceAssumptions && preconditionFailure)
            {
                throw new ApplicationException("Precondition failed: Require a unit sphere at the axis origin.");
            }

            // As per text (and see this.Transformation): leave sphere as a unit sphere
            // at the origin and transform the ray instead - simplest way to calculate intersections.
            Ray localRay = GetTransformedRay(ray, applyLocalTransformation);

            // Straight from text. See for derivation details.
            var sphere_to_ray = localRay.Origin - this.Origin;
            var a = Vector4.Dot(localRay.Direction, localRay.Direction);
            var b = 2 * Vector4.Dot(localRay.Direction, sphere_to_ray);
            var c = Vector4.Dot(sphere_to_ray, sphere_to_ray) - 1;

            var discriminant = MathF.Pow(b, 2F) - 4 * a * c;

            if (discriminant < 0)
            {
                return new List<IntersectionDto>();
            }

            var t1 = (-b - MathF.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + MathF.Sqrt(discriminant)) / (2 * a);

            // Ray origin normal, unless:
            // 1. Only one of (t1, t2) is negative - tells us ray originates inside shape.
            //     Edge case - consider t=0 *on shape* as "inside".
            // 2. Both of (t1, t2) are negative - tells us shape is behind the ray.
            IntersectionDto.RaysOrigin rayOrigin = IntersectionDto.RaysOrigin.Normal;
            if (t1 * t2 <= 0F)
            {
                rayOrigin = IntersectionDto.RaysOrigin.RayInsideShape;
            }

            if (t1 < 0F && t2 < 0F)
            {
                rayOrigin = IntersectionDto.RaysOrigin.ShapeBehindRay;
            }

            return new List<IntersectionDto>
            {
                new IntersectionDto
                {
                    Ray = ray, // NOTE: return original ray (and shape with transformation).
                    Shape = this,
                    DistanceT = t1,
                    TangentialIntersection = t1.IsApproximately(t2, null),
                    RayOrigin = rayOrigin
                },
                new IntersectionDto
                {
                    Ray = ray, // NOTE: return original ray (and shape with transformation).
                    Shape = this,
                    DistanceT = t2,
                    TangentialIntersection = t1.IsApproximately(t2, null),
                    RayOrigin = rayOrigin
                }
            };

        }

        public override Vector4 GetNormal(Vector4 point, bool applyLocalTransformation = true)
        {
            //TODO: check.require on point, to make sure is a point (W=1)?

            // These calculation assume a unit sphere at the axis origin.
            // See Transformation property to understand how we scale and translate.
            // But for this method, we should validate the assumptions as a pre-condition.
            bool preconditionFailure = !IsPerfectSphere || !IsAtAxisOrigin;
            if (_enforceAssumptions && preconditionFailure)
            {
                throw new ApplicationException("Precondition failed: Require a unit sphere at the axis origin.");
            }

            return base.GetNormal(point, applyLocalTransformation);
        }


        protected override Vector4 GetLocalNormal(Vector4 point)
        {
            return point - Origin;
        }
    }
}
