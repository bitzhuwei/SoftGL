using SoftGL;

namespace d00_HelloSoftGL
{
    class vertexCode : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [Uniform]
        mat4 mvpMatrix;

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            gl_Position = mvpMatrix * new vec4(inPosition, 1.0f);
        }
    }
}
