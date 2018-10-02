using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class SimpleFrag : FragmentShaderCode
    {
        [In]
        vec3 passColor;

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = new vec4(passColor, 1.0f);
        }
    }
}
