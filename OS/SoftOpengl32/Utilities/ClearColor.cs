using SoftGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftOpengl32
{
    partial class StaticCalls
    {
        /// <summary>
        /// specify clear values for the color buffers.
        /// </summary>
        /// <param name="r">Specify the red value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="g">Specify the green value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="b">Specify the blue value used when the color buffers are cleared. The initial value is 0.</param>
        /// <param name="a">Specify the alpha value used when the color buffers are cleared. The initial value is 0.</param>
        public static void glClearColor(float r, float g, float b, float a)
        {
            SoftGLRenderContext.glClearColor(r, g, b, a);
        }

        /// <summary>
        /// clear buffers to preset values.
        /// </summary>
        /// <param name="mask">Bitwise OR of masks that indicate the buffers to be cleared. The three masks are GL_COLOR_BUFFER_BIT, GL_DEPTH_BUFFER_BIT, and GL_STENCIL_BUFFER_BIT.</param>
        public static void glClear(uint mask)
        {
            SoftGLRenderContext.glClear(mask);
        }
    }
}
