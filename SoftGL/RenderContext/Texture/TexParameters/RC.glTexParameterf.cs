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
                context.TexParameterf((BindTextureTarget)target, pname, param);
            }
        }

        private void TexParameterf(BindTextureTarget target, uint pname, float param)
        {
            if (!Enum.IsDefined(typeof(BindTextureTarget), target)) { SetLastError(ErrorCode.InvalidEnum); return; }

            Texture texture = this.GetCurrentTexture(target);
            if (texture != null) { texture.SetProperty(pname, param); }
        }
    }
}
