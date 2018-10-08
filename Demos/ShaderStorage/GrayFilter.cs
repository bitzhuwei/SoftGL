namespace SoftGL
{
    class GrayFilterVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [In]
        vec2 inTexCoord;

        [Uniform]
        mat4 mvpMat;

        [Out]
        vec2 passTexCoord;

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            gl_Position = mvpMat * new vec4(inPosition, 1.0f);

            passTexCoord = inTexCoord;
        }
    }

    class GrayFilterFrag : FragmentCodeBase
    {
        [In]
        vec2 passTexCoord;

        [Uniform]
        sampler2D tex;

        [Out]
        vec4 outColor;

        public override void main()
        {
            vec4 color = texture(tex, passTexCoord);

            if (passTexCoord.x >= 0.5f)
            {
                float grey = color.x * 0.299f + color.y * 0.587f + color.z * 0.114f;

                outColor = new vec4(grey, grey, grey, 1.0f);
            }
            else
            {
                outColor = color;
            }
        }
    }
}
