using System;
using System.Drawing;
using Ray.Domain.Extensions;
using Color = System.Windows.Media.Color;

namespace Ray.Util.Console.ProjectileGame
{
    class ProjectileRunner
    {
        public static void SimulateProjectileLaunch(Environment environment, Projectile launchSettings,
            string outputBitmapFilePath)
        {
            System.Console.WriteLine($@"Simulating projectile launch in environment. 
                Gravity: {environment.Gravity.X} {environment.Gravity.Y} {environment.Gravity.Z}. 
                Wind: {environment.Wind.X} {environment.Wind.Y} {environment.Wind.Z}."
            );

            using var canvas = new Bitmap(50, 50);
            var projectileCalculator = new ProjectileCalculator(environment, launchSettings);

            while (!projectileCalculator.HasLanded)
            {
                OutputProjectileStatus(projectileCalculator);
                DrawProjectilePath(projectileCalculator, canvas);
                projectileCalculator.Tick();
            }
            OutputProjectileStatus(projectileCalculator);
            DrawProjectilePath(projectileCalculator, canvas);
            projectileCalculator.Tick();

            // NOTE: 0,0 is top-left of canvas (standard computer image processing), but is
            // bottom-left in the real world! Book suggests subtracting y from canvas height.
            // Instead, let's leave it and then just flip the image.
            canvas.RotateFlip(RotateFlipType.Rotate180FlipX);
            canvas.Save(outputBitmapFilePath);
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

        private static void DrawProjectilePath(ProjectileCalculator currentState, Bitmap canvas)
        {
            var roundX = Math.Round(currentState.CurrentPosition.X, 0);
            var roundY = Math.Round(currentState.CurrentPosition.Y, 0);

            // UPDATE!! TODO: The "WPF way" is insanely difficult. Basically it's not designed for direct pixel manipulation.
            // So I'm now giving serious consideration to just converting from "WPF color" to System.Drawing.Color and using
            // System.Drawing.Bitmap for pixel manipulation. 

            var wpfColor = Color.FromScRgb(1.0F, 0.0F, 0.0F, 1.0F);
            canvas.SetPixel((int)roundX, (int)roundY, wpfColor.Simplify());
        }


    }
}
