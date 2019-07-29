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
            var env = new Environment(
                gravity: new Vector3(0.0F, -0.1F, 0.0F),
                wind: new Vector3(-0.01F, 0.0F, 0.0F)
            );
            var launchProjectile = new Projectile(
                position: new Vector3(0.0F, 1.0F, 0.0F),
                velocity: new Vector3(1.0F, 1.0F, 0.0F)
            );

            ProjectileRunner.SimulateProjectileLaunch(env, launchProjectile);
        }
    }
}
