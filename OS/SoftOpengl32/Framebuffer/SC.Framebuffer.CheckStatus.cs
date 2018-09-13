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
        /// check the completeness status of a framebuffer.
        /// </summary>
        /// <param name="target">Specify the target of the framebuffer completeness check.</param>
        /// <returns></returns>
        public static uint glCheckFramebufferStatus(uint target)
        {
            return SoftGLRenderContext.glCheckFramebufferStatus(target);
        }
    }
}
