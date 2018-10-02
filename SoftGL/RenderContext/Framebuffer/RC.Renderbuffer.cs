using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextRenderbufferName = 1;

        private readonly List<uint> renderbufferNameList = new List<uint>();
        /// <summary>
        /// name -> render buffer object.
        /// </summary>
        private readonly Dictionary<uint, Renderbuffer> nameRenderbufferDict = new Dictionary<uint, Renderbuffer>();

        private Renderbuffer[] currentRenderbuffers = new Renderbuffer[1]; // [GL_RENDERBUFFER]
        private const int maxRenderbufferSize = 1024 * 8; // TODO: maxRenderbufferSize = ?

        public static void glGenRenderbuffers(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.GenRenderbuffers(count, names);
            }
        }

        private void GenRenderbuffers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = nextRenderbufferName;
                names[i] = name;
                renderbufferNameList.Add(name);
                nextRenderbufferName++;
            }
        }

        public static void glBindRenderbuffer(uint target, uint name)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.BindRenderbuffer(target, name);
            }
        }

        private void BindRenderbuffer(uint target, uint name)
        {
            if (target != GL.GL_RENDERBUFFER) { SetLastError(ErrorCode.InvalidEnum); return; }
            if ((name != 0) && (!this.renderbufferNameList.Contains(name))) { SetLastError(ErrorCode.InvalidOperation); return; }

            if (name == 0)
            { this.currentRenderbuffers[target - GL.GL_RENDERBUFFER] = null; }
            else
            {
                Dictionary<uint, Renderbuffer> dict = this.nameRenderbufferDict;
                if (!dict.ContainsKey(name)) // for the first time the name is binded, we create a renderbuffer object.
                {
                    var obj = new Renderbuffer(name);
                    dict.Add(name, obj);
                }

                this.currentRenderbuffers[target - GL.GL_RENDERBUFFER] = dict[name];
            }
        }

        public static bool glIsRenderbuffer(uint name)
        {
            bool result = false;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.IsRenderbuffer(name);
            }

            return result;
        }

        private bool IsRenderbuffer(uint name)
        {
            return ((name > 0) && (nameRenderbufferDict.ContainsKey(name)));
        }

        public static void glRenderbufferStorage(uint target, uint internalformat, int width, int height)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.RenderbufferStorage(target, internalformat, width, height);
            }
        }

        private void RenderbufferStorage(uint target, uint internalformat, int width, int height)
        {
            if (target != GL.GL_RENDERBUFFER) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (width < 0 || maxRenderbufferSize < width) { SetLastError(ErrorCode.InvalidValue); return; }
            if (height < 0 || maxRenderbufferSize < height) { SetLastError(ErrorCode.InvalidValue); return; }
            // TODO: GL_INVALID_ENUM is generated if internalformat​ is not a color-renderable, depth-renderable, or stencil-renderable format.
            // TODO: GL_OUT_OF_MEMORY is generated if the GL is unable to create a data store of the requested size.

            Renderbuffer obj = this.currentRenderbuffers[target - GL.GL_RENDERBUFFER];
            if (obj != null)
            {
                int bitSize = InternalFormatHelper.BitSize(internalformat);
                int bytes = (bitSize % 8 == 0) ? bitSize / 8 : bitSize / 8 + 1; // TODO: any better solution?
                var dataStore = new byte[width * height * bytes];
                obj.Storage(internalformat, width, height, dataStore);
            }
        }

        public static void glDeleteRenderbuffers(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DeleteRenderbuffers(count, names);
            }
        }

        private void DeleteRenderbuffers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = names[i];
                if (renderbufferNameList.Contains(name)) { renderbufferNameList.Remove(name); }
                if (nameRenderbufferDict.ContainsKey(name)) { nameRenderbufferDict.Remove(name); }
            }
        }
    }
}
