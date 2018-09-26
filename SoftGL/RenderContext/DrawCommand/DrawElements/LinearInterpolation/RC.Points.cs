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

            var gl_PositionArray = (vec4*)passBuffers[0].Mapbuffer().ToPointer();
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

                var fragment = new Fragment();
                fragment.gl_FragCoord.x = (gl_PositionArray[gl_VertexID].x + 1) / 2.0f * viewport.z + viewport.x;
                fragment.gl_FragCoord.y = (gl_PositionArray[gl_VertexID].y + 1) / 2.0f * viewport.w + viewport.y;
                fragment.gl_FragCoord.z = (gl_PositionArray[gl_VertexID].z + 1) / 2.0f * (float)(this.depthRangeFar - this.depthRangeNear) + (float)this.depthRangeNear;
            }
            return result;
        }
    }
}
