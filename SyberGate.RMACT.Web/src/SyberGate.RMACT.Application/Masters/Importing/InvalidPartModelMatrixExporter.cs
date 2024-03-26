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
    public class InvalidPartModelMatrixExporter : NpoiExcelExporterBase, IInvalidPartModelMatrixExporter, ITransientDependency
    {
        public InvalidPartModelMatrixExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportPartModelMatrixDto> partModelMatrixlistDtos)
        {
            return CreateExcelPackage(
                "InvalidPartImportList-" + Clock.Now + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.CreateSheet(L("InvalidPartImports"));

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("PartNumber"),
                        L("Quantity"),
                        L("LeadModelID"),
                        L("ImportError")
                        
                    );

                    AddObjects(
                        sheet, 2, partModelMatrixlistDtos,
                        _ => _.Name,
                        _ => _.PartNumber,
                        _ => _.Quantity,
                        _ => _.LeadModelId,
                        _ => _.Exception
                        
                    );

                    for (var i = 0; i < 5; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                });
        }
    }
}
