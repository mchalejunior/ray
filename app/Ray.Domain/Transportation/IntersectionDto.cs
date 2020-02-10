using System.Numerics;
using Ray.Domain.Model;

namespace Ray.Domain.Transportation
{
    public struct IntersectionDto
    {
        public IntersectionDto(Model.Ray ray, IBasicShape shape, float distanceT)
        {
            Ray = ray;
            Shape = shape;
            DistanceT = distanceT;
            TangentialIntersection = false;
            RayOrigin = RaysOrigin.Normal;
        }

        public Model.Ray Ray;
        public IBasicShape Shape;
        /// <summary>
        /// Distance t along <see cref="Ray"/> represented
        /// by this intersection of <see cref="Shape"/>.
        /// </summary>
        public float DistanceT;

        public Vector4 Position => Ray.GetPosition(DistanceT);
        /// <summary>
        /// Vector from the intersection back towards the eye/camera.
        /// </summary>
        public Vector4 EyeV => -Ray.Direction;
        /// <summary>
        /// Normal vector of reflection off intersection surface.
        /// </summary>
        public Vector4 NormalV => RayOriginatesInsideShape ?
            -Shape.GetNormal(Position) :
            Shape.GetNormal(Position);

        public bool HasValue => Shape != null;

        public bool TangentialIntersection { get; set; }
        public RaysOrigin RayOrigin { get; set; }
        public bool RayOriginatesInsideShape => RayOrigin == RaysOrigin.RayInsideShape;
        


        /// <summary>
        /// Relative information between the <see cref="Ray"/> and <see cref="Shape"/>.
        /// </summary>
        public enum RaysOrigin
        {
            /// <summary>
            /// Ray outside the shape and projected forwards before meeting the shape.
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Ray originated inside the shape.
            /// </summary>
            RayInsideShape = 1,
            /// <summary>
            /// Ray outside the shape, but a backwards projection was necessary to meet the shape.
            /// </summary>
            ShapeBehindRay = 2
        }
    }
}
