using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private unsafe void FragmentShaderStage(ShaderProgram program, List<Fragment> fragmentList)
        {
            FragmentShader fs = program.FragmentShader; if (fs == null) { return; }

            foreach (var fragment in fragmentList)
            {
                var instance = fs.CreateCodeInstance() as FragmentCodeBase; // an executable fragment shader.
                instance.gl_FragCoord = fragment.gl_FragCoord; // setup fragmen coordinatein window/screen space.
                // setup "in SomeType varName;" vertex attributes.
                InVariable[] inVariables = fs.inVariableDict.Values.ToArray();
                if (inVariables.Length != fragment.attributes.Length) { throw new Exception("This should not happen!"); }
                for (int index = 0; index < inVariables.Length; index++)
                {
                    InVariable inVar = inVariables[index];
                    PassBuffer attribute = fragment.attributes[index];
                    var pointer = attribute.Mapbuffer().ToPointer();
                    switch (attribute.elementType)
                    {
                        case PassType.Float: inVar.fieldInfo.SetValue(instance, ((float*)pointer)[0]); break;
                        case PassType.Vec2: inVar.fieldInfo.SetValue(instance, ((vec2*)pointer)[0]); break;
                        case PassType.Vec3: inVar.fieldInfo.SetValue(instance, ((vec3*)pointer)[0]); break;
                        case PassType.Vec4: inVar.fieldInfo.SetValue(instance, ((vec4*)pointer)[0]); break;
                        case PassType.Mat2: inVar.fieldInfo.SetValue(instance, ((mat2*)pointer)[0]); break;
                        case PassType.Mat3: inVar.fieldInfo.SetValue(instance, ((mat3*)pointer)[0]); break;
                        case PassType.Mat4: inVar.fieldInfo.SetValue(instance, ((mat4*)pointer)[0]); break;
                        default: throw new NotDealWithNewEnumItemException(typeof(PassType));
                    }
                    attribute.Unmapbuffer();
                }

                instance.main(); // execute fragment shader code.
                fragment.discard = instance.discard;
                if (!instance.discard) // if this fragment is not discarded.
                {
                    OutVariable[] outVariables = fs.outVariableDict.Values.ToArray();
                    var outBuffers = new PassBuffer[outVariables.Length];
                    for (int index = 0; index < outVariables.Length; index++)
                    {
                        OutVariable outVar = outVariables[index];
                        var outBuffer = new PassBuffer(outVar.fieldInfo.FieldType.GetPassType(), 1);
                        var pointer = outBuffer.Mapbuffer().ToPointer();
                        switch (outBuffer.elementType)
                        {
                            case PassType.Float: ((float*)pointer)[0] = (float)outVar.fieldInfo.GetValue(instance); break;
                            case PassType.Vec2: ((vec2*)pointer)[0] = (vec2)outVar.fieldInfo.GetValue(instance); break;
                            case PassType.Vec3: ((vec3*)pointer)[0] = (vec3)outVar.fieldInfo.GetValue(instance); break;
                            case PassType.Vec4: ((vec4*)pointer)[0] = (vec4)outVar.fieldInfo.GetValue(instance); break;
                            case PassType.Mat2: ((mat2*)pointer)[0] = (mat2)outVar.fieldInfo.GetValue(instance); break;
                            case PassType.Mat3: ((mat3*)pointer)[0] = (mat3)outVar.fieldInfo.GetValue(instance); break;
                            case PassType.Mat4: ((mat4*)pointer)[0] = (mat4)outVar.fieldInfo.GetValue(instance); break;
                            default: throw new NotDealWithNewEnumItemException(typeof(PassType));
                        }
                        outBuffer.Unmapbuffer();
                        outBuffers[index] = outBuffer;
                    }
                    fragment.outVariables = outBuffers;
                }
            }
        }
    }
}
