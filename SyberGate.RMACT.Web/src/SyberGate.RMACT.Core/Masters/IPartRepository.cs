using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using SyberGate.RMACT.Masters.Dto;
using SyberGate.RMACT.Tenants.Dashboard.Dto;

namespace SyberGate.RMACT.Masters
{
    public interface IPartRepository : IRepository<Part, int>
    {
        Task<List<GetPriceTrendsp>> GetAllRMTrend(Dto.GetA3DashboardDataInput input);
        Task<List<GetPriceImpactsp>> GetAllRMPriceImpact(Dto.GetA3DashboardDataInput input);
        Task<List<GetPriceImpactsp>> GetAllSubPartRMPriceImpact(Dto.GetA3DashboardDataInput input);
        Task<List<GetRMIndexPriceSP>> GetRMPriceIndexList(GetRMIndexPriceSP input);
        Task<List<Getallpricevalue>> Getallpricevalue(GetTotalRate input);
        Task<List<Getallpricevalue>> Getalltonnagevalue(GetTotalRate input);
        Task<List<Getallpricevalue>> Getalltonnagevaluebygrade(GetTotalRate input);
        Task<List<DepartmentQuarterSummary>> Getallteams(GetDepartment input);
        //Task<List<GetMonthInput>> Getallpriceyearly(Getpriceyearly input);
        //Task<List<GetMonthInput>> Getalltonnageyearly(Getpriceyearly input);
        Task<List<GetBuyerandMonth>> Getrmforcomp(Getrmcomp input);
        Task<List<GetRMPriceTrend>> GetAvobByBuyerandTeam(Dto.GetA3DashboardDataInput input);
        //Task<List<GetMonthInput>> Getallunitrate(Getpriceyearly input);
        Task<List<GetModel>> GetModelForComp(GetAVOBComp input);
        Task<bool> UpdateRMConversionCost(Dto.GetA3DashboardDataInput input);
    }
}
