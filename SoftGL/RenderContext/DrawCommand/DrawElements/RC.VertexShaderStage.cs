using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private byte[] VertexShaderStage(DrawTarget mode, int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer)
        {
            byte[] vsOutput = null;
            VertexShader vs = program.VertexShader;
            if (vs == null) { return vsOutput; }

            uint vertexCount = GetVertexCount(vao, indexBuffer, type);
            uint vertexSize = GetVertexSize(vs.outVariableDict.Values.ToArray());
            vsOutput = new byte[vertexCount * vertexSize];

            byte[] indexData = indexBuffer.Data;
            int byteLength = indexData.Length;
            GCHandle pin = GCHandle.Alloc(indexData, GCHandleType.Pinned);
            IntPtr pointer = pin.AddrOfPinnedObject();
            var gl_VertexIDList = new List<uint>();
            for (int indexID = 0; indexID < count; indexID++)
            {
                uint gl_VertexID = GetVertexID(pointer, type, byteLength, indexID);
                if (gl_VertexIDList.Contains(gl_VertexID)) { continue; }
                else { gl_VertexIDList.Add(gl_VertexID); }

                var instance = vs.CreateCodeInstance() as VertexCodeBase; // an executable vertex shader.
                instance.gl_VertexID = (int)gl_VertexID; // setup gl_VertexID.
                // setup "in SomeType varName;" vertex attributes.
                Dictionary<uint, VertexAttribDesc> locVertexAttribDict = vao.LocVertexAttribDict;
                foreach (var inVar in vs.inVariableDict.Values) // Dictionary<string, InVariable>.Values
                {
                    VertexAttribDesc desc = null;
                    if (locVertexAttribDict.TryGetValue(inVar.location, out desc))
                    {
                        byte[] dataStore = desc.vbo.Data;
                        int byteIndex = desc.GetDataIndex(indexID);
                        VertexAttribType vertexAttribType = (VertexAttribType)desc.dataType;
                        object value = dataStore.ToStruct(vertexAttribType.GetManagedType(), byteIndex);
                        inVar.fieldInfo.SetValue(instance, value);
                    }
                }

                instance.main(); // execute vertex shader code.

                foreach (var outVar in vs.outVariableDict.Values) // Dictionary<string, OutVariable>.Values
                {
                    var obj = outVar.fieldInfo.GetValue(instance);
                    byte[] bytes = obj.ToBytes();
                    for (int t = 0; t < bytes.Length; t++)
                    {
                        vsOutput[gl_VertexID * vertexSize + t] = bytes[t];
                    }
                }
            }
            pin.Free();

            return vsOutput;
        }

        /// <summary>
        /// how many bytes a vertex has?
        /// </summary>
        /// <param name="vao"></param>
        /// <returns></returns>
        private uint GetVertexSize(OutVariable[] outVariables)
        {
            uint result = 0;
            if (outVariables == null || outVariables.Length == 0) { return result; }

            foreach (var item in outVariables)
            {
                uint size = item.fieldInfo.FieldType.ByteSize();
                result += size;
            }

            return result;
        }

        private unsafe uint GetVertexID(IntPtr pointer, DrawElementsType type, int byteLength, int indexID)
        {
            uint gl_VertexID = uint.MaxValue;
            switch (type)
            {
                case DrawElementsType.UnsignedByte:
                    {
                        int length = byteLength;
                        if (indexID <= length)
                        {
                            byte* array = (byte*)pointer.ToPointer();
                            gl_VertexID = array[indexID];
                        }
                    }
                    break;
                case DrawElementsType.UnsignedShort:
                    {
                        int length = byteLength / 2;
                        if (indexID <= length)
                        {
                            ushort* array = (ushort*)pointer.ToPointer();
                            gl_VertexID = array[indexID];
                        }
                    }
                    break;
                case DrawElementsType.UnsignedInt:
                    {
                        int length = byteLength / 4;
                        if (indexID <= length)
                        {
                            uint* array = (uint*)pointer.ToPointer();
                            gl_VertexID = array[indexID];
                        }
                    }
                    break;
                default:
                    throw new NotDealWithNewEnumItemException(typeof(DrawElementsType));
            }

            return gl_VertexID;
        }

        /// <summary>
        /// How many vertexIDs are there in the specified <paramref name="byteArray"/>.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private uint GetVertexIDCount(byte[] byteArray, DrawElementsType type)
        {
            uint result = 0;
            uint byteLength = (uint)byteArray.Length;
            switch (type)
            {
                case DrawElementsType.UnsignedByte: result = byteLength; break;
                case DrawElementsType.UnsignedShort: result = byteLength / 2; break;
                case DrawElementsType.UnsignedInt: result = byteLength / 4; break;
                default:
                    throw new NotDealWithNewEnumItemException(typeof(DrawElementsType));
            }

            return result;
        }

        /// <summary>
        /// Gets the maximum vertexID in the specified <paramref name="byteArray"/>.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private unsafe uint GetMaxVertexID(byte[] byteArray, DrawElementsType type)
        {
            int byteLength = byteArray.Length;
            GCHandle pin = GCHandle.Alloc(byteArray, GCHandleType.Pinned);
            IntPtr pointer = pin.AddrOfPinnedObject();
            uint gl_VertexID = 0;
            switch (type)
            {
                case DrawElementsType.UnsignedByte:
                    {
                        byte* array = (byte*)pointer.ToPointer();
                        for (int i = 0; i < byteLength; i++)
                        {
                            if (gl_VertexID < array[i]) { gl_VertexID = array[i]; }
                        }
                    }
                    break;
                case DrawElementsType.UnsignedShort:
                    {
                        ushort* array = (ushort*)pointer.ToPointer();
                        int length = byteLength / 2;
                        for (int i = 0; i < length; i++)
                        {
                            if (gl_VertexID < array[i]) { gl_VertexID = array[i]; }
                        }
                    }
                    break;
                case DrawElementsType.UnsignedInt:
                    {
                        uint* array = (uint*)pointer.ToPointer();
                        int length = byteLength / 4;
                        for (int i = 0; i < length; i++)
                        {
                            if (gl_VertexID < array[i]) { gl_VertexID = array[i]; }
                        }
                    }
                    break;
                default:
                    throw new NotDealWithNewEnumItemException(typeof(DrawElementsType));
            }
            pin.Free();

            return gl_VertexID;
        }

        private uint GetVertexCount(VertexArrayObject vao, GLBuffer indexBuffer, DrawElementsType type)
        {
            uint vertexCount = 0;
            VertexAttribDesc[] descs = vao.LocVertexAttribDict.Values.ToArray();
            if (descs.Length > 0)
            {
                int c = descs[0].GetVertexCount();
                if (c >= 0) { vertexCount = (uint)c; }
            }
            else
            {
                uint maxvertexID = GetMaxVertexID(indexBuffer.Data, type);
                uint vertexIDCount = GetVertexIDCount(indexBuffer.Data, type);
                vertexCount = Math.Min(maxvertexID, vertexIDCount);
            }

            return vertexCount;
        }
    }
}
