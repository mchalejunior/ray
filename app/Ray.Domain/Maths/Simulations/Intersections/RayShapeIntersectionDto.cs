using System;

namespace Ray.Domain.Maths.Simulations.Intersections
{
    public class RayShapeIntersectionDto
    {
        public RayShapeSimulationState.IntersectionType IntersectionType { get; set; }
        public RayShapeSimulationState PreviousState { get; set; }
        public RayShapeSimulationState State { get; set; }

        public RayShapeSimulationState GetPreciseIntersectionPoint()
        {
            if (IntersectionType == RayShapeSimulationState.IntersectionType.None)
            {
                throw new ApplicationException("Unexpected usage: This DTO does not represent an Intersection.");
            }

            return  IntersectionType == RayShapeSimulationState.IntersectionType.FromOutsideToInside ? 
                State : 
                PreviousState;
        }

        /// <summary>
        /// Distance along ray where intersection occurred.
        /// </summary>
        /// <see cref="GetPreciseIntersectionPoint"/>
        public float IntersectionDistance => GetPreciseIntersectionPoint().Distance;
    }
}
