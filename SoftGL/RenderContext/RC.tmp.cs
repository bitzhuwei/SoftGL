﻿using System;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        public static void glSomething(int id)
        {
            SoftGLRenderContext context = ContextManager.GetCurrentContextObj();
            if (context != null)
            {
                context.Something(id);
            }
        }

        private void Something(int id)
        {

        }
    }
}
