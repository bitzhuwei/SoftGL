using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class Renderbuffer : IAttachable
    {
        public uint Id { get; private set; }

        public Renderbuffer(uint id) { this.Id = id; }

        /// <summary>
        /// RenderbufferStorage(..).
        /// </summary>
        /// <param name="internalformat"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="dataStore"></param>
        public void Storage(uint internalformat, int width, int height, byte[] dataStore)
        {
            this.Format = internalformat;
            this.Width = width;
            this.Height = height;
            this.DataStore = dataStore;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Renderbuffer:internalFormat:{0}, w:{1}, h:{2}", this.Format, this.Width, this.Height);
        }
    }
}
