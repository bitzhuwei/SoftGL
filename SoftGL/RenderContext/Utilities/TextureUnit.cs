using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    struct TextureUnit
    {
        public Texture texture1D;
        public Texture texture2D;
        public Texture texture2DMultisample;
        public Texture texture2DArray;
        public Texture texture3D;
        public Texture texture2DMultisampleArray;
        public Texture textureCubeMap;
        public Texture textureBuffer;
        public Texture textureRectangle;
    }
}
