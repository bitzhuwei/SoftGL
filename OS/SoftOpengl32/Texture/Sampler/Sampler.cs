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
        /// generate sampler object names.
        /// </summary>
        /// <param name="count">Specifies the number of sampler object names to generate.</param>
        /// <param name="names">Specifies an array in which the generated sampler object names are stored.</param>
        public static void glGenSamplers(int count, uint[] names)
        {
            SoftGLRenderContext.glGenSamplers(count, names);
        }

        /// <summary>
        /// bind a named sampler to a texturing target.
        /// </summary>
        /// <param name="unit">Specifies the index of the texture unit to which the sampler is bound.</param>
        /// <param name="name">Specifies the name of a sampler.</param>
        public static void glBindSampler(uint unit, uint name)
        {
            SoftGLRenderContext.glBindSampler(unit, name);
        }

        /// <summary>
        /// delete named sampler objects.
        /// </summary>
        /// <param name="count">Specifies the number of sampler objects to be deleted.</param>
        /// <param name="names">Specifies an array of sampler objects to be deleted.</param>
        public static void glDeleteSamplers(int count, uint[] names)
        {
            SoftGLRenderContext.glDeleteSamplers(count, names);
        }
    }
}
