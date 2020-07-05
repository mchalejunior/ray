using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Serialize.Scene
{
    public class LightDto
    {
        public VectorDto Position { get; set; }
        public ColorDto Intensity { get; set; }
    }
}
