using System;
using System.Threading.Tasks;
using DeadLetterRedemption.Common.Dto;
using DeadLetterRedemption.Web.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DeadLetterRedemption.Web.Controllers
{
    [ApiController]
    [Route("/admin")]
    public class AdminController : Controller
    {
        private static readonly Random Random = new();
        private readonly IHubContext<AppHub, IAppClient> _appHub;

        public AdminController(IHubContext<AppHub, IAppClient> appHub)
        {
            _appHub = appHub;
        }

        [HttpPost, Route("trigger")]
        public async Task<IActionResult> TriggerChange([FromBody] string action)
        {
            var appState = new AppState
            {
                DeadLetterCountTotal = Random.Next(1000, 10000),
                InFlightCountTotal = Random.Next(1000, 10000),
                SuccessfulRequeueCountTotal = Random.Next(1000, 10000)
            };
            await _appHub.Clients.All.AppStateChanged(appState);
            return Ok();
        }
    }
}
