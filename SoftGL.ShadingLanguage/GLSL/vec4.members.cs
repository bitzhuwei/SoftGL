using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial struct vec4
    {
        /// <summary>
        /// x = r = s
        /// </summary>
        [FieldOffset(sizeof(float) * 0)]
        public float x;

        /// <summary>
        /// y = g = t
        /// </summary>
        [FieldOffset(sizeof(float) * 1)]
        public float y;

        /// <summary>
        /// z = b = p
        /// </summary>
        [FieldOffset(sizeof(float) * 2)]
        public float z;

        /// <summary>
        /// w = a = q
        /// </summary>
        [FieldOffset(sizeof(float) * 3)]
        public float w;

        /// <summary>
        /// x = r = s
        /// </summary>
        public float r { get { return this.x; } set { this.x = value; } }

        /// <summary>
        /// y = g = t
        /// </summary>
        public float g { get { return this.y; } set { this.y = value; } }

        /// <summary>
        /// z = b = p
        /// </summary>
        public float b { get { return this.z; } set { this.z = value; } }

        /// <summary>
        /// w = a = q
        /// </summary>
        public float a { get { return this.w; } set { this.w = value; } }

        /// <summary>
        /// x = r = s
        /// </summary>
        public float s { get { return this.x; } set { this.x = value; } }

        /// <summary>
        /// y = g = t
        /// </summary>
        public float t { get { return this.y; } set { this.y = value; } }

        /// <summary>
        /// z = b = p
        /// </summary>
        public float p { get { return this.z; } set { this.z = value; } }

        /// <summary>
        /// w = a = q
        /// </summary>
        public float q { get { return this.w; } set { this.w = value; } }

    }
}