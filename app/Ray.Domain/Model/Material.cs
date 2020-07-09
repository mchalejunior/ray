namespace Ray.Domain.Model
{
    public struct Material
    {
        public static Material CreateDefaultInstance()
        {
            return new Material
            {
                Color = Model.Color.FromScRgb(DefaultColorA, DefaultColorR, DefaultColorG, DefaultColorB),
                Ambient = DefaultAmbient,
                Diffuse = DefaultDiffuse,
                Specular = DefaultSpecular,
                Shininess = DefaultShininess
            };
        }

        // Defaults defined for all properties. Using texts suggestions here.
        // See BasicColorOpsTests for working with colors. Starting with "fully opaque".
        public const float DefaultColorA = 1.0F, DefaultColorR = 1.0F, DefaultColorG = 1.0F, DefaultColorB = 1.0F,
            DefaultAmbient = 0.1F, DefaultDiffuse = 0.9F, DefaultSpecular = 0.9F, DefaultShininess = 200F;

        public Model.Color Color;
        public float Ambient;
        public float Diffuse;
        public float Specular;
        public float Shininess;

    }
}
