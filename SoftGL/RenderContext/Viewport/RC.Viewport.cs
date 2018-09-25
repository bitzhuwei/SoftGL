using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        /// <summary>
        /// vec4(x, y, width, height)
        /// </summary>
        private ivec4 viewport;

        public static void glViewport(int x, int y, int width, int height)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Viewport(x, y, width, height);
            }
        }

        private void Viewport(int x, int y, int width, int height)
        {
            this.viewport.x = x; this.viewport.y = y;
            this.viewport.z = width; this.viewport.w = height;
        }
    }
}
