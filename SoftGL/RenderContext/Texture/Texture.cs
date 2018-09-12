﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint nextTextureName = 0;

        private readonly List<uint> textureNameList = new List<uint>();
        /// <summary>
        /// name -> texture object.
        /// </summary>
        private readonly Dictionary<uint, Texture> nameTextureDict = new Dictionary<uint, Texture>();

        private const int maxTextureSize = 1024 * 8; // TODO: maxRenderbufferSize = ?
        private const int maxTextureImageUnits = 8;
        private TextureUnit[] textureUnits = new TextureUnit[maxTextureImageUnits];
        private uint currentTextureUnitIndex = 0;

        public void GenTextures(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = nextTextureName;
                names[i] = name;
                textureNameList.Add(name);
                nextTextureName++;
            }
        }

        public void ActiveTexture(uint textureUnit)
        {
            if (textureUnit < GL.GL_TEXTURE0) { SetLastError(ErrorCode.InvalidEnum); return; }
            if (GL.GL_TEXTURE0 + maxTextureImageUnits <= textureUnit) { SetLastError(ErrorCode.InvalidEnum); return; }

            this.currentTextureUnitIndex = textureUnit - GL.GL_TEXTURE0;
        }

        public void BindTexture(TextureTarget target, uint name)
        {
            if (target == 0) { SetLastError(ErrorCode.InvalidEnum); return; }
            if ((name != 0) && (!this.textureNameList.Contains(name))) { SetLastError(ErrorCode.InvalidOperation); return; }
            Dictionary<uint, Texture> dict = nameTextureDict;
            Texture texture = null;
            if (dict.TryGetValue(name, out texture))
            {
                if (texture.Target != target) { SetLastError(ErrorCode.InvalidOperation); return; }
            }
            else // create a new texture object.
            {
                texture = new Texture(target, name);
                dict.Add(name, texture);
            }

            TextureUnit currentUnit = this.textureUnits[this.currentTextureUnitIndex];
            switch (target)
            {
                case TextureTarget.Texture1D:
                    currentUnit.texture1D = texture;
                    break;
                case TextureTarget.Texture2D:
                    currentUnit.texture2D = texture;
                    break;
                case TextureTarget.Texture2DMultisample:
                    currentUnit.texture2DMultisample = texture;
                    break;
                case TextureTarget.Texture2DArray:
                    currentUnit.texture2DArray = texture;
                    break;
                case TextureTarget.Texture3D:
                    currentUnit.texture3D = texture;
                    break;
                case TextureTarget.Texture2DMultisampleArray:
                    currentUnit.texture2DMultisampleArray = texture;
                    break;
                case TextureTarget.TextureCubeMap:
                    currentUnit.textureCubeMap = texture;
                    break;
                case TextureTarget.TextureBuffer:
                    currentUnit.textureBuffer = texture;
                    break;
                case TextureTarget.TextureRectangle:
                    currentUnit.textureRectangle = texture;
                    break;
                default:
                    break;
            }
        }

        public void DeleteTextures(int count, uint[] names)
        {
            if (count < 0) { SetLastError(ErrorCode.InvalidValue); return; }

            for (int i = 0; i < count; i++)
            {
                uint name = names[i];
                if (textureNameList.Contains(name)) { textureNameList.Remove(name); }
                if (nameTextureDict.ContainsKey(name)) { nameTextureDict.Remove(name); }
            }
        }
    }
}
