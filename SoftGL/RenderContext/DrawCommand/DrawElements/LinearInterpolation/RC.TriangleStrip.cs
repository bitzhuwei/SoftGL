using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private unsafe List<Fragment> LinearInterpolationTriangleStrip(int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer, PassBuffer[] passBuffers)
        {
            var result = new List<Fragment>();
            int attributeCount = passBuffers.Length - 1;
            var gl_PositionArray = (vec4*)passBuffers[0].Mapbuffer().ToPointer();
            var pointers = new void*[passBuffers.Length - 1];
            for (int i = 0; i < pointers.Length; i++)
            {
                pointers[i] = passBuffers[i + 1].Mapbuffer().ToPointer();
            }
            byte[] indexData = indexBuffer.Data; int byteLength = indexData.Length;
            GCHandle pin = GCHandle.Alloc(indexData, GCHandleType.Pinned);
            IntPtr pointer = pin.AddrOfPinnedObject();
            var groupList = new List<LinearInterpolationInfoGroup>();
            ivec4 viewport = this.viewport;  // ivec4(x, y, width, height)
            for (int indexID = 0; indexID < count - 2; indexID++)
            {
                var group = new LinearInterpolationInfoGroup(3);
                for (int i = 0; i < 3; i++)
                {
                    uint gl_VertexID = GetVertexID(pointer, type, indexID + i);
                    vec4 gl_Position = gl_PositionArray[gl_VertexID];
                    vec3 fragCoord = new vec3((gl_Position.x + 1) / 2.0f * viewport.z + viewport.x,
                    (gl_Position.y + 1) / 2.0f * viewport.w + viewport.y,
                    (gl_Position.z + 1) / 2.0f * (float)(this.depthRangeFar - this.depthRangeNear) + (float)this.depthRangeNear);
                    group.array[i] = new LinearInterpolationInfo(indexID + i, gl_VertexID, fragCoord);
                }

                if (groupList.Contains(group)) { continue; } // discard the same line.
                else { groupList.Add(group); }

                vec3 fragCoord0 = group.array[0].fragCoord;
                vec3 fragCoord1 = group.array[1].fragCoord;
                vec3 fragCoord2 = group.array[2].fragCoord;

                var pixelList = new List<vec3>();
                FindPixelsAtTriangle(fragCoord0, fragCoord1, fragCoord2, pixelList);
                foreach (vec3 pixel in pixelList) // for each pixel at this line..
                {
                    var fragment = new Fragment(pixel, attributeCount);
                    var alpha = (pixel - fragCoord0).length() / (fragCoord1 - fragCoord0).length();
                    for (int i = 0; i < attributeCount; i++) // new pass-buffer objects.
                    {
                        PassType passType = passBuffers[i + 1].elementType;
                        fragment.attributes[i] = new PassBuffer(passType, 1); // only one element.
                    }
                    for (int attrIndex = 0; attrIndex < attributeCount; attrIndex++) // fill data in pass-buffer.
                    {
                        PassBuffer attribute = fragment.attributes[attrIndex];
                        void* fragmentAttribute = attribute.Mapbuffer().ToPointer(); ;
                        switch (attribute.elementType)
                        {
                            case PassType.Float:
                                {
                                    var fAttr = (float*)fragmentAttribute; var array = (float*)pointers[attrIndex];
                                    fAttr[0] = array[group.array[0].gl_VertexID] * alpha + array[group.array[1].gl_VertexID] * (1 - alpha);
                                } break;
                            case PassType.Vec2:
                                {
                                    var fAttr = (vec2*)fragmentAttribute; var array = (vec2*)pointers[attrIndex];
                                    fAttr[0] = array[group.array[0].gl_VertexID] * alpha + array[group.array[1].gl_VertexID] * (1 - alpha);
                                } break;
                            case PassType.Vec3:
                                {
                                    var fAttr = (vec3*)fragmentAttribute; var array = (vec3*)pointers[attrIndex];
                                    fAttr[0] = array[group.array[0].gl_VertexID] * alpha + array[group.array[1].gl_VertexID] * (1 - alpha);
                                } break;
                            case PassType.Vec4:
                                {
                                    var fAttr = (vec4*)fragmentAttribute; var array = (vec4*)pointers[attrIndex];
                                    fAttr[0] = array[group.array[0].gl_VertexID] * alpha + array[group.array[1].gl_VertexID] * (1 - alpha);
                                } break;
                            case PassType.Mat2:
                                {
                                    var fAttr = (mat2*)fragmentAttribute; var array = (mat2*)pointers[attrIndex];
                                    fAttr[0] = array[group.array[0].gl_VertexID] * alpha + array[group.array[1].gl_VertexID] * (1 - alpha);
                                } break;
                            case PassType.Mat3:
                                {
                                    var fAttr = (mat3*)fragmentAttribute; var array = (mat3*)pointers[attrIndex];
                                    fAttr[0] = array[group.array[0].gl_VertexID] * alpha + array[group.array[1].gl_VertexID] * (1 - alpha);
                                } break;
                            case PassType.Mat4:
                                {
                                    var fAttr = (mat4*)fragmentAttribute; var array = (mat4*)pointers[attrIndex];
                                    fAttr[0] = array[group.array[0].gl_VertexID] * alpha + array[group.array[1].gl_VertexID] * (1 - alpha);
                                } break;
                            default:
                                throw new NotDealWithNewEnumItemException(typeof(PassType));
                        }
                        attribute.Unmapbuffer();
                    }
                    result.Add(fragment);
                }
            }

            for (int i = 0; i < passBuffers.Length; i++)
            {
                passBuffers[i].Unmapbuffer();
            }

            return result;
        }
    }
}
