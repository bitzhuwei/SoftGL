namespace SoftGL
{
    class TextBillboardVert : VertexCodeBase
    {
        /// <summary>
        /// character's quad's position(in pixels) relative to left bottom(0, 0).
        /// </summary>
        [In]
        vec2 inPosition;
        /// <summary>
        /// character's quad's texture coordinate.
        /// </summary>
        [In]
        vec3 inSTR;

        [Uniform]
        mat4 mvpMat;
        [Uniform]
        int width;
        [Uniform]
        int height;
        [Uniform]
        ivec2 screenSize;

        [Out]
        vec3 passSTR;

        const float value = 0.1f;

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            vec4 position = mvpMat * vec4(0, 0, 0, 1.0);
            position = position / position.w;
            float deltaX = (inPosition.x * height - width) / screenSize.x;
            float deltaY = (inPosition.y * height - height) / screenSize.y;
            position.x += deltaX; position.y += deltaY;

            passSTR = inSTR;
        }
    }

    class TextBillboardFrag : FragmentCodeBase
    {
        [Out]
        vec3 passSTR;

        [Uniform]
        sampler2DArray glyphTexture;
        [Uniform]
        vec3 textColor;

        [Out]
        vec4 outColor;

        public override void main()
        {
            float a = texture(glyphTexture, vec3(passSTR.x, passSTR.y, (int)passSTR.z)).x;
            outColor = vec4(textColor, a);
        }
    }
}
