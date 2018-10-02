using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {

        public static void glVertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.VertexAttribPointer(index, size, (VertexAttribType)type, normalized, stride, pointer);
            }
        }

        private void VertexAttribPointer(uint index, int size, VertexAttribType type, bool normalized, int stride, IntPtr pointer)
        {

        }
    }

    enum VertexAttribType : uint
    {
        Byte = GL.GL_BYTE,
        UnsignedByte = GL.GL_UNSIGNED_BYTE,
        Short = GL.GL_SHORT,
        UnsignedShort = GL.GL_UNSIGNED_SHORT,
        Int = GL.GL_INT,
        UnsignedInt = GL.GL_UNSIGNED_INT,
        HalfFloat = GL.GL_HALF_FLOAT,
        Float = GL.GL_FLOAT,
        Double = GL.GL_DOUBLE,
        //Fixed = GL.GL_FIXED, // TODO: to be continued..
        //Int2101010Rev = GL.GL_INT_2_10_10_10_REV, // TODO: to be continued..
        UnsignedInt2101010Rev = GL.GL_UNSIGNED_INT_2_10_10_10_REV,
        UnsignedInt10f11f11fRev = GL.GL_UNSIGNED_INT_10F_11F_11F_REV
    }
}
