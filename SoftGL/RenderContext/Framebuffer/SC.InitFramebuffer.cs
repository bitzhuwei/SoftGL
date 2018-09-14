using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    /// <summary>
    /// creates render device and render context.
    /// </summary>
    public partial class SoftGLRenderContext : GLRenderContext
    {
        private void InitDefaultFramebuffer()
        {
            // Create the default framebuffer object and make it the current one.
            var ids = new uint[1];
            this.GenFramebuffers(1, ids);
            if (ids[0] != 0) { throw new Exception("This framebuffer must be 0!"); }
            this.BindFramebuffer(BindFramebufferTarget.Framebuffer, ids[0]);

            throw new NotImplementedException();
        }
    }
}
