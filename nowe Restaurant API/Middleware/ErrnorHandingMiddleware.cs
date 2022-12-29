using nowe_Restaurant_API.Exceptions;

namespace nowe_Restaurant_API.Middleware
{
    public class ErrnorHandingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrnorHandingMiddleware> logger;

        public ErrnorHandingMiddleware(ILogger<ErrnorHandingMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ForbiddenException forbiddenException)
            {
                context.Response.StatusCode = 403;
            }
            catch (InvalidLoginExepction Exception)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(Exception.Message);
            }
            catch (NotFoundException notfound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
            }

            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
                
            }
        }
    }
}
