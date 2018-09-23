using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Reflection.Emit;
using SoftGL;

namespace CodDomConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //EmitTest();
            SoftGL.ShaderTest.Test();
        }

        static void AllinOne()
        {
            Assembly originalAssembly = GetOriginalAssembly(vertexCode); // assembly of vertexCode.
            var inVariables = GetInVariables(originalAssembly); // get InVariable[] like ("in vec3 inPosition")
            string innerVertexCode = GetInnerVertexCode(inVariables); // get "in vec3 inPosition { get{..} set{..} }"
            Assembly innerAssembly = GetInnerAssembly(innerVertexCode);
            byte[] methodBytes = GetMethodBody(innerAssembly);
            // how to create "main(){..}"
            MethodBuilder methodBuilder = GetMethodBuilder();
            methodBuilder.CreateMethodBody(methodBytes, methodBytes.Length);
            // how to create properties "in vec3 inPosition { get{..} set{..} }"
            PropertyBuilder propertyBuilder = GetPropertyBuilder();
            propertyBuilder.SetGetMethod(null);
            propertyBuilder.SetSetMethod(null);
            TypeBuilder typeBuilder = GetTypeBuilder();
            Type targetType = typeBuilder.CreateType();
            object instance = Activator.CreateInstance(targetType);

        }

        private static TypeBuilder GetTypeBuilder()
        {
            throw new NotImplementedException();
        }

        private static PropertyBuilder GetPropertyBuilder()
        {
            throw new NotImplementedException();
        }

        private static MethodBuilder GetMethodBuilder()
        {
            throw new NotImplementedException();
        }

        private static byte[] GetMethodBody(Assembly innerAssembly)
        {
            throw new NotImplementedException();
        }

        private static Assembly GetInnerAssembly(string innerVertexCode)
        {
            throw new NotImplementedException();
        }

        private static string GetInnerVertexCode(object inVariables)
        {
            throw new NotImplementedException();
        }

        private static object GetInVariables(Assembly originalAssembly)
        {
            throw new NotImplementedException();
        }

        private static Assembly GetOriginalAssembly(string vertexCode)
        {
            throw new NotImplementedException();
        }
        static void EmitTest()
        {
            // specify a new assembly name
            var assemblyName = new AssemblyName("Kitty");
            // create assembly builder
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            // create module builder
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("KittyModule", "Kitty.exe");
            // create type builder for a class
            TypeBuilder typeBuilder = moduleBuilder.DefineType("HelloKittyClass", TypeAttributes.Public);
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("inPosition", PropertyAttributes.None, typeof(float), new Type[] { typeof(float) });
            //propertyBuilder.SetGetMethod(MethodBuilder)

            // create method builder
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
              "SayHelloMethod",
              MethodAttributes.Public | MethodAttributes.Static,
              null,
              null);
            //methodBuilder.CreateMethodBody()

            // then get the method il generator
            ILGenerator il = methodBuilder.GetILGenerator();

            // then create the method function
            il.Emit(OpCodes.Ldstr, "Hello, Kitty!");
            il.Emit(OpCodes.Call,
              typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine"));
            il.Emit(OpCodes.Pop); // we just read something here, throw it.
            il.Emit(OpCodes.Ret);

            // then create the whole class type
            Type helloKittyClassType = typeBuilder.CreateType();
            // set entry point for this assembly
            assemblyBuilder.SetEntryPoint(helloKittyClassType.GetMethod("SayHelloMethod"));
            // save assembly
            assemblyBuilder.Save("Kitty.exe");
        }

        const string vertexCode = @"using System;

namespace SoftGL
{
    // TODO: 动态改写C#代码。

    class SimpleVert : VertexShaderCode
    {
        [In] vec3 inPosition;
        [In] vec3 inColor;

        [Uniform] mat4 mvpMatrix;

        [Out] vec3 passColor;

        public override void main()
        {
            gl_Position = mvpMatrix * new vec4(inPosition, 1.0f);
            passColor = inColor;
        }
    }
}
";

        const string fragmentCode = @"using System;

namespace SoftGL
{
    class SimpleFrag : FragmentShaderCode
    {
        [In]
        vec3 passColor;

        [Out]
        vec4 outColor;

        public override void main()
        {
            outColor = new vec4(passColor, 1.0f);
        }
    }
}
";

    }


}
