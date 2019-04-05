using System;
namespace Selenium.Utility
{
    public static class Strings
    {
        public static readonly int INDEX_NOT_FOUND = -1;

        public static bool IsBlank(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNotBlank(this string source)
        {
            return !source.IsBlank();
        }

        public static bool ContainsIgnoreCase(this string source, string str)
        {
            return source.ToLower().Contains(str.ToLower());
        }

        public static bool IsEmpty(this string source)
        {
            return source == string.Empty;
        }

        public static int WordCount(this string source)
        {
            return source.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string[] ToWords(this string source)
        {
            return source.Trim().Split(new char[] { ' ', '.', '?', '!', ':' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string SubstringBefore(this string source, string separator)
        {
            if (source.IsEmpty() || separator == null)
            {
                return source;
            }
            if (separator.IsEmpty())
            {
                return "";
            }
            int pos = source.IndexOf(separator, StringComparison.CurrentCulture);
            if (pos == INDEX_NOT_FOUND)
            {
                return source;
            }
            return source.Substring(0, pos).Trim();
        }

        public static string SubstringAfter(this string source, string separator)
        {
            if (source.IsEmpty() || separator == null)
            {
                return source;
            }
            if (separator.IsEmpty())
            {
                return "";
            }
            int pos = source.IndexOf(separator, StringComparison.CurrentCulture);
            if (pos == INDEX_NOT_FOUND)
            {
                return source;
            }
            return source.Substring(pos + separator.Length).Trim();
        }

        public static bool EqualsIgnoreCase(this string source, string str)
        {
            return source.ToLowerInvariant().Equals(str.ToLowerInvariant());
        }
        public static bool ToBoolean(this String str)
        {
            if ("true".EqualsIgnoreCase(str))
                return true;
            if ("false".EqualsIgnoreCase(str))
                return false;
            throw new ArgumentNullException(str, "String is null.  Can't convert to Boolean.");
        }
        public static string Append(this string source, string str)
        {
            return source + str;
        }

        public static string Append(this string source, char str)
        {
            return source + new string(str;
        }

    }
}
