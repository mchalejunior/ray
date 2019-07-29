using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ray.Util.Console.ProjectileGame
{
    class ProjectileCalculator
    {
        private readonly Environment _environment;

        public ProjectileCalculator(Environment environment, Projectile launchSettings)
        {
            _environment = environment;

            CurrentPosition = launchSettings.Position;
            
            // Grab the unit vector for velocity - we'll say that after one tick, the position is affected by the unit velocity. 
            CurrentVelocity = Vector4.Normalize(launchSettings.Velocity);
        }

        public Vector4 CurrentPosition { get; private set; }
        public Vector4 CurrentVelocity { get; private set; }

        public bool HasLanded => CurrentPosition.Y <= 0.0F;

        public int TickCount { get; private set; }

        public void Tick()
        {
            TickCount++;
            var newPosition = CurrentPosition + CurrentVelocity;
            var newVelocity = CurrentVelocity + _environment.Gravity + _environment.Wind;

            CurrentPosition = newPosition;
            // Again, always normalizing this.CurrentVelocity for use in unit of time calculations.
            CurrentVelocity = Vector4.Normalize(newVelocity);
        }

    }
}
