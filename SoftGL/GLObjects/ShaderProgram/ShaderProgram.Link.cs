using System;
using System.Collections.Generic;

namespace SoftGL
{
    partial class ShaderProgram
    {
        private string logInfo = string.Empty;

        public string LogInfo
        {
            get { return logInfo; }
            set { logInfo = value; }
        }

        /// <summary>
        /// from Shader to exe.
        /// </summary>
        public void Link()
        {
            if (!FindTypedShaders()) { return; }

            throw new NotImplementedException();
        }

        private bool FindUniforms()
        {
            bool result = true;

            return result;
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
                this.vertexShader = vertexShader;
                //this.tessControlShader = tessControlShader;
                //this.tessEvaluationShader = tessEvaluationShader;
                //this.geometryShader = geometryShader;
                this.fragmentShader = fragmentShader;
                //this.computeShader = computeShader;
            }

            return result;
        }
    }
}

