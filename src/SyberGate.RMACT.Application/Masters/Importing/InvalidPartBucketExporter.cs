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
	public class InvalidPartBucketExporter : NpoiExcelExporterBase, IInvalidPartBucketExporter, ITransientDependency
	{
		public InvalidPartBucketExporter(ITempFileCacheManager tempFileCacheManager)
			: base(tempFileCacheManager)
		{
		}

		public FileDto ExportToFile(List<ImportPartBucketDto> partBucketlistDtos)
		{
			return CreateExcelPackage(
				"InvalidPartImportList-" + Clock.Now + ".xlsx",
				excelPackage =>
				{
					var sheet = excelPackage.CreateSheet(L("InvalidPartImports"));

					AddHeader(
						sheet,
						L("PartNumber"),
						L("Buckets"),
						L("Value"),
						L("Buyer"),
						L("Supplier"),
						L("ImportError")


					);

					AddObjects(
						sheet, 2, partBucketlistDtos,
						
						_ => _.RMSpec,
						_ => _.Buckets,
						_ => _.Value,
						_ => _.Buyer,
						_=> _.Supplier,
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

