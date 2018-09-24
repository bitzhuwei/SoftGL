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
            SoftGL.ShaderTest.Test();
        }
    }
}
