using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    class ComputeShader : Shader
    {
        public ComputeShader(uint id) : base(ShaderType.ComputeShader, id) { }

        protected override string AfterCompile()
        {
            throw new NotImplementedException();
        }

        public override object CreateCodeInstance()
        {
            throw new NotImplementedException();
        }
    }
}
