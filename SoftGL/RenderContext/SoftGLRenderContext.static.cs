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
                dict.Add(GL.GL_MAX_DRAW_BUFFERS, new int[] { Framebuffer.maxColorAttachments });
                // TODO: add other constants..
            }
            // create default vertex array object.
            {
                var ids = new uint[1];
                glGenVertexArrays(ids.Length, ids);
                if (ids[0] != 0) { throw new Exception("ids[0] must be 0 here!"); }
                glBindVertexArray(0);
            }
        }
    }
}
