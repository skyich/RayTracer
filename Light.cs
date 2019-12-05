using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Light
    {
        public Vec3f position;
        public float intensity;

        public Light(Vec3f position, float intensity)
        {
            this.position = position;
            this.intensity = intensity;
        }
    }
}
