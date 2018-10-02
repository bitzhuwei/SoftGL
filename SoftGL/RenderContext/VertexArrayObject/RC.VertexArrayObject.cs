using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextVertexArrayName = 0;

        private readonly List<uint> vertexArrayNameList = new List<uint>();
        /// <summary>
        /// name -> texture object.
        /// </summary>
        private readonly Dictionary<uint, VertexArrayObject> nameVertexArrayDict = new Dictionary<uint, VertexArrayObject>();

        private VertexArrayObject currentVertexArrayObject;


        public static void glGenVertexArrays(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.GenVertexArrays(count, names);
            }
        }

        private void GenVertexArrays(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = nextVertexArrayName;
                names[i] = name;
                vertexArrayNameList.Add(name);
                nextVertexArrayName++;
            }
        }

        public static void glBindVertexArray(uint name)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.BindVertexArray(name);
            }
        }

        private void BindVertexArray(uint name)
        {
            if ((name != 0) && (!this.vertexArrayNameList.Contains(name))) { SetLastError(ErrorCode.InvalidOperation); return; }
            VertexArrayObject obj = null;
            Dictionary<uint, VertexArrayObject> dict = this.nameVertexArrayDict;
            if (!dict.TryGetValue(name, out obj)) // create a new texture object.
            {
                obj = new VertexArrayObject(name);
                dict.Add(name, obj);
            }

            this.currentVertexArrayObject = obj;
        }

        public static bool glIsVertexArray(uint name)
        {
            bool result = false;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.IsVertexArray(name);
            }

            return result;
        }

        private bool IsVertexArray(uint name)
        {
            return ((name > 0) && (nameVertexArrayDict.ContainsKey(name)));
        }

        public static void glDeleteVertexArrays(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DeleteVertexArrays(count, names);
            }
        }

        private void DeleteVertexArrays(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = names[i];
                if (name > 0)
                {
                    if (vertexArrayNameList.Contains(name)) { vertexArrayNameList.Remove(name); }
                    if (nameVertexArrayDict.ContainsKey(name)) { nameVertexArrayDict.Remove(name); }
                }
            }
        }

    }
}
