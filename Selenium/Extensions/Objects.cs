using System;
using System.Linq;

namespace Selenium.Extensions
{
    public static class Objects
    {
        public static T RequireNonNull<T>(string message, T t) where T : class
        {
            if (Equals(null, t))
            {
                throw new Exception(message);
            }

            return t;
        }

        public static T RequireNonNull<T>(T t) where T : class
        {
            return RequireNonNull("Object is null", t);
        }

        public static bool IsNull<T>(T t)
        {
            return Equals(null, t);
        }

        public static void RequireNonNull<T>(params T[] t) => t.Select(e => Equals(null, t))
            .Any(e => throw new ArgumentNullException(e.GetType().Name, "value is null!"));
    }
}