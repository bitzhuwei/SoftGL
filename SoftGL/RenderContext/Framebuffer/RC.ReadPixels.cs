using System;
using System.Collections.Generic;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glReadPixels(int x, int y, int width, int height, uint format, uint type, IntPtr data)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.ReadPixels(x, y, width, height, (ReadPixelsFormat)format, (ReadPixelsType)type, data);
            }
        }

        private unsafe void ReadPixels(int x, int y, int width, int height, ReadPixelsFormat format, ReadPixelsType type, IntPtr data)
        {
            Framebuffer framebuffer = this.currentFramebuffer;
            if (framebuffer == null) { throw new Exception("This should not happen!"); }

            if (!Enum.IsDefined(typeof(ReadPixelsFormat), format)) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (!Enum.IsDefined(typeof(ReadPixelsType), type)) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (width < 0 || height < 0) { SetLastError(ErrorCode.InvalidValue); return; }
            if (format == ReadPixelsFormat.StencilIndex && framebuffer.StencilbufferAttachment == null)
            { SetLastError(ErrorCode.InvalidOperation); return; }
            if (format == ReadPixelsFormat.DepthComponent && framebuffer.DepthbufferAttachment == null)
            { SetLastError(ErrorCode.InvalidOperation); return; }
            if (format == ReadPixelsFormat.DepthStencil && (framebuffer.DepthbufferAttachment == null || framebuffer.StencilbufferAttachment == null))
            { SetLastError(ErrorCode.InvalidOperation); return; }
            if (format == ReadPixelsFormat.DepthStencil
                && (type != ReadPixelsType.UnsignedInt248 && type != ReadPixelsType.Float32UnsignedInt248Rev))
            { SetLastError(ErrorCode.InvalidEnum); return; }
            if (format != ReadPixelsFormat.RGB
                && (type == ReadPixelsType.UnsignedByte332 || type == ReadPixelsType.UnsignedByte233Rev || type == ReadPixelsType.UnsignedShort565 || type == ReadPixelsType.UnsignedShort565Rev))
            { SetLastError(ErrorCode.InvalidOperation); return; }
            if ((format != ReadPixelsFormat.RGBA && format != ReadPixelsFormat.BGRA)
                && (type == ReadPixelsType.UnsignedShort4444 || type == ReadPixelsType.UnsignedShort4444Rev || type == ReadPixelsType.UnsignedShort5551 || type == ReadPixelsType.UnsignedShort1555Rev || type == ReadPixelsType.UnsignedInt8888 || type == ReadPixelsType.UnsignedInt8888Rev || type == ReadPixelsType.UnsignedInt1010102 || type == ReadPixelsType.UnsignedInt2101010Rev))
            { SetLastError(ErrorCode.InvalidOperation); return; }
            // TODO: GL_INVALID_OPERATION is generated if a non-zero buffer object name is bound to the GL_PIXEL_PACK_BUFFER target and the buffer object's data store is currently mapped.
            // TODO: GL_INVALID_OPERATION is generated if a non-zero buffer object name is bound to the GL_PIXEL_PACK_BUFFER target and the data would be packed to the buffer object such that the memory writes required would exceed the data store size.
            // TODO: GL_INVALID_OPERATION is generated if a non-zero buffer object name is bound to the GL_PIXEL_PACK_BUFFER target and data​ is not evenly divisible into the number of bytes needed to store in memory a datum indicated by type.
            // TODO: GL_INVALID_OPERATION is generated if GL_READ_FRAMEBUFFER_BINDING is non-zero, the read framebuffer is complete, and the value of GL_SAMPLE_BUFFERS for the read framebuffer is greater than zero.

            IAttachable attachment = null;
            if (format == ReadPixelsFormat.DepthComponent)
            {
                attachment = framebuffer.DepthbufferAttachment;
            }
            else if (format == ReadPixelsFormat.StencilIndex)
            {
                attachment = framebuffer.StencilbufferAttachment;
            }
            else if (format == ReadPixelsFormat.DepthStencil)
            {
                attachment = framebuffer.DepthbufferAttachment;
            }
            else
            {
                IList<uint> drawBuffers = framebuffer.DrawBuffers;
                if (drawBuffers.Count > 0)
                {
                    uint index = drawBuffers[0].ToIndex();
                    attachment = framebuffer.ColorbufferAttachments[index];
                }
            }
            // copy data from attachment to "data".
            if (attachment != null)
            {
                ReadPixels(x, y, width, height, format, type, data, attachment);
            }
        }

        private unsafe void ReadPixels(int x, int y, int width, int height, ReadPixelsFormat format, ReadPixelsType type, IntPtr data, IAttachable attachment)
        {
            byte[] dataStore = attachment.DataStore;
            //int srcBitSize = InternalFormatHelper.BitSize(attachment.Format);
            //int srcElementByteLength = (srcBitSize % 8 == 0) ? srcBitSize / 8 : srcBitSize / 8 + 1; // TODO: any better solution?
            int srcWidth = attachment.Width, srcHeight = attachment.Height;
            //int dstBitSize = InternalFormatHelper.BitSize((uint)format);
            //int dstElementByteLength = (dstBitSize % 8 == 0) ? dstBitSize / 8 : dstBitSize / 8 + 1; // TODO: any better solution? 
            var array = (byte*)data.ToPointer();
            if (format == ReadPixelsFormat.BGRA && attachment.Format == GL.GL_RGBA)
            {
                var indexes = new int[4] { 2, 1, 0, 3 };
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        for (int t = 0; t < 4; t++)
                        {
                            int dstT = (j * width + i) * 4 + t;
                            int srcT = ((j + y) * srcWidth + (i + x)) * 4 + indexes[t];
                            array[dstT] = dataStore[srcT];
                        }
                    }
                }
            }
            else if (format == ReadPixelsFormat.BGRA && attachment.Format == GL.GL_BGRA)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        for (int t = 0; t < 4; t++)
                        {
                            int dstT = (j * width + i) * 4 + t;
                            int srcT = ((j + y) * srcWidth + (i + x)) * 4 + t;
                            array[dstT] = dataStore[srcT];
                        }
                    }
                }
            }
            else // TODO; deal with all possibilities.
            {
                throw new NotImplementedException();
            }
        }
    }

    enum ReadPixelsFormat : uint
    {
        /// <summary>
        /// For transfers of depth data.
        /// </summary>
        DepthComponent = GL.GL_DEPTH_COMPONENT,
        /// <summary>
        /// For transfers of stencil data.
        /// </summary>
        StencilIndex = GL.GL_STENCIL_INDEX,
        /// <summary>
        /// For transfers of depth/stencil data.
        /// </summary>
        DepthStencil = GL.GL_DEPTH_STENCIL,

        /// <summary>
        /// For transfers of normalized integer or floating-point color image data.
        /// </summary>
        Red = GL.GL_RED,
        /// <summary>
        /// For transfers of normalized integer or floating-point color image data.
        /// </summary>
        Green = GL.GL_GREEN,
        /// <summary>
        /// For transfers of normalized integer or floating-point color image data.
        /// </summary>
        Blue = GL.GL_BLUE,
        /// <summary>
        /// For transfers of normalized integer or floating-point color image data.
        /// </summary>
        RG = GL.GL_RG,
        /// <summary>
        /// For transfers of normalized integer or floating-point color image data.
        /// </summary>
        RGB = GL.GL_RGB,
        /// <summary>
        /// For transfers of normalized integer or floating-point color image data.
        /// </summary>
        BGR = GL.GL_BGR,
        /// <summary>
        /// For transfers of normalized integer or floating-point color image data.
        /// </summary>
        RGBA = GL.GL_RGBA,
        /// <summary>
        /// For transfers of normalized integer or floating-point color image data.
        /// </summary>
        BGRA = GL.GL_BGRA,

        /// <summary>
        /// For transfers of non-normalized integer data.
        /// </summary>
        RedInteger = GL.GL_RED_INTEGER,
        /// <summary>
        /// For transfers of non-normalized integer data.
        /// </summary>
        GreenInteger = GL.GL_GREEN_INTEGER,
        /// <summary>
        /// For transfers of non-normalized integer data.
        /// </summary>
        BlueInteger = GL.GL_BLUE_INTEGER,
        /// <summary>
        /// For transfers of non-normalized integer data.
        /// </summary>
        RGInteger = GL.GL_RG_INTEGER,
        /// <summary>
        /// For transfers of non-normalized integer data.
        /// </summary>
        RGBInteger = GL.GL_RGB_INTEGER,
        /// <summary>
        /// For transfers of non-normalized integer data.
        /// </summary>
        BGRInteger = GL.GL_BGR_INTEGER,
        /// <summary>
        /// For transfers of non-normalized integer data.
        /// </summary>
        RGBAInteger = GL.GL_RGBA_INTEGER,
        /// <summary>
        /// For transfers of non-normalized integer data.
        /// </summary>
        BGRAInteger = GL.GL_BGRA_INTEGER
    }

    enum ReadPixelsType : uint
    {
        UnsignedByte = GL.GL_UNSIGNED_BYTE,
        Byte = GL.GL_BYTE,
        UnsignedShort = GL.GL_UNSIGNED_SHORT,
        Short = GL.GL_SHORT,
        UnsignedInt = GL.GL_UNSIGNED_INT,
        Int = GL.GL_INT,
        Float = GL.GL_FLOAT,
        UnsignedByte332 = GL.GL_UNSIGNED_BYTE_3_3_2,
        UnsignedByte233Rev = GL.GL_UNSIGNED_BYTE_2_3_3_REV,
        UnsignedShort565 = GL.GL_UNSIGNED_SHORT_5_6_5,
        UnsignedShort565Rev = GL.GL_UNSIGNED_SHORT_5_6_5_REV,
        UnsignedShort4444 = GL.GL_UNSIGNED_SHORT_4_4_4_4,
        UnsignedShort4444Rev = GL.GL_UNSIGNED_SHORT_4_4_4_4_REV,
        UnsignedShort5551 = GL.GL_UNSIGNED_SHORT_5_5_5_1,
        UnsignedShort1555Rev = GL.GL_UNSIGNED_SHORT_1_5_5_5_REV,
        UnsignedInt8888 = GL.GL_UNSIGNED_INT_8_8_8_8,
        UnsignedInt8888Rev = GL.GL_UNSIGNED_INT_8_8_8_8_REV,
        UnsignedInt1010102 = GL.GL_UNSIGNED_INT_10_10_10_2,
        UnsignedInt2101010Rev = GL.GL_UNSIGNED_INT_2_10_10_10_REV,
        UnsignedInt248 = GL.GL_UNSIGNED_INT_24_8,
        Float32UnsignedInt248Rev = GL.GL_FLOAT_32_UNSIGNED_INT_24_8_REV
    }
}
