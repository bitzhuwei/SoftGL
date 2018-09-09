using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    struct ContexPair
    {
        public readonly IntPtr deviceContext;
        public readonly IntPtr renderContext;

        public ContexPair(IntPtr deviceContext, IntPtr renderContext)
        {
            this.deviceContext = deviceContext;
            this.renderContext = renderContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("dc:{0}, rc:{1}", this.deviceContext, this.renderContext);
        }
    }
}
