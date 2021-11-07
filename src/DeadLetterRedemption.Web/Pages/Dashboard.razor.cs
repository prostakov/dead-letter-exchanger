using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using DeadLetterRedemption.Common.EventArgs;
using Microsoft.AspNetCore.Components;

namespace DeadLetterRedemption.Web.Pages
{
    public partial class Dashboard : IAsyncDisposable
    {
        private AppState _appState = new();
        
        [Inject]
        public AppStateManager AppStateManager { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AppStateManager.OnChange += (appState =>
            {
                _appState = appState;
                StateHasChanged();
            });
            var username = Guid.NewGuid().ToString();
            await AppStateManager.Initialize(NavigationManager.BaseUri, username);
        }

        public async ValueTask DisposeAsync() => await AppStateManager.DisposeAsync();

        private async Task TriggerStateChange()
        {
            await AppStateManager.AppClient.Send("trigger change");
        }
    }
}
