using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{

    class OutVariable
    {
        public uint location = uint.MaxValue;
        public readonly FieldInfo field;

        public OutVariable(FieldInfo field)
        {
            this.field = field;
        }

        public override string ToString()
        {
            return string.Format("f:{0}, loc:{1}", this.field, this.location);
        }
    }
}
