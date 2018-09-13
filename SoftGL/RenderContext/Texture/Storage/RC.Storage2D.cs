using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glTexStorage2D(TexStorageTarget target, int levels, uint internalformat, int width, int height)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.TexStorage2D(target, levels, internalformat, width, height);
            }
        }

        private void TexStorage2D(TexStorageTarget target, int levels, uint internalformat, int width, int height)
        {
            throw new NotImplementedException();
        }
    }

}
