using System.Threading.Tasks;
using Abp.Dependency;

namespace SyberGate.RMACT.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}