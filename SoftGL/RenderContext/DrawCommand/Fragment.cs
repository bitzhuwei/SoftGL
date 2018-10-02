using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    unsafe class Fragment
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly vec3 gl_FragCoord;

        /// <summary>
        /// Only contains data in this fragment.
        /// </summary>
        public readonly PassBuffer[] attributes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fragCoord"></param>
        /// <param name="attributeLength"></param>
        public Fragment(vec3 fragCoord, int attributeLength)
        {
            this.gl_FragCoord = fragCoord;
            this.attributes = new PassBuffer[attributeLength];
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} attributes.", this.gl_FragCoord, this.attributes.Length);
        }
    }
}
