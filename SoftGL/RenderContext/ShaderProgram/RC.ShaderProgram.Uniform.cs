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
            FieldInfo field = v.field;
            if ((count > 1) && (!field.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

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
            FieldInfo field = v.field;
            if ((count > 1) && (!field.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

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
            FieldInfo field = v.field;
            if ((count > 1) && (!field.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

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
            FieldInfo field = v.field;
            if ((count > 1) && (!field.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

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
            FieldInfo field = v.field;
            if ((count > 1) && (!field.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

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
            FieldInfo field = v.field;
            if ((count > 1) && (!field.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

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
            FieldInfo field = v.field;
            if ((count > 1) && (!field.FieldType.IsArray)) { SetLastError(ErrorCode.InvalidOperation); return; }

            program.SetUniformuiv(location, count, value, 1);
        }

    }
}
