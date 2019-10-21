using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source) => !source.Any();

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> predicate)
        {
            foreach (T item in source ?? throw new ArgumentNullException(nameof(source)))
            {
                predicate?.Invoke(item);
            }
        }

        public static void ForFirst<T>(this IEnumerable<T> source, Action<T> predicate) =>
            predicate?.Invoke((source as T[] ?? source.ToArray()).First());
    }
}