using System.Collections.Generic;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Tenants.Dashboard.Dto;

namespace SyberGate.RMACT.Masters.Exporting
{
    public interface IPartBucketsExcelExporter
    {
        FileDto ExportToFile(List<GetRMPriceTrend> partBuckets, string supplier, string buyer, string SmonthName, string SyearName, string RmonthName, string RyearName, string templatePath);
    }
}