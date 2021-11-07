using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using Microsoft.AspNetCore.SignalR;

namespace DeadLetterRedemption.Web.Hub
{
    //[Authorize, RequiredScope(new[] { "app_hub" })] TODO: Authorization
    //public class AppHub : Hub<IAppClient>
    public class AppHub : Microsoft.AspNetCore.SignalR.Hub
    {
        /// <summary>
        /// connectionId-to-username lookup
        /// </summary>
        /// <remarks>
        /// Needs to be static as the chat is created dynamically a lot
        /// </remarks>
        private static readonly Dictionary<string, string> _userLookup = new Dictionary<string, string>();

        private static AppState _appState = new();

        private static Random Random = new Random();

        /// <summary>
        /// Send a message to all clients
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync(MessageTypes.Receive, username, message);
            await NotifyAppStateChanged();
        }

        /// <summary>
        /// Register username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task Register(string username)
        {
            var currentId = Context.ConnectionId;
            if (!_userLookup.ContainsKey(currentId))
            {
                // maintain a lookup of connectionId-to-username
                _userLookup.Add(currentId, username);
                // re-use existing message for now
                await Clients.AllExcept(currentId).SendAsync(MessageTypes.Receive, username, $"{username} joined the chat");
            }
        }

        /// <summary>
        /// Log connection
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Log disconnection
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            // try to get connection
            string id = Context.ConnectionId;
            if (!_userLookup.TryGetValue(id, out string username))
                username = "[unknown]";

            _userLookup.Remove(id);
            await Clients.AllExcept(Context.ConnectionId).SendAsync(MessageTypes.Receive, username, $"{username} has left the chat");
            await base.OnDisconnectedAsync(e);
        }

        private async Task NotifyAppStateChanged()
        {
            _appState = new AppState
            {
                DeadLetterCountTotal = Random.Next(1000, 10000),
                InFlightCountTotal = Random.Next(1000, 10000),
                SuccessfulRequeueCountTotal = Random.Next(1000, 10000)
            };
            await Clients.All.SendAsync(MessageTypes.AppStateChange, _appState);
        }
    }
}
