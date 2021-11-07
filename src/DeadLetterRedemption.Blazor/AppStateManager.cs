using System;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;

namespace DeadLetterRedemption.Blazor
{
    public class AppStateManager : IAsyncDisposable
    {
        public AppClient AppClient { get; }
        
        public event Action<AppState> OnChange;
        
        public bool IsReady => AppClient.IsConnectionEstablished;

        public AppStateManager(AppClient appClient)
        {
            AppClient = appClient;
        }

        public async Task Initialize(string hubBaseUrl, string username)
        {
            AppClient.AppStateChanged += (sender, e) => OnChange?.Invoke(e.AppState);
            await AppClient.Start(hubBaseUrl, username);
        }

        public async ValueTask DisposeAsync() => await AppClient.Stop();
    }
}
