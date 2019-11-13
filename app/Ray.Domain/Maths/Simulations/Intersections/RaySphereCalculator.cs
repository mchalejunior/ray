using System;
using System.Collections.Generic;
using Ray.Domain.Model;

namespace Ray.Domain.Maths.Simulations.Intersections
{
    public class RaySphereCalculator
    {
        private readonly Model.Ray _ray;
        private readonly Sphere _sphere;
        private readonly float _radiusAwayFromSurfaceOfSphere;

        /// <summary>
        /// Details of where Ray intersects Sphere (if any intersections).
        /// <see cref="RunSimulation"/> must be called before querying this property, otherwise it will be empty.
        /// </summary>
        public IList<RaySphereIntersectionDto> Intersections { get; private set; } = new List<RaySphereIntersectionDto>();


        public RaySphereCalculator(Model.Ray ray, Sphere sphere)
        {
            _ray = ray;
            _sphere = sphere;

            _radiusAwayFromSurfaceOfSphere = 2 * MathF.Pow(_sphere.Radius, 2F);
        }

        public void RunSimulation()
        {
            Intersections = new List<RaySphereIntersectionDto>();
            // Backwards accounts for starting in the Sphere or in front of it.
            // Safe to run regardless of where start and efficient to exit early when appropriate.
            RunSimulationForwardsAndBackwards(1);  // Forwards
            RunSimulationForwardsAndBackwards(-1); // Backwards
        }

        private void RunSimulationForwardsAndBackwards(int positiveOrNegativeIncrement)
        {
            int count = 0;
            RaySphereSimulationState
                previousState = new RaySphereSimulationState(_ray, _sphere, count += positiveOrNegativeIncrement),
                currentState = new RaySphereSimulationState(_ray, _sphere, count += positiveOrNegativeIncrement);

            while (ShouldRayContinueToTravel(currentState))
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
                currentState = new RaySphereSimulationState(_ray, _sphere, count += positiveOrNegativeIncrement);
            }
        }

        private bool ShouldRayContinueToTravel(RaySphereSimulationState currentState)
        {
            // Originally just used currentState.IsRayPositionFurtherAwayThanStartingOrigin
            // But what if you started within the circle!? E.g. at the origin - so you're
            // immediately moving further away, but if you keep going you'll intersect.

            if (Intersections.Count >= 2)
            {
                // Already got max intersections for a straight line, so stop.
                return false;
            }

            if (!currentState.IsRayPositionFurtherAwayThanStartingPoint)
            {
                // Still moving closer than where started, so continue.
                return true;
            }

            if (currentState.IsInsideSphere)
            {
                // Still inside sphere. A straight line will eventually burst out, so continue.
                return true;
            }

            // Lastly, allow a bit of a cushion e.g. tangential glances fully accounted for.
            // Simple metric to use is - are we a full radius away from the sphere.
            if (_sphere.DistanceFromOrigin(currentState.Position) < _radiusAwayFromSurfaceOfSphere)
            {
                // Less than a radius away from the sphere, so continue.
                return true;
            }

            return false;
        }

    }
}
