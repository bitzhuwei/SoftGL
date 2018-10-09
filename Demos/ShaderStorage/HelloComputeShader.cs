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
                imageStore(outImage, ivec2(xy),
                    vec4(vec2(gl_LocalInvocationID.xy) / vec2(gl_WorkGroupSize.xy), 0.0, 0.0));
            }
        }
    }

}
