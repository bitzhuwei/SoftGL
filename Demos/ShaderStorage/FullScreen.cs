namespace SoftGL
{
    class FullScreenVert : VertexCodeBase
    {
        [Out]
        vec2 passTexCoord;

        const float value = 1;

        static readonly vec2[] vertices = new vec2[] { vec2(value, value), vec2(-value, value), vec2(-value, -value), vec2(value, -value) };
        static readonly vec2[] texCoords = new vec2[] { vec2(1.0, 1.0), vec2(0.0, 1.0), vec2(0.0, 0.0), vec2(1.0, 0.0) };

        public override void main()
        {
            gl_Position = vec4(vertices[gl_VertexID], 0, 1);

            passTexCoord = texCoords[gl_VertexID];
        }
    }

    class FullScreenFrag : FragmentCodeBase
    {
        [In]
        vec2 passTexCoord;

        [Uniform]
        sampler2D colorSampler;

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = texture(colorSampler, passTexCoord);
        }
    }
}
