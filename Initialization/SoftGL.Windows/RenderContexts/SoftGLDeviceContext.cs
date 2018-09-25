using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class SoftGLDeviceContext
    {
        System.Windows.Forms.Control control;
        /// <summary>
        /// Gets the device context handle.
        /// </summary>
        public IntPtr DeviceContextHandle { get { return control.Handle; } }

        public SoftGLDeviceContext(int width, int height)
        {
            const int left = 0, top = 0;
            this.control = new System.Windows.Forms.Control("SoftGLDeviceContext", left, top, width, height);
        }
    }
}
