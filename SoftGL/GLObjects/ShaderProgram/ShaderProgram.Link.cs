using System;
using System.Collections.Generic;
using System.Reflection;

namespace SoftGL
{
    partial class ShaderProgram
    {
        private string logInfo = string.Empty;

        /// <summary>
        /// Link error exists if LogInfo is not String.Empty.
        /// </summary>
        public string LogInfo { get { return logInfo; } }

        private Dictionary<string, UniformVariable> nameUniformDict = new Dictionary<string, UniformVariable>();
        private Dictionary<int, UniformVariable> locationUniformDict = new Dictionary<int, UniformVariable>();
        /// <summary>
        /// Storage
        /// </summary>
        private byte[] uniformBytes;

        /// <summary>
        /// from Shader to exe.
        /// </summary>
        public void Link()
        {
            if (!FindTypedShaders()) { return; }
            if (!FindUniforms(this.nameUniformDict, this.locationUniformDict)) { return; }
            if (!MakeSureVariablesMatch()) { return; }
            if (!GetInnerShaderCodes()) { return; }
            // TODO: do something else.
        }

        private bool GetInnerShaderCodes()
        {
            var vs = this.VertexShader;
            if (vs != null)
            {

            }
            var fs = this.FragmentShader;

            return true;
        }

        /// <summary>
        /// ex. Make sure vs.out and fs.in match.
        /// </summary>
        /// <returns></returns>
        private bool MakeSureVariablesMatch()
        {
            if (this.VertexShader != null && this.FragmentShader != null)
            {
                Dictionary<string, OutVariable> outDict = this.VertexShader.outVariableDict;
                Dictionary<string, InVariable> inDict = this.FragmentShader.inVariableDict;
                if (outDict.Count != inDict.Count) { this.logInfo = string.Format("Variables number ({0} and {1}) not match!", outDict.Count, inDict.Count); return false; }
                foreach (var outItem in outDict)
                {
                    InVariable inVar = null;
                    if (inDict.TryGetValue(outItem.Key, out inVar))
                    {
                        if (inVar.propertyInfo.PropertyType != outItem.Value.propertyInfo.PropertyType)
                        {
                            this.logInfo = string.Format("Variable [{0}] not the same type!", outItem.Key);
                            return false;
                        }
                    }
                    else
                    {
                        this.logInfo = string.Format("No variable matches [{0}] in {1}", outItem.Key, this.VertexShader.ShaderType);
                        return false;
                    }
                }
                var list = new List<InVariable>();
                foreach (var outItem in outDict)
                {
                    var inVar = inDict[outItem.Key];
                    list.Add(inVar);
                }
                inDict.Clear();
                foreach (var item in list)
                {
                    inDict.Add(item.propertyInfo.Name, item);
                }
            }

            return true;
        }

        private bool FindUniforms(Dictionary<string, UniformVariable> nameUniformDict, Dictionary<int, UniformVariable> locationUniformDict)
        {
            nameUniformDict.Clear(); locationUniformDict.Clear();
            int nextLoc = 0;
            foreach (var shader in this.attachedShaders)
            {
                foreach (var item in shader.UniformVariableDict)
                {
                    string varName = item.Key;
                    UniformVariable v = item.Value;
                    if (nameUniformDict.ContainsKey(varName))
                    {
                        if (v.fieldInfo.FieldType != nameUniformDict[varName].fieldInfo.FieldType)
                        {
                            this.logInfo = string.Format("Different uniform variable types of the same name[{0}!]", varName);
                            return false;
                        }
                    }
                    else
                    {
                        v.location = nextLoc;
                        int byteSize = this.GetByteSize(v.fieldInfo.FieldType);
                        nextLoc += byteSize;
                        nameUniformDict.Add(varName, v);
                        locationUniformDict.Add(v.location, v);
                    }
                }
            }

            this.uniformBytes = new byte[nextLoc];

            return true;
        }

        private int GetByteSize(Type type)
        {
            int size = 0;
            if (type == typeof(float)) { size = sizeof(float); }
            else if (type == typeof(int)) { size = sizeof(int); }
            else if (type == typeof(uint)) { size = sizeof(uint); }
            else if (type == typeof(mat2)) { size = sizeof(float) * 4; }
            else if (type == typeof(mat3)) { size = sizeof(float) * 9; }
            else if (type == typeof(mat4)) { size = sizeof(float) * 16; }
            else if (type.IsArray)
            {
                Type elementType = type.GetElementType();
                size = GetByteSize(elementType);
            }
            else
            {
                throw new ArgumentException(string.Format("Type[{0}] is not supported in uniform variable!", type));
            }

            return size;
        }

        /// <summary>
        /// find the vertex shader and other shaders.
        /// </summary>
        /// <returns></returns>
        private bool FindTypedShaders()
        {
            bool result = true;

            VertexShader vertexShader = null;
            TessControlShader tessControlShader = null;
            TessEvaluationShader tessEvaluationShader = null;
            GeometryShader geometryShader = null;
            FragmentShader fragmentShader = null;
            ComputeShader computeShader = null;
            foreach (var item in this.attachedShaders)
            {
                if (item.InfoLog != string.Empty) { this.logInfo = "Shader Compiling Error!"; result = false; break; }

                switch (item.ShaderType)
                {
                    case ShaderType.VertexShader:
                        if (vertexShader != null) { this.logInfo = "Multiple VertexShader!"; result = false; break; ; }
                        else { vertexShader = item as VertexShader; }
                        break;
                    case ShaderType.TessControlShader:
                        if (tessControlShader != null) { this.logInfo = "Multiple TessControlShader!"; result = false; break; }
                        else { tessControlShader = item as TessControlShader; }
                        break;
                    case ShaderType.TessEvaluationShader:
                        if (tessEvaluationShader != null) { this.logInfo = "Multiple TessEvaluationShader!"; result = false; break; }
                        else { tessEvaluationShader = item as TessEvaluationShader; }
                        break;
                    case ShaderType.GeometryShader:
                        if (geometryShader != null) { this.logInfo = "Multiple GeometryShader!"; result = false; break; }
                        else { geometryShader = item as GeometryShader; }
                        break;
                    case ShaderType.FragmentShader:
                        if (fragmentShader != null) { this.logInfo = "Multiple FragmentShader!"; result = false; break; }
                        else { fragmentShader = item as FragmentShader; }
                        break;
                    case ShaderType.ComputeShader:
                        if (computeShader != null) { this.logInfo = "Multiple ComputeShader!"; result = false; break; }
                        else { computeShader = item as ComputeShader; }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            if (vertexShader == null) { this.logInfo = "No VertexShader found!"; result = false; return result; }

            {
                // TODO: support other shaders.
                this.VertexShader = vertexShader;
                //this.tessControlShader = tessControlShader;
                //this.tessEvaluationShader = tessEvaluationShader;
                //this.geometryShader = geometryShader;
                this.FragmentShader = fragmentShader;
                //this.computeShader = computeShader;
            }

            return result;
        }
    }
}

