using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private unsafe List<Fragment> LinearInterpolationLineStrip(int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer, PassBuffer[] passBuffers)
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
            var groupList = new List<LinearInterpolationInfoGroup>();
            ivec4 viewport = this.viewport;
            for (int indexID = 0; indexID < count - 1; indexID++)
            {
                var group = new LinearInterpolationInfoGroup(2);
                for (int i = 0; i < 2; i++)
                {
                    uint gl_VertexID = GetVertexID(pointer, type, indexID + i);
                    vec3 fragCoord = new vec3((gl_PositionArray[gl_VertexID].x + 1) / 2.0f * viewport.z + viewport.x,
                    (gl_PositionArray[gl_VertexID].y + 1) / 2.0f * viewport.w + viewport.y,
                    (gl_PositionArray[gl_VertexID].z + 1) / 2.0f * (float)(this.depthRangeFar - this.depthRangeNear) + (float)this.depthRangeNear);
                    group.array[i] = new LinearInterpolationInfo(indexID + i, gl_VertexID, fragCoord);
                }

                if (groupList.Contains(group)) { continue; }
                else { groupList.Add(group); }


            }

            for (int i = 0; i < passBuffers.Length; i++)
            {
                passBuffers[i].Unmapbuffer();
            }

            return result;
        }
    }
}
