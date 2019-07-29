using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Util.Console.ProjectileGame
{
    class ProjectileRunner
    {
        public static void SimulateProjectileLaunch(Environment environment, Projectile launchSettings)
        {
            System.Console.WriteLine($@"Simulating projectile launch in environment. 
                Gravity: {environment.Gravity.X} {environment.Gravity.Y} {environment.Gravity.Z}. 
                Wind: {environment.Wind.X} {environment.Wind.Y} {environment.Wind.Z}."
            );

            var projectileCalculator = new ProjectileCalculator(environment, launchSettings);

            while (!projectileCalculator.HasLanded)
            {
                OutputProjectileStatus(projectileCalculator);
                projectileCalculator.Tick();
            }

        }

        private static void OutputProjectileStatus(ProjectileCalculator currentState)
        {
            System.Console.WriteLine($@"Increment state details. 
                Increment: {currentState.TickCount}.
                Landed: {currentState.HasLanded}.
                Position: {currentState.CurrentPosition.X} {currentState.CurrentPosition.Y} {currentState.CurrentPosition.Z}. 
                Velocity: {currentState.CurrentVelocity.X} {currentState.CurrentVelocity.Y} {currentState.CurrentVelocity.Z}."
            );
        }
    }
}
