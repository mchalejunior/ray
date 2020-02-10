using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Domain.Model
{
    public partial class World
    {
        public List<IBasicShape> Shapes { get; }
        public Light LightSource { get; }

        public World(List<IBasicShape> shapes, Light lightSource)
        {
            // NOTE: From text:
            // Supporting a single light source only. It's not terribly difficult to support
            // more than one. You need the "calculate color" routine to iterate over all
            // light sources and add the resultant colors together.

            Shapes = shapes;
            LightSource = lightSource;
        }
    }
}
