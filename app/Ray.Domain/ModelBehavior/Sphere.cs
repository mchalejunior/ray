﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Media;
using Ray.Domain.Extensions;
using Ray.Domain.Transportation;

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

            return new List<IntersectionDto>
            {
                new IntersectionDto
                {
                    Ray = ray, // NOTE: return original ray (and sphere with transformation).
                    Shape = this,
                    DistanceT = t1
                },
                new IntersectionDto
                {
                    Ray = ray, // NOTE: return original ray (and sphere with transformation).
                    Shape = this,
                    DistanceT = t2
                }
            };

        }

        public Vector4 GetNormal(Vector4 point, bool applyLocalTransformation = true)
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

            if (applyLocalTransformation)
            {
                // Straight from text. See for derivation details.
                var object_point = Transformation.Execute(point, true);
                var object_normal = object_point - Origin;
                var world_normal = Transformation.Execute(object_normal, true, true);
                world_normal.W = 0F;
                return Vector4.Normalize(world_normal);
            }
            else
            {
                return Vector4.Normalize(point - Origin);
            }
        }


        #region Helper methods

        public void SetColor(Color color)
        {
            // For now, not adding this behavior to IBasicShape.
            // Unsure of how shape collections will be initialized and if changing the
            // Color post-instantiation is a real requirement.
            // So this method is just a helper.

            var m = Material;
            m.Color = color;
            Material = m;
        }


        private Ray GetTransformedRay(Ray ray, bool applyLocalTransformation)
        {
            if (!applyLocalTransformation)
            {
                return ray;
            }

            return new Ray
            {
                // invert:true passed to Execute, as we apply the inverse of the Sphere transform to the Ray.
                Origin = Transformation.Execute(ray.Origin, true),
                Direction = Transformation.Execute(ray.Direction, true)
            };
        }


        #endregion

    }
}
