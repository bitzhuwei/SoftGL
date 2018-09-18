using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    class FragmentShader : PipelineShader
    {
        public override int PipelineOrder { get { return 4; } }

        public FragmentShader(uint id) : base(ShaderType.FragmentShader, id) { }

        protected override string AfterCompile()
        {
            base.AfterCompile();

            return string.Empty;
        }

        private string FindOutVariables(Type vsType, Dictionary<string, OutVariable> dict)
        {
            dict.Clear();
            foreach (var item in vsType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
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
            foreach (var item in vsType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                object[] attribute = item.GetCustomAttributes(typeof(InAttribute), false);
                if (attribute != null && attribute.Length > 0) // this is a 'in ...;' field.
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

            return string.Empty;
        }

    }
}
