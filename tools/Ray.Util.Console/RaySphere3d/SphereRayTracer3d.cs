using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Maths.Simulations.Intersections;
using Ray.Domain.Model;
using Ray.Domain.Transportation;
using Color = System.Windows.Media.Color;

namespace Ray.Util.Console.RaySphere3d
{
    class SphereRayTracer3d
    {

        // NOTE: Using the 2D version as starting place. Couple of additions should turn it into a glorious 3D render!

        public static void DrawSphere(string outputBitmapFilePath, IMatrixTransformationBuilder transformation)
        {
            // Rough and ready. Fully following the pseudocode from the text.
            // Basically:
            //   For each pixel on the canvas, figure out the ray (direction) from there
            //   back to the origin of the emitting ray (light source).
            //   Then calculate the hit (first intersection), if any. If it's a hit then
            //   that pixel on the canvas gets drawn.
            //   The rest of the code is just pixel conversion e.g. determining what "1"
            //   represents in terms of numbers of pixels and also converting the half above /
            //   half below of the sphere to a non-negative set of pixels for drawing.

            // inputs: could encapsulate in class to cleanup the code.
            var ray_origin = new Vector4(0F, 0F, -5F, 1F);
            var wall_z = 10F;
            var wall_size = 7.0F;

            var sphere = Sphere.CreateDefaultInstance();
            sphere.UpdateMaterial(
                Color.FromScRgb(Material.DefaultColorA, 1.0F, 0.2F, 1.0F),
                sphere.Material.Diffuse,
                sphere.Material.Specular,
                sphere.Material.Ambient,
                sphere.Material.Shininess
            );
            IBasicShape shape = sphere;
            shape.Transformation = transformation;

            var light = new Light
            {
                Position = new Vector3(-10F, 10F, -10F).AsPoint(),
                Intensity = Color.FromScRgb(Material.DefaultColorA, 1.0F, 1.0F, 1.0F)
            };

            var xs = new SceneIntersectionCalculator(new List<IBasicShape> { shape });
            var canvas_pixels = 100;
            var pixel_size = wall_size / canvas_pixels;
            var half = wall_size / 2;


            using var canvas = new Bitmap(canvas_pixels, canvas_pixels);

            for (int y = 0; y < canvas_pixels; y++)
            {
                var world_y = half - pixel_size * y;

                for (int x = 0; x < canvas_pixels; x++)
                {
                    var world_x = -half + pixel_size * x;

                    var position = new Vector4(world_x, world_y, wall_z, 1F);

                    var r = new Domain.Model.Ray(
                        ray_origin,
                        Vector4.Normalize(position - ray_origin));

                    var hit = xs.CalculateHit(r);
                    if (hit.HasValue)
                    {
                        var color = Lighting.CalculateColorWithPhongReflection(hit, light);

                        // keep the alpha value static, otherwise you get strange results!
                        canvas.SetPixel(x, y, color.Simplify(255));
                    }
                }
            }


            canvas.Save(outputBitmapFilePath);
        }
    }
}
