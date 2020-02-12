using System.Numerics;

namespace Ray.Domain.Model
{
    public partial class Camera
    {
        // pixels.
        public int HorizontalSize { get; set; }
        public int VerticalSize { get; set; }
        
        // angle (radians e.g. pi/2).
        public float FieldOfView { get; set; }

        /// <summary>
        /// View transformation - how the world is oriented, relative to the camera.
        /// </summary>
        /// <remarks>
        /// Stored in Column Major Form.
        /// </remarks>
        /// <see cref="SetViewTransformation"/>
        public Matrix4x4 Transform { get; private set; } = Matrix4x4.Identity; // Identity RMF == CMF
    }
}
