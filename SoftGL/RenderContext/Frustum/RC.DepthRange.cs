using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private double depthRangeNear = 0.0;
        private double depthRangeFar = 1.0;

        public static void glDepthRange(double nearVal, double farVal)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DepthRange(nearVal, farVal);
            }
        }

        public static void glDepthRangef(float nearVal, float farVal)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DepthRange(nearVal, farVal);
            }
        }

        private void DepthRange(double nearVal, double farVal)
        {
            if (nearVal < 0.0) { nearVal = 0.0; }
            if (1.0 < nearVal) { nearVal = 1.0; }
            if (farVal < 0.0) { farVal = 0.0; }
            if (1.0 < farVal) { farVal = 1.0; }

            this.depthRangeNear = nearVal;
            this.depthRangeFar = farVal;
        }
    }
}
