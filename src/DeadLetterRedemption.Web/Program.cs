using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DeadLetterRedemption.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<AppClient>();
            builder.Services.AddScoped<AppStateManager>();

            await builder.Build().RunAsync();
        }
    }
}
