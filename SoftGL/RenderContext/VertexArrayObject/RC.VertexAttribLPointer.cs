using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {

        public static void glVertexAttribLPointer(uint index, int size, uint type, int stride, IntPtr pointer)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.VertexAttribLPointer(index, size, (VertexAttribLType)type, stride, pointer);
            }
        }

        private void VertexAttribLPointer(uint index, int size, VertexAttribLType type, int stride, IntPtr pointer)
        {

        }
    }

    enum VertexAttribLType : uint
    {
        Double = GL.GL_DOUBLE
    }
}
