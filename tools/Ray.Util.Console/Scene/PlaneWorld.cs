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
using Plane = Ray.Domain.Model.Plane;

namespace Ray.Util.Console.Scene
{
    class PlaneWorld
    {
        public static void ReRenderSphereCentralWithPlanes(string outputBitmapFilePath)
        {

            // Define world

            var floor = new Plane();
            floor.UpdateColor(Color.FromScRgb(Material.DefaultColorA, 1F, 0.9F, 0.9F))
                 .UpdateSpecular(0F);

            var left_wall = new Plane();
            left_wall.Transformation = new MatrixTransformationBuilder()
                .RotateX(MathF.PI / 2)
                .RotateY(-MathF.PI / 4)
                .Translate(new Vector3(0F, 0F, 5F))
                ;
            left_wall.Material = floor.Material;

            var right_wall = new Plane();
            right_wall.Transformation = new MatrixTransformationBuilder()
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

            // Use low res until happy, then crank up. Takes a lot of clock cycles!
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            int high = 1000;
            int medium = 500;
            int low = 250;
            int res = medium;
#pragma warning restore CS0219 // Variable is assigned but its value is never used

            var camera = new Camera(res, res / 2, MathF.PI / 3);
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
                    Color color = Lighting.CalculateColorWithPhongReflection(world, ray);

                    canvas.SetPixel(x, y, color.Simplify(255));
                }
            }

            canvas.Save(outputBitmapFilePath);
        }

        
        public static void RenderSphereWithPlanesFromOverhead(string outputBitmapFilePath)
        {

            // Define world

            var floor = new Plane();
            floor.UpdateColor(Color.FromScRgb(Material.DefaultColorA, 1F, 0.9F, 0.9F))
                 .UpdateSpecular(0F);

            var left_wall = new Plane();
            left_wall.Transformation = new MatrixTransformationBuilder()
                .RotateX(MathF.PI / 2)
                .RotateY(-MathF.PI / 4)
                .Translate(new Vector3(0F, 0F, 5F))
                ;
            left_wall.Material = floor.Material;

            var right_wall = new Plane();
            right_wall.Transformation = new MatrixTransformationBuilder()
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

            // Use low res until happy, then crank up. Takes a lot of clock cycles!
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            int high = 1000;
            int medium = 500;
            int low = 250;
            int res = medium;
#pragma warning restore CS0219 // Variable is assigned but its value is never used

            var camera = new Camera(res, res / 2, MathF.PI / 3);
            camera.SetViewTransformation(
                from: new Vector4(0F,8F, -1F, 1F),
                to: new Vector4(0F, 0F, 1F, 1F),
                up: new Vector4(0F, 0F, 1F, 0F)
                );


            // Render image

            using var canvas = new Bitmap(camera.HorizontalSize, camera.VerticalSize);

            for (int y = 0; y < camera.VerticalSize - 1; y++)
            {
                for (int x = 0; x < camera.HorizontalSize - 1; x++)
                {
                    var ray = camera.GetRay(x, y);
                    Color color = Lighting.CalculateColorWithPhongReflection(world, ray);

                    canvas.SetPixel(x, y, color.Simplify(255));
                }
            }

            canvas.Save(outputBitmapFilePath);
        }

    }
}
