using System;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using Microsoft.AspNetCore.Components;

namespace DeadLetterRedemption.Blazor.Pages
{
    public partial class Dashboard : IAsyncDisposable
    {
        private AppState _appState = new();
        
        [Inject]
        public AppStateManager AppStateManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AppStateManager.OnChange += 
                appState =>
                {
                    _appState = appState;
                    StateHasChanged();
                };
            await AppStateManager.Initialize(Guid.NewGuid().ToString());
        }

        public async ValueTask DisposeAsync() => await AppStateManager.DisposeAsync();

        private async Task TriggerStateChange()
        {
            await AppStateManager.AppClient.Send("trigger change");
        }
    }
}
