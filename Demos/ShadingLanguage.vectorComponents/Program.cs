using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShadingLanguage.vectorComponents
{
    class Program
    {
        static void Main(string[] args)
        {
            DumpVec2();
            DumpVec3();
            DumpVec4();
        }

        static void DumpVec4()
        {
            //public vec4 xxyz { get { return new vec4(x, x, y, z); } }
            var builder = new StringBuilder();
            var fields = new string[] { "x", "y", "z", "w" };
            for (int i = 0; i < fields.Length; i++)
            {
                for (int j = 0; j < fields.Length; j++)
                {
                    for (int k = 0; k < fields.Length; k++)
                    {
                        for (int m = 0; m < fields.Length; m++)
                        {
                            builder.AppendLine(string.Format("public vec4 {2}{3}{4}{5} {0} get {0} return new vec4({2}, {3}, {4}, {5}); {1} set {0} this.{2} = value.x; this.{3} = value.y; this.{4} = value.z; this.{5} = value.w; {1} {1}",
                                "{", "}", fields[i], fields[j], fields[k], fields[m]));
                        }
                    }
                }
            }

            File.WriteAllText("vec4.components.txt", builder.ToString());
        }

        static void DumpVec3()
        {
            //public vec3 xxx { get { return new vec3(x, x, x); } }
            var builder = new StringBuilder();
            var fields = new string[] { "x", "y", "z" };
            for (int i = 0; i < fields.Length; i++)
            {
                for (int j = 0; j < fields.Length; j++)
                {
                    for (int k = 0; k < fields.Length; k++)
                    {
                        builder.AppendLine(string.Format("public vec3 {2}{3}{4} {0} get {0} return new vec3({2}, {3}, {4}); {1} set {0} this.{2} = value.x; this.{3} = value.y; this.{4} = value.z; {1} {1}",
                            "{", "}", fields[i], fields[j], fields[k]));
                    }
                }
            }

            File.WriteAllText("vec3.components.txt", builder.ToString());
        }

        static void DumpVec2()
        {
            //public vec2 xy { get { return new vec2(x, y); } set { this.x = value.x; this.y = value.y; } }
            var builder = new StringBuilder();
            var fields = new string[] { "x", "y" };
            for (int i = 0; i < fields.Length; i++)
            {
                for (int j = 0; j < fields.Length; j++)
                {
                    builder.AppendLine(string.Format("public vec2 {2}{3} {0} get {0} return new vec2({2}, {3}); {1} set {0} this.{2} = value.x; this.{3} = value.y; {1} {1}",
                        "{", "}", fields[i], fields[j]));
                }
            }

            File.WriteAllText("vec2.components.txt", builder.ToString());
        }
    }
}
