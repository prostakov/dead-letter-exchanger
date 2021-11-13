using System;
using System.Threading.Tasks;
using DeadLetterRedemption.Common.Dto;
using DeadLetterRedemption.Web.Hub;
using Microsoft.AspNetCore.SignalR;

namespace DeadLetterRedemption.Web.Services
{
    public class AppStateNotificationService
    {
        private static readonly Random Random = new();
        
        private readonly IHubContext<AppHub, IAppClient> _appHub;

        private AppState _appState;

        public AppStateNotificationService(IHubContext<AppHub, IAppClient> appHub)
        {
            _appHub = appHub;
            _appState = new AppState
            {
                DeadLetterCountTotal = 0, 
                InFlightCountTotal = 0, 
                SuccessfulRequeueCountTotal = 0
            };
        }

        public async Task UpdateState()
        {
            _appState.DeadLetterCountTotal = Random.Next(1000, 100_000_000);
            _appState.InFlightCountTotal = Random.Next(1000, 10000);
            _appState.SuccessfulRequeueCountTotal = Random.Next(1000, 10000);
            
            await _appHub.Clients.All.AppStateChanged(_appState);
        }
    }
}
