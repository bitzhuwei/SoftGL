﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SoftGL.Windows
{
    partial class WinSoftGL
    {
        private static readonly Type thisType = typeof(SoftOpengl32.StaticCalls);

        //private static 
        public override Delegate GetDelegateFor(string functionName, Type functionDeclaration)
        {
            Delegate result = null;
            if (!extensionFunctions.TryGetValue(functionName, out result))
            {
                MethodInfo methodInfo = thisType.GetMethod(functionName, BindingFlags.Static | BindingFlags.Public);
                if (methodInfo != null)
                {
                    result = System.Delegate.CreateDelegate(functionDeclaration, methodInfo);
                }

                if (result != null)
                {
                    //  Add to the dictionary.
                    extensionFunctions.Add(functionName, result);
                }
            }

            return result;
        }

        /// <summary>
        /// The set of extension functions.
        /// </summary>
        private static readonly Dictionary<string, Delegate> extensionFunctions = new Dictionary<string, Delegate>();

    }
}