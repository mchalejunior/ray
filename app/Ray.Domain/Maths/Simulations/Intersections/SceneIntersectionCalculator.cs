using Ray.Domain.Extensions;
using Ray.Domain.Model;
using Ray.Domain.Transportation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ray.Domain.Maths.Simulations.Intersections
{
    [Obsolete(
        @"Replaced by domain model entities. 
            However, this class was important in arriving at final solution. 
            Still used by tests and demonstrates progression through the Ray Tracer Challenge textbook.")]
    public class SceneIntersectionCalculator
    {
        private readonly List<IBasicShape> _shapes;


        public SceneIntersectionCalculator(List<IBasicShape> shapes)
        {
            _shapes = shapes;
        }

        /// <summary>
        /// Calculate intersections with all shapes in the scene. Result includes all intersections
        /// for all shapes, with reference to the shape, ray and distanceT for each.
        /// </summary>
        public IEnumerable<IntersectionDto> CalculateIntersections(Model.Ray ray)
        {

            return from shape in _shapes
                   from intersection in shape.GetIntersections(ray)
                   select intersection;

        }

        /// <summary>
        /// Return only the first hit after calculating all
        /// possible intersections for all shapes in the scene. 
        /// </summary>
        /// <remarks>
        /// This method offers an efficiency compared to <see cref="CalculateIntersections"/>.
        /// However if you need the intersections as well, then use <see cref="CalculateIntersections"/>
        /// instead and call the extension method <see cref="IntersectionExtensionMethods.GetHit"/>.
        /// </remarks>
        public IntersectionDto CalculateHit(Model.Ray ray)
        {
            return (
                    from s in _shapes
                    from i in s.GetIntersections(ray)
                    where i.DistanceT > 0F
                    orderby i.DistanceT
                    select i
                ).FirstOrDefault();

        }


    }
}
