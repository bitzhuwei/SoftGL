using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class CodeBase
    {

        public static vec2 vec2(double x, double y)
        {
            return new vec2((float)x, (float)y);
        }

        public static vec3 vec3(double x, double y, double z)
        {
            return new vec3((float)x, (float)y, (float)z);
        }

        public static vec3 vec3(vec2 xy, double z)
        {
            return new vec3(xy.x, xy.y, (float)z);
        }

        public static vec3 vec3(double x, vec2 yz)
        {
            return new vec3((float)x, yz.x, yz.y);
        }

        public static vec4 vec4(double x, double y, double z, double w)
        {
            return new vec4((float)x, (float)y, (float)z, (float)w);
        }

        public static vec4 vec4(vec2 xy, vec2 zw)
        {
            return new vec4(xy.x, xy.y, zw.x, zw.y);
        }

        public static vec4 vec4(double x, double y, vec2 zw)
        {
            return new vec4((float)x, (float)y, zw.x, zw.y);
        }

        public static vec4 vec4(vec2 xy, double z, double w)
        {
            return new vec4(xy.x, xy.y, (float)z, (float)w);
        }

        public static vec4 vec4(vec3 xyz, double w)
        {
            return new vec4(xyz, (float)w);
        }

        public static vec4 vec4(double x, vec3 yzw)
        {
            return new vec4((float)x, yzw.x, yzw.y, yzw.z);
        }

        public static vec3 normalize(vec3 v)
        {
            throw new NotImplementedException();
        }

        public static float dot(vec3 left, vec3 right)
        {
            throw new NotImplementedException();
        }

        public static float length(vec3 v)
        {
            throw new NotImplementedException();
        }

    }
}
