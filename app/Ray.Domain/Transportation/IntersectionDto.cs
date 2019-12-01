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
        }

        public Model.Ray Ray;
        public IBasicShape Shape;
        /// <summary>
        /// Distance t along <see cref="Ray"/> represented
        /// by this intersection of <see cref="Shape"/>.
        /// </summary>
        public float DistanceT;

        public bool HasValue => Shape != null;
    }
}
