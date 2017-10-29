using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace se.zite.Utils
{
    public class ProxyController : Controller
    {
        private readonly ISharedHttpClient _httpClient;

        public ProxyController(ISharedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("/")]
        public async Task<IActionResult> Proxy()
        {
            var message = new HttpRequestMessage
            {
                Method = new HttpMethod(HttpContext.Request.Method),
                RequestUri = new Uri("http://www.dn.se"),
                Content = new StreamContent(HttpContext.Request.Body),
            };
            ProxyHeadersUtil.CopyProxyHeaders(HttpContext.Request, message);
            return new ProxyResult(await _httpClient.SendAsync(message));
        }
    }
}