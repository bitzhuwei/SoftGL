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

        public static void glAttachShader(uint program, uint shader)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.AttachShader(program, shader);
            }
        }

        private void AttachShader(uint programName, uint shaderName)
        {
            if (programName == 0 || shaderName == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if ((!this.nameShaderProgramDict.ContainsKey(programName)) || (!this.nameShaderDict.ContainsKey(shaderName))) { SetLastError(ErrorCode.InvalidOperation); return; }

            ShaderProgram program = this.nameShaderProgramDict[programName];
            Shader shader = this.nameShaderDict[shaderName];
            if (program.AttachedShaders.Contains(shader)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.AttachedShaders.Add(shader);
        }

        public static void glLinkProgram(uint name)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.LinkProgram(name);
            }
        }

        private void LinkProgram(uint name)
        {
            if (name == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (!this.nameShaderProgramDict.ContainsKey(name)) { SetLastError(ErrorCode.InvalidOperation); return; }
            ShaderProgram program = this.nameShaderProgramDict[name];
            // TODO: GL_INVALID_OPERATION is generated if program​ is the currently active program object and transform feedback mode is active.

            program.Link();
        }

        public static int glGetAttribLocation(uint program, string name)
        {
            int result = -1;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.GetAttribLocation(program, name);
            }

            return result;
        }

        private int GetAttribLocation(uint program, string name)
        {
            if ((program == 0) || (!this.nameShaderProgramDict.ContainsKey(program))) { SetLastError(ErrorCode.InvalidOperation); return -1; }
            // TODO: GL_INVALID_OPERATION is generated if program​ has not been successfully linked.

            ShaderProgram shaderProgram = this.nameShaderProgramDict[program];
            return shaderProgram.GetAttribLocation(name);
        }

        public static void glUseProgram(uint program)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.UseProgram(program);
            }
        }

        private void UseProgram(uint name)
        {
            if (name == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (!this.nameShaderProgramDict.ContainsKey(name)) { SetLastError(ErrorCode.InvalidOperation); return; }
            ShaderProgram program = this.nameShaderProgramDict[name];
            if (program.LogInfo != string.Empty) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO: GL_INVALID_OPERATION is generated if transform feedback mode is active.

            this.currentShaderProgram = program;
        }
    }
}
