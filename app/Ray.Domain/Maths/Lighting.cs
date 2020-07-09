using Ray.Domain.Extensions;
using Ray.Domain.Model;
using Ray.Domain.Transportation;
using System;
using System.Numerics;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Ray.Domain.Maths
{
    public static class Lighting
    {
        // NOTE: Notice below reference to the "acne" effect. This is a real rudimentary bootstrap
        //  of a "special effect". May consider abstracting an "effects engine" at some stage,
        //  But for now, just allowing for the effect, which is in essence a rounding error!

        public static LightingDto CalculateColorWithPhongReflection(World scene, Model.Ray ray, bool useAcneEffect = false)
        {
            var hit = scene.CalculateHit(ray);

            bool isInShadow = hit.HasValue && IsPointInShadow(scene, useAcneEffect ? hit.Position : hit.OverPosition);

            var color = CalculateColorWithPhongReflection(
                hit, scene.LightSource, isInShadow);

            return new LightingDto
            {
                Color = color,
                Hit = hit,
                IsInShadow = isInShadow
            };
        }

        public static Color CalculateColorWithPhongReflection(IntersectionDto hit, Light light, bool isInShadow = false, bool useAcneEffect = false)
        {
            if (!hit.HasValue)
            {
                // Ray misses everything. Default color is black.
                return Colors.Black;
            }

            return CalculateColorWithPhongReflection(
                hit.Shape.Material, light,
                useAcneEffect ? hit.Position : hit.OverPosition,
                hit.EyeV, hit.NormalV, isInShadow
            );
        }

        public static Color CalculateColorWithPhongReflection(
            Material material, Light light, Vector4 point, Vector4 eyev, Vector4 normalv, bool isInShadow)
        {
            // TODO: DBC ?

            Color ambient, diffuse, specular;

            // combine the surface color with the lights color/intensity
            Color effective_color = material.Color.Multiply(light.Intensity);

            // compute the ambient contribution
            ambient = effective_color * material.Ambient;

            if (isInShadow)
            {
                // No need to calculate diffuse or specular.
                // Only the ambient value contributes when in shadow.
                return ambient;
            }

            // find the direction to the light source
            var lightv = Vector4.Normalize(light.Position - point);

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


        #region Helper methods

        private static bool IsPointInShadow(World scene, Vector4 point)
        {
            // Vector from the point back to the light source.
            var v = scene.LightSource.Position - point;
            var distance = v.Length();
            var direction = Vector4.Normalize(v);

            // Check for a "hit", within the world.
            var r = new Model.Ray(point, direction);
            var h = scene.CalculateHit(r);

            // If got a hit, check where (at what distance),
            // to determine whether point is in shadow.
            return h.HasValue && h.DistanceT < distance;
        }

        #endregion

    }
}
