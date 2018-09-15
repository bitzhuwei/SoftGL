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

        /// <summary>
        /// creates and initializes a buffer object's data store.
        /// </summary>
        /// <param name="target">Specifies the target buffer object. The symbolic constant must be GL_ARRAY_BUFFER, GL_ATOMIC_COUNTER_BUFFER, GL_COPY_READ_BUFFER, GL_COPY_WRITE_BUFFER, GL_DRAW_INDIRECT_BUFFER, GL_DISPATCH_INDIRECT_BUFFER, GL_ELEMENT_ARRAY_BUFFER, GL_PIXEL_PACK_BUFFER, GL_PIXEL_UNPACK_BUFFER, GL_QUERY_BUFFER, GL_SHADER_STORAGE_BUFFER, GL_TEXTURE_BUFFER, GL_TRANSFORM_FEEDBACK_BUFFER, or GL_UNIFORM_BUFFER.</param>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization, or NULL if no data is to be copied.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store. The symbolic constant must be GL_STREAM_DRAW, GL_STREAM_READ, GL_STREAM_COPY, GL_STATIC_DRAW, GL_STATIC_READ, GL_STATIC_COPY, GL_DYNAMIC_DRAW, GL_DYNAMIC_READ, or GL_DYNAMIC_COPY.</param>
        public static void glBufferData(uint target, int size, IntPtr data, uint usage)
        {
            SoftGLRenderContext.glBufferData(target, size, data, usage);
        }
    }
}
