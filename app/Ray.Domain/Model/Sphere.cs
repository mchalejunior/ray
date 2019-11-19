using System;
using System.Numerics;
using Ray.Domain.Extensions;

namespace Ray.Domain.Model
{
    public partial class Sphere : IBasicShape
    {
        public Sphere() : this(new Vector4(0F, 0F, 0F, 1F), new Vector4(1F, 1F, 1F, 0F))
        {
            // As per text, considering simple unit spheres with centre at the origin, to begin with.
        }
        public Sphere(Vector4 origin, Vector4 scale)
        {
            this.Origin = origin;
            this.Scale = scale;
        }

        public Vector4 Origin { get; set; }
        public Vector4 Scale { get; set; }

        public float Radius
        {
            get
            {
                var isPerfectSphere = (Scale.X + Scale.Y + Scale.Z).IsApproximately(3 * Scale.X);
                if (!isPerfectSphere)
                {
                    throw new ApplicationException("Non-uniform Sphere. Cannot assume simple radius");
                }

                return Scale.X;
            }
        }

        /// <summary>
        /// As per text: Intersection calculations kept simple by modeling as unit
        /// spheres at the origin. This <see cref="Transformation"/> can be applied
        /// to scale and translate the sphere as appropriate.
        /// </summary>
        public Matrix4x4 Transformation { get; set; } = Matrix4x4.Identity;
        
    }
}
