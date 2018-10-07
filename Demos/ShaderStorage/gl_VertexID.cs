namespace SoftGL
{
    class gl_VertexIDVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;

        [Uniform]
        mat4 mvpMat;

        [Out]
        vec4 passColor;

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            gl_Position = mvpMat * new vec4(inPosition, 1.0f);

            // gets color value according to gl_VertexID.
            int index = gl_VertexID;
            passColor = new vec4(
                ((index & 0xFF) / 255.0f),
                (((index >> 8) & 0xFF) / 255.0f),
                (((index >> 16) & 0xFF) / 255.0f),
                (((index >> 24) & 0xFF) / 255.0f));
        }
    }

    class gl_VertexIDFrag : FragmentCodeBase
    {
        [In]
        vec4 passColor;

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = passColor;
        }
    }
}
