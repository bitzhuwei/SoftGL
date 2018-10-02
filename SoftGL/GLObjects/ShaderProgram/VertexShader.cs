using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    class VertexShader : Shader
    {
        private Dictionary<string, InVariable> inVariableDict;
        public VertexShader(uint id) : base(ShaderType.VertexShader, id) { }

        public int GetAttribLocation(string name)
        {
            int result = -1;
            if (this.infoLog.Length > 0) { return result; }
            Dictionary<string, InVariable> dict = this.inVariableDict;
            if (dict == null) { return result; }
            InVariable v = null;
            if (dict.TryGetValue(name, out v))
            {
                result = (int)v.location;
            }

            return result;
        }

        protected override string DoCompile()
        {
            var codeProvider = new CSharpCodeProvider();
            var compParameters = new CompilerParameters();
            CompilerResults res = codeProvider.CompileAssemblyFromSource(compParameters, this.Code);
            if (res.Errors.Count > 0) { return DumpLog(res); }

            Type vsType = null; // type of vertex shader code.
            Assembly asm = res.CompiledAssembly;
            Type[] types = asm.GetTypes();
            foreach (var item in types)
            {
                if (item.BaseType == typeof(VertexShaderCode))
                {
                    vsType = item;
                    break;
                }
            }

            var dict = new Dictionary<string, InVariable>(); uint nextLoc = 0;
            foreach (var item in vsType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                object[] inAttribute = item.GetCustomAttributes(typeof(InAttribute), false);
                if (inAttribute != null && inAttribute.Length > 0) // this is a 'in ...;' field.
                {
                    var v = new InVariable(item);
                    object[] locationAttribute = item.GetCustomAttributes(typeof(LocationAttribute), false);
                    if (locationAttribute != null && locationAttribute.Length > 0) // (location = ..) in ...;
                    {
                        uint loc = (locationAttribute[0] as LocationAttribute).location;
                        if (loc < nextLoc) { return "location error in VertexShader!"; }
                        v.location = loc;
                        nextLoc = loc + 1;
                    }
                    else
                    {
                        v.location = nextLoc++;
                    }
                    dict.Add(item.Name, v);
                }
            }
            this.inVariableDict = dict;

            return string.Empty;
        }

        class InVariable
        {
            public uint location;
            public readonly FieldInfo field;

            public InVariable(FieldInfo field)
            {
                this.field = field;
            }

            public override string ToString()
            {
                return string.Format("f:{0}, loc:{1}", this.field, this.location);
            }
        }
    }
}
