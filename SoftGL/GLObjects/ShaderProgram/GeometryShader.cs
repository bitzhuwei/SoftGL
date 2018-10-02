using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class GeometryShader : Shader
    {
        public GeometryShader(uint id) : base(ShaderType.GeometryShader, id) { }

        protected override string DoCompile()
        {
            throw new NotImplementedException();
        }
    }
}
