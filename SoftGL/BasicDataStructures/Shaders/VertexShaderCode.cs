using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    abstract class VertexShaderCode
    {
        [Out]
        public vec4 gl_Position;

        public abstract void main();
    }
}
