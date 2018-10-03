using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        const float epsilon = 0.001f;

        private List<Fragment> LinearInterpolation(DrawTarget mode, int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer, PassBuffer[] passBuffers)
        {
            List<Fragment> result = null;
            switch (mode)
            {
                case DrawTarget.Points: result = LinearInterpolationPoints(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.Lines: result = LinearInterpolationLines(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.LineLoop: result = LinearInterpolationLineLoop(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.LineStrip: result = LinearInterpolationLineStrip(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.Triangles: result = LinearInterpolationTriangles(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.TriangleStrip: result = LinearInterpolationTriangleStrip(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.TriangleFan: result = LinearInterpolationTriangleFan(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.Quads: result = LinearInterpolationQuads(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.QuadStrip: result = LinearInterpolationQuadStrip(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.Polygon: result = LinearInterpolationPolygon(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.LinesAdjacency: result = LinearInterpolationLinesAdjacency(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.LineStripAdjacency: result = LinearInterpolationLineStripAdjacency(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.TrianglesAdjacency: result = LinearInterpolationTrianglesAdjacency(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.TriangleStripAdjacency: result = LinearInterpolationTriangleStripAdjacency(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                case DrawTarget.Patches: result = LinearInterpolationPatches(count, type, indices, vao, program, indexBuffer, passBuffers); break;
                default: throw new NotDealWithNewEnumItemException(typeof(DrawTarget));
            }

            return result;
        }


    }
}
