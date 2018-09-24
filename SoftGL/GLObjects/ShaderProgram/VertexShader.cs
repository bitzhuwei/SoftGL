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
    class VertexShader : PipelineShader
    {
        public OutVariable gl_PositionVar { get; private set; }

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
            string result = base.AfterCompile();
            if (result != string.Empty) { return result; }

            // find the "out vec4 gl_Position;" variable.
            FieldInfo fieldInfo = this.codeType.GetField("gl_Position");
            if (fieldInfo == null) { result = "gl_Position not found!"; return result; }
            object[] attribute = fieldInfo.GetCustomAttributes(typeof(OutAttribute), false);
            if (attribute != null && attribute.Length > 0) // this is a 'in ...;' field.
            {
                this.gl_PositionVar = new OutVariable(fieldInfo);
            }
            else
            {
                result = "gl_Position has no [Out] attribute.";
            }

            return result;
        }
    }
}
