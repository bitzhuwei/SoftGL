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
        /// Creates a program object.
        /// </summary>
        /// <returns></returns>
        public static uint glCreateProgram()
        {
            return SoftGLRenderContext.glCreateProgram();
        }

        /// <summary>
        /// Attaches a shader object to a program object.
        /// </summary>
        /// <param name="program">Specifies the program object to which a shader object will be attached.</param>
        /// <param name="shader">Specifies the shader object that is to be attached.</param>
        public static void glAttachShader(uint program, uint shader)
        {
            SoftGLRenderContext.glAttachShader(program, shader);
        }

        /// <summary>
        /// Links a program object.
        /// </summary>
        /// <param name="name">Specifies the handle of the program object to be linked.</param>
        public static void glLinkProgram(uint name)
        {
            SoftGLRenderContext.glLinkProgram(name);
        }

        /// <summary>
        /// Returns the location of an attribute variable.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="name">Points to a null terminated string containing the name of the attribute variable whose location is to be queried.</param>
        public static int glGetAttribLocation(uint program, string name)
        {
            return SoftGLRenderContext.glGetAttribLocation(program, name);
        }

        /// <summary>
        /// Installs a program object as part of current rendering state.
        /// </summary>
        /// <param name="program">Specifies the handle of the program object whose executables are to be used as part of current rendering state.</param>
        public static void glUseProgram(uint program)
        {
            SoftGLRenderContext.glUseProgram(program);
        }
    }
}
