using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigurationSectionDesigner.CustomCode.CodeGeneration
{
    public class Util
    {

        public static bool IsDataEqual(byte[] oldData, byte[] data)
        {
            bool equal = true;
            if (oldData.Length == data.Length)
            {
                for (int j = 0; j < oldData.Length; j++)
                    if (oldData[j] != data[j])
                    {
                        equal = false;
                        break;
                    }
            }
            else
                equal = false;
            return equal;
        }

    }
}
