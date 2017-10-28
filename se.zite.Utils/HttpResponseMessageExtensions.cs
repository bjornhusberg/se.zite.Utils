using System.Collections.Generic;
using System.Net.Http;

namespace se.zite.Utils
{
    public static class HttpResponseMessageExtensions
    {
        public static IEnumerable<KeyValuePair<string, IEnumerable<string>>> AllHeaders(this HttpResponseMessage response)
        {
            var headers = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers.AddRange(response.Headers);
            headers.AddRange(response.Content.Headers);
            return headers;
        }
        
    }
}