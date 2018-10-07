namespace SoftGL
{
    class ZeroAttributeInVertexShaderVert : VertexCodeBase
    {
        [Out]
        vec2 passTexCoord;
        [Uniform]
        mat4 mvpMat;

        static readonly vec4[] vertices = new vec4[4] { new vec4(-1.0f, -1.0f, 0.0f, 1.0f), new vec4(1.0f, -1.0f, 0.0f, 1.0f), new vec4(-1.0f, 1.0f, 0.0f, 1.0f), new vec4(1.0f, 1.0f, 0.0f, 1.0f) };
        static readonly vec2[] texCoord = new vec2[4] { new vec2(0.0f, 0.0f), new vec2(1.0f, 0.0f), new vec2(0.0f, 1.0f), new vec2(1.0f, 1.0f) };

        public override void main()
        {
            passTexCoord = texCoord[gl_VertexID];

            gl_Position = mvpMat * vertices[gl_VertexID];
        }
    }

    class ZeroAttributeInVertexShaderFrag : FragmentCodeBase
    {
        [In]
        vec2 passTexCoord;
        [Uniform]
        sampler2D tex;

        [Out]
        vec4 outColor;

        public override void main()
        {
            //outColor = texture(tex, passTexCoord);
            outColor = new vec4(passTexCoord.x, passTexCoord.y, passTexCoord.x / 2 + passTexCoord.y / 2, 1.0f);
        }
    }
}
