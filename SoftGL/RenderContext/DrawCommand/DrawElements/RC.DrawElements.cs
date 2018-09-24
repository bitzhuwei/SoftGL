﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glDrawElements(uint mode, int count, uint type, IntPtr indices)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DrawElements((DrawTarget)mode, count, (DrawElementsType)type, indices);
            }
        }

        private void DrawElements(DrawTarget mode, int count, DrawElementsType type, IntPtr indices)
        {
            if (!Enum.IsDefined(typeof(DrawTarget), mode) || !Enum.IsDefined(typeof(DrawElementsType), type)) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }
            // TODO: GL_INVALID_OPERATION is generated if a geometry shader is active and mode​ is incompatible with the input primitive type of the geometry shader in the currently installed program object.
            // TODO: GL_INVALID_OPERATION is generated if a non-zero buffer object name is bound to an enabled array or the element array and the buffer object's data store is currently mapped.

            VertexArrayObject vao = this.currentVertexArrayObject; // data structure.
            if (vao == null) { return; }
            ShaderProgram program = this.currentShaderProgram; // algorithm.
            if (program == null) { return; }
            GLBuffer indexBuffer = this.currentBufferDict[BindBufferTarget.ElementArrayBuffer];
            if (indexBuffer == null) { return; }

            // execute vertex shader for each vertex!
            // This is a low effetient implementation.
            // vsOutput is input for the next stage: linear interpolation.
            PassBuffer[] passBuffers = VertexShaderStage(mode, count, type, indices, vao, program, indexBuffer);
            if (passBuffers == null) { return; } // this stage failed.

            // linear interpolation.
            List<Fragment> fragmentList = LinearInterpolation(mode, count, type, indices, vao, program, indexBuffer, passBuffers);

            // execute fargment shader for each fragment!
            // This is a low effetient implementation.

        }
    }
}
