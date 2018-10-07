namespace SoftGL
{
    class sampler2DVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [In]
        vec2 inUV;

        [Uniform]
        mat4 mvpMat;

        [Out]
        vec2 passUV;

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            gl_Position = mvpMat * new vec4(inPosition, 1.0f);

            passUV = inUV;
        }
    }

    class sampler2DFrag : FragmentCodeBase
    {
        [In]
        vec2 passUV;

        [Uniform]
        sampler2D tex;

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = texture(tex, passUV);
        }
    }
}
