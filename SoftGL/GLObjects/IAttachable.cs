using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    /// <summary>
    /// Objects that can be attached to framebuffer's attachment point.
    /// </summary>
    interface IAttachable
    {
        /// <summary>
        /// Clear attachment with specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        void Clear(byte[] value);
    }
}
