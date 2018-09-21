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

        public override InnerShaderCode PostProcess()
        {
            string innerCode = GetInnerCode();
            throw new NotImplementedException();
        }

        private string GetInnerCode()
        {
            var builder = new StringBuilder();
            int tabCount = 0;
            builder.PrintLine(string.Format("namespace SoftGL"), tabCount);
            builder.PrintLine("{", tabCount); tabCount++;
            {
                builder.PrintLine(string.Format("unsafe class InnerVert : InnerVertexShaderCode"), tabCount);
                builder.PrintLine("{", tabCount); tabCount++;
                {
                    // GLSL: in vec3 inPosition;
                    // [In] vec3 inPosition { get { return ((vec3*)(vboData[gl_VertexID * vertexByteLength + 0]))[0]; } }
                    InVariable[] inVars = this.inVariableDict.Values.ToArray();
                    uint offset = 0;
                    for (int i = 0; i < inVars.Length; i++)
                    {
                        InVariable inVar = inVars[i];
                        string typeName = inVar.field.FieldType.Name;
                        string varName = inVar.field.Name;
                        builder.PrintLine(string.Format("[In] {0} {1} {{ get {{ return (({0}*)(vboData[gl_VertexID * vertexByteLength + {2}]))[0]; }} }}", typeName, varName, offset), tabCount);
                        offset += inVar.field.FieldType.ByteSize();
                    }
                }
                {
                    // GLSL: uniform float alpha;
                    // [Uniform] mat4 projectionMat { get { return ((mat4*)uniformData[0])[0]; } }
                    UniformVariable[] uniforms = this.UniformVariableDict.Values.ToArray();
                    uint offset = 0;
                    for (int i = 0; i < uniforms.Length; i++)
                    {
                        UniformVariable uniform = uniforms[i];
                        string typeName = uniform.field.FieldType.Name;
                        string varName = uniform.field.Name;
                        builder.PrintLine(string.Format("[Uniform] {0} {1} {{ get {{ return (({0} *)uniformData[{2}])[0]; }} }}", typeName, varName, offset), tabCount);
                        offset += uniform.field.FieldType.ByteSize();
                    }
                }
                {
                    // GLSL: out vec3 passColor;
                    // [Out] vec3 passColor 
                    // {
                    //     get { return ((vec3*)(vsOutput[gl_VertexID * vsOutByteLength + 16]))[0]; }
                    //     set { ((vec3*)(vsOutput[gl_VertexID * vsOutByteLength + 16]))[0] = value; }
                    // }
                    OutVariable[] outVars = this.outVariableDict.Values.ToArray();
                    uint offset = 0;
                    for (int i = 0; i < outVars.Length; i++)
                    {
                        OutVariable outVar = outVars[i];
                        string typeName = outVar.field.FieldType.Name;
                        string varName = outVar.field.Name;
                        builder.PrintLine(string.Format("[Out] {0} {1}", typeName, varName), tabCount);
                        builder.PrintLine("{", tabCount); tabCount++;
                        {
                            builder.PrintLine(string.Format("get {{ return (({0}*)(vsOutput[gl_VertexID * vsOutByteLength + {1}]))[0]; }}", typeName, offset), tabCount);
                            builder.PrintLine(string.Format("set {{ (({0}*)(vsOutput[gl_VertexID * vsOutByteLength + {1}]))[0] = value; }}", typeName, offset), tabCount);
                        }
                        tabCount--; builder.PrintLine("}", tabCount);
                        offset += outVar.field.FieldType.ByteSize();
                    }
                }
                {
                    // GLSL: void main() { .. }
                    // public override void main() { .. }
                    string sign = "public override void main()";
                    int index = this.Code.IndexOf(sign);
                    int index2 = this.Code.IndexOf(sign, index + sign.Length);
                    if (index2 > 0) { }
                }
                tabCount--; builder.PrintLine("}", tabCount);
            }
            tabCount--; builder.PrintLine("}", tabCount);

            return builder.ToString();
        }
    }
}
