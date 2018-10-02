using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{

    public abstract unsafe class InnerFragmentShaderCodeBase : InnerShaderCodeBase
    {
        public int fragmentID;

        //[In]
        public vec3 gl_FragCoord;
        public int gl_FragmentID;
    }

    /// <summary>
    /// demo fragment shader code.
    /// </summary>
    unsafe class InnerDemoFrag : InnerFragmentShaderCodeBase
    {
        vec3* passColorData;
        [In]
        vec3 passColor
        {
            get { return passColorData[fragmentID]; }
            set { passColorData[fragmentID] = value; }
        }

        [Uniform]
        float Alpha;

        vec4* outColorData;
        [In]
        vec4 outColor
        {
            get { return outColorData[fragmentID]; }
            set { outColorData[fragmentID] = value; }
        }

        //public override void main()
        //{
        //    outColor = new vec4(passColor, Alpha);
        //}
    }

}
