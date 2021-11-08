using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using DeadLetterRedemption.Common.Dto;
using Microsoft.AspNetCore.Components;

namespace DeadLetterRedemption.Blazor.Pages
{
    public partial class Dashboard : IAsyncDisposable
    {
        private AppState _appState = new();
        
        [Inject]
        public AppStateManager AppStateManager { get; set; }
        
        [Inject]
        public HttpClient HttpClient { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AppStateManager.OnChange += 
                appState =>
                {
                    _appState = appState;
                    StateHasChanged();
                };
            await AppStateManager.Initialize();
        }

        public async ValueTask DisposeAsync() => await AppStateManager.DisposeAsync();
        
        private async Task TriggerStateChange()
        {
            //await AppStateManager.AppClient.Send("trigger change");
            //await HttpClient.GetFromJsonAsync<Weather[]>(NavigationManager.BaseUri.TrimEnd('/') + "/weather");
            
            await HttpClient.PostAsync(NavigationManager.BaseUri.TrimEnd('/') + "/admin/trigger", new StringContent("\"action\"", Encoding.UTF8, "application/json"));
            StateHasChanged();
        }
    }
}
