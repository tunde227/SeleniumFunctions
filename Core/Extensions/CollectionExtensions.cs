using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public class CollectionExtensions
    {
        public static bool IsEmpty<T>(IEnumerable<T> source) => !source.Any();
    }
}