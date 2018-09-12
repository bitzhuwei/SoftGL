using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glTexParameterf(uint target, uint pname, float param)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.TexParameterf((TextureTarget)target, pname, param);
            }
        }

        private void TexParameterf(TextureTarget target, uint pname, float param)
        {
            if (target == 0) { SetLastError(ErrorCode.InvalidEnum); return; }

            Texture texture = this.GetCurrentTexture(target);
            if (texture != null) { texture.SetProperty(pname, param); }
        }
    }
}
