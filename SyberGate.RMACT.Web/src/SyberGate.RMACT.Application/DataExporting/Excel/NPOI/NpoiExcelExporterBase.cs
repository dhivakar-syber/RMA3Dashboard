using System;
using System.Collections.Generic;
using System.IO;
using Abp.AspNetZeroCore.Net;
using Abp.Collections.Extensions;
using Abp.Dependency;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Masters;
using NPOI.SS.Formula.Functions;
using Stripe;
using NPOI.XSSF.Streaming.Values;
using Abp.Extensions;
using NPOI.SS.UserModel.Charts;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel.Charts;
using SyberGate.RMACT.Tenants.Dashboard.Dto;

namespace SyberGate.RMACT.DataExporting.Excel.NPOI
{
    public abstract class NpoiExcelExporterBase : RMACTServiceBase, ITransientDependency
	{
        private readonly ITempFileCacheManager _tempFileCacheManager;

        protected NpoiExcelExporterBase(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        protected FileDto CreateExcelPackage(string fileName, Action<XSSFWorkbook> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var workbook = new XSSFWorkbook();

            creator(workbook);

            Save(workbook, file);

            return file;
        }

        protected FileDto CreateExcelPackageFromFile(string fileName, string inputFile, Action<XSSFWorkbook> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var workbook = new XSSFWorkbook(inputFile);

            creator(workbook);

            Save(workbook, file);

            return file;
        }

        protected void AddHeader(ISheet sheet, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            sheet.CreateRow(0);

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, i, headerTexts[i]);
            }
        }

        protected void AddHeader(ISheet sheet, int columnIndex, string headerText)
        {
            var cell = sheet.GetRow(0).CreateCell(columnIndex);
            cell.SetCellValue(headerText);
            var cellStyle = sheet.Workbook.CreateCellStyle();
            var font = sheet.Workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 12;
            cellStyle.SetFont(font);
            cell.CellStyle = cellStyle;
        }
        
        protected void AddObjects<T>(ISheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
			if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 1; i <= items.Count; i++)
            {
                var row = sheet.CreateRow(startRowIndex + i - 1);

                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    var cell = row.CreateCell(j);
                    var value = propertySelectors[j](items[i - 1]);
                    if (value != null)
                    {
						if (value.GetType() == typeof(double) || value.GetType() == typeof(Decimal))
							cell.SetCellValue(double.Parse(value.ToString()));

						if (value.GetType() == typeof(int))
							cell.SetCellValue(Int32.Parse(value.ToString()));

						if (value.GetType() == typeof(string))
							cell.SetCellValue(value.ToString());
					}
                    else
                    {
                        cell.SetCellValue("");
                    }
                }
            }
        }

        protected void AddObjectsTemplate<T>(ISheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 1; i <= items.Count; i++)
            {
                IRow row = sheet.GetRow(startRowIndex + i - 1);
                IRow newrow = row.CopyRowTo(startRowIndex + i);
                var l = 0;

                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    //if (j == 1)
                    //    sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, j, j + 1));

                    //var cell = row.CreateCell(j);
                    var cell = row.GetCell(j + l);

                    var fcell = row.GetCell(11);
                    fcell.SetCellFormula("IF(ISERROR($I" + j + "-$J" + j + "), , $I" +j + "-$J" + j + ")");

                    var fcell1 = row.GetCell(13);
                    fcell1.SetCellFormula("IF(ISERROR($L" + j + "*$K" + j + "), , $L" + j + "*$K" + j + ")");

                    var value = propertySelectors[j](items[i - 1]);
                    if (value != null)
                    {
                        if (value.GetType() == typeof(double) || value.GetType() == typeof(Decimal))
                            cell.SetCellValue(double.Parse(value.ToString()));

                        if (value.GetType() == typeof(int))
                            cell.SetCellValue(Int32.Parse(value.ToString()));

                        if (value.GetType() == typeof(string))
                            cell.SetCellValue(value.ToString());
                    }
                    if (cell.IsMergedCell)
                        l += 1;
                }
            }
        }

        protected void AddObjects<T>(ISheet sheet, int startRowIndex, int startColIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
			if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 1; i <= items.Count; i++)
            {
                //var row = sheet. CreateRow(startRowIndex + i - 1);
                IRow row = sheet.GetRow(startRowIndex + i - 1);
                //IRow newrow = sheet.CopyRow(startRowIndex + i - 1, startRowIndex + i - 1 + 1);
                IRow newrow = row.CopyRowTo(startRowIndex + i);
                var l = 0;
                for (var j = 0; j < propertySelectors.Length + l; j++)
                {
                    var cell = row.GetCell(j + startColIndex + l);
                    //var cell = row.CreateCell(j + startColIndex);
                    var value = propertySelectors[j](items[i - 1]);
                    if (value != null && cell != null && !cell.IsMergedCell)
                    {
                        if (value.GetType() == typeof(double) || value.GetType() == typeof(Decimal))
                            cell.SetCellValue(double.Parse(value.ToString()));

                        if (value.GetType() == typeof(int))
                            cell.SetCellValue(Int32.Parse(value.ToString()));

                        if (value.GetType() == typeof(string))
                            cell.SetCellValue(value.ToString());
                    }
                    if (cell.IsMergedCell)
                        l += 1;
                }
            }

        }
        

        protected void AddLeadModelObjects<T>(ISheet sheet, int startRowIndex, int startColIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty())
            {

                return;
            }
            var i = 0;
            

			IRow LeadModelrow = sheet.GetRow(startRowIndex);
			IRow LeadDespnRow = sheet.GetRow(startRowIndex+1);
			IRow mergeLeadDespnRow = sheet.GetRow(startRowIndex+2);

            sheet.AddMergedRegion(new CellRangeAddress(LeadDespnRow.RowNum, mergeLeadDespnRow.RowNum,2 , 2));

            foreach (var leadmodel in (List<LeadModelDto>)(items))
            {

				

				var LeadModelcell = LeadModelrow.GetCell(i+3);
				LeadModelcell.CellStyle= LeadModelrow.GetCell(2).CellStyle;
				var LeadDespnCell= LeadDespnRow.GetCell(i+3);
				LeadDespnCell.CellStyle = LeadModelrow.GetCell(2).CellStyle;

				sheet.AddMergedRegion(new CellRangeAddress(LeadDespnRow.RowNum, mergeLeadDespnRow.RowNum, i + 3, i + 3));
                
                if (items.Count != i + 1)

                    {
						ICell newcolumn = LeadModelrow.CreateCell(i + 4);
						newcolumn.CellStyle = LeadModelrow.GetCell(2).CellStyle;

						ICell newLeadDespncolumn = LeadDespnRow.CreateCell(i + 4);
						newLeadDespncolumn.CellStyle = LeadModelrow.GetCell(2).CellStyle;
						newLeadDespncolumn.CellStyle.WrapText = true;

						ICell mergenewLeadDespncolumn = mergeLeadDespnRow.CreateCell(i + 4);
						mergenewLeadDespncolumn.CellStyle = LeadModelrow.GetCell(2).CellStyle;
					}
					

					var LeadModelvalue = leadmodel.Name;
					var LeadDespnValue = leadmodel.Description;

					if (LeadModelvalue.GetType() == typeof(string))
						LeadModelcell.SetCellValue(LeadModelvalue);
					LeadDespnCell.SetCellValue(LeadDespnValue);

				


				
                i++;
            }

		}

        
		protected void AddPartModelObjects<T>(ISheet sheet, int startRowIndex, int startColIndex, IList<T> items,int BuyPartNum, params Func<T, object>[] propertySelectors)
		{
            

			if (items.IsNullOrEmpty())
			{

				return;
			}

			var i = 0;
            var j = 1;
            var k = 1;

			IRow row = sheet.GetRow(startRowIndex);
            var PartModelMatrixColor = sheet.GetRow(12).GetCell(21);

            var PartNumber = "";

			foreach (var partLeadMod in (List<PartLeadModelMatrixDto>)(items))
            {

                if (i == 0)
                {
                    var PartNumberCell= row.GetCell(1);
                    var partDescription=row.GetCell(2); 
					var Qcell = row.GetCell(i+3);
                    


                    ICell newcolumn = row.CreateCell(i+4);
                    newcolumn.CellStyle = PartNumberCell.CellStyle;

                    var value = partLeadMod.Quantity;


                    if (value.GetType() == typeof(int))
                        if (value == 0 || value==null)
                        {
                            Qcell.SetCellValue("");
                            Qcell.CellStyle = PartNumberCell.CellStyle;
                            


                        }
                        else
                        {
							Qcell.SetCellValue(Int32.Parse(value.ToString()));
                            Qcell.CellStyle = PartModelMatrixColor.CellStyle;                           

                        }


				
                        if (partLeadMod.PartNo.GetType() == typeof(string))

                            PartNumberCell.SetCellValue((partLeadMod.PartNo).ToString());

                        if (partLeadMod.PartDespn.GetType() == typeof(string))

                            partDescription.SetCellValue((partLeadMod.PartDespn).ToString());

                        PartNumber = partLeadMod.PartNo;

                    }
                else if (i > 0)

                {
					

					if (PartNumber == partLeadMod.PartNo|| partLeadMod.PartNo == null)
                    {
                        

                        var Qcell = row.GetCell(j+3);
                        var partModelColor = row.GetCell(1);


                        if (sheet.GetRow(startRowIndex-1).GetCell(j + 4)!=null)
                        {
							ICell newcolumn = row.CreateCell(j + 4);
                        }
                        

                        var value = partLeadMod.Quantity;

                        if (value.GetType() == typeof(int))
                            if(value==0 || value==null)
                            {
                                Qcell.SetCellValue("");
                                Qcell.CellStyle = partModelColor.CellStyle;

                            }

                            else
                            {
                                Qcell.SetCellValue(Int32.Parse(value.ToString()));
                                Qcell.CellStyle = PartModelMatrixColor.CellStyle;
                            }
                        

                        j++;
                        
                    }

                    else
                    {   
						IRow newrow = row.CopyRowTo(startRowIndex + k);
                        row=newrow;

                         j = 0;
						var PartNumberCell = row.GetCell(1);
						var partDescription = row.GetCell(2);
						var Qcell = row.GetCell(j + 3);
                        


                        var value = partLeadMod.Quantity;

						if (value.GetType() == typeof(int))
								if (value == 0 || value==null)
                            {
                                Qcell.SetCellValue("");
                                Qcell.CellStyle = PartNumberCell.CellStyle;

                            }

                            else
                            {
                                Qcell.SetCellValue(Int32.Parse(value.ToString()));
                                Qcell.CellStyle = PartModelMatrixColor.CellStyle;

                            }	
                                

                        if (partLeadMod.PartNo.GetType() == typeof(string))
                                PartNumberCell.SetCellValue((partLeadMod.PartNo).ToString());

                            if (partLeadMod.PartDespn.GetType() == typeof(string))
                                partDescription.SetCellValue((partLeadMod.PartDespn).ToString());

                            PartNumber = partLeadMod.PartNo;

                        j++;
                        k++;
						
					}
                }

				i++;
			}

		}
       
		protected void AddObjectsNew<T>(ISheet sheet, int startRowIndex, int startColIndex, IList<T> items, List<GetRMPriceImpact> priceImpact, decimal RmImpacttotal, decimal processImpacttotal, decimal basermimpacttotal, params Func<T ,object>[] propertySelectors)
        {
            IFont font = sheet.Workbook.CreateFont();
            font.IsBold = true;
            ICellStyle style = sheet.Workbook.CreateCellStyle();
            
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }
            

			for (var i = 1; i <= items.Count; i++)
            {
                //var row = sheet. CreateRow(startRowIndex + i - 1);
                IRow row = sheet.GetRow(startRowIndex + i - 1);
                //IRow newrow = sheet.CopyRow(startRowIndex + i - 1, startRowIndex + i - 1 + 1);
                IRow newrow = row.CopyRowTo(startRowIndex + i);
                var l = 0;
                var modified= false;
                
                //var fcell = row.GetCell(31 + 18+1);
                //fcell.SetCellFormula("AM" + (startRowIndex + i).ToString() + "*AW" + (startRowIndex + i).ToString() + "*AX" + (startRowIndex + i).ToString() + "/10^6");

                var processImpactcell = row.GetCell(32 + 19+1);
                //processImpactcell.SetCellFormula("(SUM(U" + (startRowIndex + i).ToString() + ": AB" + (startRowIndex + i).ToString() + ")" + "*AV" + (startRowIndex + i) + "*AW" + (startRowIndex + i) + "/10^6" + " - SUM(K" + (startRowIndex + i).ToString() + ": R" + (startRowIndex + i).ToString() + ")" + "*AV" + (startRowIndex + i) + "*AW" + (startRowIndex + i) + "/10^6" + ")");
                processImpactcell.SetCellFormula("(SUM(W" + (startRowIndex + i).ToString() + ": AC" + (startRowIndex + i).ToString() + ")" + "*AW" + (startRowIndex + i).ToString() + "*0.01" + "*AX" + (startRowIndex + i).ToString() + "*AD" + (startRowIndex + i).ToString() + "/10^6" + " - SUM(M" + (startRowIndex + i).ToString() + ": S" + (startRowIndex + i).ToString() + ")" + "*AW" + (startRowIndex + i).ToString() + "*0.01" + "*AX" + (startRowIndex + i).ToString() + "*AD" + (startRowIndex + i).ToString() + "/10^6" + ")");

                var RMImpactcell = row.GetCell(32 + 20 + 1);
                //RMImpactcell.SetCellFormula("AN"+ (startRowIndex + i) +"*AV"+(startRowIndex + i) +"*AW"+(startRowIndex + i) + "/10^6" + "-(SUM(U" + (startRowIndex + i).ToString() + ": AB" + (startRowIndex + i).ToString() + ")" + "*AV" + (startRowIndex + i) + "*AW" + (startRowIndex + i) + "/10^6" + " - SUM(K" + (startRowIndex + i).ToString() + ": R" + (startRowIndex + i).ToString() + ")" + "*AV" + (startRowIndex + i) + "*AW" + (startRowIndex + i) + "/10^6" + ")");
                // RMImpactcell.SetCellFormula("(AN" + (startRowIndex + i).ToString() + "*AV" + (startRowIndex + i).ToString() + "*AW" + (startRowIndex + i).ToString() + "/10^6)" + "-((SUM(U" + (startRowIndex + i).ToString() + ": AB" + (startRowIndex + i).ToString() + ")" + "*AV" + (startRowIndex + i).ToString() + "*AW" + (startRowIndex + i).ToString() + "/10^6)" + " - (SUM(K" + (startRowIndex + i).ToString() + ": R" + (startRowIndex + i).ToString() + ")" + "*AV" + (startRowIndex + i).ToString() + "*AW" + (startRowIndex + i).ToString() + "/10^6)" + ")");
                RMImpactcell.SetCellFormula("AY" + (startRowIndex + i) + "-BA" + (startRowIndex + i));

                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    if (j == 2)
                        sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, j + startColIndex, j + 1 + startColIndex));

                    var cell = row.GetCell(j + startColIndex + l);
                    
                    //var cell = row.CreateCell(j + startColIndex);
                    var value = propertySelectors[j](items[i - 1]);
                    if (value != null)
                    {
                        if (value.GetType() == typeof(double) || value.GetType() == typeof(Decimal))
                            cell.SetCellValue(double.Parse(value.ToString()));

                        if (value.GetType() == typeof(int))
                            cell.SetCellValue(Int32.Parse(value.ToString()));

                        if (value.GetType() == typeof(string))
                            cell.SetCellValue(value.ToString());
                    }
                    //if (int.Parse((string)value) == 111111)
                    //{
                    //    cell.SetCellValue("");
                    //}
                    if (cell.IsMergedCell)
                        l += 1;

                    var parent = priceImpact[i - 1].IsParent;

                    if (parent == true)
                    {
                        style.CloneStyleFrom(cell.CellStyle);
                        style.SetFont(font);
                        cell.CellStyle = style;
                    }
                }
            }

            IRow frowd1 = sheet.GetRow(startRowIndex + items.Count + 2);
            IRow cavob= sheet.GetRow(6);
            IRow ravob = sheet.GetRow(7);
            IRow savings= sheet.GetRow(8);

            int intAVOBStart = startRowIndex + 1;
            int intAVOBEnd = startRowIndex + items.Count;

            var fcell1 = frowd1.GetCell(26+9+9+1+1);
            //fcell1.SetCellFormula("SUBTOTAL(9,AU" + startRowIndex + ":AU" + (startRowIndex + items.Count + 1) + ")");
            fcell1.SetCellFormula("SUMIF(A" + intAVOBStart + ":A"+ intAVOBEnd + ", \"<>*.*\", AU"+ intAVOBStart + ": AU"+ intAVOBEnd + ")");
            


            var cvob2 = cavob.GetCell(7);
            //cvob2.SetCellFormula("SUBTOTAL(9,AU" + startRowIndex + ":AU" + (startRowIndex + items.Count + 1) + ")");
            cvob2.SetCellFormula("SUMIF(A" + intAVOBStart + ":A" + intAVOBEnd + ", \"<>*.*\", AU" + intAVOBStart + ": AU" + intAVOBEnd + ")");

            var fcell2 = frowd1.GetCell(27+9+9+1+1);
            //fcell2.SetCellFormula("SUBTOTAL(9,AV" + startRowIndex + ":AV" + (startRowIndex + items.Count + 1) + ")");
            fcell2.SetCellFormula("SUMIF(A" + intAVOBStart + ":A" + intAVOBEnd + ", \"<>*.*\", AV" + intAVOBStart + ": AV" + intAVOBEnd + ")");

            var rvob2= ravob.GetCell(7);
            //rvob2.SetCellFormula("SUBTOTAL(9,AV" + startRowIndex + ":AV" + (startRowIndex + items.Count + 1) + ")");
            rvob2.SetCellFormula("SUMIF(A" + intAVOBStart + ":A" + intAVOBEnd + ", \"<>*.*\", AV" + intAVOBStart + ": AV" + intAVOBEnd + ")");

            var fcell3 = frowd1.GetCell(31+9+9+1);
			//fcell3.SetCellFormula("SUBTOTAL(9,AX" + startRowIndex + ":AX" + (startRowIndex + items.Count + 1) + ")");
			//fcell3.SetCellValue((double)RmImpacttotal);
			fcell3.SetCellFormula("SUMIF(A" + intAVOBStart + ":A" + intAVOBEnd + ", \"<>*.*\", AY" + intAVOBStart + ": AY" + intAVOBEnd + ")");

			var fcell4= frowd1.GetCell(31 + 9 + 9+2+1);
			//fcell4.SetCellFormula("SUBTOTAL(9,BA" + startRowIndex + ":BA" + (startRowIndex + items.Count + 1) + ")");
			//fcell4.SetCellValue((double)processImpacttotal);
			fcell4.SetCellFormula("SUMIF(A" + intAVOBStart + ":A" + intAVOBEnd + ", \"<>*.*\", BA" + intAVOBStart + ": BA" + intAVOBEnd + ")");

			var fcell5 = frowd1.GetCell(31 + 9 + 9 + 3 + 1);
			//fcell5.SetCellFormula("SUBTOTAL(9,BB" + startRowIndex + ":BB" + (startRowIndex + items.Count + 1) + ")");
			//fcell5.SetCellValue((double)basermimpacttotal);
			fcell5.SetCellFormula("SUMIF(A" + intAVOBStart + ":A" + intAVOBEnd + ", \"<>*.*\", BB" + intAVOBStart + ": BB" + intAVOBEnd + ")");

			var savings2 = savings.GetCell(7);
            //savings2.SetCellFormula("(SUBTOTAL(9,AV" + startRowIndex + ":AV" + (startRowIndex + items.Count + 1) + "))-(SUBTOTAL(9,AU" + startRowIndex + ":AU" + (startRowIndex + items.Count + 1) + "))");
            savings2.SetCellFormula("H8-H7");


            //var frowx = sheet.GetRow(8);
            //var fcell4 = frowx.GetCell(3);
            //fcell4.SetCellFormula($"Text(" + String.Format("Ex-Change Rate (1€ = H{0} INR)", 9) + ")");

            //var frowx1 = sheet.GetRow(6);
            //var fcell5 = frowx1.GetCell(7);
            //fcell5.SetCellFormula("AJ"+ (startRowIndex + items.Count + 3) + "/H9");

            //var frowx2 = sheet.GetRow(7);
            //var fcell6 = frowx2.GetCell(7);
            //fcell6.SetCellFormula("AK" + (startRowIndex + items.Count + 3) + "/H9");


            IRow frowd3 = sheet.GetRow(startRowIndex + items.Count + 2);
            sheet.RemoveRow(frowd3);

            IRow frowd2 = sheet.GetRow(startRowIndex + items.Count + 1);
            sheet.RemoveRow(frowd2);

            //IRow frowd4 = sheet.GetRow(startRowIndex - 6);
            //sheet.RemoveRow(frowd4);

            //IRow frowd5 = sheet.GetRow(startRowIndex - 7);
            //sheet.RemoveRow(frowd5);

        }

		protected void AddLeadModelGraphObjects<T>(ISheet sheet, int startRowIndex, int startColIndex, IList<T> items, params Func<T, object>[] propertySelectors)
		{


			if (items.IsNullOrEmpty())
			{

				return;
			}
			var i = 0;
            char ch = 'D';


			IRow LeadModelrow = sheet.GetRow(startRowIndex);
			IRow CurrentRM = sheet.GetRow(startRowIndex + 1);
			IRow RevisedRM = sheet.GetRow(startRowIndex + 2);
			IRow DeltaINR = sheet.GetRow(startRowIndex + 3);

			foreach (var leadmodel in (List<LeadModelGraphDto>)(items))
			{

                var LeadModelcell = LeadModelrow.GetCell(i + 2);
				LeadModelcell.CellStyle = LeadModelrow.GetCell(1).CellStyle;
                LeadModelcell.CellStyle.Alignment = HorizontalAlignment.Center;
                var CurrentRMcell= CurrentRM.GetCell(i + 2);
				var RevisedRMcell= RevisedRM.GetCell(i + 2);
                var DeltaINRcell=DeltaINR.GetCell(i + 2);
               



				if (items.Count != i + 1)

				{
					ICell newLeadModelcolumn = LeadModelrow.CreateCell(i + 3);
					newLeadModelcolumn.CellStyle = LeadModelrow.GetCell(1).CellStyle;
                    ICell newCurrentRMcolumn = CurrentRM.CreateCell(i + 3);
                    newCurrentRMcolumn.CellStyle=CurrentRM.GetCell(2).CellStyle;
                    ICell newRevisedRMcolumn = RevisedRM.CreateCell(i + 3);
                    newRevisedRMcolumn.CellStyle= RevisedRM.GetCell(2).CellStyle;
                    ICell newDeltaINRColumn = DeltaINR.CreateCell(i + 3);
                    newDeltaINRColumn.CellStyle= DeltaINR.GetCell(2).CellStyle;

					
                }
				var LeadModelvalue = leadmodel.LeadModelName;
                if (LeadModelvalue.GetType() == typeof(string))
                    LeadModelcell.SetCellValue(LeadModelvalue);

                var CurrentRMValue = leadmodel.CurrentRM;
                if (CurrentRMValue.GetType() == typeof(decimal))
                    //CurrentRMcell.SetCellValue(CurrentRMValue.ToString().Substring(0, CurrentRMValue.ToString().Length - 4));

					CurrentRMcell.SetCellValue((double)decimal.Round(CurrentRMValue, 2));

              

                var RevisedRMValue = leadmodel.RevisedRM;
                if (RevisedRMValue.GetType() == typeof(decimal))
                    //CurrentRMcell.SetCellValue(CurrentRMValue.ToString().Substring(0, CurrentRMValue.ToString().Length - 4));
                    RevisedRMcell.SetCellValue((double)decimal.Round(RevisedRMValue,2));
                

                var DeltaINRvalue = leadmodel.RevisedRM - leadmodel.CurrentRM;
                if (DeltaINRvalue.GetType() == typeof(decimal))
                    DeltaINRcell.SetCellValue((double)decimal.Round(DeltaINRvalue,2));




                i++;
                ch++;
			}

		}

		protected void Save(XSSFWorkbook excelPackage, FileDto file)
        {
            using (var stream = new MemoryStream())
            {
                excelPackage.Write(stream);
                _tempFileCacheManager.SetFile(file.FileToken, stream.ToArray());
            }
        }

        protected void SetCellDataFormat(ICell cell, string dataFormat)
        {
            if (cell == null)
            {
                return;
            }

            var dateStyle = cell.Sheet.Workbook.CreateCellStyle();
            var format = cell.Sheet.Workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat(dataFormat);
            cell.CellStyle = dateStyle;
            if (DateTime.TryParse(cell.StringCellValue, out var datetime))
            {
                cell.SetCellValue(datetime);
            }
        }
    }
}
