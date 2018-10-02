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
        uint Format { get; }

        int Width { get; }

        int Height { get; }

        byte[] DataStore { get; }

    }

    static class IAttachableHelper
    {
        /// <summary>
        /// set <paramref name="attachable"/>'s data store to specified <paramref name="data"/>.
        /// </summary>
        /// <param name="attachable"></param>
        /// <param name="data"></param>
        public static void Clear(this IAttachable attachable, byte[] data)
        {
            if (attachable == null || data == null || data.Length < 1) { return; }

            byte[] dataStore = attachable.DataStore;
            int width = attachable.Width;
            int height = attachable.Height;
            int singleElementByteLength = dataStore.Length / width / height;
            if (singleElementByteLength != data.Length)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        for (int t = 0; t < singleElementByteLength && t < data.Length; t++)
                        {
                            dataStore[(width * j + i) * singleElementByteLength + t] = data[t];
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataStore.Length; i++)
                {
                    dataStore[i] = data[i % data.Length];
                }
            }
        }

        public static void Set(this IAttachable attachable, int x, int y, PassBuffer passbuffer)
        {
            if (attachable == null || passbuffer == null || passbuffer.array == null) { return; }

            byte[] data = passbuffer.ConvertTo(attachable.Format);
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
