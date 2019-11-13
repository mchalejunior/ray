using System.Numerics;

namespace Ray.Domain.Model
{
    public class Ray
    {
        public Vector4 Origin { get; set; }
        public Vector4 Direction { get; set; }

        public Vector4 GetPosition(float t) // using "t" for distance, because in pure maths terms it's "time"
        {
            // For this Ray (starting position, direction, speed), what position are you at along the ray
            // after t seconds (or after distance t).

            return Origin + Direction * t;
        }
    }
}
