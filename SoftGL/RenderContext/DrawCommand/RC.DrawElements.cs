using System;

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
            if (mode == 0 || type == 0) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }
            // TODO: GL_INVALID_OPERATION is generated if a geometry shader is active and mode​ is incompatible with the input primitive type of the geometry shader in the currently installed program object.
            // TODO: GL_INVALID_OPERATION is generated if a non-zero buffer object name is bound to an enabled array or the element array and the buffer object's data store is currently mapped.

        }

    }
}
