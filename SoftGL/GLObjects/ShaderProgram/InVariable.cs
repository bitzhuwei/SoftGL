using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{

    class InVariable
    {
        public uint location = uint.MaxValue;
        public readonly FieldInfo fieldInfo;

        public InVariable(FieldInfo field)
        {
            this.fieldInfo = field;
        }

        public override string ToString()
        {
            return string.Format("f:{0}, loc:{1}", this.fieldInfo, this.location);
        }
    }
}
