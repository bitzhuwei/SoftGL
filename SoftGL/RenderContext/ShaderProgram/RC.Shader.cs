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
