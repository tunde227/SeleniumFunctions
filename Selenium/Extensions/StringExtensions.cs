using System;

namespace Selenium.Extensions
{
    public static class StringExtensions
    {
        private const int IndexNotFound = -1;

        public static bool IsBlank(this string source) => string.IsNullOrEmpty(source);

        public static bool IsNotBlank(this string source) => !source.IsBlank();

        public static string RequireNonNullOrEmpty(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source), $"String is {(source.IsEmpty() ? "empty" : "null")}");
            }

            return source;
        }

        public static bool ContainsIgnoreCase(this string source, string str) =>
            source?.IndexOf(str, StringComparison.CurrentCultureIgnoreCase) >= 0;

        public static bool IsEmpty(this string source) => source == string.Empty;

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
            if (pos == IndexNotFound)
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
            if (pos == IndexNotFound)
            {
                return source;
            }

            return source.Substring(pos + separator.Length).Trim();
        }

        public static bool EqualsIgnoreCase(this string source, string str) =>
            source.Equals(str, StringComparison.CurrentCultureIgnoreCase);

        public static bool ToBoolean(this string str)
        {
            if ("true".EqualsIgnoreCase(str))
                return true;
            if ("false".EqualsIgnoreCase(str))
                return false;
            throw new ArgumentNullException(str, "String is null.  Can't convert to Boolean.");
        }

        public static string Append(this string source, string str) => source += str;

        public static string Append(this string source, char str) => source += str.ToString();
        public static string Strip(this string source, string value) => source.Replace(value, "");
    }
}