using System;
using System.Collections.Generic;
using System.Reflection;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static int glGetUniformLocation(uint program, string name)
        {
            int result = -1;
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                result = context.GetUniformLocation(program, name);
            }

            return result;
        }

        private int GetUniformLocation(uint progName, string varNname)
        {
            int result = -1;
            if (progName == 0) { SetLastError(ErrorCode.InvalidValue); return result; }
            if (!this.nameShaderProgramDict.ContainsKey(progName)) { SetLastError(ErrorCode.InvalidOperation); return result; }
            ShaderProgram program = this.nameShaderProgramDict[progName];
            if (program.LogInfo != string.Empty) { SetLastError(ErrorCode.InvalidOperation); return result; }

            result = program.GetUniformLocation(varNname);

            return result;
        }

        public static void glUniformMatrix4fv(int location, int count, bool transpose, float[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.UniformMatrix4fv(location, count, transpose, value);
            }
        }

        private void UniformMatrix4fv(int location, int count, bool transpose, float[] value)
        {
            if (location < 0 || value == null || value.Length != 16) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniform4fv(location, count, transpose, value);
        }

        public static void glUniformMatrix3fv(int location, int count, bool transpose, float[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.UniformMatrix3fv(location, count, transpose, value);
            }
        }

        private void UniformMatrix3fv(int location, int count, bool transpose, float[] value)
        {
            if (location < 0 || value == null || value.Length != 9) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniform3fv(location, count, transpose, value);
        }

        public static void glUniformMatrix2fv(int location, int count, bool transpose, float[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.UniformMatrix2fv(location, count, transpose, value);
            }
        }

        private void UniformMatrix2fv(int location, int count, bool transpose, float[] value)
        {
            if (location < 0 || value == null || value.Length != 4) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniform2fv(location, count, transpose, value);
        }

        public static void glUniform4uiv(int location, int count, uint[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform4uiv(location, count, value);
            }
        }

        private void Uniform4uiv(int location, int count, uint[] value)
        {
            if (location < 0 || value == null || value.Length != 4) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformuiv(location, count, value, 4);
        }

        public static void glUniform3uiv(int location, int count, uint[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform3uiv(location, count, value);
            }
        }

        private void Uniform3uiv(int location, int count, uint[] value)
        {
            if (location < 0 || value == null || value.Length != 3) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformuiv(location, count, value, 3);
        }

        public static void glUniform2uiv(int location, int count, uint[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform2uiv(location, count, value);
            }
        }

        private void Uniform2uiv(int location, int count, uint[] value)
        {
            if (location < 0 || value == null || value.Length != 2) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformuiv(location, count, value, 2);
        }

        public static void glUniform1uiv(int location, int count, uint[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform1uiv(location, count, value);
            }
        }

        private void Uniform1uiv(int location, int count, uint[] value)
        {
            if (location < 0 || value == null || value.Length != 1) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformuiv(location, count, value, 1);
        }

        public static void glUniform4iv(int location, int count, int[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform4iv(location, count, value);
            }
        }

        private void Uniform4iv(int location, int count, int[] value)
        {
            if (location < 0 || value == null || value.Length != 4) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformiv(location, count, value, 4);
        }

        public static void glUniform3iv(int location, int count, int[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform3iv(location, count, value);
            }
        }

        private void Uniform3iv(int location, int count, int[] value)
        {
            if (location < 0 || value == null || value.Length != 3) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformiv(location, count, value, 3);
        }

        public static void glUniform2iv(int location, int count, int[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform2iv(location, count, value);
            }
        }

        private void Uniform2iv(int location, int count, int[] value)
        {
            if (location < 0 || value == null || value.Length != 2) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformiv(location, count, value, 2);
        }

        public static void glUniform1iv(int location, int count, int[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform1iv(location, count, value);
            }
        }

        private void Uniform1iv(int location, int count, int[] value)
        {
            if (location < 0 || value == null || value.Length != 1) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformiv(location, count, value, 1);
        }

        public static void glUniform4fv(int location, int count, float[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform4fv(location, count, value);
            }
        }

        private void Uniform4fv(int location, int count, float[] value)
        {
            if (location < 0 || value == null || value.Length != 4) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformfv(location, count, value, 4);
        }

        public static void glUniform3fv(int location, int count, float[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform3fv(location, count, value);
            }
        }

        private void Uniform3fv(int location, int count, float[] value)
        {
            if (location < 0 || value == null || value.Length != 3) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformfv(location, count, value, 3);
        }

        public static void glUniform2fv(int location, int count, float[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform2fv(location, count, value);
            }
        }

        private void Uniform2fv(int location, int count, float[] value)
        {
            if (location < 0 || value == null || value.Length != 2) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformfv(location, count, value, 2);
        }

        public static void glUniform1fv(int location, int count, float[] value)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform1fv(location, count, value);
            }
        }

        private void Uniform1fv(int location, int count, float[] value)
        {
            if (location < 0 || value == null || value.Length != 1) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO:GL_INVALID_OPERATION is generated if the size of the uniform variable declared in the shader does not match the size indicated by the glUniform command.
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if ((count > 1) && (!fieldInfo.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformfv(location, count, value, 1);
        }

        public static void glUniform4ui(int location, uint v0, uint v1, uint v2, uint v3)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform4ui(location, v0, v1, v2, v3);
            }
        }

        private void Uniform4ui(int location, uint v0, uint v1, uint v2, uint v3)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform4ui(location, v0, v1, v2, v3);
        }

        public static void glUniform3ui(int location, uint v0, uint v1, uint v2)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform3ui(location, v0, v1, v2);
            }
        }

        private void Uniform3ui(int location, uint v0, uint v1, uint v2)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform3ui(location, v0, v1, v2);
        }

        public static void glUniform2ui(int location, uint v0, uint v1)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform2ui(location, v0, v1);
            }
        }

        private void Uniform2ui(int location, uint v0, uint v1)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform2ui(location, v0, v1);
        }

        public static void glUniform1ui(int location, uint v0)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform1ui(location, v0);
            }
        }

        private void Uniform1ui(int location, uint v0)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform1ui(location, v0);
        }

        public static void glUniform4i(int location, int v0, int v1, int v2, int v3)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform4i(location, v0, v1, v2, v3);
            }
        }

        private void Uniform4i(int location, int v0, int v1, int v2, int v3)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform4i(location, v0, v1, v2, v3);
        }

        public static void glUniform3i(int location, int v0, int v1, int v2)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform3i(location, v0, v1, v2);
            }
        }

        private void Uniform3i(int location, int v0, int v1, int v2)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform3i(location, v0, v1, v2);
        }

        public static void glUniform2i(int location, int v0, int v1)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform2i(location, v0, v1);
            }
        }

        private void Uniform2i(int location, int v0, int v1)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform2i(location, v0, v1);
        }

        public static void glUniform1i(int location, int v0)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform1i(location, v0);
            }
        }

        private void Uniform1i(int location, int v0)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform1i(location, v0);
        }

        public static void glUniform4f(int location, float v0, float v1, float v2, float v3)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform4f(location, v0, v1, v2, v3);
            }
        }

        private void Uniform4f(int location, float v0, float v1, float v2, float v3)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform4f(location, v0, v1, v2, v3);
        }

        public static void glUniform3f(int location, float v0, float v1, float v2)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform3f(location, v0, v1, v2);
            }
        }

        private void Uniform3f(int location, float v0, float v1, float v2)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform3f(location, v0, v1, v2);
        }

        public static void glUniform2f(int location, float v0, float v1)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform2f(location, v0, v1);
            }
        }

        private void Uniform2f(int location, float v0, float v1)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform2f(location, v0, v1);
        }

        public static void glUniform1f(int location, float v0)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Uniform1f(location, v0);
            }
        }

        private void Uniform1f(int location, float v0)
        {
            if (location < 0) { return; }

            ShaderProgram program = this.currentShaderProgram;
            if (program == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            UniformVariable v = program.GetUniformVariable(location);
            if (v == null) { SetLastError(ErrorCode.InvalidOperation); return; }
            FieldInfo fieldInfo = v.fieldInfo;
            if (fieldInfo.FieldType.IsArray) { SetLastError(ErrorCode.InvalidOperation); return; } // TODO: not sure about this line.

            program.SetUniform1f(location, v0);
        }

    }
}
