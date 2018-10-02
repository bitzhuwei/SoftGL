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
            // TODO: GL_INVALID_VALUE is generated if index​ is greater than or equal to GL_MAX_VERTEX_ATTRIBS.
            if (size != 1 && size != 2 && size != 3 && size != 4 && size != GL.GL_BGRA) { SetLastError(ErrorCode.InvalidValue); return; }
            if (!Enum.IsDefined(typeof(VertexAttribType), type)) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (stride < 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (size == GL.GL_BGRA)
            {
                if (type != VertexAttribType.UnsignedByte
                    && type != VertexAttribType.Int2101010Rev
                    && type != VertexAttribType.UnsignedInt2101010Rev)
                { SetLastError(ErrorCode.InvalidOperation); return; }
                if (normalized == false)
                { SetLastError(ErrorCode.InvalidOperation); return; }
            }
            if (type == VertexAttribType.Int2101010Rev || type == VertexAttribType.UnsignedInt2101010Rev)
            {
                if (size != 4 && size != GL.GL_BGRA) { SetLastError(ErrorCode.InvalidOperation); return; }
            }
            if (type == VertexAttribType.UnsignedInt10f11f11fRev)
            {
                if (size != 3) { SetLastError(ErrorCode.InvalidOperation); return; }
            }
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
            desc.normalize = normalized;
            desc.startPos = (uint)pointer.ToInt32();
            desc.stride = (uint)stride;
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
        Fixed = GL.GL_FIXED,
        Int2101010Rev = GL.GL_INT_2_10_10_10_REV,
        UnsignedInt2101010Rev = GL.GL_UNSIGNED_INT_2_10_10_10_REV,
        UnsignedInt10f11f11fRev = GL.GL_UNSIGNED_INT_10F_11F_11F_REV
    }
}
