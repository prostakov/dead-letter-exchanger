using System;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using Microsoft.AspNetCore.Components;

namespace DeadLetterRedemption.Blazor.Pages
{
    public partial class SideDashboard : IAsyncDisposable
    {
        private AppState _appState = new();
        
        [Inject]
        public AppStateManager AppStateManager { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override Task OnInitializedAsync()
        {
            AppStateManager.OnChange += (appState =>
            {
                _appState = appState;
                StateHasChanged();
            });
            return Task.CompletedTask;
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
