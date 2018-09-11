using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextRenderbufferName = 0;

        private readonly List<uint> renderbufferNameList = new List<uint>();
        /// <summary>
        /// name -> render buffer object.
        /// </summary>
        private readonly Dictionary<uint, Renderbuffer> nameRenderbufferDict = new Dictionary<uint, Renderbuffer>();

        private Renderbuffer[] currentRenderbuffers = new Renderbuffer[1]; // [GL_RENDERBUFFER]
        private const int maxRenderbufferSize = 1024 * 8; // TOD: 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="names"></param>
        public void GenRenderbuffers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); }

            for (int i = 0; i < count; i++)
            {
                uint name = nextRenderbufferName;
                names[i] = name;
                renderbufferNameList.Add(name);
                nextRenderbufferName++;
            }
        }

        public void BindRenderbuffer(uint target, uint name)
        {
            if (target != GL.GL_RENDERBUFFER) { SetLastError(ErrorCode.InvalidEnum); }
            if ((name != 0) && (!this.renderbufferNameList.Contains(name))) { SetLastError(ErrorCode.InvalidOperation); }

            Dictionary<uint, Renderbuffer> dict = this.nameRenderbufferDict;
            if (!dict.ContainsKey(name)) // for the first time the name is binded, we create a renderbuffer object.
            {
                var obj = new Renderbuffer(name);
                dict.Add(name, obj);
            }

            this.currentRenderbuffers[target - GL.GL_RENDERBUFFER] = dict[name];
        }

        public void RenderbufferStorage(uint target, uint internalformat, int width, int height)
        {
            if (target != GL.GL_RENDERBUFFER) { SetLastError(ErrorCode.InvalidEnum); }
            if (width < 0 || maxRenderbufferSize < width) { SetLastError(ErrorCode.InvalidValue); }
            if (height < 0 || maxRenderbufferSize < height) { SetLastError(ErrorCode.InvalidValue); }
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

        public void DeleteRenderbuffers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); }

            for (int i = 0; i < count; i++)
            {
                uint name = names[i];
                if (renderbufferNameList.Contains(name)) { renderbufferNameList.Remove(name); }
                if (nameRenderbufferDict.ContainsKey(name)) { nameRenderbufferDict.Remove(name); }
            }
        }
    }
}
