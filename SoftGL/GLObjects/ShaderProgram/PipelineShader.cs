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
        protected Type innerPropertiesCodeType;
        protected Type innerCodeType;

        public abstract int PipelineOrder { get; }
        public readonly Dictionary<string, InVariable> inVariableDict = new Dictionary<string, InVariable>();
        public readonly Dictionary<string, OutVariable> outVariableDict = new Dictionary<string, OutVariable>();

        public PipelineShader(ShaderType shaderType, uint id) : base(shaderType, id) { }

        public override object CreateCodeInstance()
        {
            return Activator.CreateInstance(this.innerCodeType);
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
            {
                string innerPropertiesCode = GetInnerPropertiesCode();
                string result = CompileInnerPropertiesCodeType(innerPropertiesCode);
                if (result != string.Empty) { return result; }
            }
            {
                string result = GenerateInnerCodeType(string.Format("Inner{0}Code", this.ShaderType));
                if (result != string.Empty) { return result; }
            }

            return string.Empty;
        }

        protected abstract string GenerateInnerCodeType(string innerCodeTypeName);

        private string CompileInnerPropertiesCodeType(string innerPropertiesCode)
        {
            // compile inner code.
            var options = new Dictionary<string, string>();
            options.Add("CompilerVersion", "v3.5");
            var compiler = new CSharpCodeProvider(options);
            var compParameters = new CompilerParameters();
            compParameters.CompilerOptions = "/unsafe";
            compParameters.ReferencedAssemblies.Add("SoftGL.dll");
            compParameters.ReferencedAssemblies.Add("SoftGL.ShadingLanguage.dll");
            CompilerResults res = compiler.CompileAssemblyFromSource(compParameters, innerPropertiesCode);
            if (res.Errors.Count > 0) { return DumpLog(res); }
            // parse properties.
            Type innerPropertiesCodeType = res.CompiledAssembly.GetType(string.Format("SoftGL.Inner{0}Code", this.ShaderType));
            this.innerPropertiesCodeType = innerPropertiesCodeType;

            return string.Empty;
        }

        private string GetInnerPropertiesCode()
        {
            var builder = new StringBuilder();
            int tabCount = 0;
            builder.PrintLine(string.Format("namespace SoftGL"), tabCount);
            builder.PrintLine("{", tabCount); tabCount++;
            {
                builder.PrintLine(string.Format("public abstract unsafe class Inner{0}Code : Inner{0}CodeBase", this.ShaderType), tabCount);
                builder.PrintLine("{", tabCount); tabCount++;
                {
                    // GLSL: in vec3 inPosition;
                    // vec3* inPositionData;
                    // [In] vec3 inPosition { get { return inPositionData[gl_VertexID]; } }
                    InVariable[] inVars = this.inVariableDict.Values.ToArray();
                    for (int i = 0; i < inVars.Length; i++)
                    {
                        InVariable inVar = inVars[i];
                        string typeName = inVar.propertyInfo.PropertyType.Name;
                        string varName = inVar.propertyInfo.Name;
                        builder.PrintLine(string.Format("{0}* {1}Data;", typeName, varName), tabCount);
                        builder.PrintLine(string.Format("[In] {0} {1} {{ get {{ return {1}Data[{2}]; }} }}", typeName, varName,
                            this.ShaderType.GetIDName()), tabCount);
                    }
                }
                {
                    // GLSL: uniform float alpha;
                    // [Uniform] mat4 projectionMat;
                    UniformVariable[] uniforms = this.UniformVariableDict.Values.ToArray();
                    for (int i = 0; i < uniforms.Length; i++)
                    {
                        UniformVariable uniform = uniforms[i];
                        string typeName = uniform.fieldInfo.FieldType.Name;
                        string varName = uniform.fieldInfo.Name;
                        builder.PrintLine(string.Format("[Uniform] {0} {1};", typeName, varName), tabCount);
                    }
                }
                {
                    // GLSL: out vec3 passColor;
                    // vec3* passColorData;
                    // [Out] vec3 passColor 
                    // {
                    //     get { return passColorData[gl_VertexID]; }
                    //     set { passColorData[gl_VertexID] = value; }
                    // }
                    OutVariable[] outVars = this.outVariableDict.Values.ToArray();
                    for (int i = 0; i < outVars.Length; i++)
                    {
                        OutVariable outVar = outVars[i];
                        string typeName = outVar.propertyInfo.PropertyType.Name;
                        string varName = outVar.propertyInfo.Name;
                        builder.PrintLine(string.Format("{0}* {1}Data;", typeName, varName), tabCount);
                        builder.PrintLine(string.Format("[Out] {0} {1}", typeName, varName), tabCount);
                        builder.PrintLine("{", tabCount); tabCount++;
                        {
                            builder.PrintLine(string.Format("get {{ return {1}Data[{2}]; }}", typeName, varName,
                            this.ShaderType.GetIDName()), tabCount);
                            builder.PrintLine(string.Format("set {{ {1}Data[{2}] = value; }}", typeName, varName,
                            this.ShaderType.GetIDName()), tabCount);
                        }
                        tabCount--; builder.PrintLine("}", tabCount);
                    }
                }
                tabCount--; builder.PrintLine("}", tabCount);
            }
            tabCount--; builder.PrintLine("}", tabCount);

            return builder.ToString();
        }

        private string FindOutVariables(Type vsType, Dictionary<string, OutVariable> dict)
        {
            dict.Clear();
            foreach (var item in vsType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
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
            foreach (var item in vsType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
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
