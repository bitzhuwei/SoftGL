using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    /// <summary>
    /// Bind a <see cref="Sampler"/> object to a texture uint('s index), and it will affect all textures that bind to the same texture uint.
    /// </summary>
    partial class Sampler : List<SamplerParameter>
    {
        public uint Id { get; private set; }

        public Sampler(uint id) { this.Id = id; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Sampler: Id:{0}", this.Id);
        }
    }
}
