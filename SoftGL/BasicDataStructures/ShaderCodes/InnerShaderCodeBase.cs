using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public abstract unsafe class InnerShaderCodeBase : ICloneable
    {
        //public abstract void main();

        #region ICloneable 成员

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    static class InnerShaderTypeHelper
    {
        /// <summary>
        /// mapping from shader type to shader code type.
        /// </summary>
        /// <param name="shaderType"></param>
        /// <returns></returns>
        public static Type GetShaderCodeBaseType(this ShaderType shaderType)
        {
            Type type = null;
            switch (shaderType)
            {
                case ShaderType.VertexShader: type = typeof(InnerVertexShaderCodeBase); break;
                case ShaderType.TessControlShader: type = typeof(InnerTessControlShaderCodeBase); break;
                case ShaderType.TessEvaluationShader: type = typeof(InnerTessEvaluationShaderCodeBase); break;
                case ShaderType.GeometryShader: type = typeof(InnerGeometryShaderCodeBase); break;
                case ShaderType.FragmentShader: type = typeof(InnerFragmentShaderCodeBase); break;
                case ShaderType.ComputeShader: type = typeof(InnerComputeShaderCodeBase); break;
                default:
                    throw new NotImplementedException();
            }

            return type;
        }
    }
}
