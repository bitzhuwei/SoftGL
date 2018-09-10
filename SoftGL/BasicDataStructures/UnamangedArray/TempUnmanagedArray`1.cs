﻿using System;
using System.Runtime.InteropServices;

namespace SoftGL
{
    /// <summary>
    /// An temporary unmanaged huge array who don't dispose its unmanged memory.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, char, short, ushort, int, uint, long, ulong, float, double, decimal, bool or other struct types. enum not supported.</typeparam>
    sealed unsafe partial class TempUnmanagedArray<T> : UnmanagedArrayBase where T : struct
    {
        /// <summary>
        /// An temporary unmanaged huge array who don't dispose its unmanged memory.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="elementCount"></param>
        public TempUnmanagedArray(IntPtr header, int elementCount)
            : base(elementCount, Marshal.SizeOf(typeof(T)))
        {
            this.Header = header;
        }

        // Do not try to use less effitient way of accessing elements as we're using OpenGL.
        // 既然要用OpenGL，就不要试图才用低效的方式了。
        /// <summary>
        /// gets/sets element's value at specified <paramref name="index"/>.
        /// <para>Please use unsafe way when dealing with big data for efficiency purpose.</para>
        /// 获取或设置索引为<paramref name="index"/>的元素。
        /// <para>如果要处理的元素数目较大，请使用unsafe方式（为提高效率）。</para>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public unsafe T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Length)
                    throw new IndexOutOfRangeException("index of UnmanagedArray is out of range");

                var pItem = new IntPtr(this.Header.ToInt64() + (index * elementSize));
                var obj = Marshal.PtrToStructure(pItem, typeof(T));
                T result = (T)obj;
                //T result = Marshal.PtrToStructure<T>(pItem);// works in .net 4.5.1
                return result;
            }
            set
            {
                if (index < 0 || index >= this.Length)
                    throw new IndexOutOfRangeException("index of UnmanagedArray is out of range");

                var pItem = new IntPtr(this.Header.ToInt64() + (index * elementSize));
                Marshal.StructureToPtr(value, pItem, true);
                //Marshal.StructureToPtr<T>(value, pItem, true);// works in .net 4.5.1
            }
        }
    }
}