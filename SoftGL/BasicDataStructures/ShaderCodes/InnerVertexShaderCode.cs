using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{

    abstract unsafe class InnerVertexShaderCode : InnerShaderCode
    {
        public byte* vboData;
        [In]
        public int gl_VertexID;
        public int vertexByteLength;

        public byte* uniformData;

        public int vsOutByteLength;
        public byte* vsOutput;
        [Out]
        public vec4 gl_Position
        {
            get { return ((vec4*)(vsOutput[gl_VertexID * vsOutByteLength + 0]))[0]; }
            set { ((vec4*)(vsOutput[gl_VertexID * vsOutByteLength]))[0] = value; }
        }

        #region ICloneable 成员

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    unsafe class InnerSimpleVert : InnerVertexShaderCode
    {
        [In]
        vec3 inPosition { get { return ((vec3*)(vboData[gl_VertexID * vertexByteLength + 0]))[0]; } }
        [In]
        vec3 inColor { get { return ((vec3*)(vboData[gl_VertexID * vertexByteLength + 12]))[0]; } }

        [Uniform]
        mat4 projectionMat { get { return ((mat4*)uniformData[0])[0]; } }
        [Uniform]
        mat4 viewMat { get { return ((mat4*)uniformData[64])[0]; } }
        [Uniform]
        mat4 modelMat { get { return ((mat4*)uniformData[128])[0]; } }

        [Out]
        vec3 passColor
        {
            get { return ((vec3*)(vsOutput[gl_VertexID * vsOutByteLength + 16]))[0]; }
            set { ((vec3*)(vsOutput[gl_VertexID * vsOutByteLength + 16]))[0] = value; }
        }

        public override void main()
        {
            gl_Position = projectionMat * viewMat * modelMat * new vec4(inPosition, 1.0f);
            passColor = inColor;
        }
    }

}
