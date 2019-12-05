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
        static Material front_wall_material;
        static int REC_DEPTH = 4;

        public Form1()
        {
            InitializeComponent();

        }

        private static Vec3f Reflect(Vec3f I, Vec3f N)
        {
            return I - N * 2f * (I * N);
        }

        // Snell's law
        private static Vec3f Refract(Vec3f I, Vec3f N, float eta_t, float eta_i = 1f)
        {
            var cosi = -Math.Max(-1f, Math.Min(1, I * N));

            // if the ray comes from the inside the object, swap the air and the media.
            if (cosi < 0) return Refract(I, -N, eta_i, eta_t);

            var eta = eta_i / eta_t;

            var k = 1 - eta * eta * (1 - cosi * cosi);

            // k < 0 = total reflection, no ray to refract. 
            // I refract it anyways, this has no physical meaning.
            return k < 0 ? new Vec3f(1, 0, 0) : I * eta + N * (eta * cosi - (float)Math.Sqrt(k));
        }


        private static bool SceneIntersect(Vec3f orig, Vec3f dir, List<Sphere> spheres, ref Vec3f hit, ref Vec3f N, ref Material material)
        {
            var spheresDist = float.MaxValue;

            foreach (var sphere in spheres)
            {
                var disti = 0f;

                if (sphere.RayIntersect(orig, dir, ref disti) && disti < spheresDist)
                {
                    spheresDist = disti;

                    hit = orig + dir * disti;

                    N = (hit - sphere.Center).normalize();

                    material = sphere.Material;
                }
            }

            List<float> dists = new List<float>();
            dists.Add(spheresDist);

            var floor = float.MaxValue;

            if (Math.Abs(dir.y) > 1e-3)
            {
                // The checkerboard plane has equation y = -4.
                var d = -(orig.y + 8) / dir.y;

                var pt = orig + dir * d;

                if (d > 0 && Math.Abs(pt.x) < 10 && pt.z <= 0 && pt.z >= -30 && d < spheresDist)
                {
                    floor = d;
                    hit = pt;

                    N = new Vec3f(0, 1, 0);

                    var c1 = new Vec3f(.3f, .3f, .3f);
                    var c2 = new Vec3f(.3f, .2f, .1f);

                    material = new Material(1, new[] { .9f, .1f, .0f, .0f }, new Vec3f(.3f, .3f, .3f), 10);
                }
            }
            dists.Add(floor);

            var ceiling = float.MaxValue;

            if (Math.Abs(dir.y) > 1e-3)
            {
                var d = -(orig.y - 6) / dir.y;

                var pt = orig + dir * d;

                if (d > 0 && Math.Abs(pt.x) <= 10 && pt.z <= 0 && pt.z >= -30 && d < spheresDist)
                {
                    ceiling = d;
                    hit = pt;

                    N = new Vec3f(0, -1, 0);

                    var c1 = new Vec3f(.3f, .3f, .3f);
                    var c2 = new Vec3f(.3f, .2f, .1f);

                    material = new Material(1, new[] { .9f, .1f, .0f, .0f }, new Vec3f(.3f, .3f, .3f), 10);
                }
            }
            dists.Add(ceiling);

            var right_wall = float.MaxValue;

            if (Math.Abs(dir.x) > 1e-3)
            {
                // The checkerboard plane has equation y = -4.
                var d = -(orig.x - 10) / dir.x;

                var pt = orig + dir * d;

                if (d > 0 && pt.y <= 6 && pt.y >= -8 && pt.z <= 0 && pt.z >= -30 && d < spheresDist)
                {
                    right_wall = d;
                    hit = pt;

                    N = new Vec3f(-1, 0, 0);

                    var c1 = new Vec3f(0, 1, 0);
                    var c2 = new Vec3f(.3f, .2f, .1f);

                    //material.DiffColor = c1;
                    material = new Material(1, new[] { .9f, .1f, .0f, .0f }, new Vec3f(.1f, .3f, .1f), 10);
                }
            }
            dists.Add(right_wall);

            var left_wall = float.MaxValue;

            if (Math.Abs(dir.x) > 1e-3)
            {
                // The checkerboard plane has equation y = -4.
                var d = -(orig.x + 10) / dir.x;

                var pt = orig + dir * d;

                if (d > 0 && pt.y <= 6 && pt.y >= -8 && pt.z <= 0 && pt.z >= -30 && d < spheresDist)
                {
                    left_wall = d;
                    hit = pt;

                    N = new Vec3f(1, 0, 0);

                    var c1 = new Vec3f(1, 0, 0);
                    var c2 = new Vec3f(.3f, .2f, .1f);

                    material = new Material(1, new[] { .9f, .1f, .0f, .0f }, new Vec3f(.3f, .1f, .1f), 10);
                }
            }
            dists.Add(left_wall);

            var front_wall = float.MaxValue;

            if (Math.Abs(dir.z) > 1e-3f)
            {
                // The checkerboard plane has equation y = -4.
                var d = -(orig.z + 30) / dir.z;

                var pt = orig + dir * d;

                if (d > 0 && pt.y <= 6 && pt.y >= -8 &&  Math.Abs(pt.x) <= 10 &&  d < spheresDist)
                {
                    front_wall = d;
                    hit = pt;

                    N = new Vec3f(0, 0, 1);

                    var c1 = new Vec3f(.3f, .3f, .3f);
                    var c2 = new Vec3f(.3f, .2f, .1f);

                    material = front_wall_material;
                }
            }
            dists.Add(front_wall);

            var back_wall = float.MaxValue;

            if (Math.Abs(dir.z) > 1e-3f)
            {
                // The checkerboard plane has equation y = -4.
                var d = -(orig.z) / dir.z;

                var pt = orig + dir * d;

                if (d > 0 && pt.y <= 6 && pt.y >= -8 && Math.Abs(pt.x) <= 10 && d < spheresDist)
                {
                    front_wall = d;
                    hit = pt;

                    N = new Vec3f(0, 0, -1);

                    var c1 = new Vec3f(.3f, .3f, .3f);
                    var c2 = new Vec3f(.3f, .2f, .1f);

                    material = new Material(1, new[] { .9f, .1f, .0f, .0f }, new Vec3f(.1f, .1f, .7f), 10);
                }
            }
            dists.Add(front_wall);

            return dists.Min() < 1000;
        }


        private static Vec3f cast_ray(Vec3f orig, Vec3f dir, List<Sphere> spheres, List<Light> lights, int depth = 0)
        {
            var point = new Vec3f();
            var N = new Vec3f();
            var material = new Material();

            if (depth > REC_DEPTH || !SceneIntersect(orig, dir, spheres, ref point, ref N, ref material))
            {
                // Background color.
                return new Vec3f(.3f, .3f, .3f);
            }

            var reflectDir = Reflect(dir, N).normalize();
            var refractDir = Refract(dir, N, material.RefIndex).normalize();

            // Offset the original point to avoid occlusion by the object itself.
            var nscl = N * 1e-3f;

            var psub = point - nscl;
            var padd = point + nscl;

            var reflectOrig = reflectDir * N < 0 ? psub : padd;
            var refractOrig = refractDir * N < 0 ? psub : padd;

            var reflectColor = cast_ray(reflectOrig, reflectDir, spheres, lights, depth + 1);
            var refractColor = cast_ray(refractOrig, refractDir, spheres, lights, depth + 1);

            float diffuseLightIntensity = 0, specularLightIntensity = 0;

            foreach (var light in lights)
            {
                var lightDir = (light.position - point).normalize();

                var lightDistance = (light.position - point).norm();

                // Checking if the point lies in the shadow of the light.
                var shadowOrig = lightDir * N < 0 ? psub : padd;

                var shadow_pt = new Vec3f();
                var shadow_N = new Vec3f();
                var tmpMaterial = new Material();

                var sceneIntersect = SceneIntersect(shadowOrig, lightDir, spheres, ref shadow_pt, ref shadow_N, ref tmpMaterial);

                if (sceneIntersect && (shadow_pt - shadowOrig).norm() < lightDistance) continue;

                diffuseLightIntensity += light.intensity * Math.Max(0, lightDir * N);

                specularLightIntensity += (float)Math.Pow(Math.Max(0, -Reflect(-lightDir, N) * dir), material.SpecExp) * light.intensity;
            }

            return material.DiffColor * diffuseLightIntensity * material.Albedo[0]
                + new Vec3f(1, 1, 1) * specularLightIntensity * material.Albedo[1]
                + reflectColor * material.Albedo[2]
                + refractColor * material.Albedo[3];
        }

        public void Render(List<Sphere> objects, List<Light> lights)
        {
            float fov = (float)Math.PI / 3f;
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
                        var dirx = i + .5f - width / 2f;
                        var diry = -(j + .5f) + height / 2f;
                        var dirz = -height / (2f * (float)Math.Tan(fov / 2f));
                        var vcam = new Vec3f(0, 0, 0);
                        var vdir = new Vec3f(dirx, diry, dirz).normalize();
                        Vec3f res = cast_ray(vcam, vdir, objects, lights);
                        float max = Math.Max(res.x, Math.Max(res.y, res.z));
                        if (max > 1)
                            res *= (1 / max);
                        Color col = Color.FromArgb((int)(255 * res.x), (int)(255 * res.y), (int)(255 * res.z));
                        lockBitmap.SetPixel(i, j, col);
                    }
                });
            });

            lockBitmap.UnlockBits();
            pictureBox1.Image = bmp;
        }

        private void ButtonRender_Click(object sender, EventArgs e)
        {
            // Materials.
            var ivory = new Material(1, new[] { .6f, .3f, .1f, .0f }, new Vec3f(.4f, .4f, .3f), 50);

            var glass = new Material(1.5f, new[] { .0f, .5f, .1f, .8f }, new Vec3f(.6f, .7f, .8f), 125);

            var red_rubber = new Material(1, new[] { .9f, .1f, .0f, .0f }, new Vec3f(.3f, .1f, .1f), 10);

            var mirror = new Material(1, new[] { .0f, 10f, .8f, .0f }, new Vec3f(1, 1, 1), 1425);

            // Spheres.            
            var spheres = new List<Sphere>();

            if (checkCommon.Checked)
                spheres.Add(new Sphere(new Vec3f(-3, -6, -16), 2, red_rubber));
            if (checkGlass.Checked)
                spheres.Add(new Sphere( new Vec3f( 4, -5, -18 ), 3, glass ));
            if (checkMirror.Checked)
                spheres.Add(new Sphere(new Vec3f(-2, -3, -22), 4, mirror));


            // Lights.
            var lights = new List<Light>();
            lights.Add(new Light(new Vec3f(0, 3.5f, -15), 1f));
            if (checkLight1.Checked)
                lights.Add(new Light(new Vec3f(0, 5.5f, -10), 1f));
            if (checkLight2.Checked)
                lights.Add(new Light(new Vec3f(0, 5.5f, -20), 1f));


            if (checkMirrorWall.Checked)
                front_wall_material = mirror;
            else
                front_wall_material = new Material(1, new[] { .9f, .1f, .0f, .0f }, new Vec3f(.4f, .0f, .4f), 10);

            REC_DEPTH = (int)numericUpDown1.Value;

            Render(spheres, lights);
        }
    }
}
