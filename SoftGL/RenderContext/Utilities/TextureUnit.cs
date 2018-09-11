using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    struct TextureUnit
    {
        public uint texture1D;
        public uint texture2D;
        public uint texture2DMultisample;
        public uint texture2DArray;
        public uint texture3D;
        public uint texture2DMultisampleArray;
        public uint textureCubeMap;
        public uint textureBuffer;
        public uint textureRectangle;
    }
}
