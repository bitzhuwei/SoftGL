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
        byte[] array;

        public PassBuffer(PassType type, int length)
        {
            this.elementType = type;
            this.array = new byte[type.ByteSize() * length];
        }

        GCHandle pin;

        public IntPtr AddrOfPinnedObject()
        {
            return this.pin.AddrOfPinnedObject();
        }

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
            return string.Format("t:{0}, l:{1}", this.elementType, this.array.Length);
        }
    }

    enum PassType
    {
        Float, Vec2, Vec3, Vec4,
        Mat2, Mat3, Mat4,
    }
    static class PassTypeHelper
    {
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
