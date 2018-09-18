using System;
using System.Collections.Generic;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glBufferData(uint target, int size, IntPtr data, uint usage)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.BufferData((BindBufferTarget)target, size, data, (Usage)usage);
            }
        }

        private void BufferData(BindBufferTarget target, int size, IntPtr data, Usage usage)
        {
            if (!Enum.IsDefined(typeof(BindBufferTarget), target)) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (!Enum.IsDefined(typeof(Usage), usage)) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (size < 0) { SetLastError(ErrorCode.InvalidValue); return; }
            GLBuffer buffer = this.currentBufferDict[target];
            if (buffer == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO: GL_OUT_OF_MEMORY is generated if the GL is unable to create a data store with the specified size​.

            buffer.SetData(size, data, usage);
        }
    }
}
