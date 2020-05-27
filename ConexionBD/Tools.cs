using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBD
{
    public static class Tools
    {
        public static string StringLength(string text, int length)
        {
            if (text == null || text.Length <= length) return text;
            return text.Substring(0, length);
        }

        public static string float2SQL(float f)
        {
            return f.ToString().Replace(',', '.');
        }

        public static string SQL2Float(string x)
        {
            return x.Replace('.', ',');
        }
    }
}
