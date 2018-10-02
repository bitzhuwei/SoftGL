using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    class FragmentShader : Shader
    {
        public FragmentShader(uint id) : base(ShaderType.FragmentShader, id) { }

        protected override string AfterCompile(Assembly assembly)
        {
            Type codeType = this.FindShaderCodeType(assembly, typeof(FragmentShaderCode));
            if (codeType == null) { return "No FragmentShader found!"; }

            {
                Dictionary<string, UniformVariable> dict;
                string result = FindUniformVariables(codeType, out dict);
                if (result != string.Empty) { return result; }
                this.uniformVariableDict = dict;
            }

            return string.Empty;
        }
    }
}
