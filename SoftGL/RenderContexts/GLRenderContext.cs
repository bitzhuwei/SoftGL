using System;

namespace SoftGL
{
    /// <summary>
    /// OpenGL render context.
    /// </summary>
    public abstract partial class GLRenderContext : IDisposable
    {
        /// <summary>
        ///  Set the width, height and parameters.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="paramNames">parameters' names.</param>
        /// <param name="paramValues">parameters' values.</param>
        public GLRenderContext(int width, int height, string[] paramNames, uint[] paramValues)
        {
            if (paramNames != null)
            {
                if (paramValues == null || paramNames.Length != paramValues.Length)
                { throw new ArgumentException("Names no matching with values!"); }
            }
            else if (paramValues != null)
            { throw new ArgumentException("Names no matching with values!"); }
            else // both are null.
            {
                paramNames = new string[0];
                paramValues = new uint[0];
            }

            this.Width = width;
            this.Height = height;
            this.ParamNames = paramNames;
            this.ParamValues = paramValues;
        }

        /// <summary>
        /// Makes the render context current.
        /// </summary>
        public abstract void MakeCurrent();

        /// <summary>
        /// Blit the rendered data to the supplied device context.
        /// </summary>
        /// <param name="deviceContext">The HDC.</param>
        public abstract void Blit(IntPtr deviceContext);

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