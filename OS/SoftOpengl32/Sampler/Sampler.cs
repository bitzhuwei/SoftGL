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
        /// generate sampler object names.
        /// </summary>
        /// <param name="count">Specifies the number of sampler object names to generate.</param>
        /// <param name="names">Specifies an array in which the generated sampler object names are stored.</param>
        public static void glGenSamplers(int count, uint[] names)
        {
            SoftGLRenderContext context = StaticCalls.GetCurrentContextObj();
            if (context != null)
            {
                context.GenSamplers(count, names);
            }
        }

    }
}
