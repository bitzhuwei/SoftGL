using System;

namespace SoftGL
{
    /// <summary>
    ///
    /// </summary>
    partial class SoftGLRenderContext
    {
        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        ~SoftGLRenderContext()
        {
            this.Dispose(false);
        }

        private bool disposedValue = false;

        private void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Dispose unmanaged resources.
                { ////	Release the device context and the window.
                    //Win32.ReleaseDC(windowHandle, this.DeviceContextHandle);
                    //this.graphics.ReleaseHdc();
                    //this.graphics.Dispose();
                    //this.window.Dispose();

                    ////	Destroy the window.
                    //Win32.DestroyWindow(windowHandle);

                    // If we have a render context, destroy it.
                    if (this.RenderContextHandle != IntPtr.Zero)
                    {
                        //Win32.wglDeleteContext(this.RenderContextHandle);
                        this.RenderContextHandle = IntPtr.Zero;
                    }
                }
            }

            this.disposedValue = true;
        }

    }
}