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
    class VertexShader : PipelineShader
    {

        public override int PipelineOrder { get { return 0; } }

        public VertexShader(uint id) : base(ShaderType.VertexShader, id) { }

        public int GetAttribLocation(string name)
        {
            int result = -1;
            if (this.InfoLog.Length > 0) { return result; }
            Dictionary<string, InVariable> dict = this.inVariableDict;
            if (dict == null) { return result; }
            InVariable v = null;
            if (dict.TryGetValue(name, out v))
            {
                result = (int)v.location;
            }

            return result;
        }

        protected override string AfterCompile()
        {
            return base.AfterCompile();
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
            FieldInfo gl_VertexID = parentType.GetField("gl_VertexID");
            foreach (PropertyInfo propertyInfo in codeType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                string propertyName = propertyInfo.Name;
                Type fieldPointer = propertyInfo.PropertyType.MakePointerType();
                FieldBuilder fieldBuilder = typeBuilder.DefineField(string.Format("{0}Data", propertyName), fieldPointer, FieldAttributes.Private);
                PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, propertyInfo.Attributes, propertyInfo.PropertyType, new Type[] { propertyInfo.PropertyType });
                //if (propertyInfo.CanRead)
                {
                    string getMethodName = "get_" + propertyName;
                    var methodBuilder = typeBuilder.DefineMethod(getMethodName, MethodAttributes.Public, propertyInfo.PropertyType, new Type[] { });
                    ILGenerator il = methodBuilder.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, fieldBuilder);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, gl_VertexID);
                    il.Emit(OpCodes.Conv_I);
                    il.Emit(OpCodes.Sizeof, propertyInfo.PropertyType);
                    il.Emit(OpCodes.Mul);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Ldobj, propertyInfo.PropertyType);
                    il.Emit(OpCodes.Ret);
                    propertyBuilder.SetGetMethod(methodBuilder);
                }
                if (propertyInfo.GetCustomAttributes(typeof(OutAttribute), false).Length > 0) // this is an [Out] property.
                {
                    string setMethodName = "set_" + propertyName;
                    var methodBuilder = typeBuilder.DefineMethod(setMethodName, MethodAttributes.Public, null, new Type[] { propertyInfo.PropertyType });
                    ILGenerator il = methodBuilder.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, fieldBuilder);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, gl_VertexID);
                    il.Emit(OpCodes.Conv_I);
                    il.Emit(OpCodes.Sizeof, propertyInfo.PropertyType);
                    il.Emit(OpCodes.Mul);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Stobj, propertyInfo.PropertyType);
                    il.Emit(OpCodes.Ret);
                    propertyBuilder.SetSetMethod(methodBuilder);
                }
            }
            //foreach (MethodInfo methodInfo in codeType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            //{
            //    string methodName = methodInfo.Name;
            //    MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName, methodInfo.Attributes, methodInfo.ReturnType,
            //        (from item in methodInfo.GetParameters() select item.ParameterType).ToArray());
            //    byte[] ilBytes = methodInfo.GetMethodBody().GetILAsByteArray();
            //    methodBuilder.CreateMethodBody(ilBytes, ilBytes.Length);
            //}
            this.innerCodeType = typeBuilder.CreateType();

            {
                assemblyBuilder.Save(codeType.Name + ".dll");
            }

            return string.Empty;
        }
    }
}
