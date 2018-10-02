using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SoftGL
{
    /// <summary>
    /// transfer data from one stage to next.
    /// </summary>
    class PassBuffer
    {
        public readonly PassType elementType;
        public readonly byte[] array;

        public PassBuffer(PassType type, int length)
        {
            this.elementType = type;
            this.array = new byte[type.ByteSize() * length];
        }

        public int Length()
        {
            int result = this.array.Length / this.elementType.ByteSize();

            return result;
        }
        GCHandle pin;

        public unsafe IntPtr Mapbuffer()
        {
            this.pin = GCHandle.Alloc(this.array, GCHandleType.Pinned);
            IntPtr pointer = this.pin.AddrOfPinnedObject();
            return pointer;
        }

        public void Unmapbuffer()
        {
            this.pin.Free();
        }

        public override string ToString()
        {
            byte[] array = this.array;
            return string.Format("this = new byte[{1}]; inner type:{0}", this.elementType, array != null ? array.Length : 0);
        }
    }

    enum PassType
    {
        Float, Vec2, Vec3, Vec4,
        Mat2, Mat3, Mat4,
    }
    static unsafe class PassTypeHelper
    {
        public static byte[] ConvertTo(this PassBuffer passbuffer, uint internalFormat)
        {
            byte[] result;
            switch (passbuffer.elementType)
            {
                case PassType.Float: result = ConvertFloatTo(passbuffer.array, internalFormat); break;
                case PassType.Vec2: result = ConvertVec2To(passbuffer.array, internalFormat); break;
                case PassType.Vec3: result = ConvertVec3To(passbuffer.array, internalFormat); break;
                case PassType.Vec4: result = ConvertVec4To(passbuffer.array, internalFormat); break;
                case PassType.Mat2: result = ConvertMat2To(passbuffer.array, internalFormat); break;
                case PassType.Mat3: result = ConvertMat3To(passbuffer.array, internalFormat); break;
                case PassType.Mat4: result = ConvertMat4To(passbuffer.array, internalFormat); break;
                default:
                    throw new NotDealWithNewEnumItemException(typeof(PassType));
            }
            return result;
        }

        private static byte[] ConvertFloatTo(byte[] value, uint internalFormat)
        {
            byte[] result = null;
            if (internalFormat == GL.GL_RGBA)
            {

            }
            else if (internalFormat == GL.GL_BGRA)
            {

            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }

        private static byte[] ConvertVec2To(byte[] value, uint internalFormat)
        {
            byte[] result = null;
            if (internalFormat == GL.GL_RGBA)
            {

            }
            else if (internalFormat == GL.GL_BGRA)
            {

            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }

        private static byte[] ConvertVec3To(byte[] value, uint internalFormat)
        {
            byte[] result = null;
            if (internalFormat == GL.GL_RGBA)
            {

            }
            else if (internalFormat == GL.GL_BGRA)
            {

            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }

        private static byte[] ConvertVec4To(byte[] value, uint internalFormat)
        {
            byte[] result = new byte[4];
            GCHandle pin = GCHandle.Alloc(value, GCHandleType.Pinned);
            IntPtr address = Marshal.UnsafeAddrOfPinnedArrayElement(value, 0);
            var array = (vec4*)address.ToPointer();
            if (internalFormat == GL.GL_RGBA)
            {
                result[0] = (byte)(array[0].x * 255);
                result[1] = (byte)(array[0].y * 255);
                result[2] = (byte)(array[0].z * 255);
                result[3] = (byte)(array[0].w * 255);
            }
            else if (internalFormat == GL.GL_BGRA)
            {
                result[0] = (byte)(array[0].z * 255);
                result[1] = (byte)(array[0].y * 255);
                result[2] = (byte)(array[0].x * 255);
                result[3] = (byte)(array[0].w * 255);
            }
            else
            {
                throw new NotImplementedException();
            }
            pin.Free();

            return result;
        }

        private static byte[] ConvertMat2To(byte[] value, uint internalFormat)
        {
            byte[] result = null;
            if (internalFormat == GL.GL_RGBA)
            {

            }
            else if (internalFormat == GL.GL_BGRA)
            {

            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }

        private static byte[] ConvertMat3To(byte[] value, uint internalFormat)
        {
            byte[] result = null;
            if (internalFormat == GL.GL_RGBA)
            {

            }
            else if (internalFormat == GL.GL_BGRA)
            {

            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }

        private static byte[] ConvertMat4To(byte[] value, uint internalFormat)
        {
            byte[] result = null;
            if (internalFormat == GL.GL_RGBA)
            {

            }
            else if (internalFormat == GL.GL_BGRA)
            {

            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }

        public static PassType GetPassType(this Type type)
        {
            if (type == null) { throw new ArgumentNullException(); }
            if (type == typeof(float)) { return PassType.Float; }
            if (type == typeof(vec2)) { return PassType.Vec2; }
            if (type == typeof(vec3)) { return PassType.Vec3; }
            if (type == typeof(vec4)) { return PassType.Vec4; }
            if (type == typeof(mat2)) { return PassType.Mat2; }
            if (type == typeof(mat3)) { return PassType.Mat3; }
            if (type == typeof(mat4)) { return PassType.Mat4; }

            throw new ArgumentException(string.Format("Unexpected type [{0}] in TryGetPassType()", type));
        }

        public static int ByteSize(this PassType passType)
        {
            int result = 0;
            switch (passType)
            {
                case PassType.Float: result = sizeof(float); break;
                case PassType.Vec2: result = sizeof(float) * 2; break;
                case PassType.Vec3: result = sizeof(float) * 3; break;
                case PassType.Vec4: result = sizeof(float) * 4; break;
                case PassType.Mat2: result = sizeof(float) * 4; break;
                case PassType.Mat3: result = sizeof(float) * 9; break;
                case PassType.Mat4: result = sizeof(float) * 16; break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }
    }
}
