using System;
using System.Collections.Generic;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextShaderProgramName = 1;

        /// <summary>
        /// name -> ShaderProgram object.
        /// </summary>
        private readonly Dictionary<uint, ShaderProgram> nameShaderProgramDict = new Dictionary<uint, ShaderProgram>();

        private ShaderProgram currentShaderProgram = null;

        public static uint glCreateProgram()
        {
            uint id = 0;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                id = context.CreateProgram();
            }

            return id;
        }

        private uint CreateProgram()
        {
            uint name = nextShaderProgramName;
            var program = new ShaderProgram(name);
            this.nameShaderProgramDict.Add(name, program);
            nextShaderProgramName++;

            return name;
        }
    }
}
