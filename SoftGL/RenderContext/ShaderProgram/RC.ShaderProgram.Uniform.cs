using System;
using System.Collections.Generic;

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

        private int GetUniformLocation(uint progName, string name)
        {
            int result = -1;
            if (progName == 0) { SetLastError(ErrorCode.InvalidValue); return result; }
            if (!this.nameShaderProgramDict.ContainsKey(progName)) { SetLastError(ErrorCode.InvalidOperation); return result; }
            ShaderProgram program = this.nameShaderProgramDict[progName];
            if (program.LogInfo != string.Empty) { SetLastError(ErrorCode.InvalidOperation); return result; }

            throw new NotImplementedException();
        }

    }
}
