using System.Numerics;

namespace Ray.Domain.Model
{
    public struct Ray
    {
        public Ray(Vector4 origin, Vector4 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector4 Origin;
        public Vector4 Direction;

        public Vector4 GetPosition(float t) // using "t" for distance, because in pure maths terms it's "time"
        {
            // For this Ray (starting position, direction, speed), what position are you at along the ray
            // after t seconds (or after distance t).

            return Origin + Direction * t;
        }
    }
}
