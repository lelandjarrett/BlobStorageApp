using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FileUploader
{
    public class Program
    {
        public string _connectionString = "DefaultEndpointsProtocol=https;AccountName=custimage;AccountKey=YmP2PEIgU+4g9N/fFxOgiF7AqdfnjkrkrYINKj4WkgIEOnhrDamCFWnw6/uWz0gd0zAcwB/jgIhS+AStl2sNgA==;EndpointSuffix=core.windows.net";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
