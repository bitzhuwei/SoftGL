using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private unsafe List<Fragment> LinearInterpolationPolygon(int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer, PassBuffer[] passBuffers)
        {
            var result = new List<Fragment>();

            var gl_PositionArray = (vec4*)passBuffers[0].Mapbuffer().ToPointer();
            var pointers = new void*[passBuffers.Length - 1];
            for (int i = 0; i < pointers.Length; i++)
            {
                pointers[i] = passBuffers[i + 1].Mapbuffer().ToPointer();
            }
            byte[] indexData = indexBuffer.Data;
            int indexLength = indexData.Length / ByteLength(type);
            GCHandle pin = GCHandle.Alloc(indexData, GCHandleType.Pinned);
            IntPtr pointer = pin.AddrOfPinnedObject();
            var groupList = new List<LinearInterpolationInfoGroup>();
            ivec4 viewport = this.viewport;  // ivec4(x, y, width, height)
            for (int indexID = indices.ToInt32() / ByteLength(type) + 1, c = 0; c < count - 2 && indexID < indexLength - 2; indexID++, c++)
            {
                var group = new LinearInterpolationInfoGroup(3);
                for (int i = 0; i < 3; i++)
                {
                    uint gl_VertexID = GetVertexID(pointer, type, i == 0 ? 0 : indexID + i);
                    vec4 gl_Position = gl_PositionArray[gl_VertexID];
                    vec3 fragCoord = new vec3((gl_Position.x + 1) / 2.0f * viewport.z + viewport.x,
                    (gl_Position.y + 1) / 2.0f * viewport.w + viewport.y,
                    (gl_Position.z + 1) / 2.0f * (float)(this.depthRangeFar - this.depthRangeNear) + (float)this.depthRangeNear);
                    group.array[i] = new LinearInterpolationInfo(gl_VertexID, fragCoord);
                }

                if (groupList.Contains(group)) { continue; } // discard the same line.
                else { groupList.Add(group); }

                vec3 fragCoord0 = group.array[0].fragCoord;
                vec3 fragCoord1 = group.array[1].fragCoord;
                vec3 fragCoord2 = group.array[2].fragCoord;

                FindFragmentsInTriangle(fragCoord0, fragCoord1, fragCoord2, pointers, group, passBuffers, result);
            }

            for (int i = 0; i < passBuffers.Length; i++)
            {
                passBuffers[i].Unmapbuffer();
            }

            return result;
        }
    }
}
