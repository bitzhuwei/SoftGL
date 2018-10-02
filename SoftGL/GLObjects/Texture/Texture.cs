using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    /// <summary>
    /// Bind a <see cref="Sampler"/> object to a texture uint('s index), and it will affect all textures that bind to the same texture uint.
    /// </summary>
    partial class Texture : IAttachable
    {
        public BindTextureTarget Target { get; private set; }

        public uint Id { get; private set; }

        public Texture(BindTextureTarget target, uint id)
        {
            this.Target = target;
            this.Id = id;

            this.InitParameters(); // TODO: Is this needed?
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Texture: Id:{0}, T:{1}", this.Id, this.Target);
        }
    }
}
