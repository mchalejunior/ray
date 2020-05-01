using System.Numerics;

namespace Ray.Domain.Model
{
    public partial class Plane : BaseShape, IBasicShape
    {
        // Modeled as xz Plane (y=0) passing through the origin.
        private static readonly Vector4 ConstantNormal = new Vector4(0F, 1F, 0F, 0F);

    }
}
