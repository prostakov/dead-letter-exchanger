using System.Threading.Tasks;

namespace DeadLetterRedemption.Server.Hub
{
    public interface IAppClient
    {
        Task NotifyGeneralCountersChange();
    }
}
