using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DeadLetterRedemption.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<AppClient>();
            builder.Services.AddScoped<AppStateManager>();
            
            // TODO: Authorization
            // builder.Services.AddMsalAuthentication(options =>
            // {
            //     builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
            //     options.ProviderOptions.DefaultAccessTokenScopes.Add(ApiScope);
            //     options.ProviderOptions.LoginMode = "redirect";
            // });

            await builder.Build().RunAsync();
        }
    }
}
