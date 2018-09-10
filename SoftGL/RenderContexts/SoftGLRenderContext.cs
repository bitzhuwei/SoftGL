using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    /// <summary>
    /// creates render device and render context.
    /// </summary>
    partial class SoftGLRenderContext : GLRenderContext
    {
        internal static readonly Dictionary<IntPtr, SoftGLRenderContext> handleContextDict = new Dictionary<IntPtr, SoftGLRenderContext>();
        internal static readonly Dictionary<Thread, SoftGLRenderContext> threadContextDict = new Dictionary<Thread, SoftGLRenderContext>();

        /// <summary>
        /// creates render device and render context.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="parameters">parameters.</param>
        /// <returns></returns>
        public SoftGLRenderContext(int width, int height, ContextGenerationParams parameters)
            : base(width, height, parameters)
        {
            GCHandle handle = GCHandle.Alloc(this, GCHandleType.WeakTrackResurrection);
            this.RenderContextHandle = GCHandle.ToIntPtr(handle);
            handle.Free();
            handleContextDict.Add(this.RenderContextHandle, this);
            //allRenderContexts.Add(this);

            // Create a new window class, as basic as possible.
            if (!this.CreateBasicRenderContext(width, height, parameters))
            {
                throw new Exception(string.Format("Create basic render context failed!"));
            }

            ////  Make the context current.
            //this.MakeCurrent();

            //if (parameters.UpdateContextVersion)
            //{
            //    //  Update the context if required.
            //    // if I update context, something in legacy opengl will not work...
            //    this.UpdateContextVersion(parameters);
            //}

            //this.dibSection = new DIBSection(this.DeviceContextHandle, width, height, parameters);
        }

        // abstract window for now.
        private object window;

        /// <summary>
        /// Create a new window class, as basic as possible.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private bool CreateBasicRenderContext(int width, int height, ContextGenerationParams parameters)
        {
            ////	Create the window. Position and size it.
            var window = new object();
            ////	Get the window device context.
            GCHandle handle = GCHandle.Alloc(window, GCHandleType.WeakTrackResurrection);
            this.DeviceContextHandle = GCHandle.ToIntPtr(handle);
            handle.Free();
            this.window = window;

            FakeOperatingSystem.MakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);

            return true;
        }

        /// <summary>
        /// Destroys the render context provider instance.
        /// </summary>
        protected override void DisposeUnmanagedResources()
        {
            //  Destroy the internal dc.
            this.dibSection.Dispose();

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

            ////	Set the window size.
            //Win32.SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, Width, Height,
            //    SetWindowPosFlags.SWP_NOACTIVATE |
            //    SetWindowPosFlags.SWP_NOCOPYBITS |
            //    SetWindowPosFlags.SWP_NOMOVE |
            //    SetWindowPosFlags.SWP_NOOWNERZORDER);

            //	Resize dib section.
            this.dibSection.Resize(width, height, this.Parameters);
        }

        /// <summary>
        /// Blit the rendered data to the supplied device context.
        /// </summary>
        /// <param name="deviceContext">The HDC.</param>
        public override void Blit(IntPtr deviceContext)
        {
            //IntPtr dc = this.DeviceContextHandle;
            //if (dc != IntPtr.Zero || windowHandle != IntPtr.Zero)
            //{
            //    //	Swap the buffers.
            //    Win32.SwapBuffers(dc);

            //    //	Blit the DC (containing the DIB section) to the target DC.
            //    Win32.BitBlt(deviceContext, 0, 0, this.Width, this.Height, dc, 0, 0, Win32.SRCCOPY);
            //}

            if (this.DeviceContextHandle != IntPtr.Zero)
            {
                ////  Set the read buffer.
                //GL.Instance.ReadBuffer(GL.GL_COLOR_ATTACHMENT0);

                //	Read the pixels into the DIB section.
                GL.Instance.ReadPixels(0, 0, this.Width, this.Height, GL.GL_BGRA,
                    GL.GL_UNSIGNED_BYTE, this.dibSection.Bits);

                //	Blit the DC (containing the DIB section) to the target DC.
                //Win32.BitBlt(deviceContext, 0, 0, this.Width, this.Height,
                //this.dibSection.MemoryDeviceContext, 0, 0, Win32.SRCCOPY);
            }
        }

        /// <summary>
        /// Makes the render context current.
        /// </summary>
        public override void MakeCurrent()
        {
            if (this.RenderContextHandle != IntPtr.Zero)
            {
                //Win32.wglMakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
                FakeOperatingSystem.MakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
            }
        }

        /// <summary>
        /// The window handle.
        /// </summary>
        protected IntPtr windowHandle = IntPtr.Zero;


        ///// <summary>
        /////
        ///// </summary>
        //private IntPtr dibSectionDeviceContext = IntPtr.Zero;
        /// <summary>
        ///
        /// </summary>
        private DIBSection dibSection;
    }
}