using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public abstract partial class FragmentCodeBase : CodeBase
    {
        public vec3 gl_FragCoord;

        /// <summary>
        /// "discard" in GLSL.
        /// </summary>
        public bool discard { get; protected set; }

        public abstract void main();
    }
}
