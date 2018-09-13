using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SoftGL;

namespace SoftOpengl32
{
    partial class StaticCalls
    {
        /// <summary>
        /// attach a renderbuffer as a logical buffer to the currently bound framebuffer object.
        /// </summary>
        /// <param name="target">Specifies the framebuffer target. target​ must be GL_DRAW_FRAMEBUFFER, GL_READ_FRAMEBUFFER, or GL_FRAMEBUFFER. GL_FRAMEBUFFER is equivalent to GL_DRAW_FRAMEBUFFER.</param>
        /// <param name="attachmentPoint">Specifies the attachment point of the framebuffer.</param>
        /// <param name="renderbufferTarget">Specifies the renderbuffer target and must be GL_RENDERBUFFER.</param>
        /// <param name="renderbufferName">Specifies the name of an existing renderbuffer object of type renderbuffertarget​ to attach.</param>
        public static void glFramebufferRenderbuffer(uint target, uint attachmentPoint, uint renderbufferTarget, uint renderbufferName)
        {
            SoftGLRenderContext.glFramebufferRenderbuffer(target, attachmentPoint, renderbufferTarget, renderbufferName);
        }
    }
}
