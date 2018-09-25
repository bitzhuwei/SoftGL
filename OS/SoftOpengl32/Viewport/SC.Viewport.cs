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
        // TODO: When a GL context is first attached to a window, width​ and height​ are set to the dimensions of that window.
        // How MakeCurrent(..) gets the width and height of device?
        /// <summary>
        /// set the viewport.
        /// </summary>
        /// <param name="x">Specify the lower left corner of the viewport rectangle, in pixels. The initial value is (0,0).</param>
        /// <param name="y">Specify the lower left corner of the viewport rectangle, in pixels. The initial value is (0,0).</param>
        /// <param name="width">Specify the width and height of the viewport. When a GL context is first attached to a window, width​ and height​ are set to the dimensions of that window.</param>
        /// <param name="height">Specify the width and height of the viewport. When a GL context is first attached to a window, width​ and height​ are set to the dimensions of that window.</param>
        public static void glViewport(int x, int y, int width, int height)
        {
            SoftGLRenderContext.glViewport(x, y, width, height);
        }
    }
}
