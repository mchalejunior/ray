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

        public bool IsInside(Vector4 point)
        {
            if (!point.IsPoint())
            {
                throw new ArgumentOutOfRangeException("point", "Not a valid point");
            }

            float minX = Origin.X - Radius, maxX = Origin.X + Radius,
                minY = Origin.Y - Radius, maxY = Origin.Y + Radius,
                minZ = Origin.Z - Radius, maxZ = Origin.Z + Radius;

            return point.X >= minX && point.X <= maxX &&
                   point.Y >= minY && point.Y <= maxY &&
                   point.Z >= minZ && point.Z <= maxZ;
        }
    }
}
