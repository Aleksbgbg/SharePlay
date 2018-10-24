namespace StreamServer.Middleware
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class MockRequestFilter
    {
        private readonly RequestDelegate _next;

        public MockRequestFilter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["User-Agent"].ToString().StartsWith("Windows-Media-Player"))
            {
                return;
            }

            await _next(context);
        }
    }
}