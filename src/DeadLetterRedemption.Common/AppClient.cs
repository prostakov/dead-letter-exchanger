using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadLetterRedemption.Common.Dto;
using DeadLetterRedemption.Common.EventArgs;
using DeadLetterRedemption.Common.Extensions;
using Microsoft.AspNetCore.SignalR.Client;

namespace DeadLetterRedemption.Common
{
    public class AppClient
    {
        public const string HubUrl = "/app-hub";
        
        private HubConnection _hubConnection;
        private readonly HashSet<IDisposable> _hubRegistrations = new();
        
        private bool _isConnectionEstablished;

        public async Task Start(string hubBaseUrl)
        {
            if (string.IsNullOrWhiteSpace(hubBaseUrl))
                throw new ArgumentNullException(nameof(hubBaseUrl));

            if (!_isConnectionEstablished)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubBaseUrl.TrimEnd('/') + HubUrl)
                    //, options => options.AccessTokenProvider = async () => await GetAccessTokenValueAsync()) TODO: Authorization
                    .WithAutomaticReconnect()
                    .Build();

                _hubRegistrations.Add(_hubConnection.OnAppStateChanged(OnAppStateChanged));
                
                await _hubConnection.StartAsync();

                _isConnectionEstablished = true;
            }
        }
        
        public async Task Stop()
        {
            if (_isConnectionEstablished)
            { 
                // Disconnect the client
                await _hubConnection.StopAsync();
                
                // Clear all hub registrations
                if (_hubRegistrations.Count > 0)
                    foreach (var disposable in _hubRegistrations)
                        disposable.Dispose();
                _hubRegistrations.Clear();

                // There is a bug in the mono/SignalR client that does not
                // close connections even after stop/dispose
                // see https://github.com/mono/mono/issues/18628
                // this means the demo won't show "xxx left the chat" since 
                // the connections are left open
                await _hubConnection.DisposeAsync();
                
                _hubConnection = null;
                _isConnectionEstablished = false;
            }
        }

        public bool IsConnectionEstablished => _isConnectionEstablished;

        public event AppStateChangedEventHandler AppStateChanged;
        public delegate void AppStateChangedEventHandler(object sender, AppStateChangedEventArgs e);
        private void OnAppStateChanged(AppState appState) => AppStateChanged?.Invoke(this, new AppStateChangedEventArgs(appState));
    }
}
