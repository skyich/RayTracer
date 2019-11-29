using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Material ivory = new Material(1, new Vec4f(0.6f, 0.3f, 0.1f, 0), new Vec3f(0.4f, 0.4f, 0.3f), 50);
            Material red_rubber = new Material(1, new Vec4f(0.9f, 0.1f, 0, 0), new Vec3f(0.3f, 0.1f, 0.1f), 10);
            Material mirror = new Material(1, new Vec4f(0, 10, 0.8f, 0), new Vec3f(1, 1, 1), 1425);
            Material glass = new Material(1.5f, new Vec4f(0, 0.5f, 0.1f, 0.8f), new Vec3f(0.6f, 0.7f, 0.8f), 125);

            var objects = new List<IObject>();
            objects.Add(new Box(new Vec3f(-1.0f, -1.5f, -12), new Vec3f(-1.0f + 5, -1.5f + 5, -12 + 5), ivory));
            //objects.Add(new Sphere(new Vec3f(-1.0f, -1.5f, -12), 2, glass));
            //objects.Add(new Sphere(new Vec3f(1.5f, -0.5f, -18), 3, red_rubber));
           // objects.Add(new Sphere(new Vec3f(7, 5, -18), 4, mirror));
            var lights = new List<Light>();
            lights.Add(new Light(new Vec3f(-20, 20, 20), 1.5f));
            //lights.Add(new Light(new Vec3f(30, 50, -25), 1.8f));
            //lights.Add(new Light(new Vec3f(30, 20, 30), 1.7f));

            Render(objects, lights);
        }

        public Vec3f reflect(Vec3f i, Vec3f n)
        {
            return i - n * 2 * (i * n);
        }
        static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        public Vec3f refract(Vec3f i, Vec3f n, float eta_t, float eta_i = 1) // // Snell's law
        {
            float cosi = -Math.Max(-1, Math.Min(1, i * n));
            if (cosi < 0)
                return refract(i, -n, eta_i, eta_t); // if the ray comes from the inside the object, swap the air and the media
            float eta = eta_i / eta_t;
            float k = 1 - eta * eta * (1 - cosi * cosi);
            return k < 0 ? new Vec3f(1, 0, 0) : i * eta + n * (eta * cosi - (float)Math.Sqrt(k)); // k<0 = total reflection, no ray to refract. I refract it anyways, this has no physical meaning
        }

        public bool scene_intersect(Vec3f orig, Vec3f dir, List<IObject> objects, ref Vec3f point, ref Vec3f N, ref Material material)
        {
            float dist = float.MaxValue;
            Material t = new Material(0, new Vec4f(0, 0, 0, 0), new Vec3f(0, 0, 0), 0);
            Vec3f hit = new Vec3f(0, 0, 0);
            var t_N = new Vec3f(0, 0, 0);
            Parallel.ForEach(objects, (obj) =>
            {
                float dist_i = 0;
                if (obj.Intersect(orig, dir, ref dist_i) && dist_i < dist)
                {
                    dist = dist_i;
                    hit = orig + dir * dist_i;
                    t_N = (hit - obj.GetCenter()).Normalize();
                    t = obj.GetMaterial();
                }
            });
            material = t;
            point = hit;
            N = t_N;
            return dist < 1000;
        }

        public Vec3f CastRay(Vec3f orig, Vec3f dir, List<IObject> objects, List<Light> lights, int depth = 0)
        {
            Material material = new Material(0, new Vec4f(0, 0, 0, 0), new Vec3f(0, 0, 0), 0);
            Vec3f point = new Vec3f(0, 0, 0);
            Vec3f N = new Vec3f(0, 0, 0);

            if (depth > 4 || !scene_intersect(orig, dir, objects, ref point, ref N, ref material))
            {
                return new Vec3f(0.2f, 0.7f, 0.8f);
            }

            Vec3f reflect_dir = reflect(dir, N);
            Vec3f refract_dir = refract(dir, N, material.RefractiveIndex).Normalize();
            Vec3f reflect_orig = reflect_dir * N < 0 ? point - N * 1e-3f : point + N * 1e-3f; // offset the original point to avoid occlusion by the object itself
            Vec3f refract_orig = refract_dir * N < 0 ? point - N * 1e-3f : point + N * 1e-3f;
            Vec3f reflect_color = CastRay(reflect_orig, reflect_dir, objects, lights, depth + 1);
            Vec3f refract_color = CastRay(refract_orig, refract_dir, objects, lights, depth + 1);

            float diffuse_light_intensity = 0;
            float specular_light_intensity = 0;

            foreach (var x in lights)
            {
                Vec3f light_dir = (x.Position - point).Normalize();
                float light_distance = (x.Position - point).Norm();
                Vec3f shadow_orig = light_dir * N < 0 ? point - N * 1e-3f : point + N * 1e-3f; // checking if the point lies in the shadow of the lights[i]
                Vec3f shadow_pt = new Vec3f(0, 0, 0);
                Vec3f shadow_N = new Vec3f(0, 0, 0);
                Material tmpmaterial = new Material(0, new Vec4f(0, 0, 0, 0), new Vec3f(0, 0, 0), 0);
                if (scene_intersect(shadow_orig, light_dir, objects, ref shadow_pt, ref shadow_N, ref tmpmaterial) && (shadow_pt - shadow_orig).Norm() < light_distance)
                    continue;

                diffuse_light_intensity += x.Intensity * Math.Max(0f, light_dir * N);
                specular_light_intensity += (float)Math.Pow(Math.Max(0, reflect(light_dir, N) * dir), material.SpecularExponent) * x.Intensity;
            }
            return material.DiffuseColor * diffuse_light_intensity * material.Albedo.X1 + new Vec3f(1, 1, 1) * specular_light_intensity * material.Albedo.X2 + reflect_color * material.Albedo.X3 + refract_color * material.Albedo.X4 ;
        }

        public void Render(List<IObject> objects, List<Light> lights)
        {
            float fov = (float)Math.PI / 3;
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            LockBitmap lockBitmap = new LockBitmap(bmp);
            lockBitmap.LockBits();
            int width = lockBitmap.Width;
            int height = lockBitmap.Height;

            var result = Parallel.For(0, height, (j, state) =>
            {
                var result1 = Parallel.For(0, width, (i, state1) =>
                {
                    {
                        float dir_x = (i + 0.5f) - width / 2;
                        float dir_y = -(j + 0.5f) + height / 2; // this flips the image at the same time
                        float dir_z = -height / (2 * (float)Math.Tan(fov / 2));
                        Vec3f res = CastRay(new Vec3f(0, 0, 0), new Vec3f(dir_x, dir_y, dir_z).Normalize(), objects, lights);
                        float max = Math.Max(res.X, Math.Max(res.Y, res.Z));
                        if (max > 1)
                            res *= (1 / max);
                        Color col = Color.FromArgb((int)(255 * res.X), (int)(255 * res.Y), (int)(255 * res.Z));
                        lockBitmap.SetPixel(i, j, col);
                    }
                });
            });

            /*
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    float x = (2f * (i + 0.5f) / (float)width - 1f) * (float)Math.Tan(fov / 2f) * width / (float)height;
                    float y = -(2f * (j + 0.5f) / (float)height - 1f) * (float)Math.Tan(fov / 2f);
                    Vec3f dir = new Vec3f(x, y, -1).Normalize();
                    Vec3f res = CastRay(new Vec3f(0, 0, 0), dir, obj);
                    Color col = Color.FromArgb((int)(255 * res.X), (int)(255 * res.Y), (int)(255 * res.Z));
                    lockBitmap.SetPixel(i, j, col);
                }
            }*/

            lockBitmap.UnlockBits();
            pictureBox1.Image = bmp;
        }
    }
}
