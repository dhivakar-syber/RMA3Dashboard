using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class BaseRMRatesExcelExporter : NpoiExcelExporterBase, IBaseRMRatesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public BaseRMRatesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetBaseRMRateForViewDto> baseRMRates)
        {
            return CreateExcelPackage(
                "BaseRMRates.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.CreateSheet(L("BaseRMRates"));

                    AddHeader(
                        sheet,
                        L("UnitRate"),
                        L("ScrapPercent"),
                        L("Month"),
                        (L("RMGroup")) + L("Name"),
                        (L("UnitOfMeasurement")) + L("Code"),
                        (L("Year")) + L("Name"),
                        (L("Buyer")) + L("Name"),
                        (L("Supplier")) + L("Name")
                        );

                    AddObjects(
                        sheet, 2, baseRMRates,
                        _ => _.BaseRMRate.UnitRate,
                        _ => _.BaseRMRate.ScrapPercent,
                        _ => _.BaseRMRate.Month,
                        _ => _.RMGroupName,
                        _ => _.UnitOfMeasurementCode,
                        _ => _.YearName,
                        _ => _.BuyerName,
                        _ => _.SupplierName
                        );

					
					
                });
        }
    }
}
