
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
    public class InvalidApprovalUsersExporter : NpoiExcelExporterBase, IInvalidApprovalUsersExporter, ITransientDependency
    {
        public InvalidApprovalUsersExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportApprovalUsersDto> approvaluserslistDtos)
        {
            return CreateExcelPackage(
                "InvalidApprovalusersImportList-" + Clock.Now + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.CreateSheet(L("InvalidApprovalUsersImports"));

                    AddHeader(
                        sheet,
                        L("UserName"),
                        L("Department"),
                        L("EmailAddress"),
                         L("Exception")


                    );

                    AddObjects(
                        sheet, 2, approvaluserslistDtos,
                        _ => _.UserName,
                        _ => _.Department,
                        _ => _.Email,
                        _ => _.Exception

                    );

                    for (var i = 0; i < 4; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                });
        }
    }
}

