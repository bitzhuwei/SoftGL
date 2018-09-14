using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    public partial class SoftGLRenderContext
    {
        static SoftGLRenderContext()
        {
            // for glGetIntegerv(..).
            {
                Dictionary<uint, int[]> dict = SoftGLRenderContext.pValuesiDict;
                dict.Add(GL.GL_MAX_COLOR_ATTACHMENTS, new int[] { Framebuffer.maxColorAttachments });
                // TODO: add other constants..
            }
        }
    }
}
