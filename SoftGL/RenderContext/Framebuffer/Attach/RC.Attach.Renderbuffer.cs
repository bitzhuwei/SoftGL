using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glFramebufferRenderbuffer(uint target, uint attachmentPoint, uint renderbufferTarget, uint renderbufferName)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.FramebufferRenderbuffer((BindFramebufferTarget)target, attachmentPoint, renderbufferTarget, renderbufferName);
            }
        }

        private void FramebufferRenderbuffer(BindFramebufferTarget target, uint attachmentPoint, uint renderbufferTarget, uint renderbufferName)
        {
            if (target == 0) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (renderbufferTarget != GL.GL_RENDERBUFFER) { SetLastError(ErrorCode.InvalidEnum); return; }
            // TODO: GL_INVALID_OPERATION is generated if zero is bound to target.

            Framebuffer framebuffer = this.currentFramebuffers[target];
            if (framebuffer == null) { return; }



            throw new NotImplementedException();
        }
    }
}
