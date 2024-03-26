using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SyberGate.RMACT.DataExporting.Excel.NPOI
{
    public abstract class NpoiExcelImporterBase<TEntity>
    {
        protected List<TEntity> ProcessExcelFile(byte[] fileBytes, Func<ISheet, int, TEntity> processExcelRow)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                var workbook = new XSSFWorkbook(stream);
                for (var i = 0; i < workbook.NumberOfSheets; i++)
                {
                    var entitiesInWorksheet = ProcessWorksheet(workbook.GetSheetAt(i), processExcelRow);
                    entities.AddRange(entitiesInWorksheet);
                }
            }

            return entities;
        }

        protected List<TEntity> ProcessExcelFile(byte[] fileBytes, Func<ISheet, int, TEntity> processExcelRow, int ProcessNoOfSheets)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                var workbook = new XSSFWorkbook(stream);
                for (var i = 0; i < ProcessNoOfSheets; i++)
                {
                    var entitiesInWorksheet = ProcessWorksheet(workbook.GetSheetAt(i), processExcelRow);
                    entities.AddRange(entitiesInWorksheet);
                }
            }

            return entities;
        }

        private List<TEntity> ProcessWorksheet(ISheet worksheet, Func<ISheet, int, TEntity> processExcelRow)
        {
            var entities = new List<TEntity>();

            var rowEnumerator = worksheet.GetRowEnumerator();
            rowEnumerator.Reset();

            var i = 0;
            while (rowEnumerator.MoveNext())
            {
                if (i == 0)
                {
                    //Skip header
                    i++;
                    continue;
                }
                try
                {
                    var entity = processExcelRow(worksheet, i++);
                    if (entity != null)
                    {
                        entities.Add(entity);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return entities;
        }




        //Below Method Created For LEADMODEL
        //ProcessWorksheet(ISheet worksheet, Func<ISheet, int, TEntity> processExcelRow,string LeadModel)
        //private string GetRequiredValueFromRowOrNull(ISheet worksheet,int row, int column,string columnName,StringBuilder exceptionMessage,CellType? cellType = null)


        protected List<TEntity> ProcessExcelFile(byte[] fileBytes, Func<ISheet, int, TEntity> processExcelRow, string LeadModel)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                var workbook = new XSSFWorkbook(stream);
                for (var i = 0; i < workbook.NumberOfSheets; i++)
                {
                    var entitiesInWorksheet = ProcessWorksheet(workbook.GetSheetAt(i), processExcelRow,LeadModel);
                    entities.AddRange(entitiesInWorksheet);
                }
            }

            return entities;
        }


        private List<TEntity> ProcessWorksheet(ISheet worksheet, Func<ISheet, int, TEntity> processExcelRow,string LeadModel)
        {
            var entities = new List<TEntity>();

            
            var exceptionMessage = new StringBuilder();
            
            
            if (!((GetRequiredValueFromRowOrNull(worksheet, 1, 1, "LeadModel", exceptionMessage)) == "Lead Models" && (GetRequiredValueFromRowOrNull(worksheet, 2, 1, "Description", exceptionMessage)) == "Description"))
            {
                return null;
            }
            
            var StartingColumn = 2;
            while ((GetRequiredValueFromRowOrNull(worksheet, 1, StartingColumn, "level", exceptionMessage)) != null)
            {
                
                try
                {
                    var entity = processExcelRow(worksheet, StartingColumn);
                    if (entity != null)
                    {
                        entities.Add(entity);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
				StartingColumn++;
			}
            

			return entities;
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

           
            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue))
            {
                return cellValue;
            }

            
            return null;
        }
		//Below Method Created for PART--MODEL--MATRIX--MASTER AND PAR--BUCKETS
		//protected List<TEntity> ProcessExcelFile(byte[] fileBytes, Func<ISheet, int,int, TEntity> processExcelRow,int StartingRow ,int StartingColumn)
		//ProcessWorksheet(workbook.GetSheetAt(i), processExcelRow,StartingRow,StartingColumn);
		//processExcelRow(worksheet,worksheet, row, column)
		////private string GetRequiredValueFromRowOrNull(ISheet worksheet,int row, int column,string columnName,StringBuilder exceptionMessage,CellType? cellType = null)
		protected List<TEntity> ProcessExcelFile(byte[] fileBytes, Func<ISheet, int,int, TEntity> processExcelRow,int StartingRow ,int StartingColumn)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                var workbook = new XSSFWorkbook(stream);
                for (var i = 0; i < workbook.NumberOfSheets; i++)
                {
                    var entitiesInWorksheet = ProcessWorksheet(workbook.GetSheetAt(i), processExcelRow,StartingRow,StartingColumn);
                    entities.AddRange(entitiesInWorksheet);
                }
            }

            return entities;
        }
        private List<TEntity> ProcessWorksheet(ISheet worksheet, Func<ISheet, int,int, TEntity> processExcelRow, int StartingRow, int StartingColumn)
        {
            var entities = new List<TEntity>();

            var rowEnumerator = worksheet.GetRowEnumerator();
            rowEnumerator.Reset();
            var exceptionMessage = new StringBuilder();

            var endingColumn = StartingColumn;
            
            
            while ((GetRequiredValueFromRowOrNull(worksheet, 1, endingColumn, "NUllChecking", exceptionMessage)) != null)
            {

				endingColumn++;

            }
           
            var row = 0;
            while (rowEnumerator.MoveNext())
            {
					if (row < StartingRow)
					{
						//Skip-Rows
						row++;
                    continue;
                }

                for (int column = StartingColumn; column < endingColumn; column++)
                {
                    try
                    {
                        
                        var entity = processExcelRow(worksheet, row, column);
                        

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                        
                    }
                    catch (Exception)
                    {
                        //ignore
                    }

                }
				row++;

            }

            return entities;
        }

    }
}