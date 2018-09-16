using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    sealed class InAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    sealed class OutAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    sealed class UniformAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    sealed class LocationAttribute : Attribute
    {
        public readonly uint location;

        public LocationAttribute(uint location)
        {
            this.location = location;
        }

        public override string ToString()
        {
            return string.Format("(location = {0})", this.location);
        }
    }
}
