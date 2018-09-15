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
}
