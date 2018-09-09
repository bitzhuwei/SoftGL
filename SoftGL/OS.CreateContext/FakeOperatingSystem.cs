using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    /// <summary>
    /// Initialization component calls Operating System. Operating System calls Hardware Driver. Hardware Driver is actually SoftGL.
    /// This is the 'opengl32.dll' in SoftGL environment.
    /// </summary>
    public class FakeOperatingSystem
    {
        /// <summary>
        /// Creates a render context of SoftGL!
        /// </summary>
        /// <returns></returns>
        public static IntPtr CreateContext(IntPtr deviceContext, int width, int height, ContextGenerationParams parameters)
        {
            var context = new SoftGLRenderContext(width, height, parameters);
            var key = new ContexPair(deviceContext, context.RenderContextHandle);
            SoftGLRenderContext.contextDict.Add(key, context);

            return context.RenderContextHandle;
        }

        public static void MakeCurrent(IntPtr deviceContext, IntPtr renderContext)
        {
            if (deviceContext == IntPtr.Zero) { return; }

            var key = new ContexPair(deviceContext, renderContext);
            var dict = SoftGLRenderContext.contextDict;
            SoftGLRenderContext context = null;
            if (dict.TryGetValue(key, out context))
            {
                if (renderContext == IntPtr.Zero)
                {
                    dict.Remove(key);
                }
                else
                {
                    context.currentDeviceContext = deviceContext;
                }
            }
            else
            {

            }
        }
    }
}
