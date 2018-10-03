namespace SoftGL
{
    class fargmentCode : FragmentCodeBase
    {
        [In]
        vec3 passColor;
        //[Uniform]
        //vec4 color = new vec4(1, 0, 0, 1); // default: red color.

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = new vec4(passColor, 1.0f); // fill the fragment with specified color.
        }
    }
}
