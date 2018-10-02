using System;
using System.Collections.Generic;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextBufferName = 1;

        private readonly List<uint> bufferNameList = new List<uint>();
        /// <summary>
        /// name -> buffer object.
        /// </summary>
        private readonly Dictionary<uint, GLBuffer> nameBufferDict = new Dictionary<uint, GLBuffer>();

        private GLBuffer currentBuffer;

        public static void glGenBuffers(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.GenBuffers(count, names);
            }
        }

        private void GenBuffers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = nextBufferName;
                names[i] = name;
                bufferNameList.Add(name);
                nextBufferName++;
            }
        }

        public static void glBindBuffer(uint target, uint name)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.BindBuffer((BindBufferTarget)target, name);
            }
        }

        private void BindBuffer(BindBufferTarget target, uint name)
        {
            if (target == 0) { SetLastError(ErrorCode.InvalidEnum); return; }
            if ((name != 0) && (!this.bufferNameList.Contains(name))) { SetLastError(ErrorCode.InvalidValue); return; }
            GLBuffer buffer = null;
            if (name != 0)
            {
                Dictionary<uint, GLBuffer> dict = this.nameBufferDict;
                if (dict.TryGetValue(name, out buffer))
                {
                    if (buffer.Target != target) { SetLastError(ErrorCode.InvalidOperation); return; }
                }
                else // create a new buffer object.
                {
                    buffer = new GLBuffer(target, name);
                    dict.Add(name, buffer);
                }
            }

            this.currentBuffer = buffer;
        }

        public static bool glIsBuffer(uint name)
        {
            bool result = false;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.IsBuffer(name);
            }

            return result;
        }

        private bool IsBuffer(uint name)
        {
            return ((name > 0) && (nameBufferDict.ContainsKey(name)));
        }

        public static void glDeleteBuffers(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DeleteBuffers(count, names);
            }
        }

        private void DeleteBuffers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = names[i];
                if (bufferNameList.Contains(name)) { bufferNameList.Remove(name); }
                if (nameBufferDict.ContainsKey(name)) { nameBufferDict.Remove(name); }
            }
        }
    }
}
