using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class SimpleFrag : FragmentShaderCode
    {
        [In]
        vec3 passColor { get; set; }

        [Out]
        vec4 outColor { get; set; }

        public override void main()
        {
            outColor = new vec4(passColor, 1.0f);
        }
    }
}
