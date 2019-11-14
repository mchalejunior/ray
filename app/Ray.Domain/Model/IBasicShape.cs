using System.Numerics;

namespace Ray.Domain.Model
{
    public interface IBasicShape
    {
        /// <summary>
        /// Returns true if specified point is determined to be inside this shape.
        /// </summary>
        /// <remarks>
        /// "On" is considered to be "Inside". So only a determination of
        /// "Outside" will return false from this method.
        /// </remarks>
        bool IsInside(Vector4 point);
    }
}
