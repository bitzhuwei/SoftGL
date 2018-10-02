using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    class VertexArrayObject
    {
        public uint Id { get; private set; }

        public VertexArrayObject(uint id) { this.Id = id; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Vertex Array Object: Id:{0}, T:{1}", this.Id);
        }
    }
}
