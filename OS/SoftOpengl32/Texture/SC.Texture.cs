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
        /// generate texture names.
        /// </summary>
        /// <param name="count">Specifies the number of texture names to be generated.</param>
        /// <param name="names">Specifies an array in which the generated texture names are stored.</param>
        public static void glGenTextures(int count, uint[] names)
        {
            SoftGLRenderContext.glGenTextures(count, names);
        }

        /// <summary>
        /// bind a named texture to a texturing target.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture is bound. Must be either GL_TEXTURE_1D, GL_TEXTURE_2D, GL_TEXTURE_3D, or GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_RECTANGLE, GL_TEXTURE_CUBE_MAP, GL_TEXTURE_2D_MULTISAMPLE, GL_TEXTURE_2D_MULTISAMPLE_ARRAY, GL_TEXTURE_BUFFER, or GL_TEXTURE_CUBE_MAP_ARRAY.</param>
        /// <param name="name">Specifies the name of a texture.</param>
        public static void glBindTexture(uint target, uint name)
        {
            SoftGLRenderContext.glBindTexture(target, name);
        }

        /// <summary>
        /// determine if a name corresponds to a texture.
        /// </summary>
        /// <param name="name">Specifies a value that may be the name of a texture.</param>
        public static void glIsTexture(uint name)
        {
            SoftGLRenderContext.glIsTexture(name);
        }

        /// <summary>
        /// delete named textures.
        /// </summary>
        /// <param name="count">Specifies the number of textures to be deleted.</param>
        /// <param name="names">Specifies an array of textures to be deleted.</param>
        public static void glDeleteTextures(int count, uint[] names)
        {
            SoftGLRenderContext.glDeleteTextures(count, names);
        }
    }
}
