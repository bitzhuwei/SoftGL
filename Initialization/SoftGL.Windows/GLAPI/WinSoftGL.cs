using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CSharpGL;

namespace SoftGL.Windows
{
    /// <summary>
    /// Implementation of OpenGL on Windows System.
    /// </summary>
    public partial class WinSoftGL : GL
    {
        /// <summary>
        /// Single instance of <see cref="WinSoftGL"/>.
        /// </summary>
        public static readonly WinSoftGL SoftGLInstance = new WinSoftGL();
        private WinSoftGL() : base() { }

        public override IntPtr GetCurrentContext()
        {
            return FakeOperatingSystem.GetCurrentContext();
        }
    }
}