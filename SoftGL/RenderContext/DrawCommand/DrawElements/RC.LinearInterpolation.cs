using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private List<Fragment> LinearInterpolation(DrawTarget mode, int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer, byte[] vsOutput)
        {
            List<Fragment> result = null;
            switch (mode)
            {
                case DrawTarget.Points: result = LinearInterpolationPoints(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.LineStrip: result = LinearInterpolationLineStrip(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.LineLoop: result = LinearInterpolationLineLoop(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.Lines: result = LinearInterpolationLines(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.LineStripAdjacency: result = LinearInterpolationLineStripAdjacency(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.LinesAdjacency: result = LinearInterpolationLinesAdjacency(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.TriangleStrip: result = LinearInterpolationTriangleStrip(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.TriangleFan: result = LinearInterpolationTriangleFan(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.Triangles: result = LinearInterpolationTriangles(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.TriangleStripAdjacency: result = LinearInterpolationTriangleStripAdjacency(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.TrianglesAdjacency: result = LinearInterpolationTrianglesAdjacency(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                case DrawTarget.Patches: result = LinearInterpolationPatches(count, type, indices, vao, program, indexBuffer, vsOutput); break;
                default:
                    throw new NotDealWithNewEnumItemException(typeof(DrawTarget));
            }

            return result;
        }


    }
}
