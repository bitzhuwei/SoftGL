﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftGL
{
    class TessControlShader : Shader
    {
        public TessControlShader(uint id) : base(ShaderType.TessControlShader, id) { }

        protected override string AfterCompile()
        {
            throw new NotImplementedException();
        }
    }
}