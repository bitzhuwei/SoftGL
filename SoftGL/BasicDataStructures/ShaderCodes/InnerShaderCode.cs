using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    abstract unsafe class InnerShaderCode : ICloneable
    {
        public abstract void main();

        #region ICloneable 成员

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
