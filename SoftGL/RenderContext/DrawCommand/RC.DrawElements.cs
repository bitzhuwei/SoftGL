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
                context.DrawElements(mode, count, type, indices);
            }
        }

        private void DrawElements(uint mode, int count, uint type, IntPtr indices)
        {

        }

    }
}
