using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SoftGL
{
    class TessEvaluationShader : PipelineShader
    {
        public override int PipelineOrder { get { return 2; } }

        public TessEvaluationShader(uint id) : base(ShaderType.TessEvaluationShader, id) { }

        protected override string AfterCompile()
        {
            throw new NotImplementedException();
        }

        protected override string GenerateInnerCodeType(string innerCodeTypeName)
        {
            Type codeType = this.codeType;
            var assemblyName = new AssemblyName(innerCodeTypeName);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(innerCodeTypeName, codeType.Name + ".dll");
            Type parentType = this.ShaderType.GetShaderCodeBaseType();
            TypeBuilder typeBuilder = moduleBuilder.DefineType(innerCodeTypeName, TypeAttributes.Public, parentType);
            Type innerCodePropertiesType = this.innerPropertiesCodeType;
            // create Properties for the 'Inner{0}Code' type.
            FieldInfo vsOutput = parentType.GetField("vsOutput");
            FieldInfo gl_VertexID = parentType.GetField("gl_VertexID");
            FieldInfo vsOutByteLength = parentType.GetField("vsOutByteLength");
            uint offset = 0;
            //offset += uniform.propertyInfo.PropertyType.ByteSize();
            foreach (PropertyInfo propertyInfo in codeType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                string propertyName = propertyInfo.Name;
                PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, propertyInfo.Attributes, propertyInfo.PropertyType, null);
                {
                    string getMethodName = "get_" + propertyName;
                    var methodBuilder = typeBuilder.DefineMethod(getMethodName, MethodAttributes.Public);
                    ILGenerator il = methodBuilder.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, vsOutput);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, gl_VertexID);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, vsOutByteLength);
                    il.Emit(OpCodes.Mul);
                    il.Emit(OpCodes.Ldc_I4_S, (int)offset);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Ldind_U1);
                    il.Emit(OpCodes.Conv_U);
                    il.Emit(OpCodes.Ldobj, propertyInfo.PropertyType);
                    il.Emit(OpCodes.Ret);
                    propertyBuilder.SetGetMethod(methodBuilder);
                }
                {
                    string setMethodName = "set_" + propertyName;
                    //byte[] ilBytes = methodInfo.GetMethodBody().GetILAsByteArray();
                    var methodBuilder = typeBuilder.DefineMethod(setMethodName, MethodAttributes.Public);
                    //methodBuilder.CreateMethodBody(ilBytes, ilBytes.Length);
                    ILGenerator il = methodBuilder.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, vsOutput);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, gl_VertexID);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, vsOutByteLength);
                    il.Emit(OpCodes.Mul);
                    il.Emit(OpCodes.Ldc_I4_S, (int)offset);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Ldind_U1);
                    il.Emit(OpCodes.Conv_U);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Stobj, propertyInfo.PropertyType);
                    il.Emit(OpCodes.Ret);
                    propertyBuilder.SetSetMethod(methodBuilder);
                }

                offset += propertyInfo.PropertyType.ByteSize();
            }
            foreach (MethodInfo methodInfo in codeType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                string methodName = methodInfo.Name;
                MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName, methodInfo.Attributes, methodInfo.ReturnType,
                    (from item in methodInfo.GetParameters() select item.ParameterType).ToArray());
                byte[] ilBytes = methodInfo.GetMethodBody().GetILAsByteArray();
                methodBuilder.CreateMethodBody(ilBytes, ilBytes.Length);
            }
            this.innerCodeType = typeBuilder.CreateType();

            {
                assemblyBuilder.Save(codeType.Name + ".dll");
            }

            return string.Empty;
        }
    }
}
