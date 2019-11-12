using System;
using System.Numerics;
using Ray.Domain.Model;

namespace Ray.Domain.Maths.Simulations.Intersections
{
    public class RaySphereSimulationState
    {
        private readonly Model.Ray _ray;
        private readonly Sphere _sphere;
        private readonly int _step;

        private readonly float _distanceIncrement = 0.1F;
        private readonly float _initialDistanceAwayX, _initialDistanceAwayY, _initialDistanceAwayZ;

        public RaySphereSimulationState(Model.Ray ray, Sphere sphere, int step)
        {
            _ray = ray;
            _sphere = sphere;
            _step = step;

            _initialDistanceAwayX = Math.Abs(_sphere.Origin.X - _ray.Origin.X);
            _initialDistanceAwayY = Math.Abs(_sphere.Origin.Y - _ray.Origin.Y);
            _initialDistanceAwayZ = Math.Abs(_sphere.Origin.Z - _ray.Origin.Z);
        }

        public float Distance => _distanceIncrement * _step;
        public Vector4 Position => _ray.GetPosition(Distance);
        public bool IsInsideSphere => _sphere.IsInside(Position);

        public IntersectionType CheckForIntersection(RaySphereSimulationState previousState)
        {
            bool previousIsInsideVal = previousState.IsInsideSphere;
            bool thisIsInsideVal = IsInsideSphere;

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


        public bool IsRayPositionFurtherAwayThanStartingOrigin
        {
            get
            {
                // Further away if none of x, y or z getting closer.
                // Is NOT further away if is where started - so allow for this.
                var position = Position;

                if (position == _ray.Origin)
                {
                    return false;
                }

                float currentDistanceAwayX = Math.Abs(_sphere.Origin.X - position.X),
                      currentDistanceAwayY = Math.Abs(_sphere.Origin.Y - position.Y),
                      currentDistanceAwayZ = Math.Abs(_sphere.Origin.Z - position.Z);

                bool anyGettingCloser = currentDistanceAwayX < _initialDistanceAwayX ||
                                        currentDistanceAwayY < _initialDistanceAwayY ||
                                        currentDistanceAwayZ < _initialDistanceAwayZ;

                return !anyGettingCloser;
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
