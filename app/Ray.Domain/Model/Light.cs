using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using System.Windows.Media;

namespace Ray.Domain.Model
{
    public class Light
    {
        // TODO: DBC for W = 1.0F ?
        public Vector4 Position { get; set; }
        public Color Intensity { get; set; }
    }
}
