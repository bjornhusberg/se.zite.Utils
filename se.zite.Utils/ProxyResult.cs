using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace se.zite.Utils
{
    /// <summary>
    /// Example code for an ActionResult which proxies a HttpClient response.
    /// </summary>
    public class ProxyResult : ActionResult, IDisposable
    {
        private readonly HttpResponseMessage _proxiedResponse;
        
        private static readonly string[] BlackListedHeaders = { "Host", "Connection", "Transfer-Encoding" };
        
        public ProxyResult(HttpResponseMessage proxiedResponse)
        {
            _proxiedResponse = proxiedResponse;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            
            response.StatusCode = (int) _proxiedResponse.StatusCode;
            
            response.Headers.Clear();
            response.Headers.AddAll(
                _proxiedResponse
                    .AllHeaders()
                    .Where(header => !BlackListedHeaders.Contains(header.Key)));
    
            using (var inStream = await _proxiedResponse.Content.ReadAsStreamAsync())
            {
                var outStream = response.Body;
                await inStream.CopyToAsync(outStream);
            }
        }

        public void Dispose()
        {
            _proxiedResponse?.Dispose();
        }
    }
}
