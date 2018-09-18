using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glDrawElements(uint mode, int count, uint type, IntPtr indices)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DrawElements((DrawTarget)mode, count, (DrawElementsType)type, indices);
            }
        }

        private void DrawElements(DrawTarget mode, int count, DrawElementsType type, IntPtr indices)
        {
            if (!Enum.IsDefined(typeof(DrawTarget), mode) || !Enum.IsDefined(typeof(DrawElementsType), type)) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }
            // TODO: GL_INVALID_OPERATION is generated if a geometry shader is active and mode​ is incompatible with the input primitive type of the geometry shader in the currently installed program object.
            // TODO: GL_INVALID_OPERATION is generated if a non-zero buffer object name is bound to an enabled array or the element array and the buffer object's data store is currently mapped.

            VertexArrayObject vao = this.currentVertexArrayObject; // data structure.
            if (vao == null) { return; }
            ShaderProgram program = this.currentShaderProgram; // algorithm.
            if (program == null) { return; }
            GLBuffer indexBuffer = this.currentBufferDict[BindBufferTarget.ElementArrayBuffer];
            if (indexBuffer == null) { return; }

            // execute vertex shader for each vertex!
            // This is a low effetient implementation.
            {
                VertexShader vs = program.VertexShader;
                if (vs == null) { return; }
                Dictionary<uint, VertexAttribDesc> locVertexAttribDict = vao.LocVertexAttribDict;
                InVariable[] inVariables = vs.inVariableDict.Values.ToArray();
                byte[] data = indexBuffer.Data;
                int byteLength = data.Length;
                GCHandle pin = GCHandle.Alloc(indexBuffer.Data, GCHandleType.Pinned);
                IntPtr pointer = pin.AddrOfPinnedObject();
                for (int i = 0; i < count; i++)
                {
                    uint gl_VertexID;
                    if (GetVertexID(pointer, type, byteLength, i, out gl_VertexID))
                    {

                    }
                };
                pin.Free();
            }

            // execute fargment shader for each fragment!
            // This is a low effetient implementation.

        }

        private unsafe bool GetVertexID(IntPtr pointer, DrawElementsType type, int byteLength, int i, out uint gl_VertexID)
        {
            bool result = false;
            gl_VertexID = uint.MaxValue;
            switch (type)
            {
                case DrawElementsType.UnsignedByte:
                    if (i <= byteLength)
                    {
                        byte* array = (byte*)pointer.ToPointer();
                        gl_VertexID = array[i];
                        result = true;
                    }
                    break;
                case DrawElementsType.UnsignedShort:
                    if (i <= byteLength)
                    {
                        ushort* array = (ushort*)pointer.ToPointer();
                        gl_VertexID = array[i];
                        result = true;
                    }
                    break;
                case DrawElementsType.UnsignedInt:
                    if (i <= byteLength)
                    {
                        uint* array = (uint*)pointer.ToPointer();
                        gl_VertexID = array[i];
                        result = true;
                    }
                    break;
                default:
                    throw new NotDealWithNewEnumItemException(typeof(DrawElementsType));
            }

            return result;
        }
    }
}
