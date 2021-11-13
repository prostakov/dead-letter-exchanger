using System;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using DeadLetterRedemption.Common.Dto;
using Microsoft.AspNetCore.Components;

namespace DeadLetterRedemption.Blazor
{
    public class AppStateManager : IAsyncDisposable
    {
        private readonly NavigationManager _navigationManager;
        
        public AppClient AppClient { get; }
        
        public event Action<AppState> OnChange;
        
        public bool IsReady => AppClient.IsConnectionEstablished;

        public AppStateManager(NavigationManager navigationManager, AppClient appClient)
        {
            _navigationManager = navigationManager;
            AppClient = appClient;
        }

        public async Task Initialize()
        {
            AppClient.AppStateChanged += (sender, e) => OnChange?.Invoke(e.AppState);
            await AppClient.Start(_navigationManager.BaseUri);
        }

        public async ValueTask DisposeAsync() => await AppClient.Stop();
    }
}
