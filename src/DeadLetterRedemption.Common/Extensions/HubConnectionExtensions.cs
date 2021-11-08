using System;
using DeadLetterRedemption.Common.Dto;
using Microsoft.AspNetCore.SignalR.Client;

namespace DeadLetterRedemption.Common.Extensions
{
    public static class HubConnectionExtensions
    {
        public static IDisposable OnAppStateChanged(this HubConnection connection, Action<AppState> handler) => 
            connection.On("AppStateChanged", handler);
    }
}
