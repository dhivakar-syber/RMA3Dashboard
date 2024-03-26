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
    public class InvalidLeadModelsExporter : NpoiExcelExporterBase, IInvalidLeadModelsExporter, ITransientDependency
    {
        public InvalidLeadModelsExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportLeadModelsDto> leadModelistDtos)
        {
            return CreateExcelPackage(
                "InvalidPartImportList-" + Clock.Now + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.CreateSheet(L("InvalidPartImports"));

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Description"),
                        L("ImportError")
                    );

                    AddObjects(
                        sheet, 2, leadModelistDtos,
                        _ => _.Name,
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
