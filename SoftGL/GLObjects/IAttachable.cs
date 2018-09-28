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
        int Width { get; }

        int Height { get; }

        byte[] DataStore { get; }

    }

    static class IAttachableHelper
    {
        /// <summary>
        /// set <paramref name="attachable"/>'s data store to specified <paramref name="values"/>.
        /// </summary>
        /// <param name="attachable"></param>
        /// <param name="values"></param>
        public static void Clear(this IAttachable attachable, byte[] values)
        {
            if (attachable == null || values == null || values.Length < 1) { return; }

            int interval = values.Length;
            byte[] dataStore = attachable.DataStore;
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

        public static void Set(this IAttachable attachable, int x, int y, byte[] data)
        {
            if (attachable == null || data == null) { return; }

            byte[] dataStore = attachable.DataStore;
            int width = attachable.Width;
            int height = attachable.Height;
            int singleElementByteLength = dataStore.Length / width / height;
            if (singleElementByteLength != data.Length)
            {
                for (int i = 0; i < singleElementByteLength && i < data.Length; i++)
                {
                    attachable.DataStore[(width * y + x) * singleElementByteLength + i] = data[i];
                }
            }
            else
            {
                for (int i = 0; i < singleElementByteLength; i++)
                {
                    attachable.DataStore[(width * y + x) * singleElementByteLength + i] = data[i];
                }
            }
        }

    }
}
