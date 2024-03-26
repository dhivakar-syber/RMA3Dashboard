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
    public class PartExcelDataReader : NpoiExcelImporterBase<ImportPartDto>, IPartExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public PartExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(RMACTConsts.LocalizationSourceName);
        }

        public List<ImportPartDto> GetPartsFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow, 1 );
        }

        private ImportPartDto ProcessExcelRow(ISheet worksheet, int row)
        {
            row = row + 4;

            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var part = new ImportPartDto();

            try
            {
                part.Buyer = GetRequiredValueFromRowOrNull(worksheet, 2, 2, nameof(part.Supplier), exceptionMessage);
                part.Supplier = GetRequiredValueFromRowOrNull(worksheet, 3, 2, nameof(part.Supplier), exceptionMessage);
                part.PartNo = GetRequiredValueFromRowOrNull(worksheet, row, 0, nameof(part.PartNo), exceptionMessage);
                part.Description = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(part.Description), exceptionMessage);
                part.SubPartNo = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(part.SubPartNo), exceptionMessage);
                part.SubPartDescription = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(part.SubPartNo), exceptionMessage);
                if (!string.IsNullOrWhiteSpace(part.SubPartNo))
                {
                    if (string.IsNullOrWhiteSpace(part.PartNo))
                    {
                        part.PartNo = GetParent(worksheet, row - 1, 0, nameof(part.PartNo), exceptionMessage);
                        part.Description = GetParent(worksheet, row - 1, 1, nameof(part.Description), exceptionMessage);
                    }
                }
                if ( string.IsNullOrWhiteSpace(part.SubPartNo) && string.IsNullOrWhiteSpace(part.PartNo))
                {
                    
                    part.PartNo = GetParent(worksheet, row - 1, 0, nameof(part.PartNo), exceptionMessage);
                    part.Description = GetParent(worksheet, row - 1, 1, nameof(part.Description), exceptionMessage);
                    
                    part.SubPartNo = part.PartNo;
                    part.SubPartDescription = part.SubPartDescription;
                }
                part.RMGroup = GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(part.RMGroup), exceptionMessage);
                part.RMGrade = GetRequiredValueFromRowOrNull(worksheet, row, 5, nameof(part.RMGrade), exceptionMessage);
                part.GrossInputWeight = GetRequiredNumericFromRowOrNull(worksheet, row, 6, nameof(part.GrossInputWeight), exceptionMessage);
                //if (part.GrossInputWeight <= 0 && (!string.IsNullOrWhiteSpace(part.RMGrade)))
                //    part.Exception = "Gross Input Weight should be greater than zero";

                part.CastingForgingWeight = (GetRequiredNumericFromRowOrNull(worksheet, row, 7, nameof(part.CastingForgingWeight), exceptionMessage));

                if (part.CastingForgingWeight == 0 && part.GrossInputWeight > 0)
                    part.CastingForgingWeight = part.GrossInputWeight;

                part.FinishedWeight = (GetRequiredNumericFromRowOrNull(worksheet, row, 8, nameof(part.FinishedWeight), exceptionMessage));

                //if (part.FinishedWeight <= 0 && (!string.IsNullOrWhiteSpace(part.RMGrade)))
                //    part.Exception = "Finished Weight should be greater than zero";

                part.ScrapRecoveryPercent = GetRequiredNumericFromRowOrNull(worksheet, row, 9, nameof(part.ScrapRecoveryPercent), exceptionMessage);
                //if (part.ScrapRecoveryPercent > 0)
                //    part.ScrapRecoveryPercent = (part.ScrapRecoveryPercent ?? 0) / 100;

                part.RMReferenceCost = GetRequiredNumericFromRowOrNull(worksheet, row, 10, nameof(part.RMReferenceCost), exceptionMessage);

                //part.LogisticsCost = GetRequiredNumericFromRowOrNull(worksheet, row, 11, nameof(part.LogisticsCost), exceptionMessage);

                part.RMReference = GetRequiredValueFromRowOrNull(worksheet, row, 11, nameof(part.RMReference), exceptionMessage);


                if (string.IsNullOrWhiteSpace(part.RMGroup) && string.IsNullOrWhiteSpace(part.RMGrade))
                    part.IsParent = true;
                else
                    part.IsParent = false;
            }
            catch (System.Exception exception)
            {
                part.Exception = exception.Message;
            }

            return part;
        }

        private string GetParent(ISheet worksheet, int row, int col, string fieldname, StringBuilder exceptionMessage)
        {
            string partno = GetRequiredValueFromRowOrNull(worksheet, row, col, fieldname, exceptionMessage);
            if ( string.IsNullOrWhiteSpace(partno))
                partno = GetParent(worksheet, row - 1, col, fieldname, exceptionMessage);

            return partno;
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
            var cell = worksheet.GetRow(row).GetCell(column);
            var cellValue = "";

            if (cell.CellType == CellType.Formula)
            {
                cellValue = cell.StringCellValue;
            }
            else
            {
                cellValue = dataformatter.FormatCellValue(worksheet.GetRow(row).GetCell(column));
            }

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
            var cells = worksheet.GetRow(row)?.Cells ;
            return (cells.FirstOrDefault() == null || string.IsNullOrWhiteSpace(cells.FirstOrDefault().StringCellValue)) && string.IsNullOrWhiteSpace(cells[3].StringCellValue) && string.IsNullOrWhiteSpace(cells[4].StringCellValue);
        }
    }
}
