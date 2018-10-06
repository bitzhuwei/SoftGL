using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private unsafe List<Fragment> LinearInterpolationLineStripAdjacency(int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer, PassBuffer[] passBuffers)
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
            for (int indexID = indices.ToInt32() / ByteLength(type), c = 0; c < count - 3 && indexID < indexLength - 3; indexID++, c++)
            {
                var group = new LinearInterpolationInfoGroup(4);
                for (int i = 0; i < 4; i++)
                {
                    uint gl_VertexID = GetVertexID(pointer, type, indexID + i);
                    vec4 gl_Position = gl_PositionArray[gl_VertexID];
                    vec3 fragCoord = new vec3((gl_Position.x + 1) / 2.0f * viewport.z + viewport.x,
                    (gl_Position.y + 1) / 2.0f * viewport.w + viewport.y,
                    (gl_Position.z + 1) / 2.0f * (float)(this.depthRangeFar - this.depthRangeNear) + (float)this.depthRangeNear);
                    group.array[i] = new LinearInterpolationInfo(gl_VertexID, fragCoord);
                }

                if (groupList.Contains(group)) { continue; } // discard the same line.
                else { groupList.Add(group); }

                vec3 fragCoord0 = group.array[1].fragCoord, fragCoord1 = group.array[2].fragCoord;
                {
                    vec3 diff = (fragCoord0 - fragCoord1); // discard line that is too small.
                    if (Math.Abs(diff.x) < epsilon
                        && Math.Abs(diff.y) < epsilon
                        && Math.Abs(diff.z) < epsilon
                        ) { continue; }
                }

                FindFragmentsInLine(fragCoord0, fragCoord1, pointers, group, passBuffers, result);
            }

            for (int i = 0; i < passBuffers.Length; i++)
            {
                passBuffers[i].Unmapbuffer();
            }

            return result;
        }
    }
}
