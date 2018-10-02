using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class Renderbuffer
    {
        #region IAttachable

        public void Clear(byte[] values)
        {
            IAttachableHelper.Fill(this.DataStore, values);
        }

        public void Set(int x, int y, byte[] data)
        {
            if (data == null) { return; }

            int singleElementByteLength = this.DataStore.Length / this.Width / this.Height;
            for (int i = 0; i < singleElementByteLength && i < data.Length; i++)
            {
                this.DataStore[(this.Width * y + x) * singleElementByteLength + i] = data[i];
            }
        }

        #endregion IAttachable
    }
}
