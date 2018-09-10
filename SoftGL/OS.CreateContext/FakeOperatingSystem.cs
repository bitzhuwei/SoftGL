using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SoftGL
{
    /// <summary>
    /// Initialization component calls Operating System. Operating System calls Hardware Driver. Hardware Driver is actually SoftGL.
    /// This is the 'opengl32.dll' in SoftGL environment.
    /// </summary>
    public partial class FakeOperatingSystem
    {
        /// <summary>
        /// Creates a render context of SoftGL!
        /// </summary>
        /// <returns></returns>
        public static IntPtr CreateContext(IntPtr deviceContext, int width, int height, ContextGenerationParams parameters)
        {
            var context = new SoftGLRenderContext(width, height, parameters);

            return context.RenderContextHandle;
        }

        public static void MakeCurrent(IntPtr deviceContext, IntPtr renderContext)
        {
            var threadContextDict = SoftGLRenderContext.threadContextDict;
            if (renderContext == IntPtr.Zero) // cancel current render context to current thread.
            {
                SoftGLRenderContext context = null;

                Thread thread = Thread.CurrentThread;
                if (threadContextDict.TryGetValue(thread, out context))
                {
                    threadContextDict.Remove(thread);
                }
                else
                {
                    // TODO: what should I do?
                }
            }
            else // change current render context to current thread.
            {
                Thread thread = Thread.CurrentThread;
                SoftGLRenderContext oldContext = null;
                threadContextDict.TryGetValue(thread, out oldContext);
                SoftGLRenderContext context = null;
                if (SoftGLRenderContext.handleContextDict.TryGetValue(renderContext, out context))
                {
                    if (oldContext != context)
                    {
                        if (oldContext != null) { threadContextDict.Remove(thread); }
                        context.DeviceContextHandle = deviceContext;
                        threadContextDict.Add(thread, context);
                    }
                }
                else
                {
                    // TODO: update last error.
                }
            }
        }

        /// <summary>
        /// Gets current render context's handle.
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetCurrentContextHandle()
        {
            IntPtr result = IntPtr.Zero;

            var threadContextDict = SoftGLRenderContext.threadContextDict;
            SoftGLRenderContext context = null;
            Thread thread = Thread.CurrentThread;
            if (threadContextDict.TryGetValue(thread, out context))
            {
                result = context.RenderContextHandle;
            }

            return result;
        }


        /// <summary>
        /// Gets current render context.
        /// </summary>
        /// <returns></returns>
        internal static SoftGLRenderContext GetCurrentContext()
        {
            var threadContextDict = SoftGLRenderContext.threadContextDict;
            SoftGLRenderContext context = null;
            Thread thread = Thread.CurrentThread;
            if (!threadContextDict.TryGetValue(thread, out context))
            {
                context = null;
            }

            return context;
        }
    }
}
