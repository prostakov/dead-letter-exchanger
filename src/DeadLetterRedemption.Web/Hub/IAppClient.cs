using System.Threading.Tasks;

namespace DeadLetterRedemption.Web.Hub
{
    public interface IAppClient
    {
        Task NotifyGeneralCountersChange();
    }
}
