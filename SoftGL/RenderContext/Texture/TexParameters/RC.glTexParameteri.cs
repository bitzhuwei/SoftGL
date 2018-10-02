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
                context.TexParameteri((BindTextureTarget)target, pname, param);
            }
        }

        private void TexParameteri(BindTextureTarget target, uint pname, int param)
        {
            if (!Enum.IsDefined(typeof(BindTextureTarget), target)) { SetLastError(ErrorCode.InvalidEnum); return; }

            Texture texture = this.GetCurrentTexture(target);
            if (texture != null) { texture.SetProperty(pname, param); }
        }
    }
}
