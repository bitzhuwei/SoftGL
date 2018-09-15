using System;
using System.Collections.Generic;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private void InitBufferDict()
        {
            Dictionary<BindBufferTarget, GLBuffer> dict = this.currentBufferDict;
            foreach (var item in Enum.GetValues(typeof(BindBufferTarget)))
            {
                dict.Add((BindBufferTarget)item, null);
            }
        }
    }
}
