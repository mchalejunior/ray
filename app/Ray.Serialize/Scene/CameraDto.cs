using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Serialize.Scene
{
    public class CameraDto
    {
        public int HSize { get; set; }
        public int VSize { get; set; }
        public float FieldOfView { get; set; }

        public VectorDto From { get; set; }
        public VectorDto To { get; set; }
        public VectorDto Up { get; set; }

    }
}
