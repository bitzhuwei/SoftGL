using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glTexParameteri(uint target, uint pname, int param)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.TexParameteri(target, pname, param);
            }
        }

        private void TexParameteri(uint target, uint pname, int param)
        {
            Texture texture = this.GetCurrentTexture((TextureTarget)target);

        }
    }
}
