using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ray.Domain.Model;
using Ray.Serialize.Scene;

namespace Ray.Command.Scene.Factory
{
    class ShapeFactory
    {
        public static IBasicShape MapApiToDomain(ShapeDto input)
        {
            IBasicShape output = null;

            switch (input.Type)
            {
                case ShapeDto.ShapeType.Sphere:
                    // TODO: worth normalizing Shape instantiation e.g. new()
                    output = Sphere.CreateDefaultInstance();
                    break;
                case ShapeDto.ShapeType.Plane:
                    output = new Plane();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(input.Type), "Unrecognized shape type: " + input.Type);
            }

            if (input.Material != null)
            {
                output.Material = MaterialFactory.MapApiToDomain(input.Material);
            }

            if (input.Transformations != null && input.Transformations.Any())
            {
                foreach (var transform in input.Transformations)
                {
                    var domainTransformAction = TransformationFactory.MapApiTransformToDomainAction(transform);
                    domainTransformAction(output.Transformation);
                }
            }

            return output;
        }
    }
}
