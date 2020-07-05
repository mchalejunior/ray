using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Serialize.Scene
{
    public class ShearTransformationDto
    {
        public float X2Y { get; set; }
        public float X2Z { get; set; }
        public float Y2X { get; set; }
        public float Y2Z { get; set; }
        public float Z2X { get; set; }
        public float Z2Y { get; set; }
    }
}
