using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextFramebufferName = 0;

        private readonly List<uint> framebufferNameList = new List<uint>();
        /// <summary>
        /// name -> render buffer object.
        /// </summary>
        private readonly Dictionary<uint, Framebuffer> nameFramebufferDict = new Dictionary<uint, Framebuffer>();

        private Framebuffer currentFramebuffer = null;

        public static void glGenFramebuffers(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.GenFramebuffers(count, names);
            }
        }

        private void GenFramebuffers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = nextFramebufferName;
                names[i] = name;
                framebufferNameList.Add(name);
                nextFramebufferName++;
            }
        }
    }
}
