using System.Collections.Generic;
using Ray.Domain.Model;

namespace Ray.Domain.Maths.Simulations.Intersections
{
    public class RaySphereCalculator
    {
        private readonly Model.Ray _ray;
        private readonly Sphere _sphere;

        /// <summary>
        /// Details of where Ray intersects Sphere (if any intersections).
        /// <see cref="RunSimulation"/> must be called before querying this property, otherwise it will be empty.
        /// </summary>
        public IList<RaySphereIntersectionDto> Intersections { get; private set; } = new List<RaySphereIntersectionDto>();


        public RaySphereCalculator(Model.Ray ray, Sphere sphere)
        {
            _ray = ray;
            _sphere = sphere;
        }

        public void RunSimulation()
        {
            Intersections = new List<RaySphereIntersectionDto>();
            int count = 0;
            RaySphereSimulationState 
                previousState = new RaySphereSimulationState(_ray, _sphere, count++),
                currentState = new RaySphereSimulationState(_ray, _sphere, count++);

            while (!currentState.IsRayPositionFurtherAwayThanStartingOrigin)
            {
                var checkIntersection = currentState.CheckForIntersection(previousState);
                if (checkIntersection != RaySphereSimulationState.IntersectionType.None)
                {
                    Intersections.Add(new RaySphereIntersectionDto
                    {
                        IntersectionType = checkIntersection,
                        PreviousState = previousState,
                        State = currentState
                    });
                }

                previousState = currentState;
                currentState = new RaySphereSimulationState(_ray, _sphere, count++);
            }
        }

    }
}
