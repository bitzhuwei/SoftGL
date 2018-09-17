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
        /// 
        /// </summary>
        /// <param name="op"></param>
        /// <param name="value"></param>
        public static void glAccum(uint op, float value)
        {
            SoftGLRenderContext context = StaticCalls.GetCurrentContextObj();
            if (context != null)
            {
                //context.Accum(op, value);
                throw new NotImplementedException();
            }
        }
    }
}
