using System.Collections.Generic;
using SyberGate.RMACT.Tenants.Dashboard.Dto;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Masters.Dtos;
using System.Threading.Tasks;

namespace SyberGate.RMACT.Masters.Exporting
{
    public interface IA3ExcelExporter
    {
		FileDto ExportToFile(List<GetRMPriceTrend> priceTrend, List<GetRMPriceImpact> priceImpact,List<LeadModelDto>LeadModeLHeader, List<PartLeadModelMatrixDto> PartMod, List<LeadModelGraphDto> LeadP, int BuyPartNm, string Supplier, string Buyer, string TemplatePath, A3Document A3Doc, decimal RmTotal, decimal processImpacttotal, decimal basermimpacttotal);


	}
}