using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    public partial class Texture
    {
        private Dictionary<uint, float> nameValueDict = new Dictionary<uint, float>();

        public void SetValue(uint name, float value)
        {
            Dictionary<uint, float> dict = this.nameValueDict;
            if (dict.ContainsKey(name)) { dict[name] = value; }
            else { dict.Add(name, value); }
        }

        public float GetValue(uint name)
        {
            float result = 0;

            Dictionary<uint, float> dict = this.nameValueDict;
            if (dict.ContainsKey(name)) { result = dict[name]; }

            return result;
        }

        private void InitParameters()
        {

        }
    }
}
