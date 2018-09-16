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
            if (values == null || values.Length < 1) { throw new ArgumentNullException("values"); }
            byte[] dataStore = this.DataStore;
            int interval = values.Length;
            int total = dataStore.Length;
            int tail = total % interval;
            int i = 0;
            for (; i + interval < total; i += interval)
            {
                for (int j = 0; j < interval; j++)
                {
                    dataStore[i + j] = values[j];
                }
            }
            {
                for (int j = 0; j < tail; j++)
                {
                    dataStore[i + j] = values[j];
                }
            }
        }

        #endregion IAttachable
    }
}
