using System;
using System.Collections.Generic;
using System.Linq;
using Ray.Domain.Model;
using Ray.Domain.Transportation;

namespace Ray.Domain.Maths.Simulations.Intersections
{
    public class SceneIntersectionCalculator
    {
        private readonly Model.Ray _ray;
        private readonly List<IBasicShape> _shapes;

        /// <summary>
        /// Details of where Ray intersects the scene's Shapes (if any intersections).
        /// <see cref="RunSimulation"/> must be called before querying this property, otherwise it will be empty.
        /// </summary>
        public List<IntersectionDto> Intersections { get; private set; } = new List<IntersectionDto>();

        /// <summary>
        /// The Hit is the first thing the Ray hits. So return the first non-zero (in front of ray) intersection.
        /// </summary>
        public IntersectionDto Hit => (
            from i in Intersections
            where i.DistanceT > 0F
            orderby i.DistanceT
            select i
        ).FirstOrDefault();


        public SceneIntersectionCalculator(Model.Ray ray, List<IBasicShape> shapes)
        {
            _ray = ray;
            _shapes = shapes;
        }

        public void RunSimulation()
        {
            Intersections = new List<IntersectionDto>();

            _shapes.ForEach(x => Intersections.AddRange(x.GetIntersections(_ray)));

        }


    }
}
