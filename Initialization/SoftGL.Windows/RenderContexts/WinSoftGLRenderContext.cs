using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CSharpGL;

namespace SoftGL.Windows
{
    /// <summary>
    /// creates render device and render context.
    /// </summary>
    public partial class WinSoftGLRenderContext : CSharpGL.GLRenderContext
    {
        /// <summary>
        /// creates render device and render context.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="parameters">parameters.</param>
        /// <returns></returns>
        public WinSoftGLRenderContext(int width, int height, ContextGenerationParams parameters)
            : base(width, height)
        {
            this.Parameters = parameters;

            {
                IntPtr dc = SoftOpengl32.StaticCalls.CreateDeviceContext(width, height);
                var paramNames = new string[0]; var paramValues = new uint[0];
                IntPtr hrc = SoftOpengl32.StaticCalls.CreateContext(dc, width, height, paramNames, paramValues);
                SoftOpengl32.StaticCalls.MakeCurrent(IntPtr.Zero, IntPtr.Zero);
                SoftOpengl32.StaticCalls.DeleteContext(this.RenderContextHandle);
                SoftOpengl32.StaticCalls.MakeCurrent(dc, hrc);

                this.DeviceContextHandle = dc;
                this.RenderContextHandle = hrc;
            }

            //  Make the context current.
            this.MakeCurrent();
        }

        //private static WndProc wndProcDelegate = new WndProc(WndProc);

        //static private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        //{
        //    return Win32.DefWindowProc(hWnd, msg, wParam, lParam);
        //}

        /// <summary>
        /// Destroys the render context provider instance.
        /// </summary>
        protected override void DisposeUnmanagedResources()
        {
            ////	Release the device context.
            //Win32.ReleaseDC(windowHandle, this.DeviceContextHandle);

            ////	Destroy the window.
            //Win32.DestroyWindow(windowHandle);

            // If we have a render context, destroy it.
            if (this.RenderContextHandle != IntPtr.Zero)
            {
                //Win32.wglDeleteContext(this.RenderContextHandle);
                this.RenderContextHandle = IntPtr.Zero;
            }

        }

        /// <summary>
        /// Sets the dimensions of the render context provider.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public override void SetDimensions(int width, int height)
        {
            //  Call the base.
            base.SetDimensions(width, height);
            SoftOpengl32.StaticCalls.SetDimensions(this.DeviceContextHandle, width, height);

            ////	Set the window size.
            //Win32.SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, Width, Height,
            //    SetWindowPosFlags.SWP_NOACTIVATE |
            //    SetWindowPosFlags.SWP_NOCOPYBITS |
            //    SetWindowPosFlags.SWP_NOMOVE |
            //    SetWindowPosFlags.SWP_NOOWNERZORDER);

        }

        ///// <summary>
        ///// Blit the rendered data to the supplied device context.
        ///// </summary>
        ///// <param name="deviceContext">The HDC.</param>
        //public override void Blit(IntPtr deviceContext)
        //{
        //    //IntPtr dc = this.DeviceContextHandle;
        //    //if (dc != IntPtr.Zero || windowHandle != IntPtr.Zero)
        //    //{
        //    //    //	Swap the buffers.
        //    //    Win32.SwapBuffers(dc);

        //    //    //	Blit the DC (containing the DIB section) to the target DC.
        //    //    Win32.BitBlt(deviceContext, 0, 0, this.Width, this.Height, dc, 0, 0, Win32.SRCCOPY);
        //    //}

        //    if (this.DeviceContextHandle != IntPtr.Zero)
        //    {
        //        ////  Set the read buffer.
        //        //GL.Instance.ReadBuffer(GL.GL_COLOR_ATTACHMENT0);

        //        //	Read the pixels into the DIB section.
        //        //GL.Instance.ReadPixels(0, 0, this.Width, this.Height, GL.GL_BGRA, GL.GL_UNSIGNED_BYTE, this.dibSection.Bits);

        //        //	Blit the DC (containing the DIB section) to the target DC.
        //        //Win32.BitBlt(deviceContext, 0, 0, this.Width, this.Height,
        //        //this.dibSection.MemoryDeviceContext, 0, 0, Win32.SRCCOPY);
        //    }
        //}

        /// <summary>
        /// Makes the render context current.
        /// </summary>
        public override void MakeCurrent()
        {
            //if (this.RenderContextHandle != IntPtr.Zero)
            //Win32.wglMakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
            if (this.RenderContextHandle != IntPtr.Zero)
                SoftOpengl32.StaticCalls.MakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
        }

        public ContextGenerationParams Parameters { get; set; }
    }
}