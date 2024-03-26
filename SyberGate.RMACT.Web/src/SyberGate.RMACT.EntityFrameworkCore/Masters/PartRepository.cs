using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Data;
using Abp.EntityFrameworkCore;
using SyberGate.RMACT.Masters.Dto;
using SyberGate.RMACT.EntityFrameworkCore;
using SyberGate.RMACT.EntityFrameworkCore.Repositories;
using SyberGate.RMACT;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Uow;
using SyberGate.RMACT.Tenants.Dashboard.Dto;

namespace SyberGate.RMACT.Masters
{
    public class PartRepository : RMACTRepositoryBase<Part, int>, IPartRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PartRepository(IDbContextProvider<RMACTDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider, IUnitOfWorkManager unitOfWorkManager)
           : base(dbContextProvider)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _transactionProvider = transactionProvider;
        }

        public async Task<List<GetPriceTrendsp>> GetAllRMTrend(Dto.GetA3DashboardDataInput input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("GetAllPartByTrend", CommandType.StoredProcedure,
                  new SqlParameter("@SupplierId", (object)input.Supplier ?? DBNull.Value),
                  new SqlParameter("@BuyerId", (object)input.Buyer ?? DBNull.Value),
                  new SqlParameter("@Period", (object)input.Period ?? DBNull.Value),
                  new SqlParameter("@IsGenerateA3", (object)input.IsGenerateA3 ?? DBNull.Value),
                  new SqlParameter("@A3Id", (object)input.A3Id ?? DBNull.Value),
                  new SqlParameter("@SpecId", (object)input.Spec ?? DBNull.Value),
                  new SqlParameter("@GradeId", (object)input.Grade ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<GetPriceTrendsp>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add (new GetPriceTrendsp()   
                            {
                                id = dataReader["id"] == DBNull.Value ? 0 : Int32.Parse(dataReader["id"].ToString()),
                                RMGroupId = dataReader["RMGroupId"] == DBNull.Value ? 0 : Int32.Parse(dataReader["RMGroupId"].ToString()),
                                RMGrade = dataReader["RMGrade"] == DBNull.Value ? "" : dataReader["RMGrade"].ToString(),
                                Uom = dataReader["UOM"] == DBNull.Value ? "" : dataReader["UOM"].ToString(),
                                SetteledMY = dataReader["SetteledMY"] == DBNull.Value ? "" : dataReader["SetteledMY"].ToString(),
                                SetteledUR = dataReader["SRate"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["SRate"].ToString()),
                                RevisedMY = dataReader["RevisedMY"] == DBNull.Value ? "" : dataReader["RevisedMY"].ToString(),
                                RevisedUR = dataReader["RRate"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["RRate"].ToString()),
                                BaseRMPOC = dataReader["RMpchage"] == DBNull.Value ? "" : dataReader["RMpchage"].ToString() + '%',
                                ScrapSetteled = dataReader["SScrap"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["SScrap"].ToString()),
                                ScrapRevised = dataReader["RScrap"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["RScrap"].ToString()),
                                ScrapPOC = dataReader["SPchange"] == DBNull.Value ? "" : dataReader["SPchange"].ToString() + '%',
                                RevApproved = dataReader["RevApproved"] == DBNull.Value ? false : Boolean.Parse(dataReader["RevApproved"].ToString()),
                                SetApproved = dataReader["SetApproved"] == DBNull.Value ? false : Boolean.Parse(dataReader["SetApproved"].ToString()),
                                SetId = dataReader["SetId"] == DBNull.Value ? 0 : Int32.Parse(dataReader["SetId"].ToString()),
                                RMGradeId = dataReader["RMGradeId"] == DBNull.Value ? 0 : Int32.Parse(dataReader["RMGradeId"].ToString()),
                                UOMId = dataReader["UOMId"] == DBNull.Value ? 0 : Int32.Parse(dataReader["UOMId"].ToString()),
                                RevIndexName = dataReader["RevIndexName"] == DBNull.Value ? "" : dataReader["RevIndexName"].ToString(),
                                RevIndexValue = dataReader["RevIndexValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["RevIndexValue"].ToString()),
                                RevFromPeriod = dataReader["RevFromPeriod"] == DBNull.Value ? "" : dataReader["RevFromPeriod"].ToString(),
                                RevToPeriod = dataReader["RevToPeriod"] == DBNull.Value ? "" : dataReader["RevToPeriod"].ToString(),
                                SetIndexName = dataReader["SetIndexName"] == DBNull.Value ? "" : dataReader["SetIndexName"].ToString(),
                                SetIndexValue = dataReader["SetIndexValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["SetIndexValue"].ToString()),
                                SetFromPeriod = dataReader["SetFromPeriod"] == DBNull.Value ? "" : dataReader["SetFromPeriod"].ToString(),
                                SetToPeriod = dataReader["SetToPeriod"] == DBNull.Value ? "" : dataReader["SetToPeriod"].ToString(),
                                MixtureGrade = dataReader["MixtureGrade"] == DBNull.Value ? "" : dataReader["MixtureGrade"].ToString(),
                                SetWRatio = dataReader["swRatio"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["swRatio"].ToString()),
                                SetLRatio = dataReader["slRatio"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["slRatio"].ToString()),
                                RevWRatio = dataReader["rwRatio"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["rwRatio"].ToString()),
                                RevLRatio = dataReader["rlRatio"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["rlRatio"].ToString()),
                                ParentGrp = dataReader["ParentGroup"] == DBNull.Value ? "" : dataReader["ParentGroup"].ToString()
                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        public async Task<List<Getallpricevalue>> Getallpricevalue(GetTotalRate input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("Getallpricevaluetest1", CommandType.StoredProcedure,
                  new SqlParameter("@SupplierId", (object)input.SupplierId ?? DBNull.Value),
                  new SqlParameter("@BuyerId", (object)input.BuyerId ?? DBNull.Value),
                  //new SqlParameter("@GroupId", (object)input.RMGroupId ?? DBNull.Value),
                  //new SqlParameter("@month", (object)input.Month ?? "April"),
                  //new SqlParameter("@year", (object)input.Year ?? "2023"))

                  new SqlParameter("@month", (object)input.Month ?? DateTime.Now.ToString("MMMM")),
                  new SqlParameter("@year", (object)input.Year ?? DateTime.Now.ToString("yyyy")))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<Getallpricevalue>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new Getallpricevalue()
                            {
                                ParentGroup = dataReader["parentgroupname"] == DBNull.Value ? "" : dataReader["parentgroupname"].ToString(),
                                GroupId = dataReader["GroupId"] == DBNull.Value ? 0 : Int32.Parse(dataReader["GroupId"].ToString()),
                                GrossInputWeightAverage =  Convert.ToDecimal(dataReader["GrossInputWeight"] == DBNull.Value ? "0" : dataReader["GrossInputWeight"].ToString()),
                                EPUAverage= Convert.ToDecimal(dataReader["EPU"] == DBNull.Value ? "0" : dataReader["EPU"].ToString()),
                                UnitRateAverage= Convert.ToDecimal(dataReader["UnitRate"] == DBNull.Value ? "0" : dataReader["UnitRate"].ToString()),
                                SOBAverage= Convert.ToDecimal(dataReader["SOB"] == DBNull.Value ? "0" : dataReader["SOB"].ToString()),
                                TotalAverage= Convert.ToDecimal(dataReader["Total"] == DBNull.Value ? "0" : dataReader["Total"].ToString())

                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        public async Task<List<Getallpricevalue>> Getalltonnagevalue(GetTotalRate input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("Getalltonnagevaluetest1", CommandType.StoredProcedure,
                  new SqlParameter("@SupplierId", (object)input.SupplierId ?? DBNull.Value),
                  new SqlParameter("@BuyerId", (object)input.BuyerId ?? DBNull.Value),
                  new SqlParameter("@Department", (object)input.Department ?? DBNull.Value),
                  //new SqlParameter("@year", (object)input.Year ?? "2023"))

                  //new SqlParameter("@month", (object)input.Month ?? DateTime.Now.ToString("MMMM")),
                  new SqlParameter("@year", (object)input.Year ?? DateTime.Now.ToString("yyyy")))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<Getallpricevalue>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new Getallpricevalue()
                            {
                                ParentGroup = dataReader["parentgroupname"] == DBNull.Value ? "" : dataReader["parentgroupname"].ToString(),
                                GroupId = dataReader["GroupId"] == DBNull.Value ? 0 : Int32.Parse(dataReader["GroupId"].ToString()),
                                GrossInputWeightAverage = Convert.ToDecimal(dataReader["GrossInputWeight"] == DBNull.Value ? "0" : dataReader["GrossInputWeight"].ToString()),
                                EPUAverage = Convert.ToDecimal(dataReader["EPU"] == DBNull.Value ? "0" : dataReader["EPU"].ToString()),
                                SOBAverage = Convert.ToDecimal(dataReader["SOB"] == DBNull.Value ? "0" : dataReader["SOB"].ToString()),
                                TotalTon = Convert.ToDecimal(dataReader["TotalTonnage"] == DBNull.Value ? "0" : dataReader["TotalTonnage"].ToString()),
                                TotalValue = Convert.ToDecimal(dataReader["TotalValue"] == DBNull.Value ? "0" : dataReader["TotalValue"].ToString())

                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }
        public async Task<List<Getallpricevalue>> Getalltonnagevaluebygrade(GetTotalRate input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("Getalltonnagevaluebygradetest2", CommandType.StoredProcedure,
                  new SqlParameter("@SupplierId", (object)input.SupplierId ?? DBNull.Value),
                  new SqlParameter("@BuyerId", (object)input.BuyerId ?? DBNull.Value),
                  new SqlParameter("@Department", (object)input.Department ?? DBNull.Value),
                  new SqlParameter("@gradeid", (object)input.RMGradeId ?? DBNull.Value),
                  //new SqlParameter("@month", (object)input.Month ?? DateTime.Now.ToString("MMMM")),
                  new SqlParameter("@year", (object)input.Year ?? DateTime.Now.ToString("yyyy")))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<Getallpricevalue>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new Getallpricevalue()
                            {
                                Spec = dataReader["SpecName"] == DBNull.Value ? "" : dataReader["SpecName"].ToString(),
                                SpecId = dataReader["RMGradeId"] == DBNull.Value ? 0 : Int32.Parse(dataReader["RMGradeId"].ToString()),
                                GrossInputWeightAverage = Convert.ToDecimal(dataReader["GrossInputWeight"] == DBNull.Value ? "0" : dataReader["GrossInputWeight"].ToString()),
                                EPUAverage = Convert.ToDecimal(dataReader["EPU"] == DBNull.Value ? "0" : dataReader["EPU"].ToString()),
                                SOBAverage = Convert.ToDecimal(dataReader["SOB"] == DBNull.Value ? "0" : dataReader["SOB"].ToString()),
                                TotalTonByGrade = Convert.ToDecimal(dataReader["TotalTonnage"] == DBNull.Value ? "0" : dataReader["TotalTonnage"].ToString()),
                                TotalValueByGrade = Convert.ToDecimal(dataReader["TotalValue"] == DBNull.Value ? "0" : dataReader["TotalValue"].ToString())

                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        public async Task<List<DepartmentQuarterSummary>> Getallteams(GetDepartment input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("Department", CommandType.StoredProcedure,
                  new SqlParameter("@YourBuyerID", (object)input.BuyerId ?? DBNull.Value),
                  new SqlParameter("@YourDepartment", (object)input.Department ?? DBNull.Value),
                  new SqlParameter("@Year", (object)input.Year ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<DepartmentQuarterSummary>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new DepartmentQuarterSummary()
                            {
                                Department = dataReader["Department"] == DBNull.Value ? "0" : dataReader["Department"].ToString(),
                                Q1_Total = Convert.ToInt32(dataReader["Q1Total"] == DBNull.Value ? "0" : dataReader["Q1Total"].ToString()),
                                Q1_Approved = Convert.ToInt32(dataReader["Q1Approved"] == DBNull.Value ? "0" : dataReader["Q1Approved"].ToString()),
                                Q1_Pending = Convert.ToInt32(dataReader["Q1Pending"] == DBNull.Value ? "0" : dataReader["Q1Pending"].ToString()),
                                Q2_Total = Convert.ToInt32(dataReader["Q2Total"] == DBNull.Value ? "0" : dataReader["Q2Total"].ToString()),
                                Q2_Approved = Convert.ToInt32(dataReader["Q2Approved"] == DBNull.Value ? "0" : dataReader["Q2Approved"].ToString()),
                                Q2_Pending = Convert.ToInt32(dataReader["Q2Pending"] == DBNull.Value ? "0" : dataReader["Q2Pending"].ToString()),
                                Q3_Total = Convert.ToInt32(dataReader["Q3Total"] == DBNull.Value ? "0" : dataReader["Q3Total"].ToString()),
                                Q3_Approved = Convert.ToInt32(dataReader["Q3Approved"] == DBNull.Value ? "0" : dataReader["Q3Approved"].ToString()),
                                Q3_Pending = Convert.ToInt32(dataReader["Q3Pending"] == DBNull.Value ? "0" : dataReader["Q3Pending"].ToString()),
                                Q4_Total = Convert.ToInt32(dataReader["Q4Total"] == DBNull.Value ? "0" : dataReader["Q4Total"].ToString()),
                                Q4_Approved = Convert.ToInt32(dataReader["Q4Approved"] == DBNull.Value ? "0" : dataReader["Q4Approved"].ToString()),
                                Q4_Pending = Convert.ToInt32(dataReader["Q4Pending"] == DBNull.Value ? "0" : dataReader["Q4Pending"].ToString())

                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        //public async Task<List<GetMonthInput>> Getallpriceyearly(Getpriceyearly input)
        //{
        //    await EnsureConnectionOpenAsync();

        //    using (var command = CreateCommand("Getallpricebymonth", CommandType.StoredProcedure,
        //          new SqlParameter("@groupid", (object)input.RMGroupId ?? DBNull.Value),
        //          new SqlParameter("@year", (object)input.Year ?? DBNull.Value))
        //      )
        //    {
        //        using (var dataReader = await command.ExecuteReaderAsync())
        //        {
        //            var result = new List<GetMonthInput>();

        //            while (dataReader.Read())
        //            {
        //                try
        //                {
        //                    result.Add(new GetMonthInput()
        //                    {
        //                        January = Convert.ToDecimal(dataReader["January"] == DBNull.Value ? "0" : dataReader["January"].ToString()),
        //                        February = Convert.ToDecimal(dataReader["February"] == DBNull.Value ? "0" : dataReader["February"].ToString()),
        //                        March = Convert.ToDecimal(dataReader["March"] == DBNull.Value ? "0" : dataReader["March"].ToString()),
        //                        April = Convert.ToDecimal(dataReader["April"] == DBNull.Value ? "0" : dataReader["April"].ToString()),
        //                        May = Convert.ToDecimal(dataReader["May"] == DBNull.Value ? "0" : dataReader["May"].ToString()),
        //                        June = Convert.ToDecimal(dataReader["June"] == DBNull.Value ? "0" : dataReader["June"].ToString()),
        //                        July = Convert.ToDecimal(dataReader["July"] == DBNull.Value ? "0" : dataReader["July"].ToString()),
        //                        August = Convert.ToDecimal(dataReader["August"] == DBNull.Value ? "0" : dataReader["August"].ToString()),
        //                        September = Convert.ToDecimal(dataReader["September"] == DBNull.Value ? "0" : dataReader["September"].ToString()),
        //                        October = Convert.ToDecimal(dataReader["October"] == DBNull.Value ? "0" : dataReader["October"].ToString()),
        //                        November = Convert.ToDecimal(dataReader["November"] == DBNull.Value ? "0" : dataReader["November"].ToString()),
        //                        December = Convert.ToDecimal(dataReader["December"] == DBNull.Value ? "0" : dataReader["December"].ToString()),

        //                    });
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw new Exception("Error while import", ex);
        //                    return null;
        //                }
        //            }
        //            if (!dataReader.IsClosed)
        //                dataReader.Close();
        //            return result;
        //        }
        //    }
        //}

        //public async Task<List<GetMonthInput>> Getallunitrate(Getpriceyearly input)
        //{
        //    await EnsureConnectionOpenAsync();

        //    using (var command = CreateCommand("Getallunitratebymonth", CommandType.StoredProcedure,
        //          new SqlParameter("@groupid", (object)input.RMGroupId ?? DBNull.Value),
        //          new SqlParameter("@year", (object)input.Year ?? DBNull.Value))
        //      )
        //    {
        //        using (var dataReader = await command.ExecuteReaderAsync())
        //        {
        //            var result = new List<GetMonthInput>();

        //            while (dataReader.Read())
        //            {
        //                try
        //                {
        //                    result.Add(new GetMonthInput()
        //                    {
        //                        January = Convert.ToDecimal(dataReader["January"] == DBNull.Value ? "0" : dataReader["January"].ToString()),
        //                        February = Convert.ToDecimal(dataReader["February"] == DBNull.Value ? "0" : dataReader["February"].ToString()),
        //                        March = Convert.ToDecimal(dataReader["March"] == DBNull.Value ? "0" : dataReader["March"].ToString()),
        //                        April = Convert.ToDecimal(dataReader["April"] == DBNull.Value ? "0" : dataReader["April"].ToString()),
        //                        May = Convert.ToDecimal(dataReader["May"] == DBNull.Value ? "0" : dataReader["May"].ToString()),
        //                        June = Convert.ToDecimal(dataReader["June"] == DBNull.Value ? "0" : dataReader["June"].ToString()),
        //                        July = Convert.ToDecimal(dataReader["July"] == DBNull.Value ? "0" : dataReader["July"].ToString()),
        //                        August = Convert.ToDecimal(dataReader["August"] == DBNull.Value ? "0" : dataReader["August"].ToString()),
        //                        September = Convert.ToDecimal(dataReader["September"] == DBNull.Value ? "0" : dataReader["September"].ToString()),
        //                        October = Convert.ToDecimal(dataReader["October"] == DBNull.Value ? "0" : dataReader["October"].ToString()),
        //                        November = Convert.ToDecimal(dataReader["November"] == DBNull.Value ? "0" : dataReader["November"].ToString()),
        //                        December = Convert.ToDecimal(dataReader["December"] == DBNull.Value ? "0" : dataReader["December"].ToString()),

        //                    });
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw new Exception("Error while import", ex);
        //                    return null;
        //                }
        //            }
        //            if (!dataReader.IsClosed)
        //                dataReader.Close();
        //            return result;
        //        }
        //    }
        //}

        //public async Task<List<GetMonthInput>> Getalltonnageyearly(Getpriceyearly input)
        //{
        //    await EnsureConnectionOpenAsync();

        //    using (var command = CreateCommand("Getalltonnagebymonth", CommandType.StoredProcedure,
        //          new SqlParameter("@groupid", (object)input.RMGroupId ?? DBNull.Value),
        //          new SqlParameter("@year", (object)input.Year ?? DBNull.Value))
        //      )
        //    {
        //        using (var dataReader = await command.ExecuteReaderAsync())
        //        {
        //            var result = new List<GetMonthInput>();

        //            while (dataReader.Read())
        //            {
        //                try
        //                {
        //                    result.Add(new GetMonthInput()
        //                    {
        //                        January = Convert.ToDecimal(dataReader["January"] == DBNull.Value ? "0" : dataReader["January"].ToString()),
        //                        February = Convert.ToDecimal(dataReader["February"] == DBNull.Value ? "0" : dataReader["February"].ToString()),
        //                        March = Convert.ToDecimal(dataReader["March"] == DBNull.Value ? "0" : dataReader["March"].ToString()),
        //                        April = Convert.ToDecimal(dataReader["April"] == DBNull.Value ? "0" : dataReader["April"].ToString()),
        //                        May = Convert.ToDecimal(dataReader["May"] == DBNull.Value ? "0" : dataReader["May"].ToString()),
        //                        June = Convert.ToDecimal(dataReader["June"] == DBNull.Value ? "0" : dataReader["June"].ToString()),
        //                        July = Convert.ToDecimal(dataReader["July"] == DBNull.Value ? "0" : dataReader["July"].ToString()),
        //                        August = Convert.ToDecimal(dataReader["August"] == DBNull.Value ? "0" : dataReader["August"].ToString()),
        //                        September = Convert.ToDecimal(dataReader["September"] == DBNull.Value ? "0" : dataReader["September"].ToString()),
        //                        October = Convert.ToDecimal(dataReader["October"] == DBNull.Value ? "0" : dataReader["October"].ToString()),
        //                        November = Convert.ToDecimal(dataReader["November"] == DBNull.Value ? "0" : dataReader["November"].ToString()),
        //                        December = Convert.ToDecimal(dataReader["December"] == DBNull.Value ? "0" : dataReader["December"].ToString()),

        //                    });
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw new Exception("Error while import", ex);
        //                    return null;
        //                }
        //            }
        //            if (!dataReader.IsClosed)
        //                dataReader.Close();
        //            return result;
        //        }
        //    }
        //}

        public async Task<List<GetBuyerandMonth>> Getrmforcomp(Getrmcomp input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("Getallunitratebymonthfinaltest", CommandType.StoredProcedure,
                  new SqlParameter("@BuyerID", (object)input.BuyerId ?? DBNull.Value),
                  new SqlParameter("@Department", (object)input.Department ?? DBNull.Value),
                  new SqlParameter("@groupid", (object)input.RMGroupId ?? DBNull.Value),
                  new SqlParameter("@specid", (object)input.RMSpecId ?? DBNull.Value),
                  new SqlParameter("@gradeid", (object)input.RMGradeId ?? DBNull.Value),
                  new SqlParameter("@year", (object)input.Year ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<GetBuyerandMonth>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new GetBuyerandMonth()
                            {
                                Team= dataReader["Team"] == DBNull.Value ? "" : dataReader["Team"].ToString(),
                                BuyerName = dataReader["BuyerName"] == DBNull.Value ? "" : dataReader["BuyerName"].ToString(),
                                Suppliername = dataReader["suppliername"] == DBNull.Value ? "" : dataReader["suppliername"].ToString(),
                                Spec = dataReader["spec"] == DBNull.Value ? "" : dataReader["spec"].ToString(),
                                WeightedAverageEPU = Convert.ToDecimal(dataReader["avg"] == DBNull.Value ? "0" : dataReader["avg"].ToString()),
                                UnitRateAverage = Convert.ToDecimal(dataReader["Unirate_avg"] == DBNull.Value ? "0" : dataReader["Unirate_avg"].ToString()),
                                January = Convert.ToDecimal(dataReader["January"] == DBNull.Value ? "0" : dataReader["January"].ToString()),
                                February = Convert.ToDecimal(dataReader["February"] == DBNull.Value ? "0" : dataReader["February"].ToString()),
                                March = Convert.ToDecimal(dataReader["March"] == DBNull.Value ? "0" : dataReader["March"].ToString()),
                                April = Convert.ToDecimal(dataReader["April"] == DBNull.Value ? "0" : dataReader["April"].ToString()),
                                May = Convert.ToDecimal(dataReader["May"] == DBNull.Value ? "0" : dataReader["May"].ToString()),
                                June = Convert.ToDecimal(dataReader["June"] == DBNull.Value ? "0" : dataReader["June"].ToString()),
                                July = Convert.ToDecimal(dataReader["July"] == DBNull.Value ? "0" : dataReader["July"].ToString()),
                                August = Convert.ToDecimal(dataReader["August"] == DBNull.Value ? "0" : dataReader["August"].ToString()),
                                September = Convert.ToDecimal(dataReader["September"] == DBNull.Value ? "0" : dataReader["September"].ToString()),
                                October = Convert.ToDecimal(dataReader["October"] == DBNull.Value ? "0" : dataReader["October"].ToString()),
                                November = Convert.ToDecimal(dataReader["November"] == DBNull.Value ? "0" : dataReader["November"].ToString()),
                                December = Convert.ToDecimal(dataReader["December"] == DBNull.Value ? "0" : dataReader["December"].ToString()),

                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }
        public async Task<List<GetRMPriceTrend>> GetAvobByBuyerandTeam(Dto.GetA3DashboardDataInput input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("mgmtinfo", CommandType.StoredProcedure,
                  new SqlParameter("@Department", (object)input.Department ?? DBNull.Value),
                  new SqlParameter("@SupplierId", (object)input.Supplier ?? DBNull.Value),
                  new SqlParameter("@year", (object)input.Year ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<GetRMPriceTrend>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new GetRMPriceTrend()
                            {
                                Department= dataReader["Department"] == DBNull.Value ? "" : dataReader["Department"].ToString(),
                                Buyer = dataReader["Name"] == DBNull.Value ? "" : dataReader["Name"].ToString(),
                                Supplier = dataReader["SupplierName"] == DBNull.Value ? "" : dataReader["SupplierName"].ToString(),
                                SupplierId = dataReader["SupplierId"] == DBNull.Value ? "" : dataReader["SupplierId"].ToString(),
                                SupplierCode = dataReader["SupplierCode"] == DBNull.Value ? "" : dataReader["SupplierCode"].ToString(),
                                TotalParts = (int)Convert.ToDecimal(dataReader["TotalParts"] == DBNull.Value ? "0" : dataReader["TotalParts"].ToString()),
                                TotalAVOBBySupplier = Convert.ToDecimal(dataReader["TotalAVOBBySupplier"] == DBNull.Value ? "0" : dataReader["TotalAVOBBySupplier"].ToString()),

                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        public async Task<List<GetModel>> GetModelForComp(GetAVOBComp input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("AVOBbypartmodelmatrix", CommandType.StoredProcedure,
                  new SqlParameter("@Plant", (object)input.Plant ?? DBNull.Value),
                  new SqlParameter("@month", (object)input.Month ?? DBNull.Value),
                  new SqlParameter("@year", (object)input.Year ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<GetModel>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new GetModel()
                            {
                                AVOB_1015R = Convert.ToDecimal(dataReader["1015R AVOB"] == DBNull.Value ? "0" : dataReader["1015R AVOB"].ToString()),
                                AVOB_1215R = Convert.ToDecimal(dataReader["1215R AVOB"] == DBNull.Value ? "0" : dataReader["1215R AVOB"].ToString()),
                                AVOB_1415R = Convert.ToDecimal(dataReader["1415R AVOB"] == DBNull.Value ? "0" : dataReader["1415R AVOB"].ToString()),
                                AVOB_1217C = Convert.ToDecimal(dataReader["1217C AVOB"] == DBNull.Value ? "0" : dataReader["1217C AVOB"].ToString()),
                                AVOB_1617R = Convert.ToDecimal(dataReader["1617R AVOB"] == DBNull.Value ? "0" : dataReader["1617R AVOB"].ToString()),
                                AVOB_2823R = Convert.ToDecimal(dataReader["2823R AVOB"] == DBNull.Value ? "0" : dataReader["2823R AVOB"].ToString()),
                                AVOB_3523R = Convert.ToDecimal(dataReader["3523R AVOB"] == DBNull.Value ? "0" : dataReader["3523R AVOB"].ToString()),
                                AVOB_1923C = Convert.ToDecimal(dataReader["1923C AVOB"] == DBNull.Value ? "0" : dataReader["1923C AVOB"].ToString()),
                                AVOB_2823C = Convert.ToDecimal(dataReader["2823C AVOB"] == DBNull.Value ? "0" : dataReader["2823C AVOB"].ToString()),
                                AVOB_2828C = Convert.ToDecimal(dataReader["2828C AVOB"] == DBNull.Value ? "0" : dataReader["2828C AVOB"].ToString()),
                                AVOB_3528C = Convert.ToDecimal(dataReader["3528C AVOB"] == DBNull.Value ? "0" : dataReader["3528C AVOB"].ToString()),
                                AVOB_5428TS = Convert.ToDecimal(dataReader["5428TS AVOB"] == DBNull.Value ? "0" : dataReader["5428TS AVOB"].ToString()),
                                AVOB_5528T = Convert.ToDecimal(dataReader["5528T AVOB"] == DBNull.Value ? "0" : dataReader["5528T AVOB"].ToString()),
                                AVOB_4228R = Convert.ToDecimal(dataReader["4228R AVOB"] == DBNull.Value ? "0" : dataReader["4228R AVOB"].ToString()),
                                AVOB_3528CM = Convert.ToDecimal(dataReader["3528CM AVOB"] == DBNull.Value ? "0" : dataReader["3528CM AVOB"].ToString()),

                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }
        public async Task<bool> UpdateRMConversionCost(Dto.GetA3DashboardDataInput input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("UpdateCostConversion", CommandType.StoredProcedure,
                  new SqlParameter("@SupplierId", (object)input.Supplier ?? DBNull.Value),
                  new SqlParameter("@BuyerId", (object)input.Buyer ?? DBNull.Value),
                  new SqlParameter("@Period", (object)input.Period ?? DBNull.Value),
                  new SqlParameter("@IsGenerateA3", (object)input.IsGenerateA3 ?? DBNull.Value),
                  new SqlParameter("@A3Id", (object)input.A3Id ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    bool result = false;

                    while (dataReader.Read())
                    {
                        try
                        {
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while Cost Update", ex);
                            return false;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        public async Task<List<GetPriceImpactsp>> GetAllRMPriceImpact(Dto.GetA3DashboardDataInput input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("GetAllPartByImpact", CommandType.StoredProcedure,
                  new SqlParameter("@SupplierId", (object)input.Supplier ?? DBNull.Value),
                  new SqlParameter("@BuyerId", (object)input.Buyer ?? DBNull.Value),
                  new SqlParameter("@Period", (object)input.Period ?? DBNull.Value),
                  new SqlParameter("@IsGenerateA3", (object)input.IsGenerateA3 ?? DBNull.Value),
                  new SqlParameter("@A3Id", (object)input.A3Id ?? DBNull.Value),
                  new SqlParameter("@SpecId", (object)input.Spec ?? DBNull.Value),
                  new SqlParameter("@GradeId", (object)input.Grade ?? DBNull.Value),
                  new SqlParameter("@Plant", (object)input.Plant ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<GetPriceImpactsp>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new GetPriceImpactsp()
                            {
                                PartNo = (dataReader["IsSubMixture"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsSubMixture"].ToString())) == true ? "" : dataReader["PartNo"].ToString(),
                                Description = (dataReader["IsSubMixture"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsSubMixture"].ToString())) == true ? "" : dataReader["Description"].ToString(),
                                ES1 = dataReader["ES1"] == DBNull.Value ? "" : dataReader["ES1"].ToString(),
                                ES2 = dataReader["ES2"] == DBNull.Value ? "" : dataReader["ES2"].ToString(),
                                RawMaterialGroup = dataReader["RawMaterialgroup"] == DBNull.Value ? "" : dataReader["RawMaterialgroup"].ToString(),
                                RawMaterialGrade = dataReader["RMGrade"] == DBNull.Value ? "" : dataReader["RMGrade"].ToString(),
                                GrossInputWeight = dataReader["GrossInputWeight"]== DBNull.Value ? 0 : Convert.ToDecimal(dataReader["GrossInputWeight"].ToString()),
                                CastingForgingWeight = dataReader["CastingForgingWeight"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["CastingForgingWeight"].ToString()),
                                FinishedWeight = dataReader["FinishedWeight"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["FinishedWeight"].ToString()),
                                ScrapRecovery = Convert.ToDecimal(dataReader["ScrapRecovery"].ToString()),
                                ScrapRecoveryPercent = Convert.ToDouble(dataReader["ScrapRecoveryPercent"].ToString()),
                                ScrapWeight = Convert.ToDecimal(dataReader["ScrapWeight"].ToString()),
                                CurrentRMCost = Convert.ToDecimal(dataReader["CurrentRMCost"] == DBNull.Value ? "0" : dataReader["CurrentRMCost"].ToString()),
                                RevisedRMCost = Convert.ToDecimal(dataReader["RevisedRMCost"] == DBNull.Value ? "0" : dataReader["RevisedRMCost"].ToString()),
                                OtherCost = (dataReader["IsSubMixture"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsSubMixture"].ToString())) == true ? 0 : Convert.ToDecimal(dataReader["OtherCost"].ToString()),
                                CurrentExwPrice = Convert.ToDecimal(dataReader["CurrentExwPrice"].ToString()),
                                RevisedExwPrice = Convert.ToDecimal(dataReader["RevisedExwPrice"].ToString()),
                                ExwPriceChangeInCost = Convert.ToDecimal(dataReader["ExwPriceChangeInCost"].ToString()),
                                ExwPriceChangeInPer = Convert.ToDecimal(dataReader["ExwPriceChangeInPer"].ToString()),
                                PackagingCost = Convert.ToDecimal(dataReader["PackagingCost"].ToString()),
                                LogisticsCost = Convert.ToDecimal(dataReader["LogisticsCost"].ToString()),
                                CurrentFCAPrice = Convert.ToDecimal(dataReader["CurrentFCAPrice"].ToString()),
                                RevisedFCAPrice = Convert.ToDecimal(dataReader["RevisedFCAPrice"].ToString()),
                                CurrentAVOB = Convert.ToDecimal(dataReader["CurrentAVOB"].ToString()),
                                RevisedAVOB = Convert.ToDecimal(dataReader["RevisedAVOB"].ToString()),
                                PlantCode = dataReader["PlantCode"].ToString(),
                                SOB = Convert.ToDecimal(dataReader["SOB"].ToString()),
                                GlobusEPU = Convert.ToDecimal(dataReader["GlobusEPU"].ToString()),
                                RMImpact = Convert.ToDecimal(dataReader["RMImpact"].ToString()),
                                IsParent = Convert.ToBoolean(dataReader["IsParent"].ToString()),
                                CurrentCostPer = dataReader["CurrentCostPer"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["CurrentCostPer"].ToString()),
                                RevisedCostPer = dataReader["RevisedCostPer"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["RevisedCostPer"].ToString()),
                                RMReference = dataReader["RMReference"].ToString(),
                                Settleddate = dataReader["settledDate"].ToString(),
                                Reviseddate = dataReader["RevisedDate"].ToString(),
                                Submixture = dataReader["IsSubMixture"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsSubMixture"].ToString()),
                                SubPart = dataReader["IsSubPart"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsSubPart"].ToString()),
                                IsParentSubMixture = dataReader["isparentSubMixture"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["isparentSubMixture"].ToString()),
                                IsParentMixture= dataReader["isparentMixture"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["isparentMixture"].ToString()
                                ),
                                RMSurchargeGradeDiff = Convert.ToDecimal(dataReader["SRMSurchargeGradeDiff"] == DBNull.Value ? "0" : dataReader["SRMSurchargeGradeDiff"].ToString()),
                                SecondaryProcessing = Convert.ToDecimal(dataReader["SSecondaryProcessing"] == DBNull.Value ? "0" : dataReader["SSecondaryProcessing"].ToString()),
                                SurfaceProtection = Convert.ToDecimal(dataReader["SSurfaceProtection"] == DBNull.Value ? "0" : dataReader["SSurfaceProtection"].ToString()),
                                Thickness = Convert.ToDecimal(dataReader["SThickness"] == DBNull.Value ? "0" : dataReader["SThickness"].ToString()),
                                CuttingCost = Convert.ToDecimal(dataReader["SCuttingCost"] == DBNull.Value ? "0" : dataReader["SCuttingCost"].ToString()),
                                MOQVolume = Convert.ToDecimal(dataReader["SMOQVolume"] == DBNull.Value ? "0" : dataReader["SMOQVolume"].ToString()),
                                Transport = Convert.ToDecimal(dataReader["STransport"] == DBNull.Value ? "0" : dataReader["STransport"].ToString()),
                                Others = Convert.ToDecimal(dataReader["SOthers"] == DBNull.Value ? "0" : dataReader["SOthers"].ToString()),
                                BaseRMRate = Convert.ToDecimal(dataReader["SBaseRMRate"] == DBNull.Value ? "0" : dataReader["SBaseRMRate"].ToString()),

                                RevRMSurchargeGradeDiff = Convert.ToDecimal(dataReader["RRMSurchargeGradeDiff"] == DBNull.Value ? "0" : dataReader["RRMSurchargeGradeDiff"].ToString()),
                                RevSecondaryProcessing = Convert.ToDecimal(dataReader["RSecondaryProcessing"] == DBNull.Value ? "0" : dataReader["RSecondaryProcessing"].ToString()),
                                RevSurfaceProtection = Convert.ToDecimal(dataReader["RSurfaceProtection"] == DBNull.Value ? "0" : dataReader["RSurfaceProtection"].ToString()),
                                RevThickness = Convert.ToDecimal(dataReader["RThickness"] == DBNull.Value ? "0" : dataReader["RThickness"].ToString()),
                                RevCuttingCost = Convert.ToDecimal(dataReader["RCuttingCost"] == DBNull.Value ? "0" : dataReader["RCuttingCost"].ToString()),
                                RevMOQVolume = Convert.ToDecimal(dataReader["RMOQVolume"] == DBNull.Value ? "0" : dataReader["RMOQVolume"].ToString()),
                                RevTransport = Convert.ToDecimal(dataReader["RTransport"] == DBNull.Value ? "0" : dataReader["RTransport"].ToString()),
                                RevOthers = Convert.ToDecimal(dataReader["ROthers"] == DBNull.Value ? "0" : dataReader["ROthers"].ToString()),
                                RevBaseRMRate = Convert.ToDecimal(dataReader["RBaseRMRate"] == DBNull.Value ? "0" : dataReader["RBaseRMRate"].ToString()),
                                ProcessImpact= Convert.ToDecimal(dataReader["ProcessImpactTotal"] == DBNull.Value ? "0" : dataReader["ProcessImpactTotal"].ToString()),
                                RMImpactt = Convert.ToDecimal(dataReader["RMImpactTotal"] == DBNull.Value ? "0" : dataReader["RMImpactTotal"].ToString()),
                                slno = dataReader["slno"].ToString(),
                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        public async Task<List<GetRMIndexPriceSP>> GetRMPriceIndexList(GetRMIndexPriceSP input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("GetAllIndexByName", CommandType.StoredProcedure,
                  new SqlParameter("@RMGrade", (object)input.RMGrade ?? DBNull.Value),
                  new SqlParameter("@IndexName", (object)input.IndexName ?? DBNull.Value),
                  new SqlParameter("@Month", (object)input.Month ?? DBNull.Value),
                  new SqlParameter("@Year", (object)input.Year ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<GetRMIndexPriceSP>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new GetRMIndexPriceSP()
                            {
                                IndexName = dataReader["IndexName"].ToString(),
                                Year = dataReader["Year"].ToString(),
                                RMGrade = dataReader["RMGrade"].ToString(),
                                Jan = dataReader["Jan"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Jan"].ToString()),
                                Feb = dataReader["Feb"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Feb"].ToString()),
                                Mar = dataReader["Mar"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Mar"].ToString()),
                                Apr = dataReader["Apr"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Apr"].ToString()),
                                May = dataReader["May"] == DBNull.Value ? 0 : decimal.Parse(dataReader["May"].ToString()),
                                Jun = dataReader["Jun"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Jun"].ToString()),
                                Jul = dataReader["Jul"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Jul"].ToString()),
                                Aug = dataReader["Aug"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Aug"].ToString()),
                                Sep = dataReader["Sep"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Sep"].ToString()),
                                Oct = dataReader["Oct"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Oct"].ToString()),
                                Nov = dataReader["Nov"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Nov"].ToString()),
                                Dec = dataReader["Dec"] == DBNull.Value ? 0 : decimal.Parse(dataReader["Dec"].ToString()),

                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        public async Task<List<GetPriceImpactsp>> GetAllSubPartRMPriceImpact(Dto.GetA3DashboardDataInput input)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand("GetAllSubPartByImpact", CommandType.StoredProcedure,
                  new SqlParameter("@SupplierId", (object)input.Supplier ?? DBNull.Value),
                  new SqlParameter("@BuyerId", (object)input.Buyer ?? DBNull.Value),
                  new SqlParameter("@Period", (object)input.Period ?? DBNull.Value),
                  new SqlParameter("@PartNo", (object)input.Partno ?? DBNull.Value),
                  new SqlParameter("@IsGenerateA3", (object)input.IsGenerateA3 ?? DBNull.Value),
                  new SqlParameter("@A3Id", (object)input.A3Id ?? DBNull.Value),
                  new SqlParameter("@Plant", (object)input.Plant ?? DBNull.Value),
                  new SqlParameter("@GroupId", (object)input.Group ?? DBNull.Value))
              )
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<GetPriceImpactsp>();

                    while (dataReader.Read())
                    {
                        try
                        {
                            result.Add(new GetPriceImpactsp()
                            {
                                PartNo = dataReader["PartNo"].ToString(),
                                ParentPartNo = dataReader["ParentPartNo"].ToString(),
                                Description = dataReader["Description"].ToString(),
                                ES1 = dataReader["ES1"] == DBNull.Value ? "" : dataReader["ES1"].ToString(),
                                ES2 = dataReader["ES2"] == DBNull.Value ? "" : dataReader["ES2"].ToString(),
                                RawMaterialGrade = dataReader["RMGrade"].ToString(),
                                RawMaterialGroup= dataReader["RawMaterialGroup"].ToString(),
                                GrossInputWeight = Convert.ToDecimal(dataReader["GrossInputWeight"].ToString()),
                                CastingForgingWeight = Convert.ToDecimal(dataReader["CastingForgingWeight"].ToString()),
                                FinishedWeight = Convert.ToDecimal(dataReader["FinishedWeight"].ToString()),
                                ScrapRecovery = Convert.ToDecimal(dataReader["ScrapRecovery"].ToString()),
                                ScrapRecoveryPercent = Convert.ToDouble(dataReader["ScrapRecoveryPercent"].ToString()),
                                ScrapWeight = Convert.ToDecimal(dataReader["ScrapWeight"].ToString()),
                                CurrentRMCost = Convert.ToDecimal(dataReader["CurrentRMCost"].ToString()),
                                RevisedRMCost = Convert.ToDecimal(dataReader["RevisedRMCost"].ToString()),
                                OtherCost = Convert.ToDecimal(dataReader["OtherCost"].ToString()),
                                CurrentExwPrice = Convert.ToDecimal(dataReader["CurrentExwPrice"].ToString()),
                                RevisedExwPrice = Convert.ToDecimal(dataReader["RevisedExwPrice"].ToString()),
                                ExwPriceChangeInCost = Convert.ToDecimal(dataReader["ExwPriceChangeInCost"].ToString()),
                                ExwPriceChangeInPer = Convert.ToDecimal(dataReader["ExwPriceChangeInPer"].ToString()),
                                PackagingCost = Convert.ToDecimal(dataReader["PackagingCost"].ToString()),
                                LogisticsCost = Convert.ToDecimal(dataReader["LogisticsCost"].ToString()),
                                CurrentFCAPrice = Convert.ToDecimal(dataReader["CurrentFCAPrice"].ToString()),
                                RevisedFCAPrice = Convert.ToDecimal(dataReader["RevisedFCAPrice"].ToString()),
                                CurrentAVOB = Convert.ToDecimal(dataReader["CurrentAVOB"].ToString()),
                                RevisedAVOB = Convert.ToDecimal(dataReader["RevisedAVOB"].ToString()),
                                PlantCode = dataReader["PlantCode"].ToString(),
                                SOB = Convert.ToDecimal(dataReader["SOB"].ToString()),
                                GlobusEPU = Convert.ToDecimal(dataReader["GlobusEPU"].ToString()),
                                RMImpact = Convert.ToDecimal(dataReader["RMImpact"].ToString()),
                                IsParent = Convert.ToBoolean(dataReader["IsParent"].ToString()),
                                CurrentCostPer = Convert.ToDecimal(dataReader["CurrentCostPer"].ToString()),
                                RevisedCostPer = Convert.ToDecimal(dataReader["RevisedCostPer"].ToString())
                            });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                    }
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                    return result;
                }
            }
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        private void EnsureConnectionOpen()
        {
            var connection = Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
            {
                {"ContextType", typeof(RMACTDbContext) },
                {"MultiTenancySide", MultiTenancySide }
            });
        }

        private async Task EnsureConnectionOpenAsync()
        {
            var connection = Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
        }
    }
}
