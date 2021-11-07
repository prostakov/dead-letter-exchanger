using System;
using System.Threading.Tasks;
using DeadLetterRedemption.Common;
using DeadLetterRedemption.Web.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DeadLetterRedemption.Web.Controllers
{
    [ApiController]
    [Route("/admin")]
    public class AdminController : Controller
    {
        private readonly IHubContext<AppHub> _appHub;
        private static Random Random = new Random();

        public AdminController(IHubContext<AppHub> appHub)
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
            await _appHub.Clients.All.SendAsync(MessageTypes.AppStateChange, appState);
            return Ok();
        }
    }
}
