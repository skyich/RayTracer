using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Vec3f
    {
        public float X;
        public float Y;
        public float Z;

        public Vec3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vec3f operator -(Vec3f c1, Vec3f c2)
        {
            return new Vec3f(c1.X - c2.X, c1.Y - c2.Y, c1.Z - c2.Z);
        }

        public static Vec3f operator -(Vec3f c1)
        {
            return c1 * (-1);
        }

        public static Vec3f operator +(Vec3f c1, Vec3f c2)
        {
            return new Vec3f(c1.X + c2.X, c1.Y + c2.Y, c1.Z + c2.Z);
        }

        public static float operator *(Vec3f c1, Vec3f c2)
        {
            return c1.X * c2.X + c1.Y * c2.Y + c1.Z * c2.Z;
        }

        public static Vec3f operator *(Vec3f c1, float c2)
        {
            return new Vec3f(c1.X * c2, c1.Y * c2, c1.Z * c2);
        }

        // Нормализация вектора
        public Vec3f Normalize(int l = 1)
        {
            return this * (l / Norm());
        }

        public float Norm()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }
    }

    public class Vec4f
    {
        public float X1;
        public float X2;
        public float X3;
        public float X4;

        public Vec4f(float x1, float x2, float x3, float x4)
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
            X4 = x4;
        }
    }
}
