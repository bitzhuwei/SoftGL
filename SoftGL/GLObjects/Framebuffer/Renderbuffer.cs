using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class Renderbuffer : IAttachable
    {
        public uint Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint InternalFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] DataStore { get; set; }

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
            this.InternalFormat = internalformat;
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
            return string.Format("Renderbuffer:internalFormat:{0}, w:{1}, h:{2}", this.InternalFormat, this.Width, this.Height);
        }
    }
}
