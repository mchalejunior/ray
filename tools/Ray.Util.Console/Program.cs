using System;
using System.Numerics;
using Ray.Util.Console.ProjectileGame;
using Environment = Ray.Util.Console.ProjectileGame.Environment;

namespace Ray.Util.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProjectileSimulation();

            System.Console.WriteLine("Press return key to exit");
            System.Console.Read();
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
