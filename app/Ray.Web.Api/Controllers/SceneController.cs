using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ray.Command.ApiHandlers;
using Ray.Domain.Model;
using Ray.Serialize.Scene;
using Ray.Web.Api.Data;
using Ray.Web.Api.Infrastructure;

namespace Ray.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SceneController : ControllerBase
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IMediator _mediator;
        private readonly ILogger<SceneController> _logger;
        private readonly IHostEnvironment _env;

        public SceneController(IBackgroundTaskQueue taskQueue, IMediator mediator, 
            ILogger<SceneController> logger, IHostEnvironment env)
        {
            _taskQueue = taskQueue;
            _mediator = mediator;
            _logger = logger;
            _env = env;
        }

        [HttpGet("example")]
        public IActionResult Get()
        {
            return new JsonResult(GetSphereCentralWithPlanesExample());
        }

        [HttpPost]
        public IActionResult Post(SceneDto scene)
        {
            // TODO: input sanitize e.g.
            // best to accumulate errors and return a not ok with violations
            if (scene?.LightSource?.Position == null || scene?.LightSource?.Intensity == null)
            {
                throw new ArgumentOutOfRangeException(nameof(scene.LightSource));
            }
            if (scene?.Camera?.From == null || scene?.Camera?.To == null || scene?.Camera?.Up == null)
            {
                throw new ArgumentOutOfRangeException(nameof(scene.Camera));
            }


            var correlationId = Guid.NewGuid();
            var physicalProvider = _env.ContentRootFileProvider;
            var physicalPath = physicalProvider.GetFileInfo(correlationId + ".bmp");

            _taskQueue.QueueBackgroundWorkItem(async token =>
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                var createSceneCommand = new CreateSceneCommand
                {
                    Scene = scene,
                    CorrelationId = correlationId,
                    OutputFilePath = physicalPath.PhysicalPath
                };
                await _mediator.Send(createSceneCommand, token);
            });

            return Ok(new CreateSceneResponse
            {
                CorrelationId = correlationId,
                Message = "Scene submitted to renderer. TODO: info and URL to poll for rendered image."
            });
        }


        #region Example scene to serialize

        public static SceneDto GetSphereCentralWithPlanesExample()
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
            int res = medium;
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
                        R = white.ScR,
                        G = white.ScG,
                        B = white.ScB
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

            return scene;
        }

        #endregion


    }
}
