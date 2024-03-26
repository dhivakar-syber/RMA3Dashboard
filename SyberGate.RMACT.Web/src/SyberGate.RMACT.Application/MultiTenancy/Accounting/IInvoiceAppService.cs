using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.MultiTenancy.Accounting.Dto;

namespace SyberGate.RMACT.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
