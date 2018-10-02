﻿using System;

namespace SoftGL
{
    enum DrawTarget : uint
    {
        Points = GL.GL_POINTS,
        LineStrip = GL.GL_LINE_STRIP,
        LineLoop = GL.GL_LINE_LOOP,
        Lines = GL.GL_LINES,
        LineStripAdjacency = GL.GL_LINE_STRIP_ADJACENCY,
        LinesAdjacency = GL.GL_LINES_ADJACENCY,
        TriangleStrip = GL.GL_TRIANGLE_STRIP,
        TriangleFan = GL.GL_TRIANGLE_FAN,
        Triangles = GL.GL_TRIANGLES,
        TriangleStripAdjacency = GL.GL_TRIANGLE_STRIP_ADJACENCY,
        TrianglesAdjacency = GL.GL_TRIANGLES_ADJACENCY,
        Patches = GL.GL_PATCHES
    }
}
