using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SoftGL;

namespace SoftOpengl32
{
    public partial class StaticCalls
    {
        /// <summary>
        /// Creates a program object.
        /// </summary>
        /// <returns></returns>
        public static uint glCreateProgram()
        {
            return SoftGLRenderContext.glCreateProgram();
        }
    }
}
