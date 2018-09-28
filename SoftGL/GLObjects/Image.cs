using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class Image : IAttachable
    {
        #region IAttachable

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

        public Image(int width, int height, int elementByteLength)
        {
            this.Width = width; this.Height = height;
            this.DataStore = new byte[elementByteLength * width * height];
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Image, w:{0}, h:{1}", this.Width, this.Height);
        }
    }
}
