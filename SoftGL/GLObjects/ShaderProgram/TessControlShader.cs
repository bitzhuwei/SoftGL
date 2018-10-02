using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    class TessControlShader : PipelineShader
    {
        public override int PipelineOrder { get { return 1; } }

        public TessControlShader(uint id) : base(ShaderType.TessControlShader, id) { }

        protected override string AfterCompile()
        {
            throw new NotImplementedException();
        }
    }
}
