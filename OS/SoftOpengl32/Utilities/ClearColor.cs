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
    }
}
