using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftGL;

namespace d00_HelloSoftGL
{
    partial class CubeNode
    {
        private const string vertexCode = @"using SoftGL;

namespace d00_HelloSoftGL
{
    class vertexCode : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [Uniform]
        mat4 mvpMatrix;

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            gl_Position = mvpMatrix * new vec4(inPosition, 1.0f);
        }
    }
}
";
    }
}
