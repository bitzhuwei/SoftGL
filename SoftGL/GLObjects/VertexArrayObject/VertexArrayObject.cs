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

        private Dictionary<uint, VertexAttribDesc> locVertexAttribDict = new Dictionary<uint, VertexAttribDesc>();
        /// <summary>
        /// (location = ..) in vec3 inPosition -> glVertexAttrib(I|L)Pointer(..).
        /// </summary>
        public Dictionary<uint, VertexAttribDesc> LocVertexAttribDict { get { return locVertexAttribDict; } }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Vertex Array Object: Id:{0}, T:{1}", this.Id);
        }
    }

    /// <summary>
    /// glVertexAttrib(I|L)Pointer.
    /// </summary>
    class VertexAttribDesc
    {
        public uint inLocation;
        public GLBuffer vbo;
        public int dataSize;
        public uint dataType;
        /// <summary>
        /// only valid for glVertexAttribPointer(..).
        /// </summary>
        public bool normalize;
        /// <summary>
        /// pointer.
        /// </summary>
        public uint startPos;
        /// <summary>
        /// interval.
        /// </summary>
        public uint stride;
        public bool enabled;
    }
}
