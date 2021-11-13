using System.Threading.Tasks;
using DeadLetterRedemption.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeadLetterRedemption.Web.Controllers
{
    [ApiController]
    [Route("/admin")]
    public class AdminController : Controller
    {
        private readonly AppStateNotificationService _appStateNotificationService;

        public AdminController(AppStateNotificationService appStateNotificationService)
        {
            _appStateNotificationService = appStateNotificationService;
        }

        [HttpPost, Route("trigger")]
        public async Task<IActionResult> TriggerChange([FromBody] string action)
        {
            await _appStateNotificationService.UpdateState();
            return Ok();
        }
    }
}
