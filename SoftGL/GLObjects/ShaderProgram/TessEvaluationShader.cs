using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    class TessEvaluationShader : PipelineShader
    {
        public override int PipelineOrder { get { return 2; } }

        public TessEvaluationShader(uint id) : base(ShaderType.TessEvaluationShader, id) { }

        protected override string AfterCompile()
        {
            throw new NotImplementedException();
        }

        public override InnerShaderCode PostProcess()
        {
            throw new NotImplementedException();
        }
    }
}
