using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;

namespace Ray.Util.Console.ProjectileGame
{
    class Projectile
    {
        public Projectile(Vector3 position, Vector3 velocity) : this(position.AsPoint(), velocity.AsVector())
        {
        }

        public Projectile(Vector4 position, Vector4 velocity)
        {
            // TODO: Any out-of-box DBC ?
            if (!position.IsPoint())
            {
                throw new ArgumentOutOfRangeException("position", "Position must be a Point with w=1");
            }
            if (!velocity.IsVector())
            {
                throw new ArgumentOutOfRangeException("velocity", "Velocity must be a Vector with w=0");
            }

            Position = position;
            Velocity = velocity;
        }


        public Vector4 Position { get; }
        public Vector4 Velocity { get; }
    }
}
