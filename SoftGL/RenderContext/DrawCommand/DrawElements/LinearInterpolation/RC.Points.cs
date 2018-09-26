using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private unsafe List<Fragment> LinearInterpolationPoints(int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer, PassBuffer[] passBuffers)
        {
            var result = new List<Fragment>();
            int attributeCount = passBuffers.Length - 1;
            var gl_PositionArray = (vec4*)passBuffers[0].Mapbuffer().ToPointer();
            var pointers = new void*[passBuffers.Length - 1];
            for (int i = 0; i < pointers.Length; i++)
            {
                pointers[i] = passBuffers[i + 1].Mapbuffer().ToPointer();
            }
            byte[] indexData = indexBuffer.Data;
            int byteLength = indexData.Length;
            GCHandle pin = GCHandle.Alloc(indexData, GCHandleType.Pinned);
            IntPtr pointer = pin.AddrOfPinnedObject();
            var gl_VertexIDList = new List<uint>();
            ivec4 viewport = this.viewport;
            for (int indexID = 0; indexID < count; indexID++)
            {
                uint gl_VertexID = GetVertexID(pointer, type, indexID);
                if (gl_VertexIDList.Contains(gl_VertexID)) { continue; }
                else { gl_VertexIDList.Add(gl_VertexID); }

                vec3 fragCoord = new vec3((gl_PositionArray[gl_VertexID].x + 1) / 2.0f * viewport.z + viewport.x,
                    (gl_PositionArray[gl_VertexID].y + 1) / 2.0f * viewport.w + viewport.y,
                    (gl_PositionArray[gl_VertexID].z + 1) / 2.0f * (float)(this.depthRangeFar - this.depthRangeNear) + (float)this.depthRangeNear);
                var fragment = new Fragment(fragCoord, attributeCount);
                for (int i = 0; i < attributeCount; i++) // new pass-buffer objects.
                {
                    PassType passType = passBuffers[i].elementType;
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
                                fAttr[0] = array[gl_VertexID];
                            } break;
                        case PassType.Vec2:
                            {
                                var fAttr = (vec2*)fragmentAttribute; var array = (vec2*)pointers[attrIndex];
                                fAttr[0] = array[gl_VertexID];
                            } break;
                        case PassType.Vec3:
                            {
                                var fAttr = (vec3*)fragmentAttribute; var array = (vec3*)pointers[attrIndex];
                                fAttr[0] = array[gl_VertexID];
                            } break;
                        case PassType.Vec4:
                            {
                                var fAttr = (vec4*)fragmentAttribute; var array = (vec4*)pointers[attrIndex];
                                fAttr[0] = array[gl_VertexID];
                            } break;
                        case PassType.Mat2:
                            {
                                var fAttr = (mat2*)fragmentAttribute; var array = (mat2*)pointers[attrIndex];
                                fAttr[0] = array[gl_VertexID];
                            } break;
                        case PassType.Mat3:
                            {
                                var fAttr = (mat3*)fragmentAttribute; var array = (mat3*)pointers[attrIndex];
                                fAttr[0] = array[gl_VertexID];
                            } break;
                        case PassType.Mat4:
                            {
                                var fAttr = (mat4*)fragmentAttribute; var array = (mat4*)pointers[attrIndex];
                                fAttr[0] = array[gl_VertexID];
                            } break;
                        default:
                            throw new NotDealWithNewEnumItemException(typeof(PassType));
                    }
                    attribute.Unmapbuffer();
                }
                result.Add(fragment);
            }

            for (int i = 0; i < passBuffers.Length; i++)
            {
                passBuffers[i].Unmapbuffer();
            }

            return result;
        }
    }
}
