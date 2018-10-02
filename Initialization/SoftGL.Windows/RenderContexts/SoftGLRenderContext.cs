﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CSharpGL;

namespace SoftGL.Windows
{
    /// <summary>
    /// creates render device and render context.
    /// </summary>
    public partial class SoftGLRenderContext : CSharpGL.GLRenderContext
    {
        /// <summary>
        /// creates render device and render context.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="parameters">parameters.</param>
        /// <returns></returns>
        public SoftGLRenderContext(int width, int height, ContextGenerationParams parameters)
            : base(width, height)
        {
            this.Parameters = parameters;

            // Create a new window class, as basic as possible.
            if (!this.CreateBasicRenderContext(width, height, parameters))
            {
                throw new Exception(string.Format("Create basic render context failed!"));
            }

            //  Make the context current.
            this.MakeCurrent();

            if (parameters.UpdateContextVersion)
            {
                //  Update the context if required.
                // if I update context, something in legacy opengl will not work...
                this.UpdateContextVersion(width, height, parameters);
            }

            this.dibSection = new DIBSection(this.DeviceContextHandle, width, height, parameters);
        }

        /// <summary>
        /// Create a new window class, as basic as possible.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private bool CreateBasicRenderContext(int width, int height, ContextGenerationParams parameters)
        {
            // TODO: create a System.Windows.Forms.Control to work as DeviceContext.
            const int left = 0, top = 0;
            var control = new System.Windows.Forms.Control("CSharpGLRenderWindow", left, top, width, height);

            //	Get the window device context.
            this.DeviceContextHandle = control.Handle;

            return true;
        }

        /// <summary>
        /// Only valid to be called after the render context is created, this function attempts to
        /// move the render context to the OpenGL version originally requested. If this is &gt; 2.1, this
        /// means building a new context. If this fails, we'll have to make do with 2.1.
        /// </summary>
        /// <param name="parameters"></param>
        protected bool UpdateContextVersion(int width, int height, ContextGenerationParams parameters)
        {
            IntPtr dc = this.DeviceContextHandle;
            var paramNames = new string[0]; var paramValues = new uint[0];
            IntPtr hrc = SoftOpengl32.StaticCalls.CreateContext(dc, width, height, paramNames, paramValues);
            SoftOpengl32.StaticCalls.MakeCurrent(IntPtr.Zero, IntPtr.Zero);
            SoftOpengl32.StaticCalls.DeleteContext(this.RenderContextHandle);
            SoftOpengl32.StaticCalls.MakeCurrent(dc, hrc);
            this.RenderContextHandle = hrc;

            return true;
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
            //if (this.RenderContextHandle != IntPtr.Zero)
            //Win32.wglMakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
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

        public ContextGenerationParams Parameters { get; set; }
    }
}