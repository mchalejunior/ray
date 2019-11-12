using System;

namespace Ray.Domain.Maths.Simulations.Intersections
{
    public class RaySphereIntersectionDto
    {
        public RaySphereSimulationState.IntersectionType IntersectionType { get; set; }
        public RaySphereSimulationState PreviousState { get; set; }
        public RaySphereSimulationState State { get; set; }

        public RaySphereSimulationState GetPreciseIntersectionPoint()
        {
            if (IntersectionType == RaySphereSimulationState.IntersectionType.None)
            {
                throw new ApplicationException("Unexpected usage: This DTO does not represent an Intersection.");
            }

            return  IntersectionType == RaySphereSimulationState.IntersectionType.FromOutsideToInside ? 
                State : 
                PreviousState;
        }
            
    }
}
