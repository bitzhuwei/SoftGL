using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class SoftGLDeviceContext : System.Windows.Forms.Control
    {
        /// <summary>
        /// Gets the device context handle.
        /// </summary>
        public IntPtr DeviceContextHandle { get { return this.Handle; } }
    }
}
