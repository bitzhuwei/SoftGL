using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    // https://www.khronos.org/opengl/wiki/Sampler_Object
    /// <summary>
    /// Bind a <see cref="Sampler"/> object to a texture uint('s index), and it will affect all textures that bind to the same texture uint.
    /// </summary>
    partial class SoftGLRenderContext
    {
        private uint nextSamplerName = 1;

        /// <summary>
        /// name -> sampler object.
        /// </summary>
        private readonly Dictionary<uint, Sampler> nameSamplerDict = new Dictionary<uint, Sampler>();

        private Sampler[] currentSamplers = new Sampler[maxTextureImageUnits];
        private uint maxCombinedTextureImageUnits = 64; // TODO: maxCombinedTextureImageUnits = ?

        public static void glGenSamplers(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.GenSamplers(count, names);
            }
        }

        private void GenSamplers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = nextSamplerName;
                names[i] = name;
                var obj = new Sampler(name);
                this.nameSamplerDict.Add(name, obj);
                nextSamplerName++;
            }
        }

        public static void glBindSampler(uint unit, uint name)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.BindSampler(unit, name);
            }
        }

        private void BindSampler(uint unit, uint name)
        {
            if (unit >= maxCombinedTextureImageUnits) { SetLastError(ErrorCode.InvalidValue); return; }
            if ((name != 0) && (!this.nameSamplerDict.ContainsKey(name))) { SetLastError(ErrorCode.InvalidOperation); return; }

            if (name == 0) { this.currentRenderbuffers[unit] = null; }
            else { this.currentSamplers[unit] = this.nameSamplerDict[name]; }
        }

        public static bool glIsSampler(uint name)
        {
            bool result = false;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.IsSampler(name);
            }

            return result;
        }

        private bool IsSampler(uint name)
        {
            return ((name > 0) && (this.nameSamplerDict.ContainsKey(name)));
        }

        public static void glDeleteSamplers(int count, uint[] names)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DeleteSamplers(count, names);
            }
        }

        private void DeleteSamplers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = names[i];
                if (nameSamplerDict.ContainsKey(name)) { nameSamplerDict.Remove(name); }
            }
        }
    }
}
