using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    class VertexShader : PipelineShader
    {
        public override int PipelineOrder { get { return 0; } }

        public VertexShader(uint id) : base(ShaderType.VertexShader, id) { }

        public int GetAttribLocation(string name)
        {
            int result = -1;
            if (this.InfoLog.Length > 0) { return result; }
            Dictionary<string, InVariable> dict = this.inVariableDict;
            if (dict == null) { return result; }
            InVariable v = null;
            if (dict.TryGetValue(name, out v))
            {
                result = (int)v.location;
            }

            return result;
        }

        protected override string AfterCompile()
        {
            base.AfterCompile();

            return string.Empty;
        }
    }
}
