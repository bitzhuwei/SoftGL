using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static uint glCheckFramebufferStatus(uint target)
        {
            uint result = 0;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.CheckFramebufferStatus((BindFramebufferTarget)target);
            }

            return result;
        }

        private uint CheckFramebufferStatus(BindFramebufferTarget target)
        {
            if (!Enum.IsDefined(typeof(BindFramebufferTarget), target)) { SetLastError(ErrorCode.InvalidEnum); return 0; }

            // TODO: check this framebuffer.

            return GL.GL_FRAMEBUFFER_COMPLETE;
        }
    }
}
