namespace StreamServer.Extensions
{
    using Microsoft.AspNetCore.Builder;

    using StreamServer.Middleware;

    internal static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder UseMockRequestFilter(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MockRequestFilter>();
        }
    }
}