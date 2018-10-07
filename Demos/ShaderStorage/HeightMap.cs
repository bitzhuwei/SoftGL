namespace SoftGL
{
    class HeightMapVert : VertexCodeBase
    {
        [Uniform]
        mat4 mvpMat;
        /// <summary>
        /// half terrain size.
        /// </summary>
        [Uniform]
        ivec2 terrianSize;
        /// <summary>
        /// heightmap texture.
        /// </summary>
        [Uniform]
        sampler2D heightMapTexture;
        /// <summary>
        /// scale for the heightmap height.
        /// </summary>
        [Uniform]
        float scale;

        [Out]
        vec3 passColor;

        public override void main()
        {
            float u = (float)(gl_VertexID % terrianSize.x) / (float)(terrianSize.x - 1);
            float v = (float)(gl_VertexID / terrianSize.x) / (float)(terrianSize.y - 1);
            float height = (texture(heightMapTexture, new vec2(u, v)).x - 0.5f) * scale;

            float x = (u - 0.5f) * terrianSize.x;
            float z = (v - 0.5f) * terrianSize.y;

            gl_Position = mvpMat * new vec4(x, height, z, 1.0f);
        }
    }

    class HeightMapFrag : FragmentCodeBase
    {
        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = new vec4(1, 1, 1, 1);
        }
    }
}
