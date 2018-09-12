using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private void SetCurrentTexture(TextureTarget target, Texture texture)
        {
            TextureUnit currentUnit = this.textureUnits[this.currentTextureUnitIndex];
            switch (target)
            {
                case TextureTarget.Texture1D: currentUnit.texture1D = texture; break;
                case TextureTarget.Texture2D: currentUnit.texture2D = texture; break;
                case TextureTarget.Texture2DMultisample: currentUnit.texture2DMultisample = texture; break;
                case TextureTarget.Texture2DArray: currentUnit.texture2DArray = texture; break;
                case TextureTarget.Texture3D: currentUnit.texture3D = texture; break;
                case TextureTarget.Texture2DMultisampleArray: currentUnit.texture2DMultisampleArray = texture; break;
                case TextureTarget.TextureCubeMap: currentUnit.textureCubeMap = texture; break;
                case TextureTarget.TextureBuffer: currentUnit.textureBuffer = texture; break;
                case TextureTarget.TextureRectangle: currentUnit.textureRectangle = texture; break;
                default:
                    throw new NotImplementedException();
            }
        }

        private Texture GetCurrentTexture(TextureTarget target)
        {
            Texture texture = null;
            TextureUnit currentUnit = this.textureUnits[this.currentTextureUnitIndex];
            switch (target)
            {
                case TextureTarget.Texture1D: texture = currentUnit.texture1D; break;
                case TextureTarget.Texture2D: texture = currentUnit.texture2D; break;
                case TextureTarget.Texture2DMultisample: texture = currentUnit.texture2DMultisample; break;
                case TextureTarget.Texture2DArray: texture = currentUnit.texture2DArray; break;
                case TextureTarget.Texture3D: texture = currentUnit.texture3D; break;
                case TextureTarget.Texture2DMultisampleArray: texture = currentUnit.texture2DMultisampleArray; break;
                case TextureTarget.TextureCubeMap: texture = currentUnit.textureCubeMap; break;
                case TextureTarget.TextureBuffer: texture = currentUnit.textureBuffer; break;
                case TextureTarget.TextureRectangle: texture = currentUnit.textureRectangle; break;
                default:
                    throw new NotImplementedException();
            }

            return texture;
        }
    }
}
