using System;
using System.Numerics;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;

namespace Ray.Domain.Model
{
    public partial class Sphere : IBasicShape
    {
        private readonly bool _enforceAssumptions;

        public Sphere(Vector4 origin, Vector4 scale, bool enforceAssumptions)
        {
            Origin = origin;
            Scale = scale;
            _enforceAssumptions = enforceAssumptions;
        }

        public static Sphere CreateDefaultInstance(bool enforceAssumptions = true)
        {
            // As per text, considering simple unit spheres with centre at the origin,
            // and transform the rays instead.

            return new Sphere(
                new Vector4(0F, 0F, 0F, 1F),
                new Vector4(1F, 1F, 1F, 0F),
                enforceAssumptions);
        }

        public Vector4 Origin { get; set; }
        public Vector4 Scale { get; set; }

        public Material Material { get; set; } = Material.CreateDefaultInstance();

        /// <summary>
        /// As per text: Intersection calculations kept simple by modeling as unit
        /// spheres at the origin. This <see cref="Transformation"/> can be applied
        /// to scale and translate this sphere instance as appropriate.
        /// </summary>
        /// <remarks>
        /// We actually apply the transform to the ray, rather than the sphere.
        /// The ray ultimately determines the visual output, derived in no small
        /// part by the intersection calculations. So keep the spheres uniform -
        /// unit sphere @ origin, move them around world space with the transform,
        /// but actually apply this transform (inverse of) to the ray.
        /// </remarks>
        /// <seealso cref="GetIntersections"/>
        public IMatrixTransformationBuilder Transformation { get; set; } = new MatrixTransformationBuilder();

        public float Radius
        {
            get
            {
                if (!IsPerfectSphere)
                {
                    throw new ApplicationException("Non-uniform Sphere. Cannot assume simple radius");
                }

                return Scale.X;
            }
        }


        public bool IsPerfectSphere => (Scale.X + Scale.Y + Scale.Z).IsApproximately(3 * Scale.X);

        public bool IsAtAxisOrigin => (Origin.X + Origin.Y + Origin.Z).IsApproximately(0F);

    }
}
