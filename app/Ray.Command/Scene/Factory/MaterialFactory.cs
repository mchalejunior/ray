using Ray.Domain.Model;
using Ray.Serialize.Scene;

namespace Ray.Command.Scene.Factory
{
    class MaterialFactory
    {
        public static Material MapApiToDomain(MaterialDto input)
        {
            Material output = Material.CreateDefaultInstance();

            if (input.Color != null)
            {
                output.Color.ScA = input.Color.A;
                output.Color.ScR = input.Color.R;
                output.Color.ScG = input.Color.G;
                output.Color.ScB = input.Color.B;
            }

            if (input.Ambient.HasValue) output.Ambient = input.Ambient.Value;
            if (input.Diffuse.HasValue) output.Diffuse = input.Diffuse.Value;
            if (input.Specular.HasValue) output.Specular = input.Specular.Value;
            if (input.Shininess.HasValue) output.Shininess = input.Shininess.Value;

            return output;
        }
    }
}
