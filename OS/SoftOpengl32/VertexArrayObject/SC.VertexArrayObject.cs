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

        /// <summary>
        /// define an array of generic vertex attribute data.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA is accepted by glVertexAttribPointer. The initial value is 4.</param>
        /// <param name="type">Specifies the data type of each component in the array. The different functions take different values. This method takes GL_BYTE, GL_UNSIGNED_BYTE, GL_SHORT, GL_UNSIGNED_SHORT, GL_INT, and GL_UNSIGNED_INT, GL_HALF_FLOAT, GL_FLOAT, GL_DOUBLE, GL_FIXED, GL_INT_2_10_10_10_REV, GL_UNSIGNED_INT_2_10_10_10_REV, and GL_UNSIGNED_INT_10F_11F_11F_REV.
        /// The initial value is GL_FLOAT..</param>
        /// <param name="normalized">Specifies whether fixed-point data values should be normalized (GL_TRUE) or converted directly as fixed-point values (GL_FALSE) when they are accessed.</param>
        /// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. If stride​ is 0, the generic vertex attributes are understood to be tightly packed in the array. The initial value is 0.</param>
        /// <param name="pointer">Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER target. The initial value is 0.</param>
        public static void glVertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        {
            SoftGLRenderContext.glVertexAttribPointer(index, size, type, normalized, stride, pointer);
        }


        /// <summary>
        /// define an array of generic vertex attribute data.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA is accepted by glVertexAttribPointer. The initial value is 4.</param>
        /// <param name="type">Specifies the data type of each component in the array. The different functions take different values. 
        /// This method takes GL_BYTE, GL_UNSIGNED_BYTE, GL_SHORT, GL_UNSIGNED_SHORT, GL_INT, and GL_UNSIGNED_INT.
        /// The initial value is GL_FLOAT..</param>
        /// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. If stride​ is 0, the generic vertex attributes are understood to be tightly packed in the array. The initial value is 0.</param>
        /// <param name="pointer">Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER target. The initial value is 0.</param>
        public static void glVertexAttribIPointer(uint index, int size, uint type, int stride, IntPtr pointer)
        {
            SoftGLRenderContext.glVertexAttribIPointer(index, size, type, stride, pointer);
        }

        /// <summary>
        /// define an array of generic vertex attribute data.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA is accepted by glVertexAttribPointer. The initial value is 4.</param>
        /// <param name="type">Specifies the data type of each component in the array. The different functions take different values. 
        /// This method takes only GL_DOUBLE. The initial value is GL_FLOAT..</param>
        /// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. If stride​ is 0, the generic vertex attributes are understood to be tightly packed in the array. The initial value is 0.</param>
        /// <param name="pointer">Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER target. The initial value is 0.</param>
        public static void glVertexAttribLPointer(uint index, int size, uint type, int stride, IntPtr pointer)
        {
            SoftGLRenderContext.glVertexAttribLPointer(index, size, type, stride, pointer);
        }

        /// <summary>
        /// Enable a generic vertex attribute array.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be enabled or disabled.</param>
        public static void glEnableVertexAttribArray(uint index)
        {
            SoftGLRenderContext.glEnableVertexAttribArray(index);
        }

        /// <summary>
        /// Disable a generic vertex attribute array.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be enabled or disabled.</param>
        public static void glDisableVertexAttribArray(uint index)
        {
            SoftGLRenderContext.glDisableVertexAttribArray(index);
        }

    }
}
