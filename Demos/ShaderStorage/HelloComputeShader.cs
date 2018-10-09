namespace SoftGL
{
    class HelloComputeShaderComp : ComputeCodeBase
    {
        [Uniform]
        image2D outImage;
        bool reset = false;

        public override void main()
        {
            uvec2 xy = gl_GlobalInvocationID.xy;
            if (reset)
            {
                imageStore(outImage, ivec2(xy), vec4(1, 1, 1, 1));
            }
            else
            {
                vec2 index = vec2(gl_LocalInvocationID.x, gl_LocalInvocationID.xy.y);
                vec2 size = vec2(gl_WorkGroupSize.x, gl_WorkGroupSize.y);
                imageStore(outImage, ivec2(xy),
                    vec4(index.x / size.x, index.y / size.y, 0.0, 0.0));
            }
        }
    }

}
