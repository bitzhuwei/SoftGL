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

        private Dictionary<BindFramebufferTarget, Framebuffer> currentFramebuffers = new Dictionary<BindFramebufferTarget, Framebuffer>(3);

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

        public static void glBindFramebuffer(uint target, uint name)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.BindFramebuffer((BindFramebufferTarget)target, name);
            }
        }

        private void BindFramebuffer(BindFramebufferTarget target, uint name)
        {
            if (target == 0) { SetLastError(ErrorCode.InvalidEnum); return; }
            if ((name != 0) && (!this.framebufferNameList.Contains(name))) { SetLastError(ErrorCode.InvalidOperation); return; }

            Dictionary<BindFramebufferTarget, Framebuffer> currentFramebuffers = this.currentFramebuffers;
            Dictionary<uint, Framebuffer> dict = this.nameFramebufferDict;

            if (!dict.ContainsKey(name)) // for the first time the name is binded, we create a framebuffer object.
            {
                var obj = new Framebuffer(name);
                dict.Add(name, obj);
            }

            currentFramebuffers[target] = dict[name];
        }

        public static bool glIsFramebuffer(uint name)
        {
            bool result = false;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.IsFramebuffer(name);
            }

            return result;
        }

        private bool IsFramebuffer(uint name)
        {
            return ((name > 0) && (nameFramebufferDict.ContainsKey(name)));
        }

    }

    enum BindFramebufferTarget : uint
    {
        /// <summary>
        /// 0x8CA9
        /// </summary>
        DrawFramebuffer = GL.GL_DRAW_FRAMEBUFFER,
        /// <summary>
        /// 0x8CA8
        /// </summary>
        ReadFramebuffer = GL.GL_READ_FRAMEBUFFER,
        /// <summary>
        /// 0x8D40
        /// </summary>
        Framebuffer = GL.GL_FRAMEBUFFER
    }
}
