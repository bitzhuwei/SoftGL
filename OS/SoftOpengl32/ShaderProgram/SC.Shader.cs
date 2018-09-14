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
        /// Creates a shader object.
        /// </summary>
        /// <param name="shaderType">Specifies the type of shader to be created. Must be one of GL_VERTEX_SHADER, GL_TESS_CONTROL_SHADER, GL_TESS_EVALUATION_SHADER, GL_GEOMETRY_SHADER, GL_FRAGMENT_SHADER, or GL_COMPUTE_SHADER.</param>
        /// <returns></returns>
        public static uint glCreateShader(uint shaderType)
        {
            return SoftGLRenderContext.glCreateShader(shaderType);
        }

        /// <summary>
        /// Replaces the source code in a shader object.
        /// </summary>
        /// <param name="shader">Specifies the handle of the shader object whose source code is to be replaced.</param>
        /// <param name="count">Specifies the number of elements in the string​ and length​ arrays.</param>
        /// <param name="codes">Specifies an array of pointers to strings containing the source code to be loaded into the shader.</param>
        /// <param name="lengths">Specifies an array of code lengths.</param>
        public static void glShaderSource(uint shader, int count, string[] codes, int[] lengths)
        {
            SoftGLRenderContext.glShaderSource(shader, count, codes, lengths);
        }
    }
}
