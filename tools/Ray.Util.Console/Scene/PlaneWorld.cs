using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ray.Domain.Extensions;
using Ray.Domain.Maths;
using Ray.Domain.Maths.Factories;
using Ray.Domain.Model;
using Ray.Serialize.Scene;
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
                    Intensity = Color.White
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

            using var canvas = new System.Drawing.Bitmap(camera.HorizontalSize, camera.VerticalSize);

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
                    Intensity = Color.White
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

            using var canvas = new System.Drawing.Bitmap(camera.HorizontalSize, camera.VerticalSize);

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



        #region Serializaion / API


        // Background: Would like to be able to document the use of this Ray Tracer app imperatively.
        //    Best way to do this, I feel, is:
        //    - Use Docker to virtualize the run-time environment.
        //    - Stand-up an API (Json Rest) that will exercise the Ray Tracer like calling directly from code.
        //
        // Now documentation can be as simple as:
        //    - Docker build command.
        //    - Docker run command (or potentially Docker-compose).
        //    - Fire this Json at an endpoint to generate an image.


        public static string SerializeSphereCentralWithPlanesAsJson()
        {
            // Templates
            var defaultSphere = Sphere.CreateDefaultInstance();
            var defaultMaterial = Material.CreateDefaultInstance();
            var white = Color.White;

            // Copying ReRenderSphereCentralWithPlanes code using serializable DTOs.

            // Planes

            var floor = new ShapeDto
            {
                Type = ShapeDto.ShapeType.Plane,
                Material = new MaterialDto
                {
                    Color = new ColorDto
                    {
                        A = Material.DefaultColorA,
                        R = 1F,
                        G = 0.9F,
                        B = 0.9F
                    },
                    Specular = 0F,
                    Ambient = defaultMaterial.Ambient,
                    Diffuse = defaultMaterial.Diffuse,
                    Shininess = defaultMaterial.Shininess
                }
            };

            var left_wall = new ShapeDto
            {
                Type = ShapeDto.ShapeType.Plane,
                Material = floor.Material,
                Transformations = new List<TransformDto>
                {
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.RotateX,
                        RotationRadians = MathF.PI / 2
                    },
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.RotateY,
                        RotationRadians = -MathF.PI / 4
                    },
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.Translate,
                        VectorTransformation = new VectorDto
                        {
                            X = 0F, Y = 0F, Z = 5F
                        }
                    }
                }
            };

            var right_wall = new ShapeDto
            {
                Type = ShapeDto.ShapeType.Plane,
                Material = floor.Material,
                Transformations = new List<TransformDto>
                {
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.RotateX,
                        RotationRadians = MathF.PI / 2
                    },
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.RotateY,
                        RotationRadians = MathF.PI / 4
                    },
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.Translate,
                        VectorTransformation = new VectorDto
                        {
                            X = 0F, Y = 0F, Z = 5F
                        }
                    }
                }
            };


            // Spheres

            var middle = new ShapeDto
            {
                Type = ShapeDto.ShapeType.Sphere,
                Material = new MaterialDto
                {
                    Color = new ColorDto
                    {
                        A = Material.DefaultColorA,
                        R = 0.1F,
                        G = 1F,
                        B = 0.5F
                    },
                    Diffuse = 0.7F,
                    Specular = 0.3F,
                    Ambient = defaultMaterial.Ambient,
                    Shininess = defaultMaterial.Shininess
                },
                Transformations = new List<TransformDto>
                {
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.Translate,
                        VectorTransformation = new VectorDto
                        {
                            X = -0.5F, Y = 1F, Z = 0.5F
                        }
                    }
                }
            };

            var right = new ShapeDto
            {
                Type = ShapeDto.ShapeType.Sphere,
                Material = new MaterialDto
                {
                    Color = new ColorDto
                    {
                        A = Material.DefaultColorA,
                        R = 0.5F,
                        G = 1F,
                        B = 0.1F
                    },
                    Diffuse = 0.7F,
                    Specular = 0.3F,
                    Ambient = defaultMaterial.Ambient,
                    Shininess = defaultMaterial.Shininess
                },
                Transformations = new List<TransformDto>
                {
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.Scale,
                        VectorTransformation = new VectorDto
                        {
                            X = 0.5F, Y = 0.5F, Z = 0.5F
                        }
                    },
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.Translate,
                        VectorTransformation = new VectorDto
                        {
                            X = 1.5F, Y = 0.5F, Z = -0.5F
                        }
                    }
                }
            };

            var left = new ShapeDto
            {
                Type = ShapeDto.ShapeType.Sphere,
                Material = new MaterialDto
                {
                    Color = new ColorDto
                    {
                        A = Material.DefaultColorA,
                        R = 1F,
                        G = 0.8F,
                        B = 0.1F
                    },
                    Diffuse = 0.7F,
                    Specular = 0.3F,
                    Ambient = defaultMaterial.Ambient,
                    Shininess = defaultMaterial.Shininess
                },
                Transformations = new List<TransformDto>
                {
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.Scale,
                        VectorTransformation = new VectorDto
                        {
                            X = 0.33F, Y = 0.33F, Z = 0.33F
                        }
                    },
                    new TransformDto
                    {
                        TransformType = TransformDto.TransformationType.Translate,
                        VectorTransformation = new VectorDto
                        {
                            X = -1.5F, Y = 0.33F, Z = -0.75F
                        }
                    }
                }
            };


            // Scene - pull together shapes into root serializable object

            // Use low res until happy, then crank up. Takes a lot of clock cycles!
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            int high = 1000;
            int medium = 500;
            int low = 250;
            int res = low;
#pragma warning restore CS0219 // Variable is assigned but its value is never used

            var scene = new SceneDto
            {
                Shapes = new List<ShapeDto>
                {
                    floor, left_wall, right_wall,
                    left, middle, right
                },
                LightSource = new LightDto
                {
                    Position = new VectorDto
                    {
                        X = -10F,
                        Y = 10F,
                        Z = -10F
                    },
                    Intensity = new ColorDto
                    {
                        A = white.ScA,
                        R = white.R,
                        G = white.G,
                        B = white.B
                    }
                },
                Camera = new CameraDto
                {
                    HSize = res,
                    VSize = res / 2,
                    FieldOfView = MathF.PI / 3,
                    From = new VectorDto { X = 0F, Y = 1.5F, Z = -5F },
                    To = new VectorDto { X = 0F, Y = 1F, Z = 0F },
                    Up = new VectorDto { X = 0F, Y = 1F, Z = 0F }
                }
            };

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new StringEnumConverter());
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(scene, Formatting.Indented, jsonSettings);
        }

        #endregion



    }
}
