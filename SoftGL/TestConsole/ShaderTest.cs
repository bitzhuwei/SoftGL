using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public static class ShaderTest
    {
        public static void Test()
        {
            var vs = new VertexShader(0);
            vs.Code = vertexCode;
            vs.Compile();
            var program = new ShaderProgram(0);
            program.AttachedShaders.Add(vs);
            program.Link();
        }

        const string vertexCode = @"using System;

namespace SoftGL
{
    // TODO: 动态改写C#代码。

    class SimpleVert : VertexShaderCode
    {
        [In] vec3 inPosition;
        [In] vec3 inColor;

        [Uniform] mat4 mvpMatrix;

        [Out] vec3 passColor;

        public override void main()
        {
            gl_Position = mvpMatrix * new vec4(inPosition, 1.0f);
            passColor = inColor;
        }
    }
}
";

        const string fragmentCode = @"using System;

namespace SoftGL
{
    class SimpleFrag : FragmentShaderCode
    {
        [In] vec3 passColor;

        [Out] vec4 outColor;

        public override void main()
        {
            outColor = new vec4(passColor, 1.0f);
        }
    }
}
";
    }
}
