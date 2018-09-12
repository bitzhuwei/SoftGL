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
                context.TexParameteri((TextureTarget)target, pname, param);
            }
        }

        private void TexParameteri(TextureTarget target, uint pname, int param)
        {
            if (target == 0) { SetLastError(ErrorCode.InvalidEnum); return; }

            Texture texture = this.GetCurrentTexture(target);
            if (texture != null) { texture.SetProperty(pname, param); }
        }
    }
}
