using System.Threading.Tasks;
using DeadLetterRedemption.Common.Dto;

namespace DeadLetterRedemption.Web.Hub
{
    public interface IAppClient
    {
        Task AppStateChanged(AppState appState);
    }
}
