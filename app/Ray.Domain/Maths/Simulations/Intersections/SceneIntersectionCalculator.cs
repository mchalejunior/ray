using System;
using System.Collections.Generic;
using System.Linq;
using Ray.Domain.Model;

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
        public IList<RayShapeIntersectionDto> Intersections { get; private set; } = new List<RayShapeIntersectionDto>();

        /// <summary>
        /// The Hit is the first thing the Ray hits. So return the first non-zero (in front of ray) intersection.
        /// </summary>
        public RayShapeSimulationState Hit => (
            from i in Intersections
            where i.IntersectionDistance > 0F
            orderby i.IntersectionDistance
            select i.GetPreciseIntersectionPoint()
        ).FirstOrDefault();


        public SceneIntersectionCalculator(Model.Ray ray, List<IBasicShape> shapes)
        {
            _ray = ray;
            _shapes = shapes;
        }

        public void RunSimulation()
        {
            Intersections = new List<RayShapeIntersectionDto>();

            RunSimulationForwardsAndBackwards(1);  // Forwards
            RunSimulationForwardsAndBackwards(-1); // Backwards
        }

        private void RunSimulationForwardsAndBackwards(int positiveOrNegativeIncrement)
        {
            // NOTE: Refactored almost as-is to move from *single* ray *sphere* calculator
            // to an arbitrary number of *shapes*, calculating *intersections*, for given
            // ray, on all shapes in the *scene*.
            // TODO: This algorithm may need to be optimized. Consider the Flyweight pattern?
            // Also, anything can do to configure the increment to t (distance - default is 0.1)
            // or to configure and limit the canvas size in ShouldRayContinueToTravel could have
            // a dramatic effect on the number of cycles that need to be performed.
            // Parallel ForEach also an option if needed.

            int count = 0;
            do
            {
                int previousDistance = count;
                int currentDistance = previousDistance + positiveOrNegativeIncrement;
                _shapes.ForEach(s =>
                {
                    var previousState = new RayShapeSimulationState(_ray, s, previousDistance);
                    var currentState = new RayShapeSimulationState(_ray, s, currentDistance);

                    var checkIntersection = currentState.CheckForIntersection(previousState);
                    if (checkIntersection == RayShapeSimulationState.IntersectionType.None)
                    {
                        return;
                    }

                    Intersections.Add(new RayShapeIntersectionDto
                    {
                        IntersectionType = checkIntersection,
                        PreviousState = previousState,
                        State = currentState
                    });
                });

                count += positiveOrNegativeIncrement;
            }
            while (ShouldRayContinueToTravel(count));
        }


        private bool ShouldRayContinueToTravel(int step)
        {
            // TODO: This will be based on Distance / Canvas size eventually.
            // In process of a refactor to allow a Ray traveling through the scene to report
            // on intersections with an arbitrary number and type of objects.

            return Math.Abs(step) < 100;
        }

    }
}
