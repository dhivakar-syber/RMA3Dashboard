using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Localization;
using Abp.Localization.Sources;
using SyberGate.RMACT.Masters.Importing.Dto;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using NPOI.SS.UserModel;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SyberGate.RMACT.Masters.Importing
{
    public class GlobusDataExcelDataReader : NpoiExcelImporterBase<ImportGlobusDataDto>, IGlobusDataExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public GlobusDataExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(RMACTConsts.LocalizationSourceName);
        }

        public List<ImportGlobusDataDto> GetGlobusDatasFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow, 1);
        }

        private ImportGlobusDataDto ProcessExcelRow(ISheet worksheet, int row)
        {

            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var part = new ImportGlobusDataDto();

            try
            {
                part.Status = GetRequiredValueFromRowOrNull(worksheet, row, 10, nameof(part.EPU), exceptionMessage);//K

                if (part.Status.ToLower() == "auto confirmed" || part.Status.ToLower() == "released" || part.Status.ToLower() == "confirmed")
                {

                }
                else
                {
                    return null;
                }

                part.PartNo = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(part.PartNo), exceptionMessage);  //B
                part.ES1 = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(part.ES1), exceptionMessage);    //C
                part.ES2 = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(part.ES2), exceptionMessage);    //D
                part.Description = GetRequiredValueFromRowOrNull(worksheet, row, 5, nameof(part.Description), exceptionMessage);    //F
                part.Buyer = GetRequiredValueFromRowOrNull(worksheet, row, 15, nameof(part.Buyer), exceptionMessage);   //p
                part.SupplierCode = GetRequiredValueFromRowOrNull(worksheet, row, 25, nameof(part.SupplierCode), exceptionMessage); //Z
                part.SupplierName = GetRequiredValueFromRowOrNull(worksheet, row, 24, nameof(part.SupplierName), exceptionMessage); //Y
                part.CurrentExwPrice = GetRequiredNumericFromRowOrNull(worksheet, row, 28, nameof(part.CurrentExwPrice), exceptionMessage); //AC
                part.PriceCurrency = GetRequiredValueFromRowOrNull(worksheet, row, 19, nameof(part.PriceCurrency), exceptionMessage);   //T
                part.Uom = GetRequiredValueFromRowOrNull(worksheet, row, 20, nameof(part.Uom), exceptionMessage);   //U
                part.FromDate = GetRequiredValueFromRowOrNull(worksheet, row, 6, nameof(part.FromDate), exceptionMessage);  //G
                part.FromDate = part.FromDate.Substring(0, 4) + "/" + part.FromDate.Substring(4, 2) + "/" + part.FromDate.Substring(6, 2);  
                
                decimal BasePrice = GetRequiredNumericFromRowOrNull(worksheet, row, 28, nameof(part.CurrentExwPrice), exceptionMessage);//AC
                decimal TotalPrice = GetRequiredNumericFromRowOrNull(worksheet, row, 29, nameof(part.PackagingCost), exceptionMessage);//AD

                var packaging164A = GetRequiredNumericFromRowOrNull(worksheet, row, 30, "Packaging Cost", exceptionMessage);    //AE
                var packaging190A = GetRequiredNumericFromRowOrNull(worksheet, row, 32, "Packaging Cost", exceptionMessage);    //AG

                var Logistics184A = GetRequiredNumericFromRowOrNull(worksheet, row, 31, "Logistics Cost", exceptionMessage);    //AF
                //var Logistics184C = GetRequiredNumericFromRowOrNull(worksheet, row, 36, "Logistics Cost", exceptionMessage);


                part.PackagingCost = packaging164A + packaging190A;
                part.LogisticsCost = Logistics184A;

                part.ToDate = GetRequiredValueFromRowOrNull(worksheet, row, 7, nameof(part.ToDate), exceptionMessage);  //H
                part.ToDate = part.ToDate.Substring(0, 4) +  "/" + part.ToDate.Substring(4, 2) + "/" + part.ToDate.Substring(6, 2);

                part.PlantCode = GetRequiredValueFromRowOrNull(worksheet, row, 17, nameof(part.PlantCode), exceptionMessage);   //R
                part.PlantDescription = GetRequiredValueFromRowOrNull(worksheet, row, 16, nameof(part.PlantDescription), exceptionMessage); //Q
                part.ContractNo= GetRequiredValueFromRowOrNull(worksheet, row, 12, nameof(part.ContractNo), exceptionMessage);//M
                part.SOB= GetRequiredValueFromRowOrNull(worksheet, row, 26, nameof(part.SOB), exceptionMessage);    //AA
                part.EPU = GetRequiredValueFromRowOrNull(worksheet, row, 21, nameof(part.EPU), exceptionMessage);   //V
                
            }
            catch (System.Exception exception)
            {
                part.Exception = exception.Message;
            }

            return part;
        }

        private string GetRequiredValueFromRowOrNull(
            ISheet worksheet,
            int row,
            int column,
            string columnName,
            StringBuilder exceptionMessage,
            CellType? cellType = null)
        {

            ICell cell = worksheet.GetRow(row).GetCell(column);

            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case CellType.String:
                        return cell.StringCellValue;

                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            try
                            {
                                return cell.DateCellValue.ToString();
                            }
                            catch (NullReferenceException)
                            {
                                return DateTime.FromOADate(cell.NumericCellValue).ToString();
                            }
                        }
                        else if (cell.CellStyle.DataFormat >= 164 && DateUtil.IsCellDateFormatted(cell))
                        {
                            return cell.DateCellValue.ToString();
                        }
                        else
                        {
                            return cell.NumericCellValue.ToString();
                        }

                    case CellType.Boolean:
                        return cell.BooleanCellValue ? "TRUE" : "FALSE";

                    case CellType.Formula:
                        //if (eval != null)
                        //    return GetFormattedCellValue(eval.EvaluateInCell(cell));
                        //else
                        return cell.CellFormula;

                    case CellType.Error:
                        return FormulaError.ForInt(cell.ErrorCellValue).String;
                }
            }


            exceptionMessage.Append(GetLocalizedExceptionMessageGlobusData(columnName));
            return string.Empty;
        }

        //private string GetRequiredValueFromRowOrNull(
        //    ISheet worksheet,
        //    int row,
        //    int column,
        //    string columnName,
        //    StringBuilder exceptionMessage,
        //    CellType? cellType = null)
        //{
        //    DataFormatter dataformatter = new DataFormatter();
        //    var cellValue = dataformatter.FormatCellValue(worksheet.GetRow(row).GetCell(column));

        //    if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue))
        //    {
        //        return cellValue;
        //    }

        //    exceptionMessage.Append(GetLocalizedExceptionMessageGlobusData(columnName));
        //    return null;
        //}

        private decimal GetRequiredNumericFromRowOrNull(
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
                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    return Convert.ToDecimal(cellValue);
                }

                exceptionMessage.Append(GetLocalizedExceptionMessageGlobusData(columnName));
                
            }
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

        private string GetLocalizedExceptionMessageGlobusData(string parameter)
        {
            return _localizationSource.GetString("{0}IsInvalid", _localizationSource.GetString(parameter)) + "; ";
        }

        private bool IsRowEmpty(ISheet worksheet, int row)
        {
            var cell = worksheet.GetRow(row)?.Cells.FirstOrDefault();

            if (cell.CellType == CellType.String)
                return cell == null || string.IsNullOrWhiteSpace(cell.StringCellValue);
                    
            return cell == null;
                      
        }
    }
}
