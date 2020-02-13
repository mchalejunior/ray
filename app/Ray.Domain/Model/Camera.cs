using System;
using System.Numerics;

namespace Ray.Domain.Model
{
    public partial class Camera
    {
        /// <summary>
        /// Construct camera instance.
        /// This will trigger half size and pixel size calculations.
        /// </summary>
        /// <param name="hsize">Pixels</param>
        /// <param name="vsize">Pixels</param>
        /// <param name="fieldOfView">Radians e.g. Pi / 2</param>
        public Camera(int hsize, int vsize, float fieldOfView)
        {
            HorizontalSize = hsize;
            VerticalSize = vsize;
            FieldOfView = fieldOfView;

            // Once-off calculations
            float half_view = MathF.Tan(FieldOfView / 2.0F);
            float aspect = (float)HorizontalSize / (float)VerticalSize;

            if (aspect >= 1)
            {
                HalfWidth = half_view;
                HalfHeight = half_view / aspect;
            }
            else
            {
                HalfWidth = half_view * aspect;
                HalfHeight = half_view;
            }

            PixelSize = (HalfWidth * 2.0F) / HorizontalSize;
        }

        // pixels.
        public int HorizontalSize { get; }
        public int VerticalSize { get; }
        
        // angle (radians e.g. pi/2).
        public float FieldOfView { get; }

        public float HalfWidth { get; }
        public float HalfHeight { get; }
        public float PixelSize { get; }

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
