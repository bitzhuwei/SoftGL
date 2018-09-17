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
        /// Returns the location of a uniform variable.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="name">Points to a null terminated string containing the name of the uniform variable whose location is to be queried.</param>
        /// <returns></returns>
        public static int glGetUniformLocation(uint program, string name)
        {
            return SoftGLRenderContext.glGetUniformLocation(program, name);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniformMatrix4fv(int location, int count, bool transpose, float[] value)
        {
            SoftGLRenderContext.glUniformMatrix4fv(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniformMatrix3fv(int location, int count, bool transpose, float[] value)
        {
            SoftGLRenderContext.glUniformMatrix3fv(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of matrices that are to be modified. This should be 1 if the targeted uniform variable is not an array of matrices, and 1 or more if it is an array of matrices.</param>
        /// <param name="transpose">Specifies whether to transpose the matrix as the values are loaded into the uniform variable.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniformMatrix2fv(int location, int count, bool transpose, float[] value)
        {
            SoftGLRenderContext.glUniformMatrix2fv(location, count, transpose, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform4uiv(int location, int count, uint[] value)
        {
            SoftGLRenderContext.glUniform4uiv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform3uiv(int location, int count, uint[] value)
        {
            SoftGLRenderContext.glUniform3uiv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform2uiv(int location, int count, uint[] value)
        {
            SoftGLRenderContext.glUniform2uiv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform1uiv(int location, int count, uint[] value)
        {
            SoftGLRenderContext.glUniform1uiv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform4iv(int location, int count, int[] value)
        {
            SoftGLRenderContext.glUniform4iv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform3iv(int location, int count, int[] value)
        {
            SoftGLRenderContext.glUniform3iv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform2iv(int location, int count, int[] value)
        {
            SoftGLRenderContext.glUniform2iv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform1iv(int location, int count, int[] value)
        {
            SoftGLRenderContext.glUniform1iv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform4fv(int location, int count, float[] value)
        {
            SoftGLRenderContext.glUniform4fv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform3fv(int location, int count, float[] value)
        {
            SoftGLRenderContext.glUniform3fv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform2fv(int location, int count, float[] value)
        {
            SoftGLRenderContext.glUniform2fv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="count">Specifies the number of elements that are to be modified. This should be 1 if the targeted uniform variable is not an array, and 1 or more if it is an array.</param>
        /// <param name="value">Specifies a pointer to an array of count​ values that will be used to update the specified uniform variable.</param>
        public static void glUniform1fv(int location, int count, float[] value)
        {
            SoftGLRenderContext.glUniform1fv(location, count, value);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        public static void glUniform4ui(int location, uint v0, uint v1, uint v2, uint v3)
        {
            SoftGLRenderContext.glUniform4ui(location, v0, v1, v2, v3);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static void glUniform3ui(int location, uint v0, uint v1, uint v2)
        {
            SoftGLRenderContext.glUniform3ui(location, v0, v1, v2);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        public static void glUniform2ui(int location, uint v0, uint v1)
        {
            SoftGLRenderContext.glUniform2ui(location, v0, v1);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        public static void glUniform1ui(int location, uint v0)
        {
            SoftGLRenderContext.glUniform1ui(location, v0);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        public static void glUniform4i(int location, int v0, int v1, int v2, int v3)
        {
            SoftGLRenderContext.glUniform4i(location, v0, v1, v2, v3);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static void glUniform3i(int location, int v0, int v1, int v2)
        {
            SoftGLRenderContext.glUniform3i(location, v0, v1, v2);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        public static void glUniform2i(int location, int v0, int v1)
        {
            SoftGLRenderContext.glUniform2i(location, v0, v1);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        public static void glUniform1i(int location, int v0)
        {
            SoftGLRenderContext.glUniform1i(location, v0);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        public static void glUniform4f(int location, float v0, float v1, float v2, float v3)
        {
            SoftGLRenderContext.glUniform4f(location, v0, v1, v2, v3);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static void glUniform3f(int location, float v0, float v1, float v2)
        {
            SoftGLRenderContext.glUniform3f(location, v0, v1, v2);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        public static void glUniform2f(int location, float v0, float v1)
        {
            SoftGLRenderContext.glUniform2f(location, v0, v1);
        }

        /// <summary>
        /// Specify the value of a uniform variable for the current program object.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform value to be modified.</param>
        /// <param name="v0"></param>
        public static void glUniform1f(int location, float v0)
        {
            SoftGLRenderContext.glUniform1f(location, v0);
        }


    }
}
