using System.Diagnostics;

namespace nowe_Restaurant_API.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> logger;
        private readonly Stopwatch _stopwatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            this.logger = logger;
            _stopwatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
           _stopwatch.Start();   
            await next.Invoke(context);
            _stopwatch.Stop();
            var elapsedmisilecound = _stopwatch.ElapsedMilliseconds;
            if (elapsedmisilecound/1000 >= 4)
            {
                var message = $"Request {context.Request.Method} at {context.Request.Path} took {elapsedmisilecound}";
                logger.LogInformation(message);
            }
        }
    }
}
