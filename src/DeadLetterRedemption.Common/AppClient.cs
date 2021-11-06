using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace DeadLetterRedemption.Common
{
    public class AppClient
    {
        public const string HubUrl = "/AppHub";

        private readonly string _hubUrl;
        private HubConnection _hubConnection;

        public AppClient(string hubUrl)
        {
            _hubUrl = hubUrl;
        }

        public async Task Start()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .Build();
        }
    }
}
