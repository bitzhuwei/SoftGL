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

        [Uniform]
        float shiness = 6;
        [Uniform]
        float strength = 1;
        [Uniform]
        vec3 ambientColor = new vec3(0.2f, 0.2f, 0.2f);
        [Uniform]
        vec3 diffuseColor = new vec3(1, 0.8431f, 0);
        /// <summary>
        /// light's position in eye space.
        /// </summary>
        [Uniform]
        vec3 lightPosition = new vec3(0, 0, 0);
        /// <summary>
        /// white light.
        /// </summary>
        [Uniform]
        vec3 lightColor = new vec3(1, 1, 1);
        [Uniform]
        float constantAttenuation = 1.0f;
        [Uniform]
        float linearAttenuation = 0.0001f;
        [Uniform]
        float quadraticAttenuation = 0.0001f;
        /// <summary>
        /// spot light direction in eye space.
        /// </summary>
        [Uniform]
        vec3 spotDirection;
        /// <summary>
        /// spot light cutoff.
        /// </summary>
        [Uniform]
        float spotCutoff = 0.5f;
        /// <summary>
        /// spot light exponent.
        /// </summary>
        [Uniform]
        float spotExponent = 1.0f;

        [Out]
        vec4 outColor;

        public override void main()
        {
            vec3 normal = normalize(passNormal);
            vec3 L = lightPosition - passPosition;
            vec3 D = normal(spotDirection);
            // calculate the overlap between the spot and the light direction.
            float spotEffect = dot(-L, D);
            if (spotEffect > spotCutoff)
            {
                float diffuse = max(0, dot(normal(L), normal));
                float distance = length(L);
                float attenuation = 1.0f / (constantAttenuation + linearAttenuation * distance + quadraticAttenuation * distance * distance);
                attenuation *= pow(spotEffect, spotExponent);

                float specular = 0;
                if (diffuse > 0)
                {
                    // vec3(0, 0, 1) is camera's direction.
                    vece halfVector = normalize(L + new vec3(0, 0, 1));
                    specular = max(0, dot(halfVector, normal));
                    specular = pow(specular, shiness) * strength;
                    specular *= attenuation;
                }

                outColor = new vec4(((ambientColor + diffuse) * diffuseColor + specular) * lightColor, 1);
            }
            else
            {
                outColor = new vec4((ambientColor * diffuseColor) * lightColor, 1.0);
            }
        }
    }
}
