﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpGL;

namespace d00_HelloSoftGL
{
    /// <summary>
    ///        Y
    ///        |
    ///        5___________1
    ///       /|          /|
    ///      / |         / |
    ///     4--+--------0  |
    ///     |  7_ _ _ _ |_ 3____ X
    ///     |  /        |  /
    ///     | /         | /
    ///     |/__________|/
    ///     6           2
    ///    /
    ///   Z
    /// </summary>
    class CubeModel : IBufferSource
    {
        private const float halfLength = 0.5f;
        private static readonly vec3[] positions = new vec3[]
        {
            new vec3(+halfLength, +halfLength, +halfLength), // 0
            new vec3(+halfLength, +halfLength, -halfLength), // 1
            new vec3(+halfLength, -halfLength, +halfLength), // 2
            new vec3(+halfLength, -halfLength, -halfLength), // 3 
            new vec3(-halfLength, +halfLength, +halfLength), // 4 
            new vec3(-halfLength, +halfLength, -halfLength), // 5
            new vec3(-halfLength, -halfLength, +halfLength), // 6
            new vec3(-halfLength, -halfLength, -halfLength), // 7
        };
        private static readonly vec3[] colors = new vec3[]
        {
            new vec3(+halfLength+0.5f, +halfLength+0.5f, +halfLength+0.5f), // 0
            new vec3(+halfLength+0.5f, +halfLength+0.5f, -halfLength+0.5f), // 1
            new vec3(+halfLength+0.5f, -halfLength+0.5f, +halfLength+0.5f), // 2
            new vec3(+halfLength+0.5f, -halfLength+0.5f, -halfLength+0.5f), // 3 
            new vec3(-halfLength+0.5f, +halfLength+0.5f, +halfLength+0.5f), // 4 
            new vec3(-halfLength+0.5f, +halfLength+0.5f, -halfLength+0.5f), // 5
            new vec3(-halfLength+0.5f, -halfLength+0.5f, +halfLength+0.5f), // 6
            new vec3(-halfLength+0.5f, -halfLength+0.5f, -halfLength+0.5f), // 7
        };

        private static readonly uint[] indexes = new uint[]
        {
            0, 2, 1,  1, 2, 3, // +X faces.
            0, 1, 5,  0, 5, 4, // +Y faces.
            0, 4, 2,  2, 4, 6, // +Z faces.
            7, 6, 4,  7, 4, 5, // -X faces.
            7, 5, 3,  3, 5, 1, // -Z faces.
            7, 3, 2,  7, 2, 6, // -Y faces.
        };

        public const string strPosition = "position";
        private VertexBuffer positionBuffer; // array in GPU side.
        public const string strColor = "color";
        private VertexBuffer colorBuffer; // array in GPU side.

        private IDrawCommand drawCommand;


        #region IBufferSource 成员

        public IEnumerable<VertexBuffer> GetVertexAttribute(string bufferName)
        {
            if (strPosition == bufferName) // requiring position buffer.
            {
                if (this.positionBuffer == null)
                {
                    // transform managed array to vertex buffer.
                    this.positionBuffer = positions.GenVertexBuffer(
                        VBOConfig.Vec3, // mapping to 'in vec3 someVar;' in vertex shader.
                        BufferUsage.StaticDraw); // GL_STATIC_DRAW.
                }

                yield return this.positionBuffer;
            }
            else if (strColor == bufferName) // requiring position buffer.
            {
                if (this.colorBuffer == null)
                {
                    // transform managed array to vertex buffer.
                    this.colorBuffer = colors.GenVertexBuffer(
                        VBOConfig.Vec3, // mapping to 'in vec3 someVar;' in vertex shader.
                        BufferUsage.StaticDraw); // GL_STATIC_DRAW.
                }

                yield return this.positionBuffer;
            }
            else
            {
                throw new ArgumentException("bufferName");
            }
        }

        public IEnumerable<IDrawCommand> GetDrawCommand()
        {
            if (this.drawCommand == null)
            {
                // indexes in GPU side.
                IndexBuffer indexBuffer = indexes.GenIndexBuffer(BufferUsage.StaticDraw);
                //this.drawCommand = new DrawElementsCmd(indexBuffer, DrawMode.LineStrip); // GL_TRIANGLES.
                this.drawCommand = new DrawElementsCmd(indexBuffer, DrawMode.Triangles); // GL_TRIANGLES.
            }

            yield return this.drawCommand;
        }

        #endregion
    }
}
