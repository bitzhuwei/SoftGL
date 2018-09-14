using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glTexImage2D(ImageTarget target, int level, int internalFormat, int width, int height, int border, uint format, uint type, IntPtr data)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.TexImage2D(target, level, internalFormat, width, height, border, format, type, data);
            }
        }

        private void TexImage2D(ImageTarget target, int level, int internalFormat, int width, int height, int border, uint format, uint type, IntPtr data)
        {

            throw new NotImplementedException();
        }
    }

}
