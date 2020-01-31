using System;
using System.Numerics;
using Ray.Domain.Maths.Factories;
using Ray.Util.Console.ClockDrawing;
using Ray.Util.Console.ProjectileGame;
using Ray.Util.Console.RaySphere3d;
using Ray.Util.Console.RaySphereShadow;
using Environment = Ray.Util.Console.ProjectileGame.Environment;

namespace Ray.Util.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProjectileSimulation();
            RunClockDrawing();
            RunSphereRayTracer();
            RunSphereRayTracer3d();


            System.Console.WriteLine("Press return key to exit");
            System.Console.Read();
        }

        static void RunSphereRayTracer3d()
        {
            SphereRayTracer3d.DrawSphere("c:\\temp\\ray\\sphere3d-rt1.bmp", new MatrixTransformationBuilder());

            // shrink and skew
            SphereRayTracer3d.DrawSphere("c:\\temp\\ray\\sphere3d-rt2.bmp", new MatrixTransformationBuilder()
                .Scale(new Vector3(0.5F, 1F, 1F))
                .Shear(1F, 0F, 0F, 0F, 0F, 0F));

            // shrink and rotate - presumably rotate does nothing here!
            SphereRayTracer3d.DrawSphere("c:\\temp\\ray\\sphere3d-rt3.bmp", new MatrixTransformationBuilder()
                .Scale(new Vector3(1F, 0.5F, 1F))
                .RotateX(MathF.PI / 4));
        }

        static void RunSphereRayTracer()
        {

            SphereRayTracer.DrawSphere("c:\\temp\\ray\\sphere-rt1.bmp", new MatrixTransformationBuilder());

            // shrink and skew
            SphereRayTracer.DrawSphere("c:\\temp\\ray\\sphere-rt2.bmp", new MatrixTransformationBuilder()
                    .Scale(new Vector3(0.5F, 1F, 1F))
                    .Shear(1F, 0F, 0F, 0F, 0F, 0F));

            // shrink and rotate - presumably rotate does nothing here!
            SphereRayTracer.DrawSphere("c:\\temp\\ray\\sphere-rt3.bmp", new MatrixTransformationBuilder()
                    .Scale(new Vector3(1F, 0.5F, 1F))
                    .RotateX(MathF.PI / 4));
        }

        static void RunClockDrawing()
        {
            ClockRunner.DrawClock("c:\\temp\\ray\\clock1.bmp");
        }

        static void RunProjectileSimulation()
        {
            var env1 = new Environment(
                gravity: new Vector3(0.0F, -0.1F, 0.0F),
                wind: new Vector3(-0.01F, 0.0F, 0.0F)
            );
            var launchProjectile1 = new Projectile(
                position: new Vector3(0.0F, 0.0F, 0.0F),
                velocity: new Vector3(1.0F, 4.0F, 0.0F)
            );

            ProjectileRunner.SimulateProjectileLaunch(env1, launchProjectile1, 
                "c:\\temp\\ray\\projectile1.bmp");

            var env2 = new Environment(
                gravity: new Vector3(0.0F, -0.1F, 0.0F),
                wind: new Vector3(0.01F, 0.0F, 0.0F)
            );
            var launchProjectile2 = new Projectile(
                position: new Vector3(0.0F, 10.0F, 0.0F),
                velocity: new Vector3(2.0F, 1.0F, 0.0F)
            );

            ProjectileRunner.SimulateProjectileLaunch(env2, launchProjectile2,
                "c:\\temp\\ray\\projectile2.bmp");
        }
    }
}
