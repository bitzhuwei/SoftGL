using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class Framebuffer : IDisposable
    {
        public uint Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public BindFramebufferTarget Target { get; set; }

        public Framebuffer(uint id)
        {
            this.Id = id;
        }

        /// <summary>
        /// glGet(GL_MAX_COLOR_ATTACHMENTS, ..);
        /// </summary>
        internal const int maxColorAttachments = 8;
        private IAttachable[] colorbufferAttachments = new IAttachable[maxColorAttachments]; // OpenGL supports at least 8 color attachement points.

        #region for default framebuffer object only.

        public IAttachable FrontLeft { get { return this.colorbufferAttachments[0]; } set { this.colorbufferAttachments[0] = value; } }

        public IAttachable FrontRight { get { return this.colorbufferAttachments[1]; } set { this.colorbufferAttachments[1] = value; } }

        public IAttachable BackLeft { get { return this.colorbufferAttachments[2]; } set { this.colorbufferAttachments[2] = value; } }

        public IAttachable BackRight { get { return this.colorbufferAttachments[3]; } set { this.colorbufferAttachments[3] = value; } }

        #endregion for default framebuffer object only.

        public IAttachable[] ColorbufferAttachments { get { return this.colorbufferAttachments; } }

        public IAttachable DepthbufferAttachment { get; set; }

        public IAttachable StencilbufferAttachment { get; set; }

        private List<uint> drawBuffers = new List<uint>();
        public IList<uint> DrawBuffers { get { return this.drawBuffers; } }

        public List<IAttachable> GetCurrentColorBuffers()
        {
            var list = new List<IAttachable>();
            foreach (var item in this.drawBuffers)
            {
                uint index = colorbufferDict[item];
                IAttachable colorbuffer = this.colorbufferAttachments[index];
                list.Add(colorbuffer);
            }

            return list;
        }

        /// <summary>
        /// GL.XXX -> index
        /// </summary>
        private static readonly Dictionary<uint, uint> colorbufferDict = new Dictionary<uint, uint>();
        static Framebuffer()
        {
            Dictionary<uint, uint> dict = colorbufferDict;
            dict.Add(GL.GL_FRONT_LEFT, 0);
            dict.Add(GL.GL_FRONT_RIGHT, 1);
            dict.Add(GL.GL_BACK_LEFT, 2);
            dict.Add(GL.GL_BACK_RIGHT, 3);
            for (uint i = 0; i < maxColorAttachments; i++)
            {
                dict.Add(GL.GL_COLOR_ATTACHMENT0 + i, i);
            }
        }
    }

    static class DrawBufferHelper
    {
        public static uint ToIndex(this uint drawBuffer)
        {
            uint result = 0;
            if (GL.GL_FRONT_LEFT <= drawBuffer && drawBuffer <= GL.GL_BACK_RIGHT) { result = drawBuffer - GL.GL_FRONT_LEFT; }
            else if (GL.GL_COLOR_ATTACHMENT0 <= drawBuffer && drawBuffer < GL.GL_COLOR_ATTACHMENT0 + Framebuffer.maxColorAttachments)
            { result = drawBuffer - GL.GL_COLOR_ATTACHMENT0; }
            else
            { throw new ArgumentOutOfRangeException("drawBuffer"); }

            return result;
        }
    }
}
