using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public abstract partial class GeometryCodeBase : CodeBase
    {
        public readonly GeometryIn inType;
        public readonly GeometryOut outType;
        public readonly int maxVertices;

        [Out]
        public vec4 gl_Position;

        public GeometryCodeBase(GeometryIn inType, GeometryOut outType, int maxVertices)
        {
            this.inType = inType; this.outType = outType; this.maxVertices = maxVertices;
        }

        public abstract void main();

        protected void EmitVertex() { throw new NotImplementedException(); }
        protected void EndPrimitive() { throw new NotImplementedException(); }
    }
}
