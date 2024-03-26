using System.Threading.Tasks;
using SyberGate.RMACT.Sessions.Dto;

namespace SyberGate.RMACT.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
