using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Serialize.Scene
{
    public class MaterialDto
    {
        public ColorDto Color { get; set; }

        public float? Ambient { get; set; }
        public float? Diffuse { get; set; }
        public float? Specular { get; set; }
        public float? Shininess { get; set; }
    }
}
