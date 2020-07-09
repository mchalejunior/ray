using System.Numerics;

namespace Ray.Domain.Model
{
    public class Light
    {
        // TODO: DBC for W = 1.0F ?
        public Vector4 Position { get; set; }
        public System.Windows.Media.Color Intensity { get; set; }
    }
}
