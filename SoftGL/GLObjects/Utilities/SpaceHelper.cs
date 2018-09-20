using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SoftGL
{
    static class SpaceHelper
    {
        public static void PrintLine(this StringBuilder builder, string content, int tabCount)
        {
            for (int i = 0; i < tabCount; i++)
            {
                builder.Append("    ");
            }
            builder.AppendLine(content);
        }

    }
}
