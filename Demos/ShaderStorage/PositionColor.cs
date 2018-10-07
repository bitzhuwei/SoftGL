namespace SoftGL
{
    class PositionColorVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [In]
        vec3 inColor;
        [Uniform]
        mat4 mvpMat;

        [Out]
        vec3 passColor;

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            gl_Position = mvpMat * new vec4(inPosition, 1.0f);

            passColor = inColor;
        }
    }

    class PositionColorFrag : FragmentCodeBase
    {
        [In]
        vec3 passColor;

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = new vec4(passColor, 1.0f);
        }
    }
}
