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
            // passBuffers is input for the next stage: linear interpolation.
            // passBuffers[0] is gl_Position.
            // passBuffers[others] are attributes of vertexes.
            PassBuffer[] passBuffers = VertexShaderStage(count, type, indices, vao, program, indexBuffer);
            if (passBuffers == null) { return; } // this stage failed.

            Framebuffer framebuffer = this.currentFramebuffer;
            if (framebuffer == null) { throw new Exception("This should not happen!"); }

            ClipSpace2NormalDeviceSpace(passBuffers[0]);
            // linear interpolation.
            List<Fragment> fragmentList = LinearInterpolation(mode, count, type, indices, vao, program, indexBuffer, passBuffers);

            // execute fargment shader for each fragment!
            // This is a low effetient implementation.
            FragmentShaderStage(program, fragmentList);
            {
                int index = 0;
                while (index < fragmentList.Count)
                {
                    if (fragmentList[index].discard) { fragmentList.RemoveAt(index); }
                    else { index++; }
                }
            }
            // Scissor test

            // Multisampel fragment operations

            // Stencil test

            // Depth test
            DepthTest(fragmentList);

            //Blending

            // Dithering

            // Logical operations

            // write fragments to framebuffer's colorbuffer attachment(s).
            {
                uint[] drawBufferIndexes = framebuffer.DrawBuffers.ToArray();
                foreach (var fragment in fragmentList)
                {
                    if (fragment.depthTestFailed) { continue; }

                    for (int i = 0; i < fragment.outVariables.Length && i < drawBufferIndexes.Length; i++)
                    {
                        PassBuffer outVar = fragment.outVariables[i];
                        IAttachable attachment = framebuffer.ColorbufferAttachments[drawBufferIndexes[i].ToIndex()];
                        attachment.Set((int)fragment.gl_FragCoord.x, (int)fragment.gl_FragCoord.y, outVar);
                    }
                }
            }
        }

        private void DepthTest(List<Fragment> fragmentList)
        {
            Fragment[,] depthTestPlatform = new Fragment[viewport.z, viewport.w];
            foreach (var fragment in fragmentList)
            {
                int x = (int)fragment.gl_FragCoord.x;
                int y = (int)fragment.gl_FragCoord.y;
                var dst = depthTestPlatform[x, y];
                if (dst == null)
                {
                    depthTestPlatform[x, y] = fragment;
                }
                else
                {
                    // TODO: switch (depthfunc(..)) { .. }
                    if (fragment.gl_FragCoord.z < dst.gl_FragCoord.z) // fragment is nearer.
                    {
                        dst.depthTestFailed = true;
                        depthTestPlatform[x, y] = fragment;
                    }
                    else
                    {
                        fragment.depthTestFailed = true;
                    }
                }
            }
        }

        private static int ByteLength(DrawElementsType type)
        {
            int result = 0;
            switch (type)
            {
                case DrawElementsType.UnsignedByte: result = sizeof(byte); break;
                case DrawElementsType.UnsignedShort: result = sizeof(ushort); break;
                case DrawElementsType.UnsignedInt: result = sizeof(uint); break;
                default: throw new NotDealWithNewEnumItemException(typeof(DrawElementsType));
            }

            return result;
        }
        private unsafe void ClipSpace2NormalDeviceSpace(PassBuffer passBuffer)
        {
            if (passBuffer.elementType != PassType.Vec4) { throw new Exception(String.Format("This pass-buffer must be of vec4 type!")); }

            var array = (vec4*)passBuffer.Mapbuffer();
            int length = passBuffer.Length();
            for (int i = 0; i < length; i++)
            {
                vec4 gl_Position = array[i];
                float w = gl_Position.w;
                if (w != 0)
                {
                    gl_Position.x = gl_Position.x / w;
                    gl_Position.y = gl_Position.y / w;
                    gl_Position.z = gl_Position.z / w;
                    gl_Position.w = 1;
                }
                array[i] = gl_Position;
            }
            passBuffer.Unmapbuffer();
        }
    }
}
