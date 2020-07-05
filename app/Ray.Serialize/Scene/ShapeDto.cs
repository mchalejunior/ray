using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Serialize.Scene
{
    public class ShapeDto
    {
        public ShapeType Type { get; set; }
        public MaterialDto Material { get; set; }
        public List<TransformDto> Transformations { get; set; }



        public enum ShapeType
        {
            Sphere = 0,
            Plane
        }
    }
}
