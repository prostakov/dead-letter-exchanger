using Microsoft.AspNetCore.SignalR;

namespace DeadLetterRedemption.Server.Hub
{
    public class AppHub : Hub<IAppClient>
    {
    }
}
