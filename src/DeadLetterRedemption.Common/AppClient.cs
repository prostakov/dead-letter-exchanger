using System;
using System.Threading.Tasks;
using DeadLetterRedemption.Common.EventArgs;
using Microsoft.AspNetCore.SignalR.Client;

namespace DeadLetterRedemption.Common
{
    public class AppClient
    {
        public const string HubUrl = "/app-hub";

        private string _username;
        private HubConnection _hubConnection;
        private bool _isConnectionEstablished = false;

        public async Task Start(string hubBaseUrl, string username)
        {
            if (string.IsNullOrWhiteSpace(hubBaseUrl))
                throw new ArgumentNullException(nameof(hubBaseUrl));
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            
            _username = username;
            
            if (!_isConnectionEstablished)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubBaseUrl.TrimEnd('/') + HubUrl)
                    .Build();
                Console.WriteLine("ChatClient: calling Start()");

                _hubConnection.On<string, string>(MessageTypes.Receive, (username, message) => 
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message)));
                _hubConnection.On<AppState>(MessageTypes.AppStateChange, appState => 
                    AppStateChanged?.Invoke(this, new AppStateChangedEventArgs(appState)));
                
                await _hubConnection.StartAsync();

                Console.WriteLine("ChatClient: Start returned");
                _isConnectionEstablished = true;
                
                await _hubConnection.SendAsync(MessageTypes.Register, _username);
            }
        }
        
        public async Task Stop()
        {
            if (_isConnectionEstablished)
            {
                // disconnect the client
                await _hubConnection.StopAsync();
                
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
        
        public event MessageReceivedEventHandler MessageReceived;
        public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
        
        public event AppStateChangedEventHandler AppStateChanged;
        public delegate void AppStateChangedEventHandler(object sender, AppStateChangedEventArgs e);
        
        public async Task Send(string message)
        {
            if (!_isConnectionEstablished)
                throw new InvalidOperationException("Client not started");
            
            await _hubConnection.SendAsync(MessageTypes.Send, _username, message);
        }
    }
}
