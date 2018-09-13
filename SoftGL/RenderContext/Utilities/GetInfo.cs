using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private static readonly Dictionary<uint, int[]> pValuesDict = new Dictionary<uint, int[]>();

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
            if (pValuesDict.ContainsKey(pname))
            {
                int[] values = pValuesDict[pname];
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

    }
}
