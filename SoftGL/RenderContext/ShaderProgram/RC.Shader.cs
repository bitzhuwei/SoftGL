using System;
using System.Collections.Generic;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextShaderName = 1;

        /// <summary>
        /// name -> ShaderProgram object.
        /// </summary>
        private readonly Dictionary<uint, Shader> nameShaderDict = new Dictionary<uint, Shader>();

        public static uint glCreateShader(uint shaderType)
        {
            uint id = 0;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                id = context.CreateShader((ShaderType)shaderType);
            }

            return id;
        }

        private uint CreateShader(ShaderType shaderType)
        {
            if (shaderType == 0) { SetLastError(ErrorCode.InvalidEnum); return 0; }

            uint id = nextShaderName;
            Shader shader = Shader.Create(shaderType, id);
            this.nameShaderDict.Add(id, shader);
            nextShaderName++;

            return id;
        }

        public static void glShaderSource(uint name, int count, string[] codes, int[] lengths)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.ShaderSource(name, count, codes, lengths);
            }
        }

        private void ShaderSource(uint name, int count, string[] codes, int[] lengths)
        {
            if (name == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (!this.nameShaderDict.ContainsKey(name)) { SetLastError(ErrorCode.InvalidOperation); return; }
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            Shader shader = this.nameShaderDict[name];
            // a dummy implementation.
            var builder = new System.Text.StringBuilder();
            foreach (var item in codes)
            {
                builder.AppendLine(item);
            }
            shader.Code = builder.ToString();
        }

        public static void glCompileShader(uint name)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.CompileShader(name);
            }
        }

        private void CompileShader(uint name)
        {
            if (name == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (!this.nameShaderDict.ContainsKey(name)) { SetLastError(ErrorCode.InvalidOperation); return; }

            Shader shader = this.nameShaderDict[name];
            shader.Compile();
        }

        public static void glGetShaderiv(uint name, uint pname, int[] pValues)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.GetShaderiv(name, (ShaderStatus)pname, pValues);
            }
        }

        private void GetShaderiv(uint name, ShaderStatus pname, int[] pValues)
        {
            if (name == 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (!this.nameShaderDict.ContainsKey(name)) { SetLastError(ErrorCode.InvalidOperation); return; }
            if (pname == 0) { SetLastError(ErrorCode.InvalidEnum); return; }

            Shader shader = this.nameShaderDict[name];
            shader.GetShaderStatus(pname, pValues);
        }
    }

    enum ShaderStatus : uint
    {
        ShaderType = GL.GL_SHADER_TYPE,
        DeleteStatus = GL.GL_DELETE_STATUS,
        CompileStatus = GL.GL_COMPILE_STATUS,
        InfoLogLength = GL.GL_INFO_LOG_LENGTH,
        ShaderSourceLength = GL.GL_SHADER_SOURCE_LENGTH
    }
}
