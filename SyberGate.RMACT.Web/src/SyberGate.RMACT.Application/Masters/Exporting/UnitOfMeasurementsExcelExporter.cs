using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class UnitOfMeasurementsExcelExporter : NpoiExcelExporterBase, IUnitOfMeasurementsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UnitOfMeasurementsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUnitOfMeasurementForViewDto> unitOfMeasurements)
        {
            return CreateExcelPackage(
                "UnitOfMeasurements.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.CreateSheet(L("UnitOfMeasurements"));

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("Name")
                        );

                    AddObjects(
                        sheet, 2, unitOfMeasurements,
                        _ => _.UnitOfMeasurement.Code,
                        _ => _.UnitOfMeasurement.Name
                        );

					
					
                });
        }
    }
}
