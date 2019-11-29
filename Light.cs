using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Light
    {
        public Vec3f Position;
        public float Intensity;

        public Light(Vec3f position, float intensity)
        {
            Position = position;
            Intensity = intensity;
        }
    }
}
