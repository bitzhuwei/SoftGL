using SoftGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftOpengl32
{
    partial class StaticCalls
    {
        public static void glSamplerParameterf(uint sampler, uint pname, float param)
        {
            SoftGLRenderContext.glSamplerParameterf(sampler, pname, param);
        }
    }
}
