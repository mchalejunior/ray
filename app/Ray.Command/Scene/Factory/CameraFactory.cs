using System.Numerics;
using Ray.Domain.Extensions;
using Ray.Domain.Model;
using Ray.Serialize.Scene;

namespace Ray.Command.Scene.Factory
{
    class CameraFactory
    {
        public static Camera MapApiToDomain(CameraDto input)
        {
            Camera output = new Camera(input.HSize, input.VSize, input.FieldOfView);

            output.SetViewTransformation(
                new Vector3(input.From.X, input.From.Y, input.From.Z).AsPoint(),
                new Vector3(input.To.X, input.To.Y, input.To.Z).AsPoint(),
                new Vector3(input.Up.X, input.Up.Y, input.Up.Z).AsVector()
            );

            return output;
        }
    }
}
