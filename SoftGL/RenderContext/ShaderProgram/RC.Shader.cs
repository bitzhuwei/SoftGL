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

            uint name = nextShaderName;
            var shader = new Shader(shaderType, name);
            this.nameShaderDict.Add(name, shader);
            nextShaderName++;

            return name;
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

    }

    enum ShaderType : uint
    {
        VertexShader = GL.GL_VERTEX_SHADER,
        TessControlShader = GL.GL_TESS_CONTROL_SHADER,
        TessEvaluationShader = GL.GL_TESS_EVALUATION_SHADER,
        GeometryShader = GL.GL_GEOMETRY_SHADER,
        FragmentShader = GL.GL_FRAGMENT_SHADER,
        ComputeShader = GL.GL_COMPUTE_SHADER
    }
}
