using Ray.Domain.Extensions;
using System;
using System.Numerics;

namespace Ray.Domain.Model
{
    public partial class Sphere : BaseShape, IBasicShape
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
