using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public abstract class SoftGLAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class InAttribute : SoftGLAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class OutAttribute : SoftGLAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class UniformAttribute : SoftGLAttribute
    {
    }


    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class LocationAttribute : SoftGLAttribute
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
