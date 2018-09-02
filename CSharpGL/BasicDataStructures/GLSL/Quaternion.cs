﻿using System;
using System.Diagnostics;

namespace CSharpGL
{
    /// <summary>
    /// Quaternion
    /// </summary>
    public struct Quaternion
    {
        /// <summary>
        ///
        /// </summary>
        private float w;

        /// <summary>
        ///
        /// </summary>
        private float x;

        /// <summary>
        ///
        /// </summary>
        private float y;

        /// <summary>
        ///
        /// </summary>
        private float z;

        /// <summary>
        /// Quaternion
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        internal Quaternion(float w, float x, float y, float z)
        {
            this.w = w;
            if (x == 0.0f && y == 0.0f && z == 0.0f)
            {
                Debug.WriteLine("Quaternion with axis not well defined!");
            }
            this.x = x; this.y = y; this.z = z;
        }

        /// <summary>
        /// Quaternion from a rotation angle and axis.
        /// </summary>
        /// <param name="angleDegree"></param>
        /// <param name="axis"></param>
        public Quaternion(float angleDegree, vec3 axis)
        {
            if (axis.x == 0.0f && axis.y == 0.0f && axis.z == 0.0f)
            {
                Debug.WriteLine("Quaternion with axis not well defined!");
            }

            vec3 normalized = axis.normalize();
            double radian = angleDegree * Math.PI / 180.0;
            double halfRadian = radian / 2.0;
            this.w = (float)Math.Cos(halfRadian);
            float sin = (float)Math.Sin(halfRadian);
            this.x = sin * normalized.x;
            this.y = sin * normalized.y;
            this.z = sin * normalized.z;
        }

        /// <summary>
        /// Transform this quaternion to equivalent matrix.
        /// </summary>
        /// <returns></returns>
        public mat3 ToRotationMatrix()
        {
            float ww = w * w;
            float xx = x * x;
            float yy = y * y;
            float zz = z * z;
            float wx = w * x;
            float wy = w * y;
            float wz = w * z;
            float xy = x * y;
            float xz = x * z;
            float yz = y * z;
            vec3 col0 = new vec3(
                2 * (xx + ww) - 1,
                2 * (xy + wz),
                2 * (xz - wy));
            vec3 col1 = new vec3(
                2 * (xy - wz),
                2 * (yy + ww) - 1,
                2 * (yz + wx));
            vec3 col2 = new vec3(
                2 * (xz + wy),
                2 * (yz - wx),
                2 * (zz + ww) - 1);

            return new mat3(col0, col1, col2);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="angleInDegree"></param>
        /// <param name="axis"></param>
        public void Parse(out float angleInDegree, out vec3 axis)
        {
            angleInDegree = (float)(Math.Acos(w) * 2 * 180.0 / Math.PI);
            axis = (new vec3(x, y, z)).normalize();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}°, <{1}, {2}, {3}>", Math.Acos(w) * 2 * 180.0f / Math.PI, x, y, z);
        }
    }
}