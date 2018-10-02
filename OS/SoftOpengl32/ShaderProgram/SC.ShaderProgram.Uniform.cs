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
    }
}
