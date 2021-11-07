using System;
using System.Net.Http;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DeadLetterRedemption.Blazor
{
    public class Program
    {
        private const string ServerApi = nameof(ServerApi);
        
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            // Add http client
            builder.Services.AddHttpClient(ServerApi, 
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddScoped(
                sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(ServerApi));

            // Add signalr client
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
