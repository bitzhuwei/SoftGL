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

        private Sampler[] currentSamplers = new Sampler[1]; // []

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="names"></param>
        public void GenSamplers(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); }

            for (int i = 0; i < count; i++)
            {
                uint name = nextSamplerName;
                names[i] = name;
                samplerNameList.Add(name);
                nextSamplerName++;
            }
        }

    }
}
