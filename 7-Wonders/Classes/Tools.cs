using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenWonders.Classes
{
    public class Tools
    {
        public static int ConvertToInt(string value)
        {
            int temp;

            if (int.TryParse(value, out temp))
            {
                return temp;
            }
            else
            {
                return 0;
            }
        }
    }
}
