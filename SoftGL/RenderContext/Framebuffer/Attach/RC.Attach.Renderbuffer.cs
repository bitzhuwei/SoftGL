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
            Dictionary<uint, Renderbuffer> dict = this.nameRenderbufferDict;
            if ((renderbufferName != 0) && (!dict.ContainsKey(renderbufferName))) { SetLastError(ErrorCode.InvalidOperation); return; }

            Renderbuffer renderbuffer = null;
            if (renderbufferName != 0)
            {
                if (!dict.TryGetValue(renderbufferName, out renderbuffer))
                { SetLastError(ErrorCode.InvalidOperation); return; }
            }

            Framebuffer framebuffer = this.currentFramebuffer;
            if (framebuffer == null) { return; }
            if (framebuffer.Target != target)
            {
                // TODO: what should I do? Or should multiple current framebufer object exist?
            }

            if (attachmentPoint == GL.GL_DEPTH_ATTACHMENT)
            {
                framebuffer.DepthbufferAttachment = renderbuffer;
            }
            else if (attachmentPoint == GL.GL_STENCIL_ATTACHMENT)
            {
                framebuffer.StencilbufferAttachment = renderbuffer;
            }
            else if (attachmentPoint == GL.GL_DEPTH_STENCIL_ATTACHMENT)
            {
                framebuffer.DepthbufferAttachment = renderbuffer;
                framebuffer.StencilbufferAttachment = renderbuffer;
            }
            else // color attachment points.
            {
                if (attachmentPoint < GL.GL_COLOR_ATTACHMENT0) { SetLastError(ErrorCode.InvalidOperation); return; }
                uint index = attachmentPoint - GL.GL_COLOR_ATTACHMENT0;
                if (framebuffer.ColorbufferAttachments.Length <= index) { SetLastError(ErrorCode.InvalidOperation); return; }

                framebuffer.ColorbufferAttachments[index] = renderbuffer;
            }
        }
    }
}
