using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    public partial class SoftGLRenderContext
    {
        /// <summary>
        /// RenderContextHandle -> Render Context Object.
        /// </summary>
        internal static readonly Dictionary<IntPtr, SoftGLRenderContext> handleContextDict = new Dictionary<IntPtr, SoftGLRenderContext>();
        /// <summary>
        /// Thread -> Binding Render Context Object.
        /// </summary>
        internal static readonly Dictionary<Thread, SoftGLRenderContext> threadContextDict = new Dictionary<Thread, SoftGLRenderContext>();

        /// <summary>
        /// creates render device and render context.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="paramNames">parameters' names.</param>
        /// <param name="paramValues">parameters' values.</param>
        public SoftGLRenderContext(int width, int height, string[] paramNames, uint[] paramValues)
            : base(width, height, paramNames, paramValues)
        {
            {
                GCHandle handle = GCHandle.Alloc(this, GCHandleType.WeakTrackResurrection);
                this.RenderContextHandle = GCHandle.ToIntPtr(handle);
                handle.Free();
                handleContextDict.Add(this.RenderContextHandle, this);
                //allRenderContexts.Add(this);
            }

            {
                ////	Create the window. Position and size it.
                var window = new Bitmap(width, height);
                var graphics = Graphics.FromImage(window);
                ////	Get the window device context.
                //GCHandle handle = GCHandle.Alloc(window, GCHandleType.WeakTrackResurrection);
                //this.DeviceContextHandle = GCHandle.ToIntPtr(handle);
                //handle.Free();
                this.DeviceContextHandle = graphics.GetHdc();
                this.window = window;
                this.graphics = graphics;
            }

            ContextManager.MakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);

            InitDefaultFramebuffer();
            this.defaultFramebuffer = this.nameFramebufferDict[0];

        }

        // abstract window for now.
        private Bitmap window;
        private Graphics graphics;

        /// <summary>
        /// The window handle.
        /// </summary>
        protected IntPtr windowHandle = IntPtr.Zero;

        /// <summary>
        /// Destroys the render context provider instance.
        /// </summary>
        protected override void DisposeUnmanagedResources()
        {
            ////	Release the device context and the window.
            //Win32.ReleaseDC(windowHandle, this.DeviceContextHandle);
            this.graphics.ReleaseHdc();
            this.graphics.Dispose();
            this.window.Dispose();

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
        /// Blit the rendered data to the supplied device context.
        /// </summary>
        /// <param name="deviceContext">The HDC.</param>
        public override void Blit(IntPtr deviceContext)
        {
            ////IntPtr dc = this.DeviceContextHandle;
            ////if (dc != IntPtr.Zero || windowHandle != IntPtr.Zero)
            ////{
            ////    //	Swap the buffers.
            ////    Win32.SwapBuffers(dc);

            ////    //	Blit the DC (containing the DIB section) to the target DC.
            ////    Win32.BitBlt(deviceContext, 0, 0, this.Width, this.Height, dc, 0, 0, Win32.SRCCOPY);
            ////}

            //if (this.DeviceContextHandle != IntPtr.Zero)
            //{
            //    ////  Set the read buffer.
            //    //GL.Instance.ReadBuffer(GL.GL_COLOR_ATTACHMENT0);

            //    //	Read the pixels into the DIB section.
            //    GL.Instance.ReadPixels(0, 0, this.Width, this.Height, GL.GL_BGRA,
            //        GL.GL_UNSIGNED_BYTE, this.dibSection.Bits);

            //    //	Blit the DC (containing the DIB section) to the target DC.
            //    //Win32.BitBlt(deviceContext, 0, 0, this.Width, this.Height,
            //    //this.dibSection.MemoryDeviceContext, 0, 0, Win32.SRCCOPY);
            //}
        }

        /// <summary>
        /// Makes the render context current.
        /// </summary>
        public override void MakeCurrent()
        {
            if (this.RenderContextHandle != IntPtr.Zero)
            {
                //Win32.wglMakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
                ContextManager.MakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
            }
        }


    }
}