using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public abstract class VertexShaderCode
    {
        public int gl_VertexID;

        [Out]
        public vec4 gl_Position { get; set; }

        public abstract void main();
    }
}
