using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        /// <summary>
        /// p0-----p1
        /// |      |
        /// |      |
        /// p3-----p2
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="result"></param>
        private static void FindPixelsAtQuad(vec3 p0, vec3 p1, vec3 p2, vec3 p3, List<vec3> result)
        {
            FindPixelsAtTriangle(p0, p1, p3, result);
            FindPixelsAtTriangle(p1, p2, p3, result);
        }
    }
}
