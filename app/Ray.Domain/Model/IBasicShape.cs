using System.Collections.Generic;
using System.Numerics;
using Ray.Domain.Transportation;

namespace Ray.Domain.Model
{
    public interface IBasicShape
    {
        Matrix4x4 Transformation { get; set; }

        IEnumerable<IntersectionDto> GetIntersections(Ray ray, bool applyLocalTransformation = true);

    }
}
