using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SoftGL;

namespace SoftOpengl32
{
    /// <summary>
    /// Initialization component calls Operating System. Operating System calls Hardware Driver. Hardware Driver is actually SoftGL.
    /// This is the 'opengl32.dll' in SoftGL environment.
    /// </summary>
    public partial class StaticCalls
    {
        /// <summary>
        /// Creates a render context of SoftGL!
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="paramNames">parameters' names.</param>
        /// <param name="paramValues">parameters' values.</param>
        /// <returns></returns>
        public static IntPtr CreateContext(IntPtr deviceContext, int width, int height, string[] paramNames, uint[] paramValues)
        {
            return ContextManager.CreateContext(deviceContext, width, height, paramNames, paramValues);
        }

        /// <summary>
        /// Make specified <paramref name="renderContext"/> the current one of current thread.
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="renderContext"></param>
        public static void MakeCurrent(IntPtr deviceContext, IntPtr renderContext)
        {
            SoftGLRenderContext context = GetContextObj(renderContext);
            var device = SoftGLDeviceContext.FromHandle(deviceContext);
            bool firstBound = ((context != null) && (device != null) && (!context.Bounded));

            ContextManager.MakeCurrent(deviceContext, renderContext);

            if (firstBound)
            {
                int x = 0, y = 0, width = device.Width, height = device.Height;
                SoftGLRenderContext.glViewport(x, y, width, height);
            }
        }

        /// <summary>
        /// Gets current render context's handle.
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetCurrentContext()
        {
            return ContextManager.GetCurrentContext();
        }

        /// <summary>
        /// Gets current render context.
        /// </summary>
        /// <returns></returns>
        private static SoftGLRenderContext GetCurrentContextObj()
        {
            return ContextManager.GetCurrentContextObj();
        }

        /// <summary>
        /// Gets render context object with specified <paramref name="handle"/>.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        private static SoftGLRenderContext GetContextObj(IntPtr handle)
        {
            return ContextManager.GetContextObj(handle);
        }

        /// <summary>
        /// Gets current device context.
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetCurrentDC()
        {
            return ContextManager.GetCurrentDC();
        }

        /// <summary>
        /// Delete specified render context.
        /// </summary>
        /// <param name="renderContext"></param>
        public static void DeleteContext(IntPtr renderContext)
        {
            ContextManager.DeleteContext(renderContext);
        }

        public static IntPtr CreateDeviceContext(int width, int height)
        {
            var dc = new SoftGLDeviceContext(width, height);
            return dc.DeviceContextHandle;
        }
    }
}
