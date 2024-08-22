using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            Configuration = builder.Configuration;
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseUrl"]!) });
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<IEmpUIService, EmpUIService>();
            Console.WriteLine(builder.Configuration["BaseUrl"]);
            await builder.Build().RunAsync();
        }
    }
}
