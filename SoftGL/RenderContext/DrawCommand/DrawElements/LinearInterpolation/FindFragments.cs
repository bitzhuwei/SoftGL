using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        /// <summary>
        /// Find fragments in the specified triangle.
        /// </summary>
        /// <param name="fragCoord0"></param>
        /// <param name="fragCoord1"></param>
        /// <param name="fragCoord2"></param>
        /// <param name="pointers"></param>
        /// <param name="group"></param>
        /// <param name="passBuffers"></param>
        /// <param name="result"></param>
        unsafe private static void FindFragmentsInTriangle(vec3 fragCoord0, vec3 fragCoord1, vec3 fragCoord2, void*[] pointers, LinearInterpolationInfoGroup group, PassBuffer[] passBuffers, List<Fragment> result)
        {
            int attributeCount = passBuffers.Length - 1;
            var pixelList = new List<vec3>();
            FindPixelsAtTriangle(fragCoord0, fragCoord1, fragCoord2, pixelList);
            //OnSamePlane(fragCoord0, fragCoord1, fragCoord2, pixelList);
            for (int i = 0; i < pixelList.Count; i++) // for each pixel at this line..
            {
                vec3 pixel = pixelList[i];
                var fragment = new Fragment(pixel, attributeCount);
                float p0, p1, p2;
                LinearInterpolationTriangle(pixel, fragCoord0, fragCoord1, fragCoord2, out p0, out p1, out p2);
                for (int t = 0; t < attributeCount; t++) // new pass-buffer objects.
                {
                    PassType passType = passBuffers[t + 1].elementType;
                    fragment.attributes[t] = new PassBuffer(passType, 1); // only one element.
                }
                for (int attrIndex = 0; attrIndex < attributeCount; attrIndex++) // fill data in pass-buffer.
                {
                    PassBuffer attribute = fragment.attributes[attrIndex];
                    void* fragmentAttribute = attribute.Mapbuffer().ToPointer();
                    switch (attribute.elementType)
                    {
                        case PassType.Float:
                            {
                                var fAttr = (float*)fragmentAttribute; var array = (float*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1 + array[group.array[2].gl_VertexID] * p2;
                            } break;
                        case PassType.Vec2:
                            {
                                var fAttr = (vec2*)fragmentAttribute; var array = (vec2*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1 + array[group.array[2].gl_VertexID] * p2;
                            } break;
                        case PassType.Vec3:
                            {
                                var fAttr = (vec3*)fragmentAttribute; var array = (vec3*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1 + array[group.array[2].gl_VertexID] * p2;
                            } break;
                        case PassType.Vec4:
                            {
                                var fAttr = (vec4*)fragmentAttribute; var array = (vec4*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1 + array[group.array[2].gl_VertexID] * p2;
                            } break;
                        case PassType.Mat2:
                            {
                                var fAttr = (mat2*)fragmentAttribute; var array = (mat2*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1 + array[group.array[2].gl_VertexID] * p2;
                            } break;
                        case PassType.Mat3:
                            {
                                var fAttr = (mat3*)fragmentAttribute; var array = (mat3*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1 + array[group.array[2].gl_VertexID] * p2;
                            } break;
                        case PassType.Mat4:
                            {
                                var fAttr = (mat4*)fragmentAttribute; var array = (mat4*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1 + array[group.array[2].gl_VertexID] * p2;
                            } break;
                        default:
                            throw new NotDealWithNewEnumItemException(typeof(PassType));
                    }
                    attribute.Unmapbuffer();
                }
                result.Add(fragment);
            }
        }

        private static void LinearInterpolationTriangle(vec3 pixel, vec3 fragCoord0, vec3 fragCoord1, vec3 fragCoord2, out float p0, out float p1, out float p2)
        {
            var matrix = new mat3(fragCoord0, fragCoord1, fragCoord2);
            vec3 result = glm.inverse(matrix) * pixel;
            // note: "sum" is not needed.
            //float sum = result.x + result.y + result.z;
            //p0 = result.x / sum; p1 = result.y / sum; p2 = result.z / sum;
            // note: so, just need to assign values to p0, p1, p2.
            p0 = result.x; p1 = result.y; p2 = result.z;
        }

        private static void LinearInterpolationTriangle_Old(vec3 pixel, vec3 fragCoord0, vec3 fragCoord1, vec3 fragCoord2, out float p0, out float p1, out float p2)
        {
            float j0 = 0, j1 = 0, j2 = 0;
            {
                // 0 1 2 : A B C
                vec3 p = pixel - fragCoord0;
                vec3 b = fragCoord1 - fragCoord0;
                vec3 c = fragCoord2 - fragCoord0;

                j0 = GetJ(p, b, c);
            }
            {
                // 1 2 0 : B C A
                vec3 p = pixel - fragCoord1;
                vec3 b = fragCoord2 - fragCoord1;
                vec3 c = fragCoord0 - fragCoord1;

                j1 = GetJ(p, b, c);
            }
            {
                // 2 0 1: C A B
                vec3 p = pixel - fragCoord2;
                vec3 b = fragCoord0 - fragCoord2;
                vec3 c = fragCoord1 - fragCoord2;

                j2 = GetJ(p, b, c);
            }

            float sum = j0 + j1 + j2;
            p0 = j0 / sum; p1 = j1 / sum; p2 = j2 / sum;
            if (p0 < 0 || p1 < 0 || p2 < 0)
            {
                throw new Exception("Error");
            }
        }

        private static float GetJ(vec3 p, vec3 b, vec3 c)
        {
            float j = 0.0f;
            {
                var matrix = new mat2(p.x, p.y, b.x - c.x, b.y - c.y);
                mat2 inversed = glm.inverse(matrix);
                //mat2 one = inversed * matrix; Console.WriteLine(one);
                vec2 result = glm.inverse(matrix) * new vec2(b.x, b.y);
                j += result.x;
            }
            {
                var matrix = new mat2(p.x, p.z, b.x - c.x, b.z - c.z);
                mat2 inversed = glm.inverse(matrix);
                //mat2 one = inversed * matrix; Console.WriteLine(one);
                vec2 result = glm.inverse(matrix) * new vec2(b.x, b.z);
                j += result.x;
            }
            {
                var matrix = new mat2(p.y, p.z, b.y - c.y, b.z - c.z);
                mat2 inversed = glm.inverse(matrix);
                //mat2 one = inversed * matrix; Console.WriteLine(one);
                vec2 result = glm.inverse(matrix) * new vec2(b.y, b.z);
                j += result.x;
            }

            j = j / 3.0f;
            return j;
        }

        /// <summary>
        /// Find fragments in the specified line.
        /// </summary>
        /// <param name="fragCoord0"></param>
        /// <param name="fragCoord1"></param>
        /// <param name="pointers"></param>
        /// <param name="group"></param>
        /// <param name="passBuffers"></param>
        /// <param name="result"></param>
        unsafe private static void FindFragmentsInLine(vec3 fragCoord0, vec3 fragCoord1, void*[] pointers, LinearInterpolationInfoGroup group, PassBuffer[] passBuffers, List<Fragment> result)
        {
            int attributeCount = passBuffers.Length - 1;
            var pixelList = new List<vec3>();
            FindPixelsAtLine(fragCoord0, fragCoord1, pixelList);
            foreach (vec3 pixel in pixelList) // for each pixel at this line..
            {
                var fragment = new Fragment(pixel, attributeCount);
                float lp0 = (pixel - fragCoord0).length();
                float lp1 = (pixel - fragCoord1).length();
                float sum = lp0 + lp1;
                float p0 = lp1 / sum;
                float p1 = lp0 / sum;
                for (int i = 0; i < attributeCount; i++) // new pass-buffer objects.
                {
                    PassType passType = passBuffers[i].elementType;
                    fragment.attributes[i] = new PassBuffer(passType, 1); // only one element.
                }
                for (int attrIndex = 0; attrIndex < attributeCount; attrIndex++) // fill data in pass-buffer.
                {
                    PassBuffer attribute = fragment.attributes[attrIndex];
                    void* fragmentAttribute = attribute.Mapbuffer().ToPointer();
                    switch (attribute.elementType)
                    {
                        case PassType.Float:
                            {
                                var fAttr = (float*)fragmentAttribute; var array = (float*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1;
                            } break;
                        case PassType.Vec2:
                            {
                                var fAttr = (vec2*)fragmentAttribute; var array = (vec2*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1;
                            } break;
                        case PassType.Vec3:
                            {
                                var fAttr = (vec3*)fragmentAttribute; var array = (vec3*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1;
                            } break;
                        case PassType.Vec4:
                            {
                                var fAttr = (vec4*)fragmentAttribute; var array = (vec4*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1;
                            } break;
                        case PassType.Mat2:
                            {
                                var fAttr = (mat2*)fragmentAttribute; var array = (mat2*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1;
                            } break;
                        case PassType.Mat3:
                            {
                                var fAttr = (mat3*)fragmentAttribute; var array = (mat3*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1;
                            } break;
                        case PassType.Mat4:
                            {
                                var fAttr = (mat4*)fragmentAttribute; var array = (mat4*)pointers[attrIndex];
                                fAttr[0] = array[group.array[0].gl_VertexID] * p0 + array[group.array[1].gl_VertexID] * p1;
                            } break;
                        default:
                            throw new NotDealWithNewEnumItemException(typeof(PassType));
                    }
                    attribute.Unmapbuffer();
                }
                result.Add(fragment);
            }
        }


        private static bool OnSameLine(vec3 p0, vec3 p1, List<vec3> result)
        {
            bool sameLine = true;
            vec3 p01 = p1 - p0;
            float length = p01.length();
            foreach (var item in result)
            {
                vec3 other = item - (p0 + p1) / 2;
                float otherLength = other.length();
                float sum = Math.Abs(other.dot(p01));
                float diff = length * otherLength - sum;
                if (diff / sum > 0.01f)
                {
                    sameLine = false;
                }
            }

            return sameLine;
        }
        private static bool OnSamePlane(vec3 p0, vec3 p1, vec3 p2, List<vec3> result)
        {
            bool samePlane = true;

            vec3 p01 = p1 - p0; vec3 p02 = p2 - p0;
            vec3 direction = p01.cross(p02);
            float A = direction.x, B = direction.y, C = direction.z;
            // A(x - p0.x) + B(y - p0.y) + C(z - p0.z) = 0
            // Ax + By + Cz + D = 0
            float D = -direction.dot(p0);
            float length = direction.length();
            direction = direction / length;
            D = D / length;
            {
                float diff = direction.dot(p0) + D;
                Console.WriteLine(diff);
            }
            {
                float diff = direction.dot(p1) + D;
                Console.WriteLine(diff);
            }
            {
                float diff = direction.dot(p2) + D;
                Console.WriteLine(diff);
            }
            for (int i = 0; i < result.Count; i++)
            {
                vec3 p = new vec3(result[i]);
                float diff = direction.dot(p) + D;
                if (Math.Abs(diff) / direction.length() > 0.01f)
                {
                    //Console.WriteLine("Not same plane!");
                    samePlane = false;
                }
            }

            return samePlane;
        }

    }
}
