using System;
using System.Numerics;
using Ray.Domain.Model;

namespace Ray.Domain.Maths.Simulations.Intersections
{
    public class RayShapeSimulationState
    {
        private readonly Model.Ray _ray;
        public IBasicShape Shape { get; private set; }
        private readonly int _step;

        private readonly float _distanceIncrement = 0.1F;

        public RayShapeSimulationState(Model.Ray ray, IBasicShape shape, int step)
        {
            _ray = ray;
            Shape = shape;
            _step = step;
        }

        public float Distance => _distanceIncrement * _step;
        public Vector4 Position => _ray.GetPosition(Distance);
        public bool IsInsideShape => Shape.IsInside(Position);


        public IntersectionType CheckForIntersection(RayShapeSimulationState previousState)
        {
            bool previousIsInsideVal = previousState.IsInsideShape;
            bool thisIsInsideVal = IsInsideShape;

            if (previousIsInsideVal == thisIsInsideVal)
            {
                return IntersectionType.None;
            }

            // They're not equal, so there's an intersection of one type or another.
            if (previousIsInsideVal)
            {
                return IntersectionType.FromInsideToOutside;
            }
            else
            {
                return IntersectionType.FromOutsideToInside;
            }
        }



        public enum IntersectionType
        {
            None = 0,
            FromOutsideToInside,
            FromInsideToOutside
        }
    }
}
