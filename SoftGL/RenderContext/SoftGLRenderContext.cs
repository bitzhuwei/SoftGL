﻿using System;

namespace SoftGL
{
    /// <summary>
    /// OpenGL render context.
    /// </summary>
    public partial class SoftGLRenderContext : IDisposable
    {
        /// <summary>
        /// Gets the render context handle.
        /// </summary>
        public IntPtr RenderContextHandle { get; protected set; }

        /// <summary>
        /// Gets the device context handle.
        /// </summary>
        public IntPtr DeviceContextHandle { get; internal set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; protected set; }

        /// <summary>
        /// Gets or sets the parameters' names.
        /// </summary>
        /// <value>The bit depth.</value>
        public string[] ParamNames { get; protected set; }

        /// <summary>
        /// Gets or sets the parameters' values.
        /// </summary>
        public uint[] ParamValues { get; protected set; }
    }
}