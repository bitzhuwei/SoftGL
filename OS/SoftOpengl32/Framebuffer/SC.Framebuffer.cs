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
        /// generate framebuffer object names.
        /// </summary>
        /// <param name="count">Specifies the number of framebuffer object names to generate.</param>
        /// <param name="names">Specifies an array in which the generated framebuffer object names are stored.</param>
        public static void glGenFramebuffers(int count, uint[] names)
        {
            SoftGLRenderContext.glGenFramebuffers(count, names);
        }

        /// <summary>
        /// bind a framebuffer to a framebuffer target.
        /// </summary>
        /// <param name="target">Specifies the framebuffer target of the binding operation.</param>
        /// <param name="name">Specifies the name of the framebuffer object to bind.</param>
        public static void glBindFramebuffer(uint target, uint name)
        {
            SoftGLRenderContext.glBindFramebuffer(target, name);
        }

        /// <summary>
        /// determine if a name corresponds to a framebuffer object.
        /// </summary>
        /// <param name="name">Specifies a value that may be the name of a framebuffer object.</param>
        /// <returns></returns>
        public static bool glIsFramebuffer(uint name)
        {
            return SoftGLRenderContext.glIsFramebuffer(name);
        }


        /// <summary>
        /// delete framebuffer objects.
        /// </summary>
        /// <param name="count">Specifies the number of framebuffer objects to be deleted.</param>
        /// <param name="names">A pointer to an array containing <paramref name="count"/>​ framebuffer objects to be deleted.</param>
        public static void glDeleteFramebuffers(int count, uint[] names)
        {
            SoftGLRenderContext.glDeleteFramebuffers(count, names);
        }
    }
}
