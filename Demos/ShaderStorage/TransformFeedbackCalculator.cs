namespace SoftGL
{
    class TransformFeedbackCalculatorVert : VertexCodeBase
    {
        [In]
        float inValue;

        [Out]
        float outValue;

        public override void main()
        {
            outValue = inValue;
        }
    }
}
