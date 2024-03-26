using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace SyberGate.RMACT.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
