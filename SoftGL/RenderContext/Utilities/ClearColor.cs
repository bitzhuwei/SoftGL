using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private vec4 clearColor = new vec4(0, 0, 0, 0);
        public static void glClearColor(float r, float g, float b, float a)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.ClearColor(r, g, b, a);
            }
        }

        private void ClearColor(float r, float g, float b, float a)
        {
            this.clearColor = new vec4(r, g, b, a);
        }

        public static void glClear(uint mask)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Clear(mask);
            }
        }

        private void Clear(uint mask)
        {

        }

    }
}
