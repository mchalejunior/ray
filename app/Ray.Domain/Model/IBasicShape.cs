using System.Collections.Generic;
using System.Numerics;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Transportation;

namespace Ray.Domain.Model
{
    public interface IBasicShape
    {
        Material Material { get; set; }

        IMatrixTransformationBuilder Transformation { get; set; }

        IEnumerable<IntersectionDto> GetIntersections(Ray ray, bool applyLocalTransformation = true);

        Vector4 GetNormal(Vector4 point, bool applyLocalTransformation = true);

    }
}
