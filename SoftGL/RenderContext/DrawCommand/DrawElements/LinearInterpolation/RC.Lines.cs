using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private List<Fragment> LinearInterpolationLines(int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer, PassBuffer[] passBuffers)
        {
            var result = new List<Fragment>();
            FragmentShader fs = program.FragmentShader;
            if (fs == null) { return result; }

            return result;
        }
    }
}
