using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftOpengl32
{
    class SoftGLDeviceContext
    {
        private static readonly Dictionary<IntPtr, SoftGLDeviceContext> handleDeviceDict = new Dictionary<IntPtr, SoftGLDeviceContext>();
        internal static SoftGLDeviceContext FromHandle(IntPtr handle)
        {
            SoftGLDeviceContext result = null;
            if (!handleDeviceDict.TryGetValue(handle, out result))
            {
                result = null;
            }

            return result;
        }

        private System.Windows.Forms.Control control;
        /// <summary>
        /// Gets the device context handle.
        /// </summary>
        internal IntPtr DeviceContextHandle { get { return control.Handle; } }

        internal SoftGLDeviceContext(int width, int height)
        {
            const int left = 0, top = 0;
            this.control = new System.Windows.Forms.Control("SoftGLDeviceContext", left, top, width, height);
            handleDeviceDict.Add(this.DeviceContextHandle, this);
        }

        internal int Width { get { return this.control.Width; } }

        internal int Height { get { return this.control.Height; } }

    }
}
