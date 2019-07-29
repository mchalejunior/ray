using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;

namespace Ray.Util.Console.ProjectileGame
{
    class Environment
    {
        public Environment(Vector3 gravity, Vector3 wind)
        {
            Gravity = gravity.AsVector();
            Wind = wind.AsVector();
        }

        public Vector4 Gravity { get; }
        public Vector4 Wind { get; }
    }
}
