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
            return string.Format("Vertex Array Object: Id:{0}", this.Id);
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

        public int GetVertexCount()
        {
            int result = -1;
            if (this.vbo == null) { return result; }
            int byteLength = this.vbo.Data.Length;
            // we know that all data types are in VertexAttribType.
            VertexAttribType type = (VertexAttribType)this.dataType;
            if (!Enum.IsDefined(typeof(VertexAttribType), type)) { return result; }
            uint typeLength = type.GetByteLength(); // byte length of specified type.
            uint typeCount = (uint)(this.dataSize == GL.GL_BGRA ? 4 : this.dataSize); // how many elements of specified type in one vertex?
            uint vertexLength = typeLength * typeCount + stride; // byte length of a single vertex.
            result = (int)(byteLength / vertexLength); // how many vertex?
            if ((byteLength % vertexLength) != 0)
            { throw new Exception(string.Format("GetVertexCount() error! [{0} % {1}, t:{2}, s:{3}]", byteLength, vertexLength, type, typeCount)); }

            return result;
        }

        public int GetDataIndex(uint indexID)
        {
            int result = -1;
            if (this.vbo == null) { return result; }
            int byteLength = this.vbo.Data.Length;
            // we know that all data types are in VertexAttribType.
            VertexAttribType type = (VertexAttribType)this.dataType;
            if (!Enum.IsDefined(typeof(VertexAttribType), type)) { return result; }
            uint typeLength = type.GetByteLength(); // byte length of specified type.(float, int, uint, ..)
            uint typeCount = (uint)(this.dataSize == GL.GL_BGRA ? 4 : this.dataSize); // how many elements of specified type in one vertex? (1, 2, 3 or 4)
            uint vertexLength = typeLength * typeCount + stride; // byte length of a single vertex.
            result = (int)(vertexLength * indexID + this.startPos);

            return result;
        }
    }
}
