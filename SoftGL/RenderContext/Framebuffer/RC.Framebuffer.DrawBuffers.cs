using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glDrawBuffers(int count, uint[] buffers)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.DrawBuffers(count, buffers);
            }
        }

        private void DrawBuffers(params uint[] buffers)
        {
            this.DrawBuffers(buffers.Length, buffers);
        }

        private void DrawBuffers(int count, uint[] buffers)
        {
            Framebuffer framebuffer = this.currentFramebuffer;
            if (framebuffer == null) { return; } // this is when something is wrong with this implementation.

            if (count < 0) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (buffers == null) { return; }
            foreach (var item in buffers)
            {
                if (item == 0) { continue; }
                if (GL.GL_FRONT_LEFT <= item && item <= GL.GL_BACK_RIGHT) { continue; }
                if (GL.GL_COLOR_ATTACHMENT0 <= item && item < GL.GL_COLOR_ATTACHMENT0 + Framebuffer.maxColorAttachments) { continue; }

                { SetLastError(ErrorCode.InvalidEnum); return; }
            }
            // GL_INVALID_OPERATION is generated if a symbolic constant other than GL_NONE appears more than once in buffers.
            for (int i = 0; i < buffers.Length; i++)
            {
                if (buffers[i] == GL.GL_NONE) { continue; }
                for (int j = i + 1; j < buffers.Length; j++)
                {
                    if (buffers[j] == GL.GL_NONE) { continue; }
                    if (buffers[i] == buffers[j]) { SetLastError(ErrorCode.InvalidOperation); return; }
                }
            }

            if (framebuffer == this.defaultFramebuffer)
            {
                foreach (var item in buffers)
                {
                    if (!(GL.GL_FRONT_LEFT <= item && item <= GL.GL_BACK_RIGHT)) { SetLastError(ErrorCode.InvalidEnum); return; }
                }
            }
            else
            {
                foreach (var item in buffers)
                {
                    if (!(GL.GL_COLOR_ATTACHMENT0 <= item && item < GL.GL_COLOR_ATTACHMENT0 + Framebuffer.maxColorAttachments)) { SetLastError(ErrorCode.InvalidEnum); return; }
                }
            }

            framebuffer.DrawBuffers.Clear();
            foreach (var item in buffers)
            {
                if (item != GL.GL_NONE)
                {
                    framebuffer.DrawBuffers.Add(item);
                }
            }
        }
    }
}
