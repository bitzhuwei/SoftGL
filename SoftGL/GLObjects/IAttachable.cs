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
        /// Clear attachment with specified <paramref name="values"/>.
        /// </summary>
        /// <param name="values"></param>
        void Clear(byte[] values);
    }

    static class IAttachableHelper
    {
        /// <summary>
        /// Fill specified <paramref name="dataStore"/> with specified <paramref name="values"/>.
        /// </summary>
        /// <param name="dataStore"></param>
        /// <param name="values"></param>
        public static void Fill(this byte[] dataStore, byte[] values)
        {
            if (dataStore == null) { throw new ArgumentNullException("dataStore"); }
            if (values == null || values.Length < 1) { throw new ArgumentNullException("values"); }

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
    }
}
