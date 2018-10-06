using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private unsafe PassBuffer[] VertexShaderStage(int count, DrawElementsType type, IntPtr indices, VertexArrayObject vao, ShaderProgram program, GLBuffer indexBuffer)
        {
            PassBuffer[] passBuffers = null;
            VertexShader vs = program.VertexShader; if (vs == null) { return passBuffers; }

            // init pass-buffers to record output from vertex shader.
            FieldInfo[] outFieldInfos = (from item in vs.outVariableDict select item.Value.fieldInfo).ToArray();
            uint vertexCount = GetVertexCount(vao, indexBuffer, type);
            //int vertexSize = GetVertexSize(outFieldInfos);
            passBuffers = new PassBuffer[1 + outFieldInfos.Length];
            void*[] pointers = new void*[1 + outFieldInfos.Length];
            {
                // the first pass-buffer stores gl_Position.
                var passBuffer = new PassBuffer(PassType.Vec4, (int)vertexCount);
                pointers[0] = (void*)passBuffer.Mapbuffer();
                passBuffers[0] = passBuffer;
            }
            for (int i = 1; i < passBuffers.Length; i++)
            {
                var outField = outFieldInfos[i - 1];
                PassType passType = outField.FieldType.GetPassType();
                var passBuffer = new PassBuffer(passType, (int)vertexCount);
                pointers[i] = (void*)passBuffer.Mapbuffer();
                passBuffers[i] = passBuffer;
            }

            // execute vertex shader for each vertex.
            byte[] indexData = indexBuffer.Data;
            int indexLength = indexData.Length / ByteLength(type);
            GCHandle pin = GCHandle.Alloc(indexData, GCHandleType.Pinned);
            IntPtr pointer = pin.AddrOfPinnedObject();
            var gl_VertexIDList = new List<uint>();
            for (int indexID = indices.ToInt32() / ByteLength(type), c = 0; c < count && indexID < indexLength; indexID++, c++)
            {
                uint gl_VertexID = GetVertexID(pointer, type, indexID);
                if (gl_VertexIDList.Contains(gl_VertexID)) { continue; }
                else { gl_VertexIDList.Add(gl_VertexID); }

                var instance = vs.CreateCodeInstance() as VertexCodeBase; // an executable vertex shader.
                instance.gl_VertexID = (int)gl_VertexID; // setup gl_VertexID.
                // setup "in SomeType varName;" vertex attributes.
                Dictionary<uint, VertexAttribDesc> locVertexAttribDict = vao.LocVertexAttribDict;
                foreach (InVariable inVar in vs.inVariableDict.Values) // Dictionary<string, InVariable>.Values
                {
                    VertexAttribDesc desc = null;
                    if (locVertexAttribDict.TryGetValue(inVar.location, out desc))
                    {
                        byte[] dataStore = desc.vbo.Data;
                        int byteIndex = desc.GetDataIndex(gl_VertexID);
                        VertexAttribType vertexAttribType = (VertexAttribType)desc.dataType;
                        object value = dataStore.ToStruct(inVar.fieldInfo.FieldType, byteIndex);
                        inVar.fieldInfo.SetValue(instance, value);
                    }
                }
                // setup "uniform SomeType varName;" in vertex shader.
                Dictionary<string, UniformValue> nameUniformDict = program.nameUniformDict;
                foreach (UniformVariable uniformVar in vs.UniformVariableDict.Values)
                {
                    string name = uniformVar.fieldInfo.Name;
                    UniformValue obj = null;
                    if (nameUniformDict.TryGetValue(name, out obj))
                    {
                        if (obj.value != null)
                        {
                            uniformVar.fieldInfo.SetValue(instance, obj.value);
                        }
                    }
                }

                instance.main(); // execute vertex shader code.

                // copy data to pass-buffer.
                {
                    PassBuffer passBuffer = passBuffers[0];
                    var array = (vec4*)pointers[0];
                    array[gl_VertexID] = instance.gl_Position;
                }
                for (int i = 1; i < passBuffers.Length; i++)
                {
                    var outField = outFieldInfos[i - 1];
                    var obj = outField.GetValue(instance);
                    switch (outField.FieldType.GetPassType())
                    {
                        case PassType.Float: { var array = (float*)pointers[i]; array[gl_VertexID] = (float)obj; } break;
                        case PassType.Vec2: { var array = (vec2*)pointers[i]; array[gl_VertexID] = (vec2)obj; } break;
                        case PassType.Vec3: { var array = (vec3*)pointers[i]; array[gl_VertexID] = (vec3)obj; } break;
                        case PassType.Vec4: { var array = (vec4*)pointers[i]; array[gl_VertexID] = (vec4)obj; } break;
                        case PassType.Mat2: { var array = (mat2*)pointers[i]; array[gl_VertexID] = (mat2)obj; } break;
                        case PassType.Mat3: { var array = (mat3*)pointers[i]; array[gl_VertexID] = (mat3)obj; } break;
                        case PassType.Mat4: { var array = (mat4*)pointers[i]; array[gl_VertexID] = (mat4)obj; } break;
                        default:
                            throw new NotImplementedException();
                    }
                    // a general way to do this:
                    //var obj = outField.GetValue(instance);
                    //byte[] bytes = obj.ToBytes();
                    //PassBuffer passBuffer = passBuffers[i];
                    //var array = (byte*)passBuffer.AddrOfPinnedObject();
                    //for (int t = 0; t < bytes.Length; t++)
                    //{
                    //    array[gl_VertexID * vertexSize + t] = bytes[t];
                    //}
                }
            }
            pin.Free();

            for (int i = 0; i < passBuffers.Length; i++)
            {
                passBuffers[i].Unmapbuffer();
            }

            return passBuffers;
        }

        /// <summary>
        /// how many bytes a vertex has?
        /// </summary>
        /// <param name="vao"></param>
        /// <returns></returns>
        private int GetVertexSize(FieldInfo[] outVariables)
        {
            int result = 0;
            if (outVariables == null || outVariables.Length == 0) { return result; }

            foreach (var item in outVariables)
            {
                int size = item.FieldType.GetPassType().ByteSize();
                result += size;
            }

            return result;
        }

        /// <summary>
        /// Get the vertex id at specified <paramref name="indexID"/> of the array represented by <paramref name="pointer"/>.
        /// The <paramref name="type"/> indicates the type of the array(byte[], ushort[] or uint[]).
        /// </summary>
        /// <param name="pointer"></param>
        /// <param name="type"></param>
        /// <param name="indexID"></param>
        /// <returns></returns>
        private unsafe uint GetVertexID(IntPtr pointer, DrawElementsType type, int indexID)
        {
            uint gl_VertexID = uint.MaxValue;
            switch (type)
            {
                case DrawElementsType.UnsignedByte:
                    {
                        byte* array = (byte*)pointer.ToPointer();
                        gl_VertexID = array[indexID];
                    }
                    break;
                case DrawElementsType.UnsignedShort:
                    {
                        ushort* array = (ushort*)pointer.ToPointer();
                        gl_VertexID = array[indexID];
                    }
                    break;
                case DrawElementsType.UnsignedInt:
                    {
                        uint* array = (uint*)pointer.ToPointer();
                        gl_VertexID = array[indexID];
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
