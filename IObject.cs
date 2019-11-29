using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public interface IObject
    {
        bool Intersect(Vec3f orig, Vec3f dir, ref float t0);
        Material GetMaterial();
        Vec3f GetCenter();
    }

    public class Sphere : IObject
    {
        public Vec3f Center;
        public float Radius;
        public Material Material;

        public Sphere(Vec3f center, float radius, Material material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }
        public bool Intersect(Vec3f orig, Vec3f dir, ref float t0)
        {
            Vec3f L = Center - orig;
            float tca = L * dir;
            float d2 = L * L - tca * tca;
            if (d2 > Radius * Radius)
                return false;
            float thc = (float)Math.Sqrt(Radius * Radius - d2);
            t0 = tca - thc;
            float t1 = tca + thc;
            if (t0 < 0)
                t0 = t1;
            if (t0 < 0)
                return false;
            return true;
        }

        public Material GetMaterial()
        {
            return Material;
        }

        public Vec3f GetCenter()
        {
            return Center;
        }

    }

    public class Box : IObject
    {

        public Material Material;
        public Vec3f[] bounds = new Vec3f[2];
        public Box(Vec3f vmin, Vec3f vmax, Material material) 
        { 
            bounds[0] = vmin; 
            bounds[1] = vmax; 
        }

        public Vec3f GetCenter()
        {
            return new Vec3f((bounds[0].X + bounds[1].X) / 2, (bounds[0].Y + bounds[1].Y) / 2, (bounds[0].Z + bounds[1].Z) / 2);
        }

        public Material GetMaterial()
        {
            return Material;
        }

        // http://www.cs.utah.edu/~awilliam/box/box.pdf
        public bool Intersect(Vec3f orig, Vec3f dir, ref float t0)
        {
            var invdir = new Vec3f(1 / dir.X, 1 / dir.Y, 1 / dir.Z);
            int sign0 = invdir.X < 0 ? 1 : 0;
            int sign1 = invdir.Y < 0 ? 1 : 0;
            int sign2 = invdir.Z < 0 ? 1 : 0;
            float tmin, tmax, tymin, tymax, tzmin, tzmax;
            tmin = (bounds[sign0].X - orig.X) * invdir.X;
            tmax = (bounds[1 - sign0].X - orig.X) * invdir.X;
            tymin = (bounds[sign1].Y - orig.Y) * invdir.Y;
            tymax = (bounds[1 - sign1].Y - orig.Y) * invdir.Y;
            if ((tmin > tymax) || (tymin > tmax))
            {
                t0 = tmin;
                return false;
            }
            if (tymin > tmin)
                tmin = tymin;
            if (tymax < tmax)
                tmax = tymax;
            tzmin = (bounds[sign2].Z - orig.Z) * invdir.Z;
            tzmax = (bounds[1 - sign2].Z - orig.Z) * invdir.Z;
            if ((tmin > tzmax) || (tzmin > tmax))
            {
                t0 = tmin;
                return false;
            }
            if (tzmax < tmax)
                tmax = tzmax;
            t0 = tmin;
            return true;
        }
    };
    
}
