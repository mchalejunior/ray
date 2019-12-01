using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ray.Domain.Transportation;

namespace Ray.Domain.Extensions
{
    public static class IntersectionExtensionMethods
    {
        public static IntersectionDto GetHit(this IEnumerable<IntersectionDto> intersections)
        {
            return (
                from i in intersections
                where i.DistanceT > 0F
                orderby i.DistanceT
                select i
            ).FirstOrDefault();
        }
    }
}
