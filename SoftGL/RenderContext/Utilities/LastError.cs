﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private uint lastErrorCode = 0;

        private void SetLastError(ErrorCode errorCode)
        {
            this.lastErrorCode = (uint)errorCode;
        }

        public uint GetError()
        {
            return this.lastErrorCode;
        }
    }
}
