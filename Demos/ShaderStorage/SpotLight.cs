namespace SoftGL
{
    class SpotLightVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [In]
        vec3 inNormal;

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
        mat4 normalMat;

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

        public override void main()
        {
            // transform vertex' position from model space to clip space.
            gl_Position = projectionMat * viewMat * modelMat * new vec4(inPosition, 1.0f);

            passPosition = new vec3(viewMat * modelMat * new vec4(inPosition, 1.0f));
            passNormal = new vec3(normalMat * new vec4(inNormal, 0));
        }
    }

    class SpotLightFrag : FragmentCodeBase
    {
        [In]
        vec3 passPosition;
        [In]
        vec3 passNormal;

        /// <summary>
        /// ambient color of whole scene.
        /// </summary>
        [Uniform]
        vec3 ambientColor = new vec3(0.2f);
        /// <summary>
        /// vertex' properties of refelcting light.
        /// </summary>
        [Uniform]
        Material material = new Material(new vec3(1), new vec3(1), new vec3(1), 6.0f, 10);
        /// <summary>
        /// white light.
        /// </summary>
        [Uniform]
        SpotLight light = new SpotLight(new vec3(1), new vec3(1), new vec3(1), new vec3(0), 0.5f, 1);
        [Uniform]
        float constantAttenuation = 1.0f;
        [Uniform]
        float linearAttenuation = 0.0001f;
        [Uniform]
        float quadraticAttenuation = 0.0001f;

        [Out]
        vec4 outColor;

        public override void main()
        {
            vec3 normal = normalize(passNormal);
            vec3 L = light.position - passPosition;
            vec3 D = normalize(light.direction);
            // calculate the overlap between the spot and the light direction.
            float spotEffect = dot(-L, D);
            if (spotEffect > light.cutoff)
            {
                float diffuse = max(0, dot(normalize(L), normal));
                float distance = length(L);
                float attenuation = 1.0f / (constantAttenuation + linearAttenuation * distance + quadraticAttenuation * distance * distance);
                attenuation *= pow(spotEffect, light.exponent);

                float specular = 0;
                if (diffuse > 0)
                {
                    // vec3(0, 0, 1) is camera's direction.
                    vec3 halfVector = normalize(L + new vec3(0, 0, 1));
                    specular = max(0, dot(halfVector, normal));
                    specular = pow(specular, material.shiness) * material.strength;
                    specular *= attenuation;
                }

                outColor = new vec4(ambientColor * material.ambient
                    + diffuse * material.diffuse * light.diffuse
                    + specular * material.specular * light.specular, 1.0f);
            }
            else
            {
                outColor = new vec4(ambientColor * material.ambient, 1.0f);
            }
        }
    }
}
