namespace SoftGL
{
    class tVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [In]
        vec3 inNormal;
        [In]
        vec3 inUV;
        [In]
        vec4 inBlendWeights;
        [In]
        ivec4 inBlendIndices;

        [Uniform]
        mat4 projectionMat;
        [Uniform]
        mat4 viewMat;
        [Uniform]
        mat4 modelMat;
        /// <summary>
        /// glm.transpose(glm.inverse(viewMat * modelMat))
        /// </summary>
        [Uniform]
        mat3 normalMat;
        [Uniform]
        mat4[] Bones;

        /// <summary>
        /// position in eye space.
        /// </summary>
        [Out]
        vec3 passPosition;
        /// <summary>
        /// normal in eye space.
        /// </summary>
        [Out]
        vec3 passNormal;
        [Out]
        vec2 passUV;

        public override void main()
        {
            vec4 blendPosition = vec4(0);
            vec3 blendNormal = vec3(0);
            vec4 inPos = vec4(inPosition, 1.0);

            for (int i = 0; i < 4; i++)
            {
                int index = inBlendIndices[i];
                blendPosition += (bones[index] * inPos) * inBlendWeights[i];
                blendNormal += (Bones[index] * vec4(inNormal, 0.0)).xyz * inBlendWeights[i];
            }

            // transform vertex' position from model space to clip space.
            gl_Position = projectionMat * vec4((viewMat * modelMat * inPos).xyz, 1.0);

            passNormal = normalize(normalMat * blendNormal);
            passUV = inUV;
        }
    }

    class SpotLightFrag : FragmentCodeBase
    {
        [In]
        vec3 passPosition;
        [In]
        vec3 passNormal;
        [In]
        vec2 passUV;

        [Uniform]
        sampler2D textureMap;
        /// <summary>
        /// 0-> submesh has texture, 1-> submesh has no texture.
        /// </summary>
        [Uniform]
        float useDefault;
        [Uniform]
        vec3 light_position;
        [Uniform]
        mat4 MV;

        ///// <summary>
        ///// ambient color of whole scene.
        ///// </summary>
        //[Uniform]
        //vec3 ambientColor = vec3(0.2);
        ///// <summary>
        ///// vertex' properties of refelcting light.
        ///// </summary>
        //[Uniform]
        //Material material = new Material(vec3(1), vec3(1), vec3(1), 6.0, 10);
        ///// <summary>
        ///// white light.
        ///// </summary>
        //[Uniform]
        //Light light = new Light(LightType.SpotLight, vec3(1), vec3(1), vec3(1), vec3(0), 0.5, 1);
        //[Uniform]
        //float constantAttenuation = 1.0f;
        //[Uniform]
        //float linearAttenuation = 0.0001f;
        //[Uniform]
        //float quadraticAttenuation = 0.0001f;

        [Out]
        vec4 outColor;

        public override void main()
        {
            vec4 vEyeSpaceLightPos = MV * vec4(light_position, 1.0);
            vec3 L = vEyeSpaceLightPos.xyz - passPosition;
            L = normalize(L);
            float diffuse = max(0, dot(normalize(passNormal), L));

            outColor = diffuse * mix(texture(textureMap, passUV), vec4(1), useDefault);
        }
    }
}
