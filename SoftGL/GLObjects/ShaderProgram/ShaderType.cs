using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    enum ShaderType : uint
    {
        VertexShader = GL.GL_VERTEX_SHADER,
        TessControlShader = GL.GL_TESS_CONTROL_SHADER,
        TessEvaluationShader = GL.GL_TESS_EVALUATION_SHADER,
        GeometryShader = GL.GL_GEOMETRY_SHADER,
        FragmentShader = GL.GL_FRAGMENT_SHADER,
        ComputeShader = GL.GL_COMPUTE_SHADER
    }

    static class ShaderTypeHelper
    {
        /// <summary>
        /// mapping from shader type to shader code type.
        /// </summary>
        /// <param name="shaderType"></param>
        /// <returns></returns>
        public static Type GetShaderCodeType(this ShaderType shaderType)
        {
            Type type = null;
            switch (shaderType)
            {
                case ShaderType.VertexShader: type = typeof(VertexCode); break;
                case ShaderType.TessControlShader: type = typeof(TessControlCode); break;
                case ShaderType.TessEvaluationShader: type = typeof(TessEvaluationShaderCode); break;
                case ShaderType.GeometryShader: type = typeof(GeometryCode); break;
                case ShaderType.FragmentShader: type = typeof(FragmentCode); break;
                case ShaderType.ComputeShader: type = typeof(ComputeCode); break;
                default:
                    throw new NotImplementedException();
            }

            return type;
        }

        public static string GetIDName(this ShaderType shaderType)
        {
            string result = string.Empty;
            switch (shaderType)
            {
                case ShaderType.VertexShader: result = "gl_VertexID"; break;
                case ShaderType.TessControlShader:
                    break;
                case ShaderType.TessEvaluationShader:
                    break;
                case ShaderType.GeometryShader:
                    break;
                case ShaderType.FragmentShader: result = "fragmentID"; break;
                case ShaderType.ComputeShader:
                    break;
                default:
                    break;
            }

            if (result == string.Empty) { throw new NotImplementedException(); }

            return result;
        }
    }
}
