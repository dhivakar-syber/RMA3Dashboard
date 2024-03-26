using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Localization;
using Abp.Localization.Sources;
using SyberGate.RMACT.Masters.Importing.Dto;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using NPOI.SS.UserModel;

namespace SyberGate.RMACT.Masters.Importing
{
  
        public class LeadModelsExcelDataReader : NpoiExcelImporterBase<ImportLeadModelsDto>, ILeadModelsExcelDataReader
    {
            private readonly ILocalizationSource _localizationSource;

            public LeadModelsExcelDataReader(ILocalizationManager localizationManager)
            {
                _localizationSource = localizationManager.GetSource(RMACTConsts.LocalizationSourceName);
            }

            public List<ImportLeadModelsDto> GetLeadModelsFromExcel(byte[] fileBytes)
            {
                return ProcessExcelFile(fileBytes, ProcessExcelRow,"LeadModel");
            }




            private ImportLeadModelsDto ProcessExcelRow(ISheet worksheet, int Column)
            {
                

                

                var exceptionMessage = new StringBuilder();
                var LeadModel = new ImportLeadModelsDto();


                try
                {
                var leadmodelNUllChecking = GetRequiredValueFromRowOrNull(worksheet, 1, Column, "NUllChecking", exceptionMessage);
                 var DescriptionNullChecking= GetRequiredValueFromRowOrNull(worksheet, 2, Column, "NUllChecking", exceptionMessage);
                

				if (leadmodelNUllChecking != null && DescriptionNullChecking !=null)
                    {
                         LeadModel.Name = GetRequiredValueFromRowOrNull(worksheet, 1, Column, nameof(LeadModel.Name), exceptionMessage);
                         LeadModel.Description= GetRequiredValueFromRowOrNull(worksheet, 2, Column, nameof(LeadModel.Description), exceptionMessage);
                       
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (System.Exception exception)
                {
                    LeadModel.Exception = exception.Message;
                }

                return LeadModel;
            }

            private string GetRequiredValueFromRowOrNull(
                ISheet worksheet,
                int row,
                int column,
                string columnName,
                StringBuilder exceptionMessage,
                CellType? cellType = null)
            {
                DataFormatter dataformatter = new DataFormatter();
            string cellValue = dataformatter.FormatCellValue(worksheet.GetRow(row).GetCell(column));

                //if (cellType.HasValue)
                //{
                //    cell.SetCellType(cellType.Value);
                //}

                //var cellValue = cell.StringCellValue;
                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue))
                {
                    return cellValue;
                }

                exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
                return null;
            }

            private decimal GetRequiredNumericFromRowOrNull(
                ISheet worksheet,
                int row,
                int column,
                string columnName,
                StringBuilder exceptionMessage)
            {
                DataFormatter dataformatter = new DataFormatter();
                var cell = worksheet.GetRow(row).GetCell(column);

                //if (cellType.HasValue)
                //{
                //    cell.SetCellType(cellType.Value);
                //}

                var cellValue = cell.NumericCellValue;
                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    return Convert.ToDecimal(cellValue);
                }

                exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
                return 0;
            }

            private string GetOptionalValueFromRowOrNull(ISheet worksheet, int row, int column, StringBuilder exceptionMessage, CellType? cellType = null)
            {
                var cell = worksheet.GetRow(row).GetCell(column);
                if (cell == null)
                {
                    return string.Empty;
                }

                if (cellType != null)
                {
                    cell.SetCellType(cellType.Value);
                }

                var cellValue = worksheet.GetRow(row).GetCell(column).StringCellValue;
                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue))
                {
                    return cellValue;
                }

                return String.Empty;
            }

            private string[] GetAssignedRoleNamesFromRow(ISheet worksheet, int row, int column)
            {
                var cellValue = worksheet.GetRow(row).GetCell(column).StringCellValue;
                if (cellValue == null || string.IsNullOrWhiteSpace(cellValue))
                {
                    return new string[0];
                }

                return cellValue.ToString().Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToArray();
            }

            private string GetLocalizedExceptionMessagePart(string parameter)
            {
                return _localizationSource.GetString("{0}IsInvalid", _localizationSource.GetString(parameter)) + "; ";
            }

            private bool IsRowEmpty(ISheet worksheet, int row)
            {
                var cell = worksheet.GetRow(row)?.Cells.FirstOrDefault();
                return cell == null || string.IsNullOrWhiteSpace(cell.StringCellValue);
            }
        }
   
}
