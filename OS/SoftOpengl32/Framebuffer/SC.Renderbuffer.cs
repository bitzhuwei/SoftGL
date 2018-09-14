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
        /// generate renderbuffer object names.
        /// </summary>
        /// <param name="count">Specifies the number of renderbuffer object names to generate.</param>
        /// <param name="names">Specifies an array in which the generated renderbuffer object names are stored.</param>
        public static void glGenRenderbuffers(int count, uint[] names)
        {
            SoftGLRenderContext.glGenRenderbuffers(count, names);
        }

        /// <summary>
        /// bind a renderbuffer to a renderbuffer target.
        /// </summary>
        /// <param name="target">Specifies the renderbuffer target of the binding operation. target​ must be GL_RENDERBUFFER.</param>
        /// <param name="name">Specifies the name of the renderbuffer object to bind.</param>
        public static void glBindRenderbuffer(uint target, uint name)
        {
            SoftGLRenderContext.glBindRenderbuffer(target, name);
        }

        /// <summary>
        /// determine if a name corresponds to a renderbuffer object.
        /// </summary>
        /// <param name="name">Specifies a value that may be the name of a renderbuffer object.</param>
        /// <returns></returns>
        public static bool glIsRenderbuffer(uint name)
        {
            return SoftGLRenderContext.glIsRenderbuffer(name);
        }

        /// <summary>
        /// establish data storage, format and dimensions of a renderbuffer object's image.
        /// </summary>
        /// <param name="target">Specifies a binding to which the target of the allocation and must be GL_RENDERBUFFER.</param>
        /// <param name="internalformat">Specifies the internal format to use for the renderbuffer object's image.</param>
        /// <param name="width">Specifies the width of the renderbuffer, in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer, in pixels.</param>
        public static void glRenderbufferStorage(uint target, uint internalformat, int width, int height)
        {
            SoftGLRenderContext.glRenderbufferStorage(target, internalformat, width, height);
        }

        /// <summary>
        /// delete renderbuffer objects.
        /// </summary>
        /// <param name="count">Specifies the number of renderbuffer objects to be deleted.</param>
        /// <param name="names">A pointer to an array containing <paramref name="count"/>​ renderbuffer objects to be deleted.</param>
        public static void glDeleteRenderbuffers(int count, uint[] names)
        {
            SoftGLRenderContext.glDeleteRenderbuffers(count, names);
        }
    }
}
