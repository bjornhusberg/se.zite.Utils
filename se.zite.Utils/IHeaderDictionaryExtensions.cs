using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace se.zite.Utils
{
    public static class HeaderDictionaryExtensions
    {
        public static void AddAll(this IHeaderDictionary dictionary, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            headers.ToList().ForEach(header =>
                dictionary.Add(header.Key, new StringValues(header.Value.ToArray())));
        }
    }
}