using System.Collections.Generic;
using System.Numerics;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Transportation;

namespace Ray.Domain.Model
{
    public interface IBasicShape
    {
        IMatrixTransformationBuilder Transformation { get; set; }

        IEnumerable<IntersectionDto> GetIntersections(Ray ray, bool applyLocalTransformation = true);

    }
}
