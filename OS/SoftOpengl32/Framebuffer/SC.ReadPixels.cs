using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SoftGL;

namespace SoftOpengl32
{
    partial class StaticCalls
    {
        /// <summary>
        /// read a block of pixels from the frame buffer.
        /// </summary>
        /// <param name="x">Specify the window coordinates of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
        /// <param name="y">Specify the window coordinates of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
        /// <param name="width">Specify the dimensions of the pixel rectangle. width​ and height​ of one correspond to a single pixel.</param>
        /// <param name="height">Specify the dimensions of the pixel rectangle. width​ and height​ of one correspond to a single pixel.</param>
        /// <param name="format">Specifies the format of the pixel data. For transfers of depth, stencil, or depth/stencil data, you must use GL_DEPTH_COMPONENT, GL_STENCIL_INDEX, or GL_DEPTH_STENCIL, where appropriate. For transfers of normalized integer or floating-point color image data, you must use one of the following: GL_RED, GL_GREEN, GL_BLUE, GL_RG, GL_RGB, GL_BGR, GL_RGBA, and GL_BGRA. For transfers of non-normalized integer data, you must use one of the following: GL_RED_INTEGER, GL_GREEN_INTEGER, GL_BLUE_INTEGER, GL_RG_INTEGER, GL_RGB_INTEGER, GL_BGR_INTEGER, GL_RGBA_INTEGER, and GL_BGRA_INTEGER. Even if no actual pixel transfer is made (data​ is NULL and no buffer is bound to GL_PIXEL_UNPACK_BUFFER), you must set this parameter correctly for the internal format of the destination image.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, and GL_UNSIGNED_INT_2_10_10_10_REV.</param>
        /// <param name="data">Specifies a pointer to the image data in memory, or if a buffer is bound to GL_PIXEL_PACK_BUFFER, this provides an integer offset into the bound buffer object. If a buffer is not bound to GL_PIXEL_PACK_BUFFER, this parameter may not be NULL.</param>
        public static void glReadPixels(int x, int y, int width, int height, uint format, uint type, IntPtr data)
        {
            SoftGLRenderContext.glReadPixels(x, y, width, height, format, type, data);
        }
    }
}
