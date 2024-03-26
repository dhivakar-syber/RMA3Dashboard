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
using SyberGate.RMACT.Masters.Dtos;
using NPOI.SS.Formula.Functions;
using System.Data.Common;
using System.Globalization;

namespace SyberGate.RMACT.Masters.Importing
{

    public class ApprovalUsersExcelDataReader : NpoiExcelImporterBase<ImportApprovalUsersDto>, IApprovalUsersExcelDataReader
    {


        private readonly ILocalizationSource _localizationSource;

        public ApprovalUsersExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(RMACTConsts.LocalizationSourceName);
        }


        
        public List<ImportApprovalUsersDto> GetApprovalUserFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow, 1);
        }



        private ImportApprovalUsersDto ProcessExcelRow(ISheet worksheet, int row)
        {


            row = row + 1;

            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var Approvaluser = new ImportApprovalUsersDto();

            try
            {
                var NullHeaderChecking = GetRequiredValueFromRowOrNull(worksheet, 0, 0, " ", exceptionMessage);


                if (NullHeaderChecking != null)
                {
                    Approvaluser.UserName = GetRequiredValueFromRowOrNull(worksheet, row, 0, nameof(Approvaluser.UserName), exceptionMessage);
                    Approvaluser.Department = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(Approvaluser.Department), exceptionMessage);
                    Approvaluser.Email = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(Approvaluser.Email), exceptionMessage);





                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception exception)
            {
                Approvaluser.Exception = exception.Message;
            }

            return Approvaluser;
        }



















        //private ImportPartBucketDto ProcessExcelRow(ISheet worksheet, int row, int column)
        //{
        //    //if (IsRowEmpty(worksheet, row))
        //    //{
        //    //	return null;
        //    //}

        //    var exceptionMessage = new StringBuilder();
        //    var PartBucket = new ImportPartBucketDto();

        //    try
        //    {
        //        var NullHeaderChecking = GetRequiredValueFromRowOrNull(worksheet, 0, 0, " ", exceptionMessage);


        //        if (NullHeaderChecking != null)
        //        {
        //            PartBucket.RMSpec = GetRequiredValueFromRowOrNull(worksheet, row, 0, nameof(PartBucket.RMSpec), exceptionMessage);
        //            PartBucket.Buyer = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(PartBucket.Buyer), exceptionMessage);
        //            PartBucket.Supplier = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(PartBucket.Supplier), exceptionMessage);
        //            PartBucket.Month = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(PartBucket.Month), exceptionMessage);
        //            PartBucket.Year = GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(PartBucket.Year), exceptionMessage);
        //            PartBucket.Buckets = GetRequiredValueFromRowOrNull(worksheet, 1, column, nameof(PartBucket.Buckets), exceptionMessage);
        //            PartBucket.Value = (decimal)GetRequiredNumericFromRowOrNull(worksheet, row, column, nameof(PartBucket.Value), exceptionMessage);
        //            PartBucket.CreatedOn = DateTime.Now;



        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (System.Exception exception)
        //    {
        //        PartBucket.Exception = exception.Message;
        //    }

        //    return PartBucket;
        //}

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

            if (cell != null)
            {
                var cellValue = cell.NumericCellValue;

                return (decimal)cellValue;
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


