using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private vec4 clearColor = new vec4(0, 0, 0, 0);
        public static void glClearColor(float r, float g, float b, float a)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.ClearColor(r, g, b, a);
            }
        }

        private void ClearColor(float r, float g, float b, float a)
        {
            this.clearColor = new vec4(r, g, b, a);
        }


        private float clearDepthf = 1;
        private double clearDepth = 1;

        public static void glClearDepthf(float depth)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.ClearDepthf(depth);
            }
        }

        private void ClearDepthf(float depth)
        {
            this.clearDepthf = depth;
        }

        public static void glClearDepth(double depth)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.ClearDepth(depth);
            }
        }

        private void ClearDepth(double depth)
        {
            this.clearDepth = depth;
        }

        private int clearStencil = 0;
        public static void glClearStencil(int s)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.ClearStencil(s);
            }
        }

        private void ClearStencil(int s)
        {
            this.clearStencil = s;
        }

        public static void glClear(uint mask)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Clear(mask);
            }
        }

        private void Clear(uint mask)
        {
            if (mask == 0) { return; }
            const uint all = (~GL.GL_COLOR_BUFFER_BIT) & (~GL.GL_DEPTH_BUFFER_BIT) & (~GL.GL_STENCIL_BUFFER_BIT);
            if ((mask & all) != 0) { SetLastError(ErrorCode.InvalidValue); return; }

            bool colorBIT = (mask & GL.GL_COLOR_BUFFER_BIT) != 0;
            bool depthBIT = (mask & GL.GL_DEPTH_BUFFER_BIT) != 0;
            bool stencilBIT = (mask & GL.GL_STENCIL_BUFFER_BIT) != 0;
            if (!(colorBIT || depthBIT || stencilBIT)) { return; }

            Framebuffer framebuffer = this.currentFramebuffer;
            if (colorBIT) // clear all colorbuffers.
            {
                List<IAttachable> list = framebuffer.GetCurrentColorBuffers();
                vec4 c = this.clearColor;
                byte[] value = c.ToBytes();
                foreach (var item in list)
                {
                    item.Clear(value);
                }
            }

            if (depthBIT) // clear depthbuffer.
            {
                IAttachable item = framebuffer.DepthbufferAttachment;
                if (item != null)
                {
                    byte[] value = this.clearDepth.ToBytes();
                    item.Clear(value);
                }
            }

            if (stencilBIT) // clear stencilbuffer.
            {
                IAttachable item = framebuffer.StencilbufferAttachment;
                if (item != null)
                {
                    byte[] value = this.clearStencil.ToBytes();
                    item.Clear(value);
                }
            }
        }

    }
}
