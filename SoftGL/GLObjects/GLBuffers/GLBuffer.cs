using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class GLBuffer
    {
        public BindBufferTarget Target { get; private set; }

        public uint Id { get; private set; }

        public int Size { get; private set; }

        private byte[] data;
        public byte[] Data { get { return this.data; } }

        public Usage Usage { get; private set; }

        public GLBuffer(BindBufferTarget target, uint id)
        {
            this.Target = target;
            this.Id = id;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("GLBuffer: Id:{0}, T:{1}", this.Id, this.Target);
        }

        public unsafe void SetData(int size, IntPtr data, Usage usage)
        {
            this.Size = size;

            var newData = new byte[size];
            if (data != IntPtr.Zero)
            {
                byte* array = (byte*)data.ToPointer();
                for (int i = 0; i < size; i++)
                {
                    newData[i] = array[i];
                }
            }
            this.data = newData;

            this.Usage = usage;
        }
    }
}
