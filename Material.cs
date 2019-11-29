using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Material
    {
        public Vec3f DiffuseColor;
        public float SpecularExponent;
        //Истинное или плоское альбедо — коэффициент диффузного отражения
        public Vec4f Albedo;
        public float RefractiveIndex;

        public Material (float refractive, Vec4f albedo, Vec3f color, float specular)
        {
            RefractiveIndex = refractive;
            Albedo = albedo;
            DiffuseColor = color;
            SpecularExponent = specular;
        }
    }
}
