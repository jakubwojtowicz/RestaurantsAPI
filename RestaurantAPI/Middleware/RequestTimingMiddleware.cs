using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RestaurantAPI.Middleware
{
    public class RequestTimingMiddleware : IMiddleware
    {
        private ILogger<RequestTimingMiddleware> logger;
        private Stopwatch stopwatch;
        public RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger)
        {
            this.logger = logger;
            stopwatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        { 
            stopwatch.Start();
            await next.Invoke(context);
            stopwatch.Stop();

            var elapsedMiliseconds = stopwatch.ElapsedMilliseconds;

            if(elapsedMiliseconds/1000 > 4)
            {
                logger.LogInformation($"Request {context.Request.Method} at {context.Request.Path} took {elapsedMiliseconds}");
            }
        }
    }
}
