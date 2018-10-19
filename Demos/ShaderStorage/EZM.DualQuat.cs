namespace SoftGL
{
    class EZMDualQuatVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;
        [In]
        vec2 inUV;
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
        [Uniform]
        vec4[] Bones;

        [Out]
        vec2 passUV;

        mat4 dualQuatToMatrix(vec4 Qn, vec4 Qd)
        {
            float len2 = Qn.dot(Qn);
            float w = Qn.w, x = Qn.x, y = Qn.y, z = Qn.z;
            float t0 = Qd.w, t1 = Qd.x, t2 = Qd.y, t3 = Qd.z;
            vec4 col0 = new vec4(w * w + x * x - y * y - z * z,
                2 * x * y + 2 * w * z,
                2 * x * z - 2 * w * y,
                0) / len2;
            vec4 col1 = new vec4(2 * x * y - 2 * w * z,
                w * w + y * y - x * x - z * z,
                2 * y * z + 2 * w * x,
                0) / len2;
            vec4 col2 = new vec4(2 * x * z + 2 * w * y,
                2 * y * z - 2 * w * x,
                w * w + z * z - x * x - y * y,
                0) / len2;
            vec4 col3 = new vec4(-2 * t0 * x + 2 * w * t1 - 2 * t2 * z + 2 * y * t3,
                -2 * t0 * y + 2 * t1 * z - 2 * x * t3 + 2 * w * t2,
                -2 * t0 * z + 2 * x * t2 + 2 * w * t3 - 2 * t1 * y,
                len2) / len2;
            mat4 m = new mat4(col0, col1, col2, col3);

            return m;
        }

        public override void main()
        {
            vec4 blendPosition = vec4(0);
            vec3 blendNormal = vec3(0);
            vec4[] blendDQ = new SoftGL.vec4[2];

            //here we check the dot product between the two quaternions
            var yc = 1.0; var zc = 1.0; var wc = 1.0;

            //if the dot product is < 0 they are opposite to each other
            //hence we multiply the -1 which would subtract the blended result
            if (dot(Bones[inBlendIndices.x * 2], Bones[inBlendIndices.y * 2]) < 0.0)
                yc = -1.0;

            if (dot(Bones[inBlendIndices.x * 2], Bones[inBlendIndices.z * 2]) < 0.0)
                zc = -1.0;

            if (dot(Bones[inBlendIndices.x * 2], Bones[inBlendIndices.w * 2]) < 0.0)
                wc = -1.0;

            for (int i = 0; i < 4; i++)
            {
                int index = inBlendIndices[i];
                blendDQ[0] += (Bones[index * 2]) * inBlendWeights[i];
                blendDQ[1] += (Bones[index * 2 + 1]) * inBlendWeights[i];
            }

            //get the skinning matrix from the dual quaternion
            mat4 skinTransform = dualQuatToMatrix(blendDQ[0], blendDQ[1]);
            blendPosition = skinTransform * vec4(inPosition, 1.0);

            // transform vertex' position from model space to clip space.
            gl_Position = projectionMat * vec4((viewMat * modelMat * blendPosition).xyz, 1.0);

            passUV = inUV;
        }
    }

    class EZMDualQuatFrag : FragmentCodeBase
    {
        [In]
        vec2 passUV;

        [Uniform]
        sampler2D textureMap;

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = texture(textureMap, passUV);
        }
    }
}
