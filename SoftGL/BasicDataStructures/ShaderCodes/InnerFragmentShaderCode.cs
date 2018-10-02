using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{

    abstract unsafe class InnerFragmentShaderCode : InnerShaderCode
    {
        [In]
        public vec3 gl_FragCoord;
        public int gl_FragmentID;

        /// <summary>
        /// Linear Interpolated output of vertex shader.
        /// </summary>
        public byte* vsOutput;
        public int vsOutByteLength;

        public byte* uniformData;

        public byte* fsOutput;
        public int fsOutByteLength;

        #region ICloneable 成员

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    /// <summary>
    /// demo fragment shader code.
    /// </summary>
    unsafe class InnerSimpleFrag : InnerFragmentShaderCode
    {
        [In]
        vec3 passColor
        {
            get { return ((vec3*)(vsOutput[gl_FragmentID * vsOutByteLength]))[0]; }
            set { ((vec3*)(vsOutput[gl_FragmentID * vsOutByteLength]))[0] = value; }
        }

        [Uniform]
        float Alpha { get { return ((float*)uniformData[0])[0]; } }

        [In]
        vec4 outColor
        {
            get { return ((vec4*)(fsOutput[gl_FragmentID * fsOutByteLength]))[0]; }
            set { ((vec4*)(fsOutput[gl_FragmentID * fsOutByteLength]))[0] = value; }
        }

        public override void main()
        {
            outColor = new vec4(passColor, Alpha);
        }
    }

}
