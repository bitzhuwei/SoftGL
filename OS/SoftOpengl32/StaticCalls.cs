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
        /// <returns></returns>
        public static IntPtr CreateContext(IntPtr deviceContext, int width, int height, ContextGenerationParams parameters)
        {
            return ContextManager.CreateContext(deviceContext, width, height, parameters);
        }

        /// <summary>
        /// Make specified <paramref name="renderContext"/> the current one of current thread.
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="renderContext"></param>
        public static void MakeCurrent(IntPtr deviceContext, IntPtr renderContext)
        {
            ContextManager.MakeCurrent(deviceContext, renderContext);
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
        public static SoftGLRenderContext GetCurrentContextObj()
        {
            return ContextManager.GetCurrentContextObj();
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
    }
}
