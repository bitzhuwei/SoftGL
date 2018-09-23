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
    class FragmentShader : PipelineShader
    {
        public override int PipelineOrder { get { return 4; } }

        public FragmentShader(uint id) : base(ShaderType.FragmentShader, id) { }

    }
}
