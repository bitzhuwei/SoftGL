﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public abstract class FragmentShaderCode
    {
        public vec3 gl_FragCoord;

        public abstract void main();
    }
}