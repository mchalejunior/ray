using System;
using System.Numerics;
using Ray.Domain.Maths.Factories;
using Ray.Util.Console.ClockDrawing;
using Ray.Util.Console.ProjectileGame;
using Ray.Util.Console.RaySphere3d;
using Ray.Util.Console.RaySphereShadow;
using Ray.Util.Console.Scene;
using Environment = Ray.Util.Console.ProjectileGame.Environment;

namespace Ray.Util.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Projectile start");
            RunProjectileSimulation();
            System.Console.WriteLine("Projectile end");
            System.Console.WriteLine("Clock start");
            RunClockDrawing();
            System.Console.WriteLine("Clock end");
            System.Console.WriteLine("2D Ray start");
            RunSphereRayTracer();
            System.Console.WriteLine("2D Ray end");
            System.Console.WriteLine("3D Ray start");
            RunSphereRayTracer3d();
            System.Console.WriteLine("3D Ray end");
            System.Console.WriteLine("Camera render start");
            RunCameraRenderer();
            System.Console.WriteLine("Camera render end");
            System.Console.WriteLine("World render start");
            RunWorldRenderer(false);
            System.Console.WriteLine("World render end");
            System.Console.WriteLine("World render (with acne) start");
            RunWorldRenderer(true);
            System.Console.WriteLine("World render (with acne) end");

            System.Console.WriteLine("Files at C:\\temp\\ray");
            System.Console.WriteLine("Press return key to exit");
            System.Console.Read();
        }

        private static void RunWorldRenderer(bool useAcneEffect)
        {
            DrawWorld.RenderSphereCentral(
                $"c:\\temp\\ray\\sphere-central-{(useAcneEffect ? "acne" : "clear")}.bmp",
                useAcneEffect);
        }

        private static void RunCameraRenderer()
        {
            CameraRender.DrawDefaultWorld("c:\\temp\\ray\\default-world.bmp");
            CameraRender.DrawDefaultWorldLarger("c:\\temp\\ray\\default-world-larger.bmp");
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
