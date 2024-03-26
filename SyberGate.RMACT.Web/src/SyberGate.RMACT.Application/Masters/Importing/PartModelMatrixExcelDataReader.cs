
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Localization;
using Abp.Localization.Sources;
using SyberGate.RMACT.Masters.Importing.Dto;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using NPOI.SS.UserModel;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Notifications;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Importing
{

    public class PartModelMatrixExcelDataReader : NpoiExcelImporterBase<ImportPartModelMatrixDto>, IPartModelMatrixExcelDataReader
    {

       
        private readonly ILocalizationSource _localizationSource;

        public PartModelMatrixExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(RMACTConsts.LocalizationSourceName);
        }
        

        public List<ImportPartModelMatrixDto> GetPartModelMatrixFromExcel(byte[] fileBytes)
        {
            var startingRow = 3;
            var StartingColumn = 2;
            return ProcessExcelFile(fileBytes, ProcessExcelRow, startingRow, StartingColumn);
        }




        private ImportPartModelMatrixDto ProcessExcelRow(ISheet worksheet, int row,int column)
        {
            var exceptionMessage = new StringBuilder();
            var PartModelmatrix = new ImportPartModelMatrixDto();
            
                try
                {
                    var NullHeaderChecking = GetRequiredValueFromRowOrNull(worksheet, 1, 1, " ", exceptionMessage);


                    if (NullHeaderChecking != null)
                    {
                        PartModelmatrix.PartNumber = GetRequiredValueFromRowOrNull(worksheet, row, 0, nameof(PartModelmatrix.PartNumber), exceptionMessage);
                        PartModelmatrix.Name = GetRequiredValueFromRowOrNull(worksheet, 1, column, nameof(PartModelmatrix.Name), exceptionMessage);
                        PartModelmatrix.Quantity = GetRequiredNumericFromRowOrNull (worksheet, row, column, nameof(PartModelmatrix.Quantity), exceptionMessage);
                        
                }
                    else
                    {
                        return null;
                    }
                }
                catch (System.Exception exception)
                {
                    PartModelmatrix.Exception = exception.Message;
                }

            return PartModelmatrix;
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

        private int GetRequiredNumericFromRowOrNull(
            ISheet worksheet,
            int row,
            int column,
            string columnName,
            StringBuilder exceptionMessage)
        {
            DataFormatter dataformatter = new DataFormatter();
            var cell = worksheet.GetRow(row).GetCell(column);

            if (cell != null)
            {
                var cellValue = cell.NumericCellValue;

                return (int)cellValue;
            }
            else 
            {
                return 0;            
            }
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
