using System.Collections.Generic;
using System.Text;

namespace Selenium.Extensions
{
    public static class Arrays
    {
        public static List<T> AsList<T>(params T[] arg)
        {
            return new List<T>(arg);
        }

        public static string ToString(params object[] a)
        {
            if (a == null)
                return "null";

            int iMax = a.Length - 1;
            if (iMax == -1)
                return "[]";

            StringBuilder b = new StringBuilder();
            b.Append('[');
            for (int i = 0;; i++)
            {
                b.Append(a[i].ToString());
                if (i == iMax)
                    return b.Append(']').ToString();
                b.Append(", ");
            }
        }
    }
}