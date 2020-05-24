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
            if (text == null) Console.WriteLine("Texto: NULL");
            else Console.WriteLine("Texto: " + text);
            if (text == null || text == "" || text.Length <= length) return text;
            return text.Substring(0, length);
        }
    }
}
