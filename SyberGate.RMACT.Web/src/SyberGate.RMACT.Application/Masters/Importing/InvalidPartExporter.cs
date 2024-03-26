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
    public class InvalidPartExporter : NpoiExcelExporterBase, IInvalidPartExporter, ITransientDependency
    {
        public InvalidPartExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportPartDto> partListDtos)
        {
            return CreateExcelPackage(
                "InvalidPartImportList-"+ Clock.Now +".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.CreateSheet(L("InvalidPartImports"));
                    
                    AddHeader(
                        sheet,
                        L("PartNo"),
                        L("Description"),
                        L("RMGrade"),
                        L("ImportError")
                    );

                    AddObjects(
                        sheet, 2, partListDtos,
                        _ => _.PartNo,
                        _ => _.Description,
                        _ => _.RMGrade,
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
