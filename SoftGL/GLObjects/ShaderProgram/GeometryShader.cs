using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SoftGL
{
    class GeometryShader : PipelineShader
    {
        public override int PipelineOrder { get { return 3; } }

        public GeometryShader(uint id) : base(ShaderType.GeometryShader, id) { }
    }
}
