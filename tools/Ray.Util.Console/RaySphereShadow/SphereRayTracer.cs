using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Maths.Simulations.Intersections;
using Ray.Domain.Model;
using Ray.Domain.Transportation;
using Color = System.Windows.Media.Color;

namespace Ray.Util.Console.RaySphereShadow
{
    class SphereRayTracer
    {
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
            IBasicShape shape = Sphere.CreateDefaultInstance();
            shape.Transformation = transformation;
#pragma warning disable CS0618 // Type or member is obsolete
            var xs = new SceneIntersectionCalculator(new List<IBasicShape> { shape });
#pragma warning restore CS0618 // Type or member is obsolete
            var canvas_pixels = 100;
            var pixel_size = wall_size / canvas_pixels;
            var half = wall_size / 2;
            var color = Color.FromScRgb(1.0F, 0.0F, 0.0F, 1.0F).Simplify();



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


                    if (xs.CalculateHit(r).HasValue)
                    {
                        canvas.SetPixel(x, y, color);
                    }
                }
            }


            canvas.Save(outputBitmapFilePath);
        }
    }
}
