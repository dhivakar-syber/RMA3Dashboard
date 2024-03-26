using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
