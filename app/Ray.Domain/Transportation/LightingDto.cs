using Ray.Domain.Model;

namespace Ray.Domain.Transportation
{
    public struct LightingDto
    {
        public IntersectionDto Hit;
        public Color Color;
        public bool IsInShadow;

        public static implicit operator Color(LightingDto dto) => dto.Color;
    }
}
