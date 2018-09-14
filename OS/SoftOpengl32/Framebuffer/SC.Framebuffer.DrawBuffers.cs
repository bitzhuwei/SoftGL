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
        /// Specifies a list of color buffers to be drawn into.
        /// </summary>
        /// <param name="count">Specifies the number of buffers in <paramref name="buffers"/>.</param>
        /// <param name="buffers">Points to an array of symbolic constants specifying the buffers into which fragment colors or data values will be written.</param>
        public static uint glDrawBuffers(int count, uint[] buffers)
        {
            return SoftGLRenderContext.glDrawBuffers(count, buffers);
        }
    }
}
