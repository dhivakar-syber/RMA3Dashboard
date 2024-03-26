using System.Collections.Generic;
using Abp.Collections.Extensions;
using Abp.Dependency;
using SyberGate.RMACT.Masters.Importing.Dto;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;
using Abp.Timing;

namespace SyberGate.RMACT.Masters.Importing
{
    public class InvalidGlobusDataExporter : NpoiExcelExporterBase, IInvalidGlobusDataExporter, ITransientDependency
    {
        public InvalidGlobusDataExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportGlobusDataDto> partListDtos)
        {
            return CreateExcelPackage(
                "InvalidGlobusDataImportList-" + Clock.Now + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.CreateSheet(L("InvalidGlobusDataImports"));
                    
                    AddHeader(
                        sheet,
                        L("PartNo"),
                        L("Description"),
                        L("Reason")
                    );

                    AddObjects(
                        sheet, 2, partListDtos,
                        _ => _.PartNo,
                        _ => _.Description,
                        _ => _.Exception
                    );

                    for (var i = 0; i < 3; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                });
        }
    }
}
