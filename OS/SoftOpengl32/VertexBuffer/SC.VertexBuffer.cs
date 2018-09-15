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
        /// generate buffer object names.
        /// </summary>
        /// <param name="count">Specifies the number of buffer object names to be generated.</param>
        /// <param name="names">Specifies an array in which the generated buffer object names are stored.</param>
        public static void glGenBuffers(int count, uint[] names)
        {
            SoftGLRenderContext.glGenBuffers(count, names);
        }

        /// <summary>
        /// determine if a name corresponds to a buffer object.
        /// </summary>
        /// <param name="target">Specifies the target buffer object. The symbolic constant must be GL_ARRAY_BUFFER, GL_ATOMIC_COUNTER_BUFFER, GL_COPY_READ_BUFFER, GL_COPY_WRITE_BUFFER, GL_DRAW_INDIRECT_BUFFER, GL_DISPATCH_INDIRECT_BUFFER, GL_ELEMENT_ARRAY_BUFFER, GL_PIXEL_PACK_BUFFER, GL_PIXEL_UNPACK_BUFFER, GL_QUERY_BUFFER, GL_SHADER_STORAGE_BUFFER, GL_TEXTURE_BUFFER, GL_TRANSFORM_FEEDBACK_BUFFER, or GL_UNIFORM_BUFFER.</param>
        /// <param name="name">Specifies the name of a buffer object.</param>
        public static void glBindBuffer(uint target, uint name)
        {
            SoftGLRenderContext.glBindBuffer(target, name);
        }

        /// <summary>
        /// determine if a name corresponds to a buffer object.
        /// </summary>
        /// <param name="name">Specifies a value that may be the name of a buffer object.</param>
        public static bool glIsBuffer(uint name)
        {
            return SoftGLRenderContext.glIsBuffer(name);
        }

        /// <summary>
        /// delete named buffer objects.
        /// </summary>
        /// <param name="count">Specifies the number of buffer objects to be deleted.</param>
        /// <param name="names">Specifies an array of buffer objects to be deleted.</param>
        /// <returns></returns>
        public static void glDeleteBuffers(int count, uint[] names)
        {
            SoftGLRenderContext.glDeleteBuffers(count, names);
        }
    }
}
