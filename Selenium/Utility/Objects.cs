using System;
namespace Selenium.Utility
{
    public static class Objects
    {
        public static T RequireNonNull<T>(string message, T t) where T : class
        {
            if (Object.Equals(null, t))
            {
                throw new Exception(message);
            }
            return t;
        }

        public static T RequireNonNull<T>(T t) where T : class
        {
            return RequireNonNull<T>("Object is null", t);
        }

        public static bool IsNull<T>(T t)
        {
            return Object.Equals(null, t);
        }
    }
}
