namespace StreamServer
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    using Shared.Configuration;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .UseUrls($"http://localhost:{Constants.StreamServerPort}")
                   .Build()
                   .Run();
        }
    }
}