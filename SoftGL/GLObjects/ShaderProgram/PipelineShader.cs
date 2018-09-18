using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    abstract class PipelineShader : Shader
    {
        public abstract int PipelineOrder { get; }

        public PipelineShader(ShaderType shaderType, uint id) : base(shaderType, id) { }

    }
}
