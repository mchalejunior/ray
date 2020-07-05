using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Serialize.Scene
{
    public class SceneDto
    {
        public List<ShapeDto> Shapes { get; set; }
        public LightDto LightSource { get; set; }

        public CameraDto Camera { get; set; }
    }
}
