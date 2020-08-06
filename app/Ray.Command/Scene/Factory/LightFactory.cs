using System.Numerics;
using Ray.Domain.Extensions;
using Ray.Domain.Model;
using Ray.Serialize.Scene;

namespace Ray.Command.Scene.Factory
{
    class LightFactory
    {
        public static Light MapApiToDomain(LightDto input)
        {
            return new Light
            {
                Position = new Vector3(input.Position.X, input.Position.Y, input.Position.Z).AsPoint(),
                Intensity = new Color
                {
                    ScR = input.Intensity.R,
                    ScG = input.Intensity.G,
                    ScB = input.Intensity.B,
                    ScA = input.Intensity.A
                }
            };
        }
    }
}
