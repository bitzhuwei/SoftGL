using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        /// <summary>
        /// ivec4(x, y, width, height)
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
            if (width < 0 || height < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            this.viewport.x = x; this.viewport.y = y;
            this.viewport.z = width; this.viewport.w = height;
        }
    }
}
