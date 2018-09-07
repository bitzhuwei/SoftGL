using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SoftGL
{
    /// <summary>
    /// Implementation of OpenGL on Windows System.
    /// </summary>
    public partial class WinSoftGL : GL
    {
        private SoftGLRenderContext currentContext;

        /// <summary>
        /// Single instance of <see cref="WinSoftGL"/>.
        /// </summary>
        public static readonly WinSoftGL SoftGLInstance = new WinSoftGL();
        private WinSoftGL() : base() { }

        public override IntPtr GetCurrentContext()
        {
            SoftGLRenderContext context = this.currentContext;

            return context == null ? IntPtr.Zero : context.Pointer;
        }
    }
}