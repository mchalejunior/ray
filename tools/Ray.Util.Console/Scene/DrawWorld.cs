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
    class DrawWorld
    {
        public static void RenderSphereCentral(string outputBitmapFilePath)
        {

            // Define world

            var floor = Sphere.CreateDefaultInstance();
            floor.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(10F, 0.01F, 10F));
            floor.UpdateColor(Color.FromScRgb(Material.DefaultColorA, 1F, 0.9F, 0.9F))
                 .UpdateSpecular(0F);

            var left_wall = Sphere.CreateDefaultInstance();
            left_wall.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(10F, 0.01F, 10F))
                .RotateX(MathF.PI / 2)
                .RotateY(-MathF.PI / 4)
                .Translate(new Vector3(0F, 0F, 5F))
                ;
            left_wall.Material = floor.Material;

            var right_wall = Sphere.CreateDefaultInstance();
            right_wall.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(10F, 0.01F, 10F))
                .RotateX(MathF.PI / 2)
                .RotateY(MathF.PI / 4)
                .Translate(new Vector3(0F, 0F, 5F))
                ;
            right_wall.Material = floor.Material;

            var middle = Sphere.CreateDefaultInstance();
            middle.Transformation = new MatrixTransformationBuilder()
                .Translate(new Vector3(-0.5F, 1F, 0.5F));
            middle.UpdateColor(Color.FromScRgb(Material.DefaultColorA, 0.1F, 1F, 0.5F))
                  .UpdateDiffuse(0.7F)
                  .UpdateSpecular(0.3F);

            var right = Sphere.CreateDefaultInstance();
            right.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(0.5F, 0.5F, 0.5F))
                .Translate(new Vector3(1.5F, 0.5F, -0.5F))
                ;
            right.UpdateColor(Color.FromScRgb(Material.DefaultColorA, 0.5F, 1F, 0.1F))
                 .UpdateDiffuse(0.7F)
                 .UpdateSpecular(0.3F);

            var left = Sphere.CreateDefaultInstance();
            left.Transformation = new MatrixTransformationBuilder()
                .Scale(new Vector3(0.33F, 0.33F, 0.33F))
                .Translate(new Vector3(-1.5F, 0.33F, -0.75F))
                ;
            left.UpdateColor(Color.FromScRgb(Material.DefaultColorA, 1F, 0.8F, 0.1F))
                .UpdateDiffuse(0.7F)
                .UpdateSpecular(0.3F);

            var world = new World(new List<IBasicShape>
                {
                    floor, left_wall, right_wall,
                    left, middle, right
                }, 
                new Light
                {
                    Position = new Vector4(-10.0F, 10.0F, -10.0F, 1.0F),
                    Intensity = System.Windows.Media.Colors.White
                });

            var camera = new Camera(1000, 500, MathF.PI / 3);
            camera.SetViewTransformation(
                from: new Vector4(0F, 1.5F, -5F, 1F),
                to: new Vector4(0F, 1F, 0F, 1F),
                up: new Vector4(0F, 1F, 0F, 0F)
                );


            // Render image

            using var canvas = new Bitmap(camera.HorizontalSize, camera.VerticalSize);

            for (int y = 0; y < camera.VerticalSize - 1; y++)
            {
                for (int x = 0; x < camera.HorizontalSize - 1; x++)
                {
                    var ray = camera.GetRay(x, y);
                    var color = Lighting.CalculateColorWithPhongReflection(world, ray);

                    canvas.SetPixel(x, y, color.Simplify(255));
                }
            }

            canvas.Save(outputBitmapFilePath);
        }
    }
}
