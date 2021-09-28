using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Middlewares
{
    public class RequestConsoleLoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("======== Request Started =======");
            await next(context);
            Console.WriteLine("======== Request End =======");
        }
    }
}
