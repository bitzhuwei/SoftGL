using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    /// <summary>
    /// Bind a <see cref="Sampler"/> object to a texture uint('s index), and it will affect all textures that bind to the same texture uint.
    /// </summary>
    partial class SoftGLRenderContext
    {
        private uint nextSamplerName = 0;

        private readonly List<uint> samplerNameList = new List<uint>();
        /// <summary>
        /// name -> render buffer object.
        /// </summary>
        private readonly Dictionary<uint, Sampler> nameSamplerDict = new Dictionary<uint, Sampler>();

        private Sampler currentSampler;
        private uint maxCombinedTextureImageUnits = 64; // TODO: maxCombinedTextureImageUnits = ?

        public void GenSamplers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = nextSamplerName;
                names[i] = name;
                samplerNameList.Add(name);
                nextSamplerName++;
            }
        }

        public void BindSampler(uint unit, uint name)
        {
            if (unit >= maxCombinedTextureImageUnits) { SetLastError(ErrorCode.InvalidValue); return; }
            if ((name != 0) && (!this.samplerNameList.Contains(name))) { SetLastError(ErrorCode.InvalidOperation); return; }

            Dictionary<uint, Sampler> dict = this.nameSamplerDict;
            if (!dict.ContainsKey(name)) // for the first time the name is binded, we create a renderbuffer object.
            {
                var obj = new Sampler(name);
                dict.Add(name, obj);
            }

            this.currentSampler = dict[name];
        }

        public void DeleteSamplers()
        {

        }
    }
}
