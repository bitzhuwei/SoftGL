using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class Renderbuffer
    {
        #region IAttachable

        public uint Format { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] DataStore { get; private set; }

        #endregion IAttachable
    }
}
