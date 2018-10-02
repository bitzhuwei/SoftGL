﻿using SoftGL;

namespace d00_HelloSoftGL
{
    class fargmentCode : FragmentCodeBase
    {
        [Uniform]
        vec4 color = new vec4(1, 0, 0, 1); // default: red color.

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = color; // fill the fragment with specified color.
        }
    }
}
