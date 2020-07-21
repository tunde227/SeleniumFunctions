using System;
using Core.Utility;

namespace Core.Extensions
{
    public static class Objects
    {
        public static T RequireNonNull<T>(string message, T t) where T : class
        {
            return t ?? throw new Exception(message);
        }

        public static T RequireNonNull<T>(T t) where T : class
        {
            return RequireNonNull($"{typeof(T).Name} is null", t);
        }

        public static bool IsNull<T>(T t)
        {
            return Equals(null, t);
        }

        public static void RequireNonNull<T>(params T[] t) where T : class
        {
            Arrays.AsList(t).ForEach(item => RequireNonNull($"{item.GetType().Name}: {nameof(item)} is null", item));
        }

        public static string ToString(this object source)
        {
            return ObjectDumper.Write(source);
        }
    }
}