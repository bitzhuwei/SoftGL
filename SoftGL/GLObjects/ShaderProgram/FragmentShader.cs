using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class FragmentShader : Shader
    {
        public FragmentShader(uint id) : base(ShaderType.FragmentShader, id) { }


        protected override string DoCompile()
        {
            throw new NotImplementedException();
        }
    }
}
