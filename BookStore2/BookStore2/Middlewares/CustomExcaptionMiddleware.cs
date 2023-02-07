using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace BookStore2.Middlewares
{
    public class CustomExcaptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExcaptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew(); //burda işlem içinde ne kadar zaman harcandığına bakıyorum.
            try
            {

                string message = "{Request} HTTP " + context.Request.Method + " - " + context.Request.Path;
                System.Console.WriteLine(message);

                await _next(context);

                watch.Stop();

                message = "{Response} HTTP" + context.Request.Method + " - " + context.Request.Path + " responded "
                    + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                System.Console.WriteLine(message);



            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private  Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            string message = "{ERROR} HTTP" + context.Request.Method + " - " + context.Response.StatusCode +
                "Error Message" + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms ";
            
            Console.WriteLine(message);
            
            context.Response.ContentType = "application/json";
            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            var result =JsonConvert.SerializeObject(new {error =ex.Message}, Formatting.None);
          
            return context.Response.WriteAsync(result);

          
            
        }
    }
    static public class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExcaptionMiddleware>();

        }
    }
}
