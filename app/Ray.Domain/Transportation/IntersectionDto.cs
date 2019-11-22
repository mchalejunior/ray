using Ray.Domain.Model;

namespace Ray.Domain.Transportation
{
    public class IntersectionDto
    {
        public Model.Ray Ray { get; set; }
        public IBasicShape Shape { get; set; }
        /// <summary>
        /// Distance t along <see cref="Ray"/> represented
        /// by this intersection of <see cref="Shape"/>.
        /// </summary>
        public float DistanceT { get; set; }
    }
}
