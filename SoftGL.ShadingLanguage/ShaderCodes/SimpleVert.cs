using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    // TODO: 动态改写C#代码。

    class SimpleVert : VertexShaderCode
    {
        [In]
        vec3 inPosition { get; set; }
        [In]
        vec3 inColor { get; set; }

        [Uniform]
        mat4 mvpMatrix;

        [Out]
        vec3 passColor { get; set; }

        public override void main()
        {
            gl_Position = mvpMatrix * new vec4(inPosition, 1.0f);
            passColor = inColor;
        }
    }



}
