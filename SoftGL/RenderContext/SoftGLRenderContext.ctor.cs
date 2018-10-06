using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
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
        /// creates render context.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="paramNames">parameters' names.</param>
        /// <param name="paramValues">parameters' values.</param>
        public SoftGLRenderContext(int width, int height, string[] paramNames, uint[] paramValues)
        {
            {
                if (paramNames != null)
                {
                    if (paramValues == null || paramNames.Length != paramValues.Length)
                    { throw new ArgumentException("Names no matching with values!"); }
                }
                else if (paramValues != null)
                { throw new ArgumentException("Names no matching with values!"); }
                else // both are null.
                {
                    paramNames = new string[0];
                    paramValues = new uint[0];
                }

                this.Width = width;
                this.Height = height;
                this.ParamNames = paramNames;
                this.ParamValues = paramValues;
            }
            {
                GCHandle handle = GCHandle.Alloc(this, GCHandleType.WeakTrackResurrection);
                this.RenderContextHandle = GCHandle.ToIntPtr(handle);
                handle.Free();
                handleContextDict.Add(this.RenderContextHandle, this);
                //allRenderContexts.Add(this);
            }

            // TODO: move this dc to SoftGL.Windows.
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

            // Create the default framebuffer object and make it the current one.
            {
                var ids = new uint[1];
                glGenFramebuffers(ids.Length, ids);
                if (ids[0] != 0) { throw new Exception("This framebuffer must be 0!"); }
                glBindFramebuffer((uint)BindFramebufferTarget.Framebuffer, ids[0]); // bind the default framebuffer object.
                this.defaultFramebuffer = this.nameFramebufferDict[0];
                InitDefaultFramebuffer(width, height);
            }

            InitBufferDict();
        }

        // abstract window for now.
        private Bitmap window;
        private Graphics graphics;
        /// <summary>
        /// Blit the rendered data to the supplied device context.
        /// </summary>
        /// <param name="deviceContext">The HDC.</param>
        public void Blit(IntPtr deviceContext)
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
        public void MakeCurrent()
        {
            if (this.RenderContextHandle != IntPtr.Zero)
            {
                //Win32.wglMakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
                ContextManager.MakeCurrent(this.DeviceContextHandle, this.RenderContextHandle);
            }
        }


    }
}