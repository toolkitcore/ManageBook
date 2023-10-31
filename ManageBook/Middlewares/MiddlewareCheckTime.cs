using System.Diagnostics;

namespace ManageBook.Middlewares
{
    public class MiddlewareCheckTime
    {
        private readonly RequestDelegate _next;
        public MiddlewareCheckTime(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
               await context.Response.WriteAsync("\nrequests take more than 500ms - Run Time is "+elapsedMilliseconds);
            }
        }
    }
}
