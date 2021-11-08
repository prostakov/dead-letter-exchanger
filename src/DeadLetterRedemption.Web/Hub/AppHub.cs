using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DeadLetterRedemption.Web.Hub
{
    //[Authorize, RequiredScope(new[] { "app_hub" })] TODO: Authorization
    public class AppHub : Hub<IAppClient>
    {
        private ILogger<AppHub> _logger;

        public AppHub(ILogger<AppHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client #{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            _logger.LogInformation($"Client #{Context.ConnectionId} disconnected. Exception: {e?.Message ?? "none"}");
            await base.OnDisconnectedAsync(e);
        }
    }
}
