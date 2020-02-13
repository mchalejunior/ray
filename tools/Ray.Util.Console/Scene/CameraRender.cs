using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using Ray.Domain.Extensions;
using Ray.Domain.Maths;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Model;
using Color = System.Windows.Media.Color;

namespace Ray.Util.Console.Scene
{
    // The last unit test in the "Making a Scene" > "Implementing a Camera" section
    // is probably better represented as a little sample drawing app.
    // Create a larger version to make sure the logic is all correct.
    // As we "simplify" the color, the test wouldn't pass anyway, but this program
    // verifies the code and the test was very limited anyway (as called out in the text).

    public class CameraRender
    {
        public static void DrawDefaultWorld(string outputBitmapFilePath = null, Bitmap canvas = null)
        {
            var world = CreateDefaultWorld();
            var camera = new Camera(11, 11, MathF.PI / 2);

            var from = new Vector4(0F, 0F, -5F, 1F);
            var to = new Vector4(0F, 0F, 0F, 1F);
            var up = new Vector4(0F, 1F, 0F, 0F);

            camera.SetViewTransformation(from, to, up);

            bool shouldDispose = canvas == null;
            if (canvas == null)
            {
                canvas = new Bitmap(camera.HorizontalSize, camera.VerticalSize);
            }

            for (int y = 0; y < camera.VerticalSize -1; y++)
            {
                for (int x = 0; x < camera.HorizontalSize -1; x++)
                {
                    var ray = camera.GetRay(x, y);
                    var color = Lighting.CalculateColorWithPhongReflection(world, ray);

                    canvas.SetPixel(x, y, color.Simplify(255));
                }
            }

            if (outputBitmapFilePath != null)
            {
                canvas.Save(outputBitmapFilePath);
            }

            if (shouldDispose)
            {
                canvas.Dispose();
            }
        }

        public static void DrawDefaultWorldLarger(string outputBitmapFilePath = null, Bitmap canvas = null)
        {
            var world = CreateDefaultWorldLarger();
            var camera = new Camera(110, 110, MathF.PI / 2);

            // Pull back out a bit more to allow for larger sphere size.
            // Result kind of cool when you don't - you're seeing the inner circle,
            // with the silhouette of the outer circle.
            var from = new Vector4(0F, 0F, -10F, 1F); 
            var to = new Vector4(0F, 0F, 0F, 1F);
            var up = new Vector4(0F, 1F, 0F, 0F);

            camera.SetViewTransformation(from, to, up);

            bool shouldDispose = canvas == null;
            if (canvas == null)
            {
                canvas = new Bitmap(camera.HorizontalSize, camera.VerticalSize);
            }

            for (int y = 0; y < camera.VerticalSize - 1; y++)
            {
                for (int x = 0; x < camera.HorizontalSize - 1; x++)
                {
                    var ray = camera.GetRay(x, y);
                    var color = Lighting.CalculateColorWithPhongReflection(world, ray);

                    canvas.SetPixel(x, y, color.Simplify(255));
                }
            }

            if (outputBitmapFilePath != null)
            {
                canvas.Save(outputBitmapFilePath);
            }

            if (shouldDispose)
            {
                canvas.Dispose();
            }
        }

        private static World CreateDefaultWorld()
        {
            var outerSphere = Sphere.CreateDefaultInstance();
            var innerSphere = Sphere.CreateDefaultInstance();
            outerSphere.UpdateMaterial(
                Color.FromScRgb(Material.DefaultColorA, 0.8F, 1.0F, 0.6F),
                0.7F,
                0.2F,
                outerSphere.Material.Ambient,
                outerSphere.Material.Shininess
            );

            // As feature file: concentric circles with inner sphere scaled down.
            innerSphere.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(0.5F, 0.5F, 0.5F));

            return new World(
                new List<IBasicShape> { outerSphere, innerSphere },
                new Domain.Model.Light
                {
                    Position = new Vector4(-10.0F, 10.0F, -10.0F, 1.0F),
                    Intensity = System.Windows.Media.Colors.White
                }
            );
        }

        private static World CreateDefaultWorldLarger()
        {
            var outerSphere = Sphere.CreateDefaultInstance();
            var innerSphere = Sphere.CreateDefaultInstance();
            outerSphere.UpdateMaterial(
                Color.FromScRgb(Material.DefaultColorA, 0.8F, 1.0F, 0.6F),
                0.7F,
                0.2F,
                outerSphere.Material.Ambient,
                outerSphere.Material.Shininess
            );

            // As feature file: concentric circles with inner sphere scaled down.
            innerSphere.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(10.0F, 10.0F, 10.0F));
            innerSphere.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(5.0F, 5.0F, 5.0F));

            return new World(
                new List<IBasicShape> { outerSphere, innerSphere },
                new Domain.Model.Light
                {
                    Position = new Vector4(-30.0F, 30.0F, -30.0F, 1.0F),
                    Intensity = System.Windows.Media.Colors.White
                }
            );
        }
    }
}
