using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;

namespace Ray.Domain.Model
{
    public class Sphere
    {
        public Sphere() : this(new Vector4(0F, 0F, 0F, 1F), 1F)
        {
            // As per text, considering simple unit spheres with centre at the origin, to begin with.
        }
        public Sphere(Vector4 origin, float radius)
        {
            this.Origin = origin;
            this.Radius = radius;
        }

        public Vector4 Origin { get; set; }
        public float Radius { get; set; }


        /* From this link: https://www.geeksforgeeks.org/check-whether-a-point-lies-inside-a-sphere-or-not/
         * - A point (x, y, z) is inside the sphere with center (cx, cy, cz) and radius r if:
         *     ( x-cx ) ^2 + (y-cy) ^2 + (z-cz) ^ 2 < r^2
         * - A point (x, y, z) lies on the sphere with center (cx, cy, cz) and radius r if:
         *     ( x-cx ) ^2 + (y-cy) ^2 + (z-cz) ^ 2 = r^2
         * - A point (x, y, z) is outside the sphere with center (cx, cy, cz) and radius r if:
         *     ( x-cx ) ^2 + (y-cy) ^2 + (z-cz) ^ 2 > r^2
         *
         * I'm getting away with just Inside or Outside and dropping On (for now anyway), as we're simulating
         * a moving ray and sphere intersections - we hold the state of position n-1 and n. In this model
         * tangential glances record 2 intersections at the same point. This is what the Ray Tracer text's
         * unit test assertions expect.
         */
        public bool IsInside(Vector4 point)
        {
            if (!point.IsPoint())
            {
                throw new ArgumentOutOfRangeException("point", "Not a valid point");
            }

            float result = MathF.Pow(point.X - Origin.X, 2F) +
                           MathF.Pow(point.Y - Origin.Y, 2F) +
                           MathF.Pow(point.Z - Origin.Z, 2F);
            return result <= MathF.Pow(Radius, 2F);
        }
    }
}
