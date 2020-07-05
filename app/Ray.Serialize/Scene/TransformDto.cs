using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Serialize.Scene
{
    public class TransformDto
    {
        public TransformationType TransformType { get; set; }

        public float RotationRadians { get; set; }

        public VectorDto VectorTransformation { get; set; }

        public ShearTransformationDto ShearTransformation { get; set; }




        public enum TransformationType
        {
            RotateX = 0,
            RotateY,
            RotateZ,
            Scale,
            Translate,
            Shear
        }
    }
}
