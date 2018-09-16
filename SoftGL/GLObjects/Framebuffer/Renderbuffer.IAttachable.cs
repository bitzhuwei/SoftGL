using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class Renderbuffer
    {
        #region IAttachable

        public void Clear(byte[] values)
        {
            IAttachableHelper.Fill(this.DataStore, values);
        }

        #endregion IAttachable
    }
}
