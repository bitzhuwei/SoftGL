using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class StaticCalls
    {
        /// <summary>
        /// specify mapping of depth values from normalized device coordinates to window coordinates.
        /// </summary>
        /// <param name="nearVal">Specifies the mapping of the near clipping plane to window coordinates. The initial value is 0.</param>
        /// <param name="farVal">Specifies the mapping of the far clipping plane to window coordinates. The initial value is 1.</param>
        public static void glDepthRange(double nearVal, double farVal)
        {
            SoftGLRenderContext.glDepthRange(nearVal, farVal);
        }
    }
}
