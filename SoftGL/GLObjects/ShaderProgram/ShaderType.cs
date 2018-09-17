﻿using System;
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
                case ShaderType.VertexShader: type = typeof(VertexShaderCode); break;
                case ShaderType.TessControlShader: type = typeof(TessControlShaderCode); break;
                case ShaderType.TessEvaluationShader: type = typeof(TessEvaluationShaderCode); break;
                case ShaderType.GeometryShader: type = typeof(GeometryShaderCode); break;
                case ShaderType.FragmentShader: type = typeof(FragmentShaderCode); break;
                case ShaderType.ComputeShader: type = typeof(ComputeShaderCode); break;
                default:
                    throw new NotImplementedException();
            }

            return type;
        }
    }
}