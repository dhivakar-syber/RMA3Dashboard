using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;
using Microsoft.AspNetCore.Routing.Template;
using SyberGate.RMACT.Tenants.Dashboard.Dto;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class PartBucketsExcelExporter : NpoiExcelExporterBase, IPartBucketsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PartBucketsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetRMPriceTrend> partBuckets, string supplier, string buyer, string SmonthName, string SyearName, string RmonthName, string RyearName, string templatePath)
        {
            return CreateExcelPackageFromFile(
                "PartBuckets.xlsx",
                templatePath,
                excelPackage =>
                {
                    
                    var sheet = excelPackage.GetSheetAt(0);

                    AddObjects(
                        sheet, 2, partBuckets,
                        _ => _.RMGrade,
                        _ => buyer,
                        _ => supplier,
                        _ => SmonthName,
                        _ => SyearName,
                        _ => _.BaseRMRate,
                        _ => _.RMSurchargeGradeDiff,
                        _ => _.SecondaryProcessing,
                        _ => _.SurfaceProtection,
                        _ => _.Thickness,
                        _ => _.CuttingCost,
                        _ => _.MOQVolume,
                        _ => _.Transport,
                        _ => _.Others
                        );

                });
        }
    }
}
