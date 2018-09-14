using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glDrawBuffers(int count, uint[] buffers)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DrawBuffers(count, buffers);
            }
        }

        private void DrawBuffers(int count, uint[] buffers)
        {
            throw new NotImplementedException();
        }
    }
}
