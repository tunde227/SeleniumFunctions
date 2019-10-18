using System.Collections.Generic;
using System.Linq;

namespace Selenium.Extensions
{
    public class CollectionExtensions
    {
        public static bool IsEmpty<T>(IEnumerable<T> source) => !source.Any();
    }
}