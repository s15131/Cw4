using Cw4.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cw4.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
                httpContext.Request.EnableBuffering();
                string sciezka = httpContext.Request.Path; //"weatherforecast/cos"
                string querystring = httpContext.Request.QueryString.ToString();
                string metoda = httpContext.Request.Method;
                string bodyStr = "";

                using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                    
                }

                using (StreamWriter writer = new StreamWriter(@"requestsLog.txt", true))
                {
                    writer.WriteLine("");
                    writer.WriteLine(sciezka);
                    writer.WriteLine(querystring);
                    writer.WriteLine(metoda);
                    writer.WriteLine(bodyStr);

                }

            
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next(httpContext);
        }


    }
}