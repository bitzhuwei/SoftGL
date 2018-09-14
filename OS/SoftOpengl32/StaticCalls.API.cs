using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SoftGL;

namespace SoftOpengl32
{
    /// <summary>
    /// Initialization component calls Operating System. Operating System calls Hardware Driver. Hardware Driver is actually SoftGL.
    /// This is the 'opengl32.dll' in SoftGL environment.
    /// </summary>
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
