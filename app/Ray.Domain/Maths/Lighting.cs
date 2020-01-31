using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Media;
using Ray.Domain.Extensions;
using Ray.Domain.Model;
using Ray.Domain.Transportation;

namespace Ray.Domain.Maths
{
    public static class Lighting
    {
        public static Color CalculateColorWithPhongReflection(IntersectionDto hit, Light light)
        {
            var point = hit.Position;
            var normal = hit.Shape.GetNormal(point);
            var eye = -hit.Ray.Direction;

            return Lighting.CalculateColorWithPhongReflection(hit.Shape.Material, light,
                point, eye, normal);
        }

        public static Color CalculateColorWithPhongReflection(
            Material material, Light light, Vector4 point, Vector4 eyev, Vector4 normalv)
        {
            // TODO: DBC ?

            Color ambient, diffuse, specular;

            // combine the surface color with the lights color/intensity
            Color effective_color = material.Color.Multiply(light.Intensity);

            // find the direction to the light source
            var lightv = Vector4.Normalize(light.Position - point);

            // compute the ambient contribution
            ambient = effective_color * material.Ambient;

            // light_dot_normal represents the cosine of the angle between the
            // light vector and the normal vector. A negative number means the
            // light is on the other side of the surface.
            var light_dot_normal = Vector4.Dot(lightv, normalv);
            if (light_dot_normal < 0F)
            {
                diffuse = System.Windows.Media.Colors.Black;
                specular = System.Windows.Media.Colors.Black;
            }
            else
            {
                // compute the diffuse contribution
                diffuse = effective_color * material.Diffuse * light_dot_normal;

                // reflect_dot_eye represents the cosine of the angle between the
                // reflection vector and the eye vector. A negative number means the
                // light reflects away from the eye.
                var reflectv = (-lightv).Reflect(normalv);
                var reflect_dot_eye = Vector4.Dot(reflectv, eyev);

                if (reflect_dot_eye <= 0F)
                {
                    specular = System.Windows.Media.Colors.Black;
                }
                else
                {
                    // compute the specular contribution
                    var factor = MathF.Pow(reflect_dot_eye, material.Shininess);
                    specular = light.Intensity * material.Specular * factor;
                }
            }

            // add the three contributions together to get the final shading
            return ambient + diffuse + specular;
        }
    }
}
