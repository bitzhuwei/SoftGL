using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class Texture
    {
        private Dictionary<uint, float> pnameParamDict = new Dictionary<uint, float>();

        public void SetProperty(uint pname, float param)
        {
            // TODO: if (if params​ should have a defined constant value (based on the value of pname​) and does not)
            //  { SetLastError(ErrorCode.InvalidEnum); return; }

            Dictionary<uint, float> dict = this.pnameParamDict;
            if (dict.ContainsKey(pname)) { dict[pname] = param; }
            else { dict.Add(pname, param); }
        }

        public float GetProperty(uint pname)
        {
            float result = 0;

            Dictionary<uint, float> dict = this.pnameParamDict;
            if (dict.ContainsKey(pname)) { result = dict[pname]; }

            return result;
        }

        private void InitParameters()
        {

        }
    }
}
