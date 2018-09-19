using System;
using System.Runtime.InteropServices;

namespace SoftGL
{
    /// <summary>
    ///
    /// </summary>
    static class StructHelper
    {
        ///// <summary>
        ///// 从当前位置读取一个struct并前移Stream的位置<code>Marshal.SizeOf(typeof(T))</code>的距离。
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="br"></param>
        ///// <param name="result"></param>
        //public static T ReadStruct<T>(this BinaryReader br) where T : struct
        //{
        //    T result;

        //    int size = Marshal.SizeOf(typeof(T));
        //    byte[] bytes = br.ReadBytes(size);
        //    bytes.GetStruct(out result);

        //    return result;
        //}

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="structObj"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this object structObj)
        {
            Int32 size = Marshal.SizeOf(structObj);
            Byte[] bytes = new Byte[size];
            IntPtr buffer = IntPtr.Zero;
            try
            {
                buffer = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(structObj, buffer, false);
                Marshal.Copy(buffer, bytes, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="packet"></param>
        /// <param name="type"></param>
        /// <param name="startPos"></param>
        /// <returns></returns>
        public static object ToStruct(this byte[] packet, Type type, int startPos = 0)
        {
            GCHandle pin = GCHandle.Alloc(packet, GCHandleType.Pinned);
            IntPtr address = Marshal.UnsafeAddrOfPinnedArrayElement(packet, startPos);
            object result = Marshal.PtrToStructure(address, type);
            pin.Free();

            return result;
        }
    }
}