using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisProject.Internal.Helpers
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, T element)
        {
            return collection.Except(new List<T> { element });
        }
    }
}
