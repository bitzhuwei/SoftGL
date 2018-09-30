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
            UniformValue v = null;
            if (!this.locationUniformDict.TryGetValue(location, out v))
            {
                v = null;
            }

            return (v != null ? v.variable : null);
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
            this.SetUniform(location, values);
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
            this.SetUniform(location, values);
        }

        public unsafe void SetUniform2fv(int location, int count, bool transpose, float[] value)
        {
            float[] values = value;
            if (transpose)
            {
                values[0] = value[0]; values[1] = value[2];
                values[2] = value[1]; values[3] = value[3];
            }
            this.SetUniform(location, values);
        }

        public unsafe void SetUniformuiv(int location, int count, uint[] value, int componentCount)
        {
            this.SetUniform(location, value);
        }

        public unsafe void SetUniformiv(int location, int count, int[] value, int componentCount)
        {
            this.SetUniform(location, value);
        }

        public unsafe void SetUniformfv(int location, int count, float[] value, int componentCount)
        {
            this.SetUniform(location, value);
        }

        public unsafe void SetUniform4ui(int location, uint v0, uint v1, uint v2, uint v3)
        {
            var values = new uint[] { v0, v1, v2, v3 };
            SetUniformuiv(location, 1, values, 4);
        }

        public unsafe void SetUniform3ui(int location, uint v0, uint v1, uint v2)
        {
            var values = new uint[] { v0, v1, v2 };
            SetUniformuiv(location, 1, values, 3);
        }

        public unsafe void SetUniform2ui(int location, uint v0, uint v1)
        {
            var values = new uint[] { v0, v1 };
            SetUniformuiv(location, 1, values, 2);
        }

        public unsafe void SetUniform1ui(int location, uint v0)
        {
            var values = new uint[] { v0 };
            SetUniformuiv(location, 1, values, 1);
        }

        public unsafe void SetUniform4i(int location, int v0, int v1, int v2, int v3)
        {
            var values = new int[] { v0, v1, v2, v3 };
            SetUniformiv(location, 1, values, 4);
        }

        public unsafe void SetUniform3i(int location, int v0, int v1, int v2)
        {
            var values = new int[] { v0, v1, v2 };
            SetUniformiv(location, 1, values, 3);
        }

        public unsafe void SetUniform2i(int location, int v0, int v1)
        {
            var values = new int[] { v0, v1 };
            SetUniformiv(location, 1, values, 2);
        }

        public unsafe void SetUniform1i(int location, int v0)
        {
            var values = new int[] { v0 };
            SetUniformiv(location, 1, values, 1);
        }

        public unsafe void SetUniform4f(int location, float v0, float v1, float v2, float v3)
        {
            var values = new float[] { v0, v1, v2, v3 };
            SetUniformfv(location, 1, values, 4);
        }

        public unsafe void SetUniform3f(int location, float v0, float v1, float v2)
        {
            var values = new float[] { v0, v1, v2 };
            SetUniformfv(location, 1, values, 3);
        }

        public unsafe void SetUniform2f(int location, float v0, float v1)
        {
            var values = new float[] { v0, v1 };
            SetUniformfv(location, 1, values, 2);
        }

        public unsafe void SetUniform1f(int location, float v0)
        {
            var values = new float[] { v0 };
            SetUniformfv(location, 1, values, 1);
        }


        private void SetUniform(int location, Object value)
        {
            Dictionary<int, UniformValue> locationUniformDict = this.locationUniformDict;
            UniformValue uniformValue = null;
            if (locationUniformDict.TryGetValue(location, out uniformValue))
            {
                uniformValue.value = value;
            }
            else
            {
                // TODO: what to do when specified uniform variable not exists? Silent or throwing exception?
            }
        }
    }
}
