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
    abstract class PipelineShader : Shader
    {
        public abstract int PipelineOrder { get; }

        public readonly Dictionary<string, InVariable> inVariableDict = new Dictionary<string, InVariable>();
        public readonly Dictionary<string, OutVariable> outVariableDict = new Dictionary<string, OutVariable>();

        public PipelineShader(ShaderType shaderType, uint id) : base(shaderType, id) { }

        public override object CreateCodeInstance()
        {
            return Activator.CreateInstance(this.codeType);
        }

        protected override string AfterCompile()
        {
            {
                string result = FindInVariables(this.codeType, this.inVariableDict);
                if (result != string.Empty) { return result; }
            }
            {
                string result = FindOutVariables(this.codeType, this.outVariableDict);
                if (result != string.Empty) { return result; }
            }

            return string.Empty;
        }

        private string FindOutVariables(Type vsType, Dictionary<string, OutVariable> dict)
        {
            dict.Clear();
            foreach (var item in vsType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                object[] attribute = item.GetCustomAttributes(typeof(OutAttribute), false);
                if (attribute != null && attribute.Length > 0) // this is a 'in ...;' field.
                {
                    var v = new OutVariable(item);
                    dict.Add(item.Name, v);
                }
            }

            return string.Empty;
        }

        private string FindInVariables(Type vsType, Dictionary<string, InVariable> dict)
        {
            dict.Clear();
            uint nextLoc = 0;
            foreach (var item in vsType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                object[] attribute = item.GetCustomAttributes(typeof(InAttribute), false);
                if (attribute != null && attribute.Length > 0) // this is a 'in ...;' field.
                {
                    var v = new InVariable(item);
                    object[] locationAttribute = item.GetCustomAttributes(typeof(LocationAttribute), false);
                    if (locationAttribute != null && locationAttribute.Length > 0) // (location = ..) in ...;
                    {
                        uint loc = (locationAttribute[0] as LocationAttribute).location;
                        if (loc < nextLoc) { return string.Format("location error in {0}!", this.GetType().Name); }
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

            return string.Empty;
        }

    }
}
