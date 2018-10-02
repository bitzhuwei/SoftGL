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
        /// generate vertex array object names.
        /// </summary>
        /// <param name="count">Specifies the number of vertex array object names to generate.</param>
        /// <param name="names">Specifies an array in which the generated vertex array object names are stored.</param>
        public static void glGenVertexArrays(int count, uint[] names)
        {
            SoftGLRenderContext.glGenVertexArrays(count, names);
        }

        /// <summary>
        /// bind a vertex array object.
        /// </summary>
        /// <param name="name">Specifies the name of the vertex array to bind.</param>
        public static void glBindVertexArray(uint name)
        {
            SoftGLRenderContext.glBindVertexArray(name);
        }

        /// <summary>
        /// determine if a name corresponds to a vertex array object.
        /// </summary>
        /// <param name="name">Specifies a value that may be the name of a vertex array object.</param>
        public static bool glIsVertexArray(uint name)
        {
            return SoftGLRenderContext.glIsVertexArray(name);
        }

        /// <summary>
        /// delete vertex array objects.
        /// </summary>
        /// <param name="count">Specifies the number of vertex array objects to be deleted.</param>
        /// <param name="names">Specifies the address of an array containing the n​ names of the objects to be deleted.</param>
        public static void glDeleteVertexArrays(int count, uint[] names)
        {
            SoftGLRenderContext.glDeleteVertexArrays(count, names);
        }
    }
}
