using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class ShaderProgram
    {
        public UniformVariable GetUniformVariable(int location)
        {
            UniformVariable v = null;
            if (!this.locationUniformDict.TryGetValue(location, out v))
            {
                v = null;
            }

            return v;
        }

        public unsafe void SetUniform4fv(int location, int count, bool transpose, float[] value)
        {
            float[] values = value;
            if (transpose)
            {
                values[00] = value[0]; values[01] = value[4]; values[02] = value[08]; values[03] = value[12];
                values[04] = value[1]; values[05] = value[5]; values[06] = value[09]; values[07] = value[13];
                values[08] = value[2]; values[09] = value[6]; values[10] = value[10]; values[11] = value[14];
                values[12] = value[3]; values[13] = value[7]; values[14] = value[11]; values[15] = value[15];
            }
            byte[] uniformBytes = this.uniformBytes;
            GCHandle pin = GCHandle.Alloc(values, GCHandleType.Pinned);
            IntPtr address = pin.AddrOfPinnedObject();
            byte* array = (byte*)address.ToPointer();
            const int valuesLength = 16 * sizeof(float);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < valuesLength; j++)
                {
                    uniformBytes[location + i * valuesLength + j] = array[j];
                }
            }
            pin.Free();
        }

        public unsafe void SetUniform3fv(int location, int count, bool transpose, float[] value)
        {
            float[] values = value;
            if (transpose)
            {
                values[0] = value[0]; values[1] = value[3]; values[2] = value[6];
                values[3] = value[1]; values[4] = value[4]; values[5] = value[7];
                values[6] = value[2]; values[7] = value[5]; values[8] = value[8];
            }
            byte[] uniformBytes = this.uniformBytes;
            GCHandle pin = GCHandle.Alloc(values, GCHandleType.Pinned);
            IntPtr address = pin.AddrOfPinnedObject();
            byte* array = (byte*)address.ToPointer();
            const int valuesLength = 9 * sizeof(float);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < valuesLength; j++)
                {
                    uniformBytes[location + i * valuesLength + j] = array[j];
                }
            }
            pin.Free();
        }

        public unsafe void SetUniform2fv(int location, int count, bool transpose, float[] value)
        {
            float[] values = value;
            if (transpose)
            {
                values[0] = value[0]; values[1] = value[2];
                values[2] = value[1]; values[3] = value[3];
            }
            byte[] uniformBytes = this.uniformBytes;
            GCHandle pin = GCHandle.Alloc(values, GCHandleType.Pinned);
            IntPtr address = pin.AddrOfPinnedObject();
            byte* array = (byte*)address.ToPointer();
            const int valuesLength = 4 * sizeof(float);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < valuesLength; j++)
                {
                    uniformBytes[location + i * valuesLength + j] = array[j];
                }
            }
            pin.Free();
        }

        public unsafe void SetUniformuiv(int location, int count, uint[] value, int componentCount)
        {
            uint[] values = value;
            byte[] uniformBytes = this.uniformBytes;
            GCHandle pin = GCHandle.Alloc(values, GCHandleType.Pinned);
            IntPtr address = pin.AddrOfPinnedObject();
            byte* array = (byte*)address.ToPointer();
            int valuesLength = componentCount * sizeof(uint);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < valuesLength; j++)
                {
                    uniformBytes[location + i * valuesLength + j] = array[j];
                }
            }
            pin.Free();
        }

    }
}
