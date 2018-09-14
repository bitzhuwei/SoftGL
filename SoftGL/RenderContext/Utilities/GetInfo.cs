using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private static readonly Dictionary<uint, int[]> pValuesiDict = new Dictionary<uint, int[]>();
        private static readonly Dictionary<uint, float[]> pValuesfDict = new Dictionary<uint, float[]>();

        public static void glGetIntegerv(uint pname, int[] pValues)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.GetIntegerv(pname, pValues);
            }
        }

        private void GetIntegerv(uint pname, int[] pValues)
        {
            if (pValues == null) { return; }

            if (pValuesiDict.ContainsKey(pname))
            {
                int[] values = pValuesiDict[pname];
                for (int i = 0; i < pValues.Length; i++)
                {
                    pValues[i] = values[i];
                }
            }
            else
            {
                // TODO: do something..
            }
        }

        public static void glGetFloatv(uint pname, float[] pValues)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.GetFloatv(pname, pValues);
            }
        }

        private void GetFloatv(uint pname, float[] pValues)
        {
            if (pValues == null) { return; }

            if (pValuesfDict.ContainsKey(pname))
            {
                float[] values = pValuesfDict[pname];
                for (int i = 0; i < pValues.Length; i++)
                {
                    pValues[i] = values[i];
                }
            }
            else
            {
                // TODO: do something..
                if (pname == GL.GL_COLOR_CLEAR_VALUE)
                {
                    int length = pValues.Length;
                    vec4 c = this.clearColor;
                    if (length >= 1) { pValues[0] = c.x; } // r
                    if (length >= 2) { pValues[1] = c.y; } // g
                    if (length >= 3) { pValues[2] = c.z; } // b
                    if (length >= 4) { pValues[3] = c.w; } // a
                }
            }
        }

    }
}
