using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {

        public static void glVertexAttribIPointer(uint index, int size, uint type, int stride, IntPtr pointer)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.VertexAttribIPointer(index, size, (VertexAttribIType)type, stride, pointer);
            }
        }

        private void VertexAttribIPointer(uint index, int size, VertexAttribIType type, int stride, IntPtr pointer)
        {

        }
    }

    enum VertexAttribIType : uint
    {
        Byte = GL.GL_BYTE,
        UnsignedByte = GL.GL_UNSIGNED_BYTE,
        Short = GL.GL_SHORT,
        UnsignedShort = GL.GL_UNSIGNED_SHORT,
        Int = GL.GL_INT,
        UnsignedInt = GL.GL_UNSIGNED_INT
    }
}
