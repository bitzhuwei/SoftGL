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
            // TODO: GL_INVALID_VALUE is generated if index​ is greater than or equal to GL_MAX_VERTEX_ATTRIBS.
            if (size != 1 && size != 2 && size != 3 && size != 4) { SetLastError(ErrorCode.InvalidValue); return; }
            if (type == 0) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (stride < 0) { SetLastError(ErrorCode.InvalidValue); return; }
            VertexArrayObject vao = this.currentVertexArrayObject;
            if (vao == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            GLBuffer buffer = this.currentBufferDict[BindBufferTarget.ArrayBuffer];
            // GL_INVALID_OPERATION is generated if zero is bound to the GL_ARRAY_BUFFER buffer object binding point and the pointer​ argument is not NULL.
            // TODO: why only when "pointer​ argument is not NULL"?
            if (buffer == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            VertexAttribDesc desc = null;
            if (!vao.LocVertexAttribDict.TryGetValue(index, out desc))
            {
                desc = new VertexAttribDesc();
                vao.LocVertexAttribDict.Add(index, desc);
            }
            desc.inLocation = index;
            desc.vbo = buffer;
            desc.dataSize = size;
            desc.dataType = (uint)type;
            //desc.normalize = false; // not needed.
            desc.startPos = (uint)pointer.ToInt32();
            desc.stride = (uint)stride;
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
