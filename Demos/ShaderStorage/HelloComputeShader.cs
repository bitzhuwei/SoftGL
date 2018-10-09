namespace SoftGL
{
    class HelloComputeShaderComp : ComputeCodeBase
    {
        [Uniform]
        image2D outImage;

        public override void main()
        {
            uvec2 xy = gl_GlobalInvocationID.xy;
            imageStore(outImage, ivec2(xy), vec4(1, 1, 1, 1));
        }
    }

}
