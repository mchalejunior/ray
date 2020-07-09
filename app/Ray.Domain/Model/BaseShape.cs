using Ray.Domain.Maths.Factories;
using System.Numerics;

namespace Ray.Domain.Model
{
    public abstract class BaseShape
    {
        public Material Material { get; set; } = Material.CreateDefaultInstance();


        // NOTE: Leaving comment for Sphere on Transformation property, as most descriptive.
        //  But same principles for other shapes. E.g. for Plane, we assume xz - that is 2D "wall"
        //  extending infinitely far in both x and z dimensions, passing through the origin.
        //  Then, like Sphere, we use the Transformation to rotate etc. however we desire.

        /// <summary>
        /// As per text: Intersection calculations kept simple by modeling as unit
        /// spheres at the origin. This <see cref="Transformation"/> can be applied
        /// to scale and translate this sphere instance as appropriate.
        /// </summary>
        /// <remarks>
        /// We actually apply the transform to the ray, rather than the sphere.
        /// The ray ultimately determines the visual output, derived in no small
        /// part by the intersection calculations. So keep the spheres uniform -
        /// unit sphere @ origin, move them around world space with the transform,
        /// but actually apply this transform (inverse of) to the ray.
        /// </remarks>
        /// <seealso cref="GetIntersections"/>
        public IMatrixTransformationBuilder Transformation { get; set; } = new MatrixTransformationBuilder();


        protected abstract Vector4 GetLocalNormal(Vector4 point);

        public virtual Vector4 GetNormal(Vector4 point, bool applyLocalTransformation = true)
        {
            if (applyLocalTransformation)
            {
                // Straight from text. See for derivation details.
                var object_point = Transformation.Execute(point, true);
                var object_normal = GetLocalNormal(object_point);
                var world_normal = Transformation.Execute(object_normal, true, true);
                world_normal.W = 0F;
                return Vector4.Normalize(world_normal);
            }
            else
            {
                return Vector4.Normalize(GetLocalNormal(point));
            }
        }


        protected Ray GetTransformedRay(Ray ray, bool applyLocalTransformation)
        {
            if (!applyLocalTransformation)
            {
                return ray;
            }

            return new Ray
            {
                // invert:true passed to Execute, as we apply the inverse of the Shape transform to the Ray.
                Origin = Transformation.Execute(ray.Origin, true),
                Direction = Transformation.Execute(ray.Direction, true)
            };
        }



        #region Fluent Material manipulation

        // Fluent syntax for updating select material attributes appears to be the most useful.
        // This might apply at IBasicShape level, not sure yet.
        public BaseShape UpdateColor(System.Windows.Media.Color color)
        {
            var m = Material;
            m.Color = color;
            Material = m;
            return this;
        }

        public BaseShape UpdateDiffuse(float diffuse)
        {
            var m = Material;
            m.Diffuse = diffuse;
            Material = m;
            return this;
        }

        public BaseShape UpdateSpecular(float specular)
        {
            var m = Material;
            m.Specular = specular;
            Material = m;
            return this;
        }

        public BaseShape UpdateAmbient(float ambient)
        {
            var m = Material;
            m.Ambient = ambient;
            Material = m;
            return this;
        }

        public BaseShape UpdateShininess(float shininess)
        {
            var m = Material;
            m.Shininess = shininess;
            Material = m;
            return this;
        }

        #endregion
    }
}
