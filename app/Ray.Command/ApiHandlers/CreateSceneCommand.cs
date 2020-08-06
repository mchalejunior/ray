using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Ray.Command.Scene.Factory;
using Ray.Domain.Extensions;
using Ray.Domain.Maths;
using Ray.Domain.Model;
using Ray.Serialize.Scene;

namespace Ray.Command.ApiHandlers
{
    public class CreateSceneCommand : IRequest<Guid>
    {
        public SceneDto Scene { get; set; }

        public string OutputFilePath { get; set; }
    }

    
    public class CreateSceneHandler : RequestHandler<CreateSceneCommand, Guid>
    {
        protected override Guid Handle(CreateSceneCommand request)
        {
            var shapes = new List<IBasicShape>();
            shapes.AddRange(request.Scene.Shapes.Select(ShapeFactory.MapApiToDomain));

            var world = new World(shapes, LightFactory.MapApiToDomain(request.Scene.LightSource));

            var camera = CameraFactory.MapApiToDomain(request.Scene.Camera);

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

            canvas.Save(request.OutputFilePath);



            return Guid.NewGuid();
        }
    }
}
