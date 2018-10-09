namespace SoftGL
{
    class AmbientVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;

        [Uniform]
        mat4 mvpMat;

        public override void main()
        {
            gl_Position = mvpMat * vec4(inPosition, 1.0);
        }
    }

    class AmbientFrag : FragmentCodeBase
    {
        [Uniform]
        vec3 ambientColor;

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = vec4(ambientColor, 0.1);
        }
    }

    class extrudeVert : VertexCodeBase
    {
        [In]
        vec3 inPosition;

        [Out]
        vec3 passPosition;

        public override void main()
        {
            passPosition = inPosition;
        }
    }

    class extrudeGeom : GeometryCodeBase
    {
        public extrudeGeom()
            : base(GeometryIn.triangles_adjacency, // six vertices in.
            GeometryOut.triangle_strip,
            18) // 4 per quad * 3 triangle vertices + 6 for near/far caps
        { }

        [In]
        vec3[] passPosition;
        /// <summary>
        /// light's position is infinitly far away.
        /// </summary>
        [Uniform]
        bool farAway = false;
        /// <summary>
        /// if farAway is true, lightPosition means direction to light source; otherwise, it means light's position.
        /// </summary>
        [Uniform]
        vec3 lightPosition;
        [Uniform]
        mat4 vpMat;
        [Uniform]
        mat4 modelMat;

        const float EPSILON = 0.0001f;

        struct _Vertex { public vec3 position; public vec3 normal;}
        [Out]
        _Vertex v;

        /// <summary>
        /// Emit a quad using a triangle strip.
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        void EmitQuad(vec3 startPos, vec3 endPos)
        {
            vec3 lightDirection;
            if (farAway) { lightDirection = -lightPosition; }
            else { lightDirection = normalize(startPos - lightPosition); }

            // Vertex #1: the starting vertex (just a tiny bit below the original edge)
            v.position = startPos;
            v.normal = -cross((endPos - startPos), lightDirection);
            gl_Position = vpMat * vec4((startPos + lightDirection * EPSILON), 1.0);
            EmitVertex();

            // Vertex #2: the starting vertex projected to infinity
            v.position = startPos;
            v.normal = -cross((endPos - startPos), lightDirection);
            gl_Position = vpMat * vec4(lightDirection, 0.0);
            EmitVertex();

            if (farAway) { lightDirection = -lightPosition; }
            else { lightDirection = normalize(endPos - lightPosition); }

            // Vertex #3: the ending vertex (just a tiny bit below the original edge)
            v.position = endPos;
            v.normal = -cross((endPos - startPos), lightDirection);
            gl_Position = vpMat * vec4((endPos + lightDirection * EPSILON), 1.0);
            EmitVertex();

            // Vertex #4: the ending vertex projected to infinity
            v.position = endPos;
            v.normal = -cross((endPos - startPos), lightDirection);
            gl_Position = vpMat * vec4(lightDirection, 0.0);
            EmitVertex();

            EndPrimitive();
        }

        public override void main()
        {
            var worldSpacePos = new vec3[6];
            worldSpacePos[0] = vec3(modelMat * vec4(passPosition[0], 1.0));
            worldSpacePos[1] = vec3(modelMat * vec4(passPosition[1], 1.0));
            worldSpacePos[2] = vec3(modelMat * vec4(passPosition[2], 1.0));
            worldSpacePos[3] = vec3(modelMat * vec4(passPosition[3], 1.0));
            worldSpacePos[4] = vec3(modelMat * vec4(passPosition[4], 1.0));
            worldSpacePos[5] = vec3(modelMat * vec4(passPosition[5], 1.0));
            vec3 e1 = worldSpacePos[2] - worldSpacePos[0];
            vec3 e2 = worldSpacePos[4] - worldSpacePos[0];
            vec3 e3 = worldSpacePos[1] - worldSpacePos[0];
            vec3 e4 = worldSpacePos[3] - worldSpacePos[2];
            vec3 e5 = worldSpacePos[4] - worldSpacePos[2];
            vec3 e6 = worldSpacePos[5] - worldSpacePos[0];

            vec3 Normal = normalize(cross(e1, e2));
            vec3 lightDirection;
            if (farAway) { lightDirection = lightPosition; }
            else { lightDirection = normalize(lightPosition - worldSpacePos[0]); }

            // Handle only light facing triangles
            if (dot(Normal, lightDirection) > 0)
            {

                Normal = cross(e3, e1);

                if (dot(Normal, lightDirection) <= 0)
                {
                    vec3 startPos = worldSpacePos[0];
                    vec3 endPos = worldSpacePos[2];
                    EmitQuad(startPos, endPos);
                }

                Normal = cross(e4, e5);
                if (farAway) { lightDirection = lightPosition; }
                else { lightDirection = normalize(lightPosition - worldSpacePos[2]); }

                if (dot(Normal, lightDirection) <= 0)
                {
                    vec3 startPos = worldSpacePos[2];
                    vec3 endPos = worldSpacePos[4];
                    EmitQuad(startPos, endPos);
                }

                Normal = cross(e2, e6);
                if (farAway) { lightDirection = lightPosition; }
                else { lightDirection = normalize(lightPosition - worldSpacePos[4]); }

                if (dot(Normal, lightDirection) <= 0)
                {
                    vec3 startPos = worldSpacePos[4];
                    vec3 endPos = worldSpacePos[0];
                    EmitQuad(startPos, endPos);
                }

                // render the front cap
                if (farAway) { lightDirection = -lightPosition; }
                else { lightDirection = (normalize(worldSpacePos[0] - lightPosition)); }
                gl_Position = vpMat * vec4((worldSpacePos[0] + lightDirection * EPSILON), 1.0);
                EmitVertex();

                if (farAway) { lightDirection = -lightPosition; }
                else { lightDirection = (normalize(worldSpacePos[2] - lightPosition)); }
                gl_Position = vpMat * vec4((worldSpacePos[2] + lightDirection * EPSILON), 1.0);
                EmitVertex();

                if (farAway) { lightDirection = -lightPosition; }
                else { lightDirection = (normalize(worldSpacePos[4] - lightPosition)); }
                gl_Position = vpMat * vec4((worldSpacePos[4] + lightDirection * EPSILON), 1.0);
                EmitVertex();
                EndPrimitive();

                // render the back cap
                if (farAway) { lightDirection = -lightPosition; }
                else { lightDirection = worldSpacePos[0] - lightPosition; }
                gl_Position = vpMat * vec4(lightDirection, 0.0);
                EmitVertex();

                if (farAway) { lightDirection = -lightPosition; }
                else { lightDirection = worldSpacePos[4] - lightPosition; }
                gl_Position = vpMat * vec4(lightDirection, 0.0);
                EmitVertex();

                if (farAway) { lightDirection = -lightPosition; }
                else { lightDirection = worldSpacePos[2] - lightPosition; }
                gl_Position = vpMat * vec4(lightDirection, 0.0);
                EmitVertex();

                EndPrimitive();
            }
        }

        class extrudeFrag : FragmentCodeBase
        {
            struct _Vertex { public vec3 position; public vec3 normal;}

            [Uniform]
            Light light;

            public override void main()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
