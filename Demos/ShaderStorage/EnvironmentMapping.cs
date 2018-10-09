namespace SoftGL
{
    class EnvironmentMappingVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [In]
        vec3 inNormal;

        [Uniform]
        mat4 mvpMat;
        [Uniform]
        mat4 modelMat;
        /// <summary>
        /// transpose(inverse(modelMat)).
        /// </summary>
        [Uniform]
        mat3 normalMat;

        [Out]
        vec3 passPosition;
        [Out]
        vec3 passNormal;

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            gl_Position = mvpMat * vec4(inPosition, 1.0);

            passPosition = vec3(modelMat * vec4(inPosition, 1.0));
            passNormal = normalMat * inNormal;
        }
    }

    class EnvironmentMappingReflectFrag : FragmentCodeBase
    {
        [In]
        vec3 passPosition;
        [In]
        vec3 passNormal;

        [Uniform]
        vec3 cameraPos;
        [Uniform]
        samplerCube skybox;

        [Out]
        vec4 outColor;

        public override void main()
        {
            vec3 I = normalize(passPosition - cameraPos);
            vec3 R = reflect(I, normalize(passNormal));
            //outColor = vec4(texture(skybox, R).xyz, 1.0);
            outColor = vec4(vec3(texture(skybox, R)), 1.0);
        }
    }

    class EnvironmentMappingRefractFrag : FragmentCodeBase
    {
        [In]
        vec3 passPosition;
        [In]
        vec3 passNormal;

        [Uniform]
        vec3 cameraPos;
        [Uniform]
        samplerCube skybox;
        [Uniform]
        float ratio;

        [Out]
        vec4 outColor;

        public override void main()
        {
            vec3 I = normalize(passPosition - cameraPos);
            vec3 R = refract(I, normalize(passNormal), ratio);
            //outColor = vec4(texture(skybox, R).rgb, 1);
            outColor = vec4(vec3(texture(skybox, R)), 1);
        }
    }
}
