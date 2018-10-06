using System;
using System.Drawing;

namespace SoftGL
{
    /// <summary>
    /// OpenGL render context.
    /// </summary>
    public unsafe partial class SoftGLRenderContext : IDisposable
    {
        private Bitmap window;
        private Graphics graphics;

        /// <summary>
        /// Gets the render context handle.
        /// </summary>
        public IntPtr RenderContextHandle { get; protected set; }

        private bool bounded = false;

        public bool Bounded
        {
            get { return bounded; }
        }

        private IntPtr deviceContextHandle;
        /// <summary>
        /// Gets the device context handle.
        /// </summary>
        public IntPtr DeviceContextHandle
        {
            get { return this.deviceContextHandle; }
            internal set { this.deviceContextHandle = value; }
        }

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