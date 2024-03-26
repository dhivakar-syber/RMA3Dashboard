using Abp;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Mozilla;
using PayPalCheckoutSdk.Orders;
using Stripe;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Authorization.Users.Dto;
using SyberGate.RMACT.Configuration;
using SyberGate.RMACT.Configuration.Host.Dto;
using SyberGate.RMACT.DemoUiComponents.Dto;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Install;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Masters.Exporting;
using SyberGate.RMACT.Net.Emailing;
using SyberGate.RMACT.Notifications;
using SyberGate.RMACT.Storage;
using SyberGate.RMACT.Tenants.Dashboard.Dto;
using SyberGate.RMACT.Url;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NPOI.HSSF.Util.HSSFColor;

namespace SyberGate.RMACT.Tenants.Dashboard
{
    [DisableAuditing]
    [AbpAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class TenantDashboardAppService : RMACTAppServiceBase, ITenantDashboardAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<BaseRMRate> _baseRMRateRepository;
        private readonly IRepository<RawMaterialGrade> _rmGroupRepository;
        private readonly IPartRepository _partRepository;
        private readonly IRepository<Buyer> _buyerrepository;
        private readonly IRepository<Plant> _plantrepository;
        private readonly IRepository<Supplier> _supplierrepository;
        private readonly IRepository<SupplierBuyerMap> _supplierBuyerMap;
        private readonly IRepository<A3PriceTrend> _a3PriceTrend;
        private readonly IRepository<A3PriceImpact> _a3PriceImpact;
        private readonly IRepository<A3SubPartImpact> _a3SubPartImpact;
        private readonly IRepository<A3Document> _a3DocumentRepository;
        private readonly IRepository<Part> _Part;
        private readonly IRepository<SubPart> _subPart;
        private readonly IA3ExcelExporter _A3ExcelExporter;
        private readonly InstallAppService _appConfiguration;
        private readonly IRepository<A3LeadModel> _a3LeadModelRepository;
        private readonly IRepository<A3PartModelMatrix> _a3PartModelMatrixRepository;
        private readonly IRepository<PartModelMatrix> _partModelMatrixRepository;
        private readonly IRepository<LeadModel> _leadModelRepository;
        private readonly IRepository<PartBucket> _partBucketRepository;
        private readonly IRepository<A3PartBucket> _a3partBucketRepository;
        private readonly IPartBucketsExcelExporter _IPartBucketsExcelExporter;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IConfigurationRoot _approotConfiguration;
        private readonly IAbpSession _abpSession;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IRepository<GlobusData> _globusdataRepository;
        private readonly IRepository<RawMaterialMixture> _rawmaterialmixtureRepository;
        private readonly IRepository<ApprovalUser> _approvalUserRepository;
        private readonly IRepository<SupportAttachments> _supportAttachmentsRepository;
        private readonly ISettingManager _settingManager;
		private readonly IEmailTemplateProvider _emailTemplateProvider;
		private readonly IWebUrlService _webUrlService;
        private readonly IRepository<RawMaterialGrade, int> _lookup_rmGroupRepository;
        private readonly IRepository<UnitOfMeasurement, int> _lookup_unitOfMeasurementRepository;
        private readonly IRepository<Year, int> _lookup_yearRepository;
        private readonly IRepository<Buyer, int> _lookup_buyerRepository;
        private readonly IRepository<Supplier, int> _lookup_supplierRepository;

        private string _emailapproveButtonStyle =
			"padding-left: 30px; padding-right: 30px; padding-top: 12px; padding-bottom: 12px; color: #ffffff; background-color: #00bb77; font-size: 14pt; text-decoration: none;";
        private string _emailrejectButtonStyle =
            "padding-left: 30px; padding-right: 30px; padding-top: 12px; padding-bottom: 12px; color: #f9f9f9; background-color: red; font-size: 14pt; text-decoration: none;";
		//private string _emailapproveButtonColor = "#00bb77";
		//private string _emailrejectButtonColor = "red";
		public TenantDashboardAppService(IRepository<BaseRMRate> baseRMRateRepository,
            IPartRepository partRepository, IRepository<Buyer> buyerRepository, IRepository<Plant> plantrepository,
            IRepository<Supplier> supplierRepository, IRepository<SupplierBuyerMap> supplierBuyerMap, UserManager userManager,
            IRepository<A3PriceTrend> a3PriceTrend, IRepository<A3PriceImpact> a3PriceImpact, IRepository<A3SubPartImpact> a3SubPartImpact, IA3ExcelExporter a3ExcelExporter, 
            IRepository<A3Document> a3DocumentRepository, InstallAppService appConfiguration, IRepository<Part> part, IRepository<SubPart> subPart, IRepository<RawMaterialGrade> rmGroupRepository,
            IRepository<PartModelMatrix> partModelMatrixRepository, IRepository<LeadModel> leadModelRepository, IRepository<A3LeadModel>A3LeadModelRepository,
            IRepository<A3PartModelMatrix>A3PartModelMatrixRepository, IRepository<PartBucket> PartBucketRepository ,
            IRepository<A3PartBucket> A3PartBucketRepository, IPartBucketsExcelExporter iPartBucketsExcelExporter,
            ITempFileCacheManager tempFileCacheManager, IWebHostEnvironment webHostEnvironment, IHostEnvironment hostEnvironment,
            IAppConfigurationAccessor configurationAccessor, IAbpSession abpSession,
            IUnitOfWorkManager unitOfWorkManager,
            IAppNotifier appNotifier, IRepository<GlobusData> GlobusData,
            IRepository<RawMaterialMixture> Rawmaterialmixture,
            IRepository<ApprovalUser> ApprovalUserRepository,
            IRepository<SupportAttachments> SupportAttachmentsRepository,
            ISettingManager settingManager, IEmailTemplateProvider emailTemplateProvider, IWebUrlService webUrlService, IRepository<RawMaterialGrade, int> lookup_rmGroupRepository,
            IRepository<UnitOfMeasurement, int> lookup_unitOfMeasurementRepository, IRepository<Year, int> lookup_yearRepository, IRepository<Buyer, int> lookup_buyerRepository, 
            IRepository<Supplier, int> lookup_supplierRepository)
        
        {
            _baseRMRateRepository = baseRMRateRepository;
            _partRepository = partRepository;
            _buyerrepository = buyerRepository;
            _supplierrepository = supplierRepository;
            _supplierBuyerMap = supplierBuyerMap;
            _userManager = userManager;
            _a3PriceTrend = a3PriceTrend;
            _a3PriceImpact = a3PriceImpact;
            _a3SubPartImpact = a3SubPartImpact;
            _A3ExcelExporter = a3ExcelExporter;
            _appConfiguration = appConfiguration;
            _Part = part;
            _subPart =subPart;
            _rmGroupRepository = rmGroupRepository;
            _plantrepository = plantrepository;
            _a3DocumentRepository = a3DocumentRepository;
            _partModelMatrixRepository= partModelMatrixRepository;
            _leadModelRepository= leadModelRepository;
            _a3LeadModelRepository = A3LeadModelRepository;
            _a3PartModelMatrixRepository= A3PartModelMatrixRepository;
            _partBucketRepository = PartBucketRepository;
            _a3partBucketRepository= A3PartBucketRepository;
            _IPartBucketsExcelExporter = iPartBucketsExcelExporter;
            _tempFileCacheManager = tempFileCacheManager;
            _webHostEnvironment = webHostEnvironment;
            _hostEnvironment = hostEnvironment;
            _approotConfiguration = configurationAccessor.Configuration;
            _abpSession = abpSession;
            _unitOfWorkManager = unitOfWorkManager;
            _appNotifier = appNotifier;
            _globusdataRepository = GlobusData;
            _rawmaterialmixtureRepository = Rawmaterialmixture;
            _approvalUserRepository= ApprovalUserRepository;
            _supportAttachmentsRepository = SupportAttachmentsRepository;
            _settingManager = settingManager;
			_emailTemplateProvider = emailTemplateProvider;
			_webUrlService = webUrlService;
            _lookup_rmGroupRepository = lookup_rmGroupRepository;
            _lookup_unitOfMeasurementRepository = lookup_unitOfMeasurementRepository;
            _lookup_yearRepository = lookup_yearRepository;
            _lookup_buyerRepository = lookup_buyerRepository;
            _lookup_supplierRepository = lookup_supplierRepository;
        }
        public string l3templatepath = "";
        public GetMemberActivityOutput GetMemberActivity()
        {
            return new GetMemberActivityOutput
            (
                DashboardRandomDataGenerator.GenerateMemberActivities()
            );
        }

        public GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input)
        {
            var output = new GetDashboardDataOutput
            {
                TotalProfit = DashboardRandomDataGenerator.GetRandomInt(5000, 9000),
                NewFeedbacks = DashboardRandomDataGenerator.GetRandomInt(1000, 5000),
                NewOrders = DashboardRandomDataGenerator.GetRandomInt(100, 900),
                NewUsers = DashboardRandomDataGenerator.GetRandomInt(50, 500),
                SalesSummary = DashboardRandomDataGenerator.GenerateSalesSummaryData(input.SalesSummaryDatePeriod),
                Expenses = DashboardRandomDataGenerator.GetRandomInt(5000, 10000),
                Growth = DashboardRandomDataGenerator.GetRandomInt(5000, 10000),
                Revenue = DashboardRandomDataGenerator.GetRandomInt(1000, 9000),
                TotalSales = DashboardRandomDataGenerator.GetRandomInt(10000, 90000),
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                DailySales = DashboardRandomDataGenerator.GetRandomArray(30, 10, 50),
                ProfitShares = DashboardRandomDataGenerator.GetRandomPercentageArray(3)
            };

            return output;
        }

        public GetTopStatsOutput GetTopStats()
        {
            return new GetTopStatsOutput
            {
                TotalProfit = DashboardRandomDataGenerator.GetRandomInt(5000, 9000),
                NewFeedbacks = DashboardRandomDataGenerator.GetRandomInt(1000, 5000),
                NewOrders = DashboardRandomDataGenerator.GetRandomInt(100, 900),
                NewUsers = DashboardRandomDataGenerator.GetRandomInt(50, 500)
            };
        }

        public GetProfitShareOutput GetProfitShare()
        {
            return new GetProfitShareOutput
            {
                ProfitShares = DashboardRandomDataGenerator.GetRandomPercentageArray(3)
            };
        }

        public GetDailySalesOutput GetDailySales()
        {
            return new GetDailySalesOutput
            {
                DailySales = DashboardRandomDataGenerator.GetRandomArray(30, 10, 50)
            };
        }

        public GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input)
        {
            var salesSummary = DashboardRandomDataGenerator.GenerateSalesSummaryData(input.SalesSummaryDatePeriod);
            return new GetSalesSummaryOutput(salesSummary)
            {
                Expenses = DashboardRandomDataGenerator.GetRandomInt(0, 3000),
                Growth = DashboardRandomDataGenerator.GetRandomInt(0, 3000),
                Revenue = DashboardRandomDataGenerator.GetRandomInt(0, 3000),
                TotalSales = DashboardRandomDataGenerator.GetRandomInt(0, 3000)
            };
        }

        public GetRegionalStatsOutput GetRegionalStats()
        {
            return new GetRegionalStatsOutput(
                DashboardRandomDataGenerator.GenerateRegionalStat()
            );
        }

        public GetGeneralStatsOutput GetGeneralStats()
        {
            return new GetGeneralStatsOutput
            {
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100)
            };
        }

        public GetPriceTrendList GetA3RMPriceTrend(Masters.Dto.GetA3DashboardDataInput input)
        {
            var rmPrice = _a3PriceTrend.GetAll().Where(w => w.DocId == input.A3Id)
                        .Select(item => new GetRMPriceTrend
                        {
                            Id = item.RevId ?? 0,
                            MixtureGrade = item.MixtureGrade,
                            RMGroupId = item.RMGroupId ?? 0,
                            BaseRMPOC = item.BaseRMPOC,
                            RevisedMY = item.RevisedMY,
                            RevisedUR = item.RevisedUR ?? 0,
                            RMGrade = item.RMGrade,
                            ScrapPOC = item.ScrapPOC,
                            ScrapRevised = item.ScrapRevised ?? 0,
                            ScrapSetteled = item.ScrapSetteled ?? 0,
                            SetteledMY = item.SetteledMY,
                            SetteledUR = item.SetteledUR ?? 0,
                            Uom = item.Uom,
                            RevApproved = item.RevApproved ?? false,
                            SetApproved = item.SetApproved ?? false,
                            SetId = item.SetId ?? 0,
                            RevFromPeriod = item.RevFromPeriod,
                            RevToPeriod = item.RevToPeriod,
                            RevIndexName = item.RevIndexName,
                            RevIndexValue = item.RevIndexValue ?? 0
                        });

            return new GetPriceTrendList(rmPrice.ToList());
        }
        
        public GetPriceTrendList GetSupplierRMPriceTrend(Masters.Dto.GetA3DashboardDataInput input)
        {
            var items = _partRepository.GetAllRMTrend(input);
            List<GetRMPriceTrend> rmPrice = new List<GetRMPriceTrend>();

            foreach (var item in items.Result)
            {
                string rformattedDate = "";
                string sformattedDate = "";

                if (item.RevisedMY != "" && item.RevisedMY != "01-01-0001 00:00:00")
                {
                    string rdateString = item.RevisedMY;
                    DateTime rdate = DateTime.ParseExact(rdateString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                     rformattedDate = rdate.ToString("dd-MM-yyyy");
                }

                if (item.SetteledMY != "" && item.SetteledMY != "01-01-0001 00:00:00")
                {
                    string sdateString = item.SetteledMY;
                    DateTime sdate = DateTime.ParseExact(sdateString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    sformattedDate = sdate.ToString("dd-MM-yyyy");
                }
               
                rmPrice.Add(new GetRMPriceTrend
                {
                    Id = item.id,
                    RMGroupId = item.RMGroupId,
                    BaseRMPOC = item.BaseRMPOC,
                    RevisedMY = rformattedDate,
                    RevisedUR = item.RevisedUR,
                    RMGrade = item.RMGrade,
                    ScrapPOC = item.ScrapPOC,
                    ScrapRevised = item.ScrapRevised,
                    ScrapSetteled = item.ScrapSetteled,
                    SetteledMY = sformattedDate,
                    SetteledUR = item.SetteledUR,
                    Uom = item.Uom,
                    RevApproved = item.RevApproved,
                    SetApproved = item.SetApproved,
                    SetId = item.SetId,
                    SetIndexName = item.SetIndexName,
                    SetIndexValue = item.SetIndexValue,
                    SetFromPeriod = item.SetFromPeriod,
                    SetToPeriod = item.SetToPeriod,
                    RevIndexName = item.RevIndexName,
                    RevIndexValue = item.RevIndexValue,
                    RevFromPeriod = item.RevFromPeriod,
                    RevToPeriod = item.RevToPeriod,
                    MixtureGrade = item.MixtureGrade,
                    SetteledWRatio = item.SetWRatio,
                    SetteledLRatio = item.SetLRatio,
                    RevisedWRatio = item.RevWRatio,
                    RevisedLRatio = item.RevLRatio,
                    ParentGrp = item.ParentGrp,
                });
            }

            return new GetPriceTrendList(rmPrice);
        }
        public GetPriceTrendList GetSupplierRMPriceHistory(Masters.Dto.GetA3DashboardDataInput input)
        {
            var items = _baseRMRateRepository.GetAll().Include(w => w.RMGroupFk)
                .WhereIf(!string.IsNullOrEmpty(input.Buyer), w => w.BuyerId == int.Parse(input.Buyer))
                .WhereIf(!string.IsNullOrEmpty(input.Supplier), w => w.SupplierId == int.Parse(input.Supplier))
                .WhereIf(!string.IsNullOrEmpty(input.Grade), w => w.RMGroupFk.RawMaterialGradeId == int.Parse(input.Grade))
                .WhereIf(!string.IsNullOrEmpty(input.Spec), w => w.RMGroupId == int.Parse(input.Spec));

            var baseRMRates = from o in items
                              join o1 in _lookup_rmGroupRepository.GetAll() on o.RMGroupId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              join o2 in _lookup_rmGroupRepository.GetAll() on s1.RawMaterialGradeId equals o2.Id into j2
                              from s2 in j2.DefaultIfEmpty()

                              join o3 in _lookup_rmGroupRepository.GetAll() on s2.RawMaterialGradeId equals o3.Id into j3
                              from s3 in j3.DefaultIfEmpty()

                              join o4 in _lookup_unitOfMeasurementRepository.GetAll() on o.UnitOfMeasurementId equals o4.Id into j4
                              from s4 in j4.DefaultIfEmpty()

                              join o5 in _lookup_buyerRepository.GetAll() on o.BuyerId equals o5.Id into j5
                              from s5 in j5.DefaultIfEmpty()

                              join o6 in _lookup_supplierRepository.GetAll() on o.SupplierId equals o6.Id into j6
                              from s6 in j6.DefaultIfEmpty()

                              select new GetRMPriceTrend
                              {
                                  Id = o.Id,
                                  RMGroupId = (int)o.RMGroupId,
                                  MixtureGrade = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                  RevisedUR = o.UnitRate,
                                  RMGrade = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                  ScrapRevised = o.ScrapAmount,
                                  SetteledMY = Convert.ToString(o.SettledDate),
                                  Uom = s4 == null || s4.Code == null ? "" : s4.Code.ToString(),
                                  ParentGrp = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                                  SetApproved = (bool)o.IsApproved,
                                  Buyer = s5 == null || s5.Name == null ? "" : s5.Name.ToString(),
                                  Supplier = s6 == null || s6.Name == null ? "" : s6.Name.ToString(),

                              };


            List <GetRMPriceTrend> rmPrice = new List<GetRMPriceTrend>();

            foreach (var item in baseRMRates)
            {
                string sformattedDate = "";

                if (item.SetteledMY != "" && item.SetteledMY != "01-01-0001 00:00:00")
                {
                    string sdateString = item.SetteledMY;
                    DateTime sdate = DateTime.ParseExact(sdateString, "yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture);
                    sformattedDate = sdate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
               
                rmPrice.Add(new GetRMPriceTrend
                {
                    Id = item.Id,
                    RMGroupId = item.RMGroupId,
                    RevisedUR = item.RevisedUR,
                    RMGrade = item.RMGrade,
                    ScrapRevised = item.ScrapRevised,
                    SetteledMY = sformattedDate,
                    SetteledUR = item.SetteledUR,
                    Uom = item.Uom,
                    MixtureGrade = item.MixtureGrade,
                    ParentGrp = item.ParentGrp,
                    SetApproved = item.SetApproved,
                    Buyer=item.Buyer,
                    Supplier = item.Supplier,
                });
            }

            return new GetPriceTrendList(rmPrice.ToList());
        }

        public async Task<List<gettotalrate>> Gettotalrate(Masters.Dto.GetTotalRate input)
        {
            //if (input.Month == "Select Month" || input.Month == " ") 
            //{
            //    input.Month = "April";
            //}
            //if(input.Year == "Select Year" || input.Year == " ")
            //{
            //    input.Year = "2023";
            //}
                var items = await _partRepository.Getallpricevalue(input);
                List<gettotalrate> rmValue = new List<gettotalrate>();
                foreach (var item in items)
                {
                    rmValue.Add(new gettotalrate
                    {
                        ParentGroup=item.ParentGroup,
                        GroupId=item.GroupId,
                        TotalAverage = item.TotalAverage,
                        GrossInputWeightAverage = item.GrossInputWeightAverage,
                        EPUAverage = item.EPUAverage,
                        SOBAverage = item.SOBAverage,
                        UnitRateAverage = item.UnitRateAverage
                    });
                }
            

            return new List<gettotalrate>(rmValue.ToList());
        }

        public async Task<List<gettotalrate>> Gettotalton(Masters.Dto.GetTotalRate input)
        {
            //if (input.Month == "Select Month" || input.Month == " ")
            //{
            //    input.Month = "April";
            //}
            //if (input.Year == "Select Year" || input.Year == " ")
            //{
            //    input.Year = "2023";
            //}
            var items = await _partRepository.Getalltonnagevalue(input);
            List<gettotalrate> rmValue = new List<gettotalrate>();
            foreach (var item in items)
            {
                rmValue.Add(new gettotalrate
                {
                    ParentGroup = item.ParentGroup,
                    GroupId = item.GroupId,
                    TotalTon = item.TotalTon,
                    TotalValue = item.TotalValue,
                    GrossInputWeightAverage = item.GrossInputWeightAverage,
                    EPUAverage = item.EPUAverage,
                    SOBAverage = item.SOBAverage,
                });
            }

            return new List<gettotalrate>(rmValue.ToList());
        }
        public async Task<List<gettotalrate>> Gettotaltonbygroup(Masters.Dto.GetTotalRate input)
        {
            //if (input.Month == "Select Month" || input.Month == " ")
            //{
            //    input.Month = "April";
            //}
            //if (input.Year == "Select Year" || input.Year == " ")
            //{
            //    input.Year = "2023";
            //}
            var items = await _partRepository.Getalltonnagevaluebygrade(input);
            List<gettotalrate> rmValue = new List<gettotalrate>();
            foreach (var item in items)
            {
                rmValue.Add(new gettotalrate
                {
                    Spec = item.Spec,
                    SpecId = item.SpecId,
                    TotalTonByGrade = item.TotalTonByGrade,
                    TotalValueByGrade = item.TotalValueByGrade,
                    GrossInputWeightAverage = item.GrossInputWeightAverage,
                    EPUAverage = item.EPUAverage,
                    SOBAverage = item.SOBAverage,
                });
            }

            return new List<gettotalrate>(rmValue.ToList());
        }

        public List<DepartmentQuarterSummary> GetDepartmentQuarterSummary(GetDepartment input)
        {
            if(input.BuyerId == "0")
            {
                input.BuyerId = null;
            }
            var items = _partRepository.Getallteams(input);
            List<DepartmentQuarterSummary> teams = new List<DepartmentQuarterSummary>();
            foreach (var item in items.Result)
            {
                teams.Add(new DepartmentQuarterSummary
                {
                    Department = item.Department,
                    Q1_Total = item.Q1_Total,
                    Q1_Approved = item.Q1_Approved,
                    Q1_Pending = item.Q1_Pending,
                    Q2_Total = item.Q2_Total,
                    Q2_Approved = item.Q2_Approved,
                    Q2_Pending = item.Q2_Pending,
                    Q3_Total = item.Q3_Total,
                    Q3_Approved = item.Q3_Approved,
                    Q3_Pending = item.Q3_Pending,
                    Q4_Total = item.Q4_Total,
                    Q4_Approved = item.Q4_Approved,
                    Q4_Pending = item.Q4_Pending,
                });
            }

            return new List<DepartmentQuarterSummary>(teams.ToList());
        }

        //public List<GetMonthInput> Getpricemonthly(Getpriceyearly input)
        //{
        //    var items = _partRepository.Getallpriceyearly(input);
        //    List<GetMonthInput> price = new List<GetMonthInput>();
        //    if (items.Result.Count > 0)
        //    {
        //        foreach (var item in items.Result)
        //        {
        //            price.Add(new GetMonthInput
        //            {
        //                January = item.January,
        //                February = item.February,
        //                March = item.March,
        //                April = item.April,
        //                May = item.May,
        //                June = item.June,
        //                July = item.July,
        //                August = item.August,
        //                September = item.September,
        //                October = item.October,
        //                November = item.November,
        //                December = item.December,
        //            });
        //        }
        //    }
        //    else
        //    {
        //        price.Add(new GetMonthInput
        //        {
        //            January = 0,
        //            February = 0,
        //            March = 0,
        //            April = 0,
        //            May = 0,
        //            June = 0,
        //            July = 0,
        //            August = 0,
        //            September = 0,
        //            October = 0,
        //            November = 0,
        //            December = 0,
        //        });
        //    }

        //    return new List<GetMonthInput>(price.ToList());
        //}

        //public List<GetMonthInput> Gettonmonthly(Getpriceyearly input)
        //{
        //    var items = _partRepository.Getalltonnageyearly(input);
        //    List<GetMonthInput> price = new List<GetMonthInput>();
        //    if (items.Result.Count > 0)
        //    {
        //        foreach (var item in items.Result)
        //        {
        //            price.Add(new GetMonthInput
        //            {
        //                January = item.January,
        //                February = item.February,
        //                March = item.March,
        //                April = item.April,
        //                May = item.May,
        //                June = item.June,
        //                July = item.July,
        //                August = item.August,
        //                September = item.September,
        //                October = item.October,
        //                November = item.November,
        //                December = item.December,
        //            });
        //        }
        //    }
        //    else
        //    {
        //        price.Add(new GetMonthInput
        //        {
        //            January = 0,
        //            February = 0,
        //            March = 0,
        //            April = 0,
        //            May = 0,
        //            June = 0,
        //            July = 0,
        //            August = 0,
        //            September = 0,
        //            October = 0,
        //            November = 0,
        //            December = 0,
        //        });
        //    }
        

        //    return new List<GetMonthInput>(price.ToList());
        //}

        public List<GetBuyerandMonth> Getrmforcompbybuyer(Getrmcomp input)
        {
            if(input.BuyerId == "0")
            {
                input.BuyerId=null;
            }
            var items = _partRepository.Getrmforcomp(input);
            List<GetBuyerandMonth> team = new List<GetBuyerandMonth>();
            foreach (var item in items.Result)
            {
                team.Add(new GetBuyerandMonth
                {
                    Team=item.Team,
                    BuyerName = item.BuyerName,
                    Suppliername=item.Suppliername,
                    Spec=item.Spec,
                    WeightedAverageEPU=item.WeightedAverageEPU,
                    UnitRateAverage=item.UnitRateAverage,
                    January = item.January,
                    February = item.February,
                    March = item.March,
                    April = item.April,
                    May = item.May,
                    June = item.June,
                    July = item.July,
                    August = item.August,
                    September = item.September,
                    October = item.October,
                    November = item.November,
                    December = item.December,
                });
            }
            return new List<GetBuyerandMonth>(team.ToList());
        }

        public List<GetModel> GetModelForCompByPlant(GetAVOBComp input)
        {
            if (input.Plant == "0")
            {
                input.Plant = null;
            }

            List<GetModel> model = new List<GetModel>();

            if (input.Month != "Select Month To Compare")
            {
                var items = _partRepository.GetModelForComp(input);

                foreach (var item in items.Result)
                {
                    model.Add(new GetModel
                    {
                        AVOB_1015R = item.AVOB_1015R,
                        AVOB_1215R = item.AVOB_1215R,
                        AVOB_1415R = item.AVOB_1415R,
                        AVOB_1217C = item.AVOB_1217C,
                        AVOB_1617R = item.AVOB_1617R,
                        AVOB_2823R = item.AVOB_2823R,
                        AVOB_3523R = item.AVOB_3523R,
                        AVOB_1923C = item.AVOB_1923C,
                        AVOB_2823C = item.AVOB_2823C,
                        AVOB_2828C = item.AVOB_2828C,
                        AVOB_3528C = item.AVOB_3528C,
                        AVOB_5428TS = item.AVOB_5428TS,
                        AVOB_5528T = item.AVOB_5528T,
                        AVOB_4228R = item.AVOB_4228R,
                        AVOB_3528CM = item.AVOB_3528CM,
                    });
                }
            }
            else
            {
                model.Add(new GetModel
                {
                    AVOB_1015R = 0,
                    AVOB_1215R = 0,
                    AVOB_1415R = 0,
                    AVOB_1217C = 0,
                    AVOB_1617R = 0,
                    AVOB_2823R = 0,
                    AVOB_3523R = 0,
                    AVOB_1923C = 0,
                    AVOB_2823C = 0,
                    AVOB_2828C = 0,
                    AVOB_3528C = 0,
                    AVOB_5428TS = 0,
                    AVOB_5528T = 0,
                    AVOB_4228R = 0,
                    AVOB_3528CM = 0,
                });

            }

            return model.ToList();
        }
        public GetPriceTrendList GetTotalSupplier(Masters.Dto.GetA3DashboardDataInput input)
        {
            if (input.Department == "All Team")
            {
                input.Department = null;
            }
            var totalBuyers = (from p in _partRepository.GetAll()
                                  join b in _buyerrepository.GetAll() on p.BuyerId equals b.Id
                                  where (input.Department == null || b.Department == input.Department) &&
                                  (input.Supplier == null || p.SupplierId == int.Parse(input.Supplier))
                                  select p.BuyerId).Distinct().Count();


            var totalparts = (from p in _partRepository.GetAll()
                              join b in _buyerrepository.GetAll() on p.BuyerId equals b.Id
                              where (input.Department == null || b.Department == input.Department) &&
                              (input.Supplier == null || p.SupplierId == int.Parse(input.Supplier))
                              select p.PartNo).Count();

            var totalAVOB = (from p in _partRepository.GetAll()
                             join gd in _globusdataRepository.GetAll() on new { p.BuyerId, p.SupplierId, p.PartNo } equals new { gd.BuyerId, gd.SupplierId, gd.PartNo }
                             join b in _buyerrepository.GetAll() on p.BuyerId equals b.Id
                             where gd.Id == (from dd in _globusdataRepository.GetAll()
                                             where gd.PartNo == dd.PartNo
                                                  && gd.BuyerId == dd.BuyerId
                                                  && gd.SupplierId == dd.SupplierId
                                                  && gd.PlantCode == dd.PlantCode
                                                  && dd.FromDate.Year <= int.Parse(input.Year)
                                                  && dd.ToDate.Year >= int.Parse(input.Year)
                                             select dd.Id).Max()
                                   && (input.Department == null || b.Department == input.Department)
                                   && (input.Supplier == null || gd.SupplierId == int.Parse(input.Supplier))
                             select (gd.CurrentExwPrice + gd.PackagingCost + gd.LogisticsCost) * gd.EPU * (gd.SOB * 0.01m) * 0.000001m).Sum();

            if (totalAVOB == 0)
            {
                totalBuyers = 0;
                totalparts = 0;
            }

            List<GetRMPriceTrend> rmPrice = new List<GetRMPriceTrend>();


            rmPrice.Add(new GetRMPriceTrend
            {
                TotalBuyers = totalBuyers,
                TotalParts = totalparts,
                TotalAVOB = totalAVOB
            });


            return new GetPriceTrendList(rmPrice.ToList());
        }
        public GetPriceTrendList GetAVOBForSupplier(Masters.Dto.GetA3DashboardDataInput input)
        {
            var query = from p in _partRepository.GetAll()
                        join gd in _globusdataRepository.GetAll() on new { p.BuyerId, p.SupplierId, p.PartNo } equals new { gd.BuyerId, gd.SupplierId, gd.PartNo }
                        where gd.Id == _globusdataRepository.GetAll()
                                        .Where(dd => gd.PartNo == dd.PartNo &&
                                                     gd.BuyerId == dd.BuyerId &&
                                                     gd.SupplierId == dd.SupplierId &&
                                                     gd.PlantCode == dd.PlantCode &&
                                                     dd.FromDate.Year <= int.Parse(input.Year) &&
                                                     dd.ToDate.Year >= int.Parse(input.Year))
                                        .Max(dd => dd.Id)
                              && gd.SupplierId == int.Parse(input.Supplier)
                        group gd by new { gd.BuyerId } into grouped
                        select new GetRMPriceTrend
                        {
                            Buyer = _supplierBuyerMap.GetAll()
                                .Where(s => s.BuyerId == grouped.Key.BuyerId)
                                .Select(s => s.BuyerName)
                                .FirstOrDefault(),
                            Department = _buyerrepository.GetAll()
                                .Where(s => s.Id == grouped.Key.BuyerId)
                                .Select(s => s.Department)
                                .FirstOrDefault(),
                            BuyerId = _supplierBuyerMap.GetAll()
                                .Where(s => s.BuyerId == grouped.Key.BuyerId)
                                .Select(s => s.BuyerId)
                                .FirstOrDefault(),
                            TotalParts = _partRepository.GetAll()
                            .Where(s => s.BuyerId == grouped.Key.BuyerId && s.SupplierId == int.Parse(input.Supplier))
                            .Count(),
                            TotalAVOBBySupplier = grouped.Sum(g => (g.CurrentExwPrice + g.PackagingCost + g.LogisticsCost) *
                                                                   g.EPU * (g.SOB * 0.01m) * 0.000001m)
                        };

            var result = query.ToList();

            List<GetRMPriceTrend> rmPrice = new List<GetRMPriceTrend>();

            foreach (var item in result)
            {
                rmPrice.Add(new GetRMPriceTrend
                {
                    Buyer = item.Buyer,
                    Department = item.Department,
                    TotalParts = item.TotalParts,
                    TotalAVOBBySupplier = item.TotalAVOBBySupplier,
                    BuyerId = item.BuyerId
                });
            }

            return new GetPriceTrendList(rmPrice.ToList());
        }
        public List<GetRMPriceTrend> GetAVOBForSupplierandTeams(Masters.Dto.GetA3DashboardDataInput input)
        {
            if(input.Department=="All Team")
            {
                input.Department = null;
            }
            var items = _partRepository.GetAvobByBuyerandTeam(input);
            List<GetRMPriceTrend> avob = new List<GetRMPriceTrend>();
            foreach (var item in items.Result)
            {
                avob.Add(new GetRMPriceTrend
                {
                    Department = item.Department,
                    Buyer = item.Buyer,
                    Supplier = item.Supplier,
                    SupplierId = item.SupplierId,
                    SupplierCode = item.SupplierCode,
                    TotalParts = item.TotalParts,
                    TotalAVOBBySupplier = item.TotalAVOBBySupplier,
                });
            }
            return new List<GetRMPriceTrend>(avob.ToList());

        }


        public GetPriceTrendList GetRMPriceForEdit(Masters.Dto.GetA3DashboardDataInput input)
        { 
            var items = _partRepository.GetAllRMTrend(input);
            List<GetRMPriceTrend> rmPrice = new List<GetRMPriceTrend>();

            foreach (var item in items.Result)
            {
                string rformattedDate = "";
                string sformattedDate = "";

                if (item.RevisedMY != "" && item.RevisedMY != "01-01-0001 00:00:00")
                {
                    string rdateString = item.RevisedMY;
                    DateTime rdate = DateTime.ParseExact(rdateString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    sformattedDate = rdate.ToString("dd-MM-yyyy");

                }
                if (item.SetteledMY != "" && item.SetteledMY != "01-01-0001 00:00:00")
                {
                    string sdateString = item.SetteledMY;
                    DateTime sdate = DateTime.ParseExact(sdateString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    rformattedDate = sdate.ToString("dd-MM-yyyy");

                }

                if (item.RevApproved)
                {
                    item.MixtureGrade = item.MixtureGrade;
                    item.SetteledMY = rformattedDate;
                    item.SetteledUR = item.RevisedUR;
                    item.ScrapSetteled = item.ScrapRevised;
                    item.SetApproved = item.RevApproved;
                    item.SetId = item.id;
                    item.id = 0;
                    item.RevisedMY = "";
                    item.RevisedUR = item.RevisedUR;
                    item.ScrapRevised = item.ScrapRevised;
                    item.RevApproved = false;
                    item.SetIndexName = item.RevIndexName;
                    item.SetIndexValue = item.RevIndexValue;
                    item.SetFromPeriod = item.RevFromPeriod;
                    item.SetToPeriod = item.RevToPeriod;
                    item.RevIndexName = "";
                    item.RevIndexValue = 0;
                    item.RevFromPeriod = "";
                    item.RevToPeriod = "";
                    item.SetWRatio = item.RevWRatio;
                    item.SetLRatio = item.RevLRatio;
                }

                rmPrice.Add(new GetRMPriceTrend
                {
                    Id = item.id,
                    MixtureGrade = item.MixtureGrade,
                    RMGroupId = item.RMGroupId,
                    BaseRMPOC = item.BaseRMPOC,
                    RevisedMY = rformattedDate,
                    RevisedUR = item.RevisedUR,
                    ScrapRevised = item.ScrapRevised,
                    RevApproved = item.RevApproved,

                    RMGrade = item.RMGrade,
                    ScrapPOC = item.ScrapPOC,
                    ScrapSetteled = item.ScrapSetteled,
                    SetteledMY = sformattedDate,
                    SetteledUR = item.SetteledUR,
                    Uom = item.Uom,
                    SetApproved = item.SetApproved,
                    SetId = item.SetId,
                    RMGradeId = item.RMGradeId,
                    UomId = item.UOMId,
                    SetIndexName = item.SetIndexName,
                    SetIndexValue = item.SetIndexValue,
                    SetFromPeriod = item.SetFromPeriod,
                    SetToPeriod = item.SetToPeriod,
                    RevIndexName = item.RevIndexName,
                    RevIndexValue = item.RevIndexValue,
                    RevFromPeriod = item.RevFromPeriod,
                    RevToPeriod = item.RevToPeriod,
                    SetteledWRatio = item.SetWRatio,
                    SetteledLRatio = item.SetLRatio,
                    RevisedWRatio = item.RevWRatio,
                    RevisedLRatio = item.RevLRatio
                });
            }

            return new GetPriceTrendList(rmPrice);
        }
        
        public GetPriceImpactList GetA3RMPriceImpact(Masters.Dto.GetA3DashboardDataInput input)
        {
            var rmPrice = _a3PriceImpact.GetAll().Where(w => w.DocId == input.A3Id);
                List<GetRMPriceImpact> rmPrice1 = new List<GetRMPriceImpact>();



            int slno = 1;
            int subpartslno = 1;
            var slnocom = "1";
            int submix = 1;
            foreach (var price in rmPrice)
            {
                

                var priceImpactDto = ObjectMapper.Map<GetRMPriceImpact>(price);

                if(priceImpactDto.IsParent == true && priceImpactDto.IsParentMixture == false && priceImpactDto.IsParentSubMixture == false)
                {
                    //priceImpactDto.CurrentRMCost = 0;
                    //priceImpactDto.RevisedRMCost = 0; 
                    //priceImpactDto.ScrapRecoveryPercent = 0;
                }


                if (priceImpactDto.SubPart == true)
                {

                    

                    if (priceImpactDto.SubMixture == false)

                    {
                        priceImpactDto.SlNo = (slno - 1).ToString() + "." + subpartslno.ToString();
                        
                        slnocom = slno.ToString();

                        subpartslno++;
                    }

                    else if (priceImpactDto.SubMixture == true)

                    {
                        priceImpactDto.SlNo = (slno - 1).ToString() + "." + (subpartslno - 1).ToString() + "." + submix.ToString();

                        slnocom = slno.ToString();
                        submix++;
                    }


                }
                else if (priceImpactDto.SubPart == false && priceImpactDto.SubMixture == true)
                {
                    if (priceImpactDto.SubMixture == true)
                    {
                        priceImpactDto.PartNo = "";
                        priceImpactDto.Description = "";
                        priceImpactDto.OtherCost = 0;
                        

                        priceImpactDto.SlNo = (slno - 1).ToString() + "." + subpartslno.ToString();

                        slnocom = slno.ToString();

                        subpartslno++;

                    }

                   
                    submix = 1;

                }

                else
                {
                    subpartslno = 1;
                    submix = 1;
                    priceImpactDto.SlNo = slno++.ToString();
                }

                rmPrice1.Add(priceImpactDto);


                
            }

            return new GetPriceImpactList(rmPrice1);
        }



        public GetPriceImpactList GetSupplierRMPriceImpact(Masters.Dto.GetA3DashboardDataInput input)
        {
            var items = _partRepository.GetAllRMPriceImpact(input).Result.ToList();
            List<GetRMPriceImpact> rmPrice = new List<GetRMPriceImpact>();

            ObjectMapper.Map<List<GetPriceImpactsp>,List<GetRMPriceImpact>>(items,rmPrice);

            //        int slno = 1;
            //        int subpartslno = 1;
            //        var slnocom = "1";
            //        int submix = 1;

            //        foreach (var item in items.Result)
            //        {
            
            //if (item.IsParent == true && item.IsParentMixture == false && item.IsParentSubMixture == false)
            //{
            //	//item.CurrentRMCost = 0;
            //	//item.RevisedRMCost = 0;
            //	//item.ScrapRecoveryPercent = 0;


            //}

            //if (item.SubPart == true)
            //            {



            //                if (item.Submixture == false)

            //                {
            //                    item.slno = (slno - 1).ToString() + "." + subpartslno.ToString();

            //                    slnocom = slno.ToString();

            //                    subpartslno++;
            //                }

            //                else if (item.Submixture == true)

            //                {
            //                    item.slno = (slno - 1).ToString() + "." + (subpartslno - 1).ToString() + "." + submix.ToString();

            //                    slnocom = slno.ToString();
            //                    submix++;
            //                }


            //            }
            //            else if (item.SubPart == false && item.Submixture == true)
            //            {
            //                if (item.Submixture == true)
            //                {
            //                    item.PartNo = "";
            //                    item.Description = "";
            //                    item.OtherCost = 0;

            //		item.slno = (slno - 1).ToString() + "." + subpartslno.ToString();

            //		slnocom = slno.ToString();

            //		subpartslno++;

            //	}


            //                submix = 1;

            //            }

            //            else
            //            {
            //                subpartslno = 1;
            //                submix = 1;
            //                item.slno = slno++.ToString();
            //            }


            //            rmPrice.Add(ObjectMapper.Map<GetRMPriceImpact>(item));
            //        }
            return new GetPriceImpactList(rmPrice);
        }

        public GetPriceImpactList UpdateRMCostConversion(Masters.Dto.GetA3DashboardDataInput input)
        {
            var items = _partRepository.UpdateRMConversionCost(input);
            
            return new GetPriceImpactList(items.Result);
        }

        public GetPriceImpactList GetA3SubPartRMPriceImpact(Masters.Dto.GetA3DashboardDataInput input)
        {
            var rmPrice = _a3SubPartImpact.GetAll().Where(w => w.DocId == input.A3Id && w.ParentPartNo == input.Partno && w.PlantCode == input.Plant);
            List<GetRMPriceImpact> rmPrice1 = new List<GetRMPriceImpact>();

             int slno = 1;
            foreach (var price in rmPrice)
            {
                price.slno = slno++;
                var priceImpactDto = ObjectMapper.Map<GetRMPriceImpact>(price);

                var bucketdetails = _a3partBucketRepository.GetAll().Where(w => w.RMSpec == priceImpactDto.RawMaterialGrade && w.Buyer == input.BuyerName && w.Supplier == input.SupplierName && w.DocId == input.A3Id);
               
                if (bucketdetails != null)
                {
                    foreach (var pbs in bucketdetails)
                    {
                        if (pbs.Buckets == "Base Rm Rate")
                        {
                            priceImpactDto.BaseRMRate = pbs.Value;
                        }
                        if (pbs.Buckets == "RM Surcharge(Grade Diff)")
                        {
                            priceImpactDto.RMSurchargeGradeDiff = pbs.Value;
                        }
                        if (pbs.Buckets == "Secondary Processing")
                        {
                            priceImpactDto.SecondaryProcessing = pbs.Value;
                        }
                        if (pbs.Buckets == "Surface Protection")
                        {
                            priceImpactDto.SurfaceProtection = pbs.Value;
                        }
                        if (pbs.Buckets == "Thickness")
                        {
                            priceImpactDto.Thickness = pbs.Value;
                        }
                        if (pbs.Buckets == "Cutting Cost")
                        {
                            priceImpactDto.CuttingCost = pbs.Value;
                        }
                        if (pbs.Buckets == "MOQ (Volume)")
                        {
                            priceImpactDto.MOQVolume = pbs.Value;
                        }
                        if (pbs.Buckets == "Transport")
                        {
                            priceImpactDto.Transport = pbs.Value;
                        }
                        if (pbs.Buckets == "Others")
                        {
                            priceImpactDto.Others = pbs.Value;
                        }
                    }

                }
                priceImpactDto.BaseRMRate = priceImpactDto.RevisedCostPer - (priceImpactDto.RMSurchargeGradeDiff + priceImpactDto.SecondaryProcessing + priceImpactDto.SurfaceProtection + priceImpactDto.Thickness + priceImpactDto.CuttingCost + priceImpactDto.MOQVolume + priceImpactDto.Transport + priceImpactDto.Others);

                rmPrice1.Add(priceImpactDto);
            }

            return new GetPriceImpactList(rmPrice1);
        }
        

        public GetPriceImpactList GetSubPartRMPriceImpact(Masters.Dto.GetA3DashboardDataInput input)
        {
            var items = _partRepository.GetAllSubPartRMPriceImpact(input);
            List<GetRMPriceImpact> rmPrice = new List<GetRMPriceImpact>();

            var monthNumbers = new Dictionary<string, int>
                                  {
                                      { "January", 1 },
                                      { "February", 2 },
                                      { "March", 3 },
                                      { "April", 4 },
                                      { "May", 5 },
                                      { "June", 6 },
                                      { "July", 7 },
                                      { "August", 8 },
                                      { "September", 9 },
                                      { "October", 10 },
                                      { "November", 11 },
                                      { "December", 12 },
                                   };

            int slno = 1;
            foreach (var item in items.Result)
            {
                item.slno = slno++.ToString();

                var bucketdetails = _partBucketRepository.GetAll().Where(w => w.RMSpec == item.RawMaterialGrade && w.Buyer == input.BuyerName && w.Supplier == input.SupplierName);

                string SettledDate = item.Settleddate;
                
                if (item.Settleddate == null || item.Settleddate == "")
                {
                    SettledDate = DateTime.Now.ToString();
                }
                DateTime Sdate = DateTime.ParseExact(SettledDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string SmonthName = Sdate.ToString("MMMM");
                string SyearName = Sdate.ToString("yyyy");

                var Sbucketdetails = _partBucketRepository.GetAll()
                .AsEnumerable()
               .Where(w => w.RMSpec == item.RawMaterialGrade &&
               w.Buyer == input.BuyerName &&
               w.Supplier == input.SupplierName &&
			   new DateTime(int.Parse(w.Year), monthNumbers[w.Month], 1) <= new DateTime(int.Parse(SyearName), monthNumbers[SmonthName], 1)
			   ).OrderByDescending(w => w.Id)
                .Take(9);

                if (bucketdetails != null)
                {
                    foreach (var pbs in bucketdetails)
                    {
                        if (pbs.Buckets == "Base Rm Rate")
                        {
                            item.BaseRMRate = pbs.Value;
                            item.BaseRMRate = pbs.Value;
                        }
                        if (pbs.Buckets == "RM Surcharge(Grade Diff)")
                        {
                            item.RMSurchargeGradeDiff = pbs.Value;
                        }
                        if (pbs.Buckets == "Secondary Processing")
                        {
                            item.SecondaryProcessing = pbs.Value;
                        }
                        if (pbs.Buckets == "Surface Protection")
                        {
                            item.SurfaceProtection = pbs.Value;
                        }
                        if (pbs.Buckets == "Thickness")
                        {
                            item.Thickness = pbs.Value;
                        }
                        if (pbs.Buckets == "Cutting Cost")
                        {
                            item.CuttingCost = pbs.Value;
                        }
                        if (pbs.Buckets == "MOQ (Volume)")
                        {
                            item.MOQVolume = pbs.Value;
                        }
                        if (pbs.Buckets == "Transport")
                        {
                            item.Transport = pbs.Value;
                        }
                        if (pbs.Buckets == "Others")
                        {
                            item.Others = pbs.Value;
                        }
                    }

                }
                item.BaseRMRate = item.CurrentCostPer - (item.SecondaryProcessing + item.SurfaceProtection + item.Thickness + item.CuttingCost + item.MOQVolume + item.Transport + item.Others);

                string Reviseddate = item.Reviseddate;

                if (item.Reviseddate == null || item.Reviseddate == "")
                {
                    Reviseddate = DateTime.Now.ToString();
                }
                DateTime Rdate = DateTime.ParseExact(Reviseddate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string RmonthName = Rdate.ToString("MMMM");
                string RyearName = Rdate.ToString("yyyy");

                var Rbucketdetails = _partBucketRepository.GetAll()
                 .AsEnumerable()
                 .Where(w => w.RMSpec == item.RawMaterialGrade && w.Buyer == input.BuyerName && w.Supplier == input.SupplierName && new DateTime(int.Parse(w.Year), monthNumbers[w.Month], 1) <= new DateTime(int.Parse(SyearName), monthNumbers[SmonthName], 1)).OrderByDescending(w => w.Id)
                 .Take(9); 

                if (Rbucketdetails != null)
                {
                    foreach (var pbs in Rbucketdetails)
                    {
                        if (pbs.Buckets == "Base Rm Rate")
                        {
                            item.RevBaseRMRate = pbs.Value;
                        }
                        if (pbs.Buckets == "RM Surcharge(Grade Diff)")
                        {
                            item.RevRMSurchargeGradeDiff = pbs.Value;
                        }
                        if (pbs.Buckets == "Secondary Processing")
                        {
                            item.RevSecondaryProcessing = pbs.Value;
                        }
                        if (pbs.Buckets == "Surface Protection")
                        {
                            item.RevSurfaceProtection = pbs.Value;
                        }
                        if (pbs.Buckets == "Thickness")
                        {
                            item.RevThickness = pbs.Value;
                        }
                        if (pbs.Buckets == "Cutting Cost")
                        {
                            item.RevCuttingCost = pbs.Value;
                        }
                        if (pbs.Buckets == "MOQ (Volume)")
                        {
                            item.RevMOQVolume = pbs.Value;
                        }
                        if (pbs.Buckets == "Transport")
                        {
                            item.RevTransport = pbs.Value;
                        }
                        if (pbs.Buckets == "Others")
                        {
                            item.RevOthers = pbs.Value;
                        }
                    }

                }
                item.RevBaseRMRate = item.RevisedCostPer - (item.RevSecondaryProcessing + item.RevSurfaceProtection + item.RevThickness + item.RevCuttingCost + item.RevMOQVolume + item.RevTransport + item.RevOthers);


                rmPrice.Add(ObjectMapper.Map<GetRMPriceImpact>(item));


            }
            return new GetPriceImpactList(rmPrice);
        }

        public GetHasMixture GetPartHasMixture(Masters.Dto.GetA3DashboardDataInput input)
        {
            var hasMixture = false;

            if (!String.IsNullOrEmpty(input.Buyer) && !String.IsNullOrEmpty(input.Supplier))
            {
                var result = _Part.GetAll()
                            .Include(e => e.SupplierFk)
                            .Include(e => e.BuyerFk)
                            .Include(e => e.RMGroupFk)
                            .Include(e => e.RMGradeFk)
                            .Where(w => w.BuyerId == Int32.Parse(input.Buyer) && w.SupplierId == Int32.Parse(input.Supplier) && w.RMGroupFk.HasMixture == true);

                var totalRecord = result.CountAsync();
                hasMixture = totalRecord.Result > 0 ? true : false;
            }
            return new GetHasMixture( hasMixture);
        
        }

        public List<Year> GetYears()
        {
            var years = _lookup_yearRepository.GetAll();
            var result = from o in years.OrderBy(year => year.SeqNo)
                         select new Year()
                         {
                             SeqNo=o.SeqNo,
                             Name = o.Name
                         };

            return result.Distinct().ToList();
        }

        public List<string> GetDepartments()
        {
            var distinctDepartments = _buyerrepository.GetAll()
                .Where(buyer => buyer.Department != null)
                .Select(buyer => buyer.Department)
                .Distinct()
                .OrderBy(department => department == "TT/GI-3" ? 0 : 1)
                .ThenBy(department => department)
                .ToList();

            return distinctDepartments;
        }

        public List<Buyer> GetBuyersFromDepartment(string department)
        {
            var buyers = _buyerrepository.GetAll()
                .Where(buyer => buyer.Department == department)
                .ToList();

            return buyers;
        }

        public List<string> GetBuyersFromSupplier(int? supplierid)
        {
            if (supplierid == 0)
            {
                supplierid = null;
            }
            var query = from sbm in _supplierBuyerMap.GetAll()
                        where (supplierid == null || sbm.SupplierId == supplierid)
                        select sbm.BuyerName;

            if (supplierid == null)
            {
                return query.Distinct().ToList();
            }
            else
            {
                return query.ToList();
            }

        }

        public List<RawMaterialGrade> Getgroup()
        {
            var query = from rmg1 in _rmGroupRepository.GetAll()
                        where _rmGroupRepository.GetAll().Any(rmg2 => rmg2.IsGroup == true && rmg1.Id == rmg2.RawMaterialGradeId) &&
                              rmg1.RawMaterialGradeId == null
                        select rmg1;

            var result = query.ToList();

            return result;

        }

        public List<RawMaterialGrade> getallgrade(int? GradeId)
        {
            var gradequery = from rmg1 in _rmGroupRepository.GetAll().Where(w => w.RawMaterialGradeId == GradeId)
                             select rmg1;
            var result = gradequery.ToList();

            return result;

        }
        public List<RawMaterialGrade> GetAllSpec(int? GroupId, int? GradeId)
        {
            List<RawMaterialGrade> result;

            if (GroupId == 47)
            {
                var distinctRawMaterialGradesWithMixtures = (
                    from rg in _rmGroupRepository.GetAll()
                    join rm in _rawmaterialmixtureRepository.GetAll() on rg.Id equals rm.RawMaterialGradeId
                    join br in _baseRMRateRepository.GetAll() on rg.Id equals br.RMGroupId
                    where rm.RMGroupId == GradeId
                    select rg
                ).Distinct();

                result = distinctRawMaterialGradesWithMixtures.ToList();
            }
            else
            {
                var distinctRawMaterialGradesWithBaseRates = (
                    from rg in _rmGroupRepository.GetAll()
                    join br in _baseRMRateRepository.GetAll() on rg.Id equals br.RMGroupId
                    where rg.RawMaterialGradeId == GradeId
                    select rg
                ).Distinct();

                result = distinctRawMaterialGradesWithBaseRates.ToList();
            }

            return result;
        }

        public List<Plant> GetPlants()
        {

            List<Plant> plants;
            //var logedinUser = AbpSession.GetUserId();

            plants = _plantrepository.GetAll().ToList();

            return plants.Distinct().ToList();


            //var result = (from p in _Part.GetAll()
            //              join gd in _globusdataRepository.GetAll() on p.PartNo equals gd.PartNo
            //              join pt in _plantrepository.GetAll() on gd.PlantCode equals pt.Code
            //              where p.BuyerId == Convert.ToInt32(input.Buyer) && p.SupplierId == Convert.ToInt32(input.Supplier)
            //              select new Plant
            //              {
            //                  Id = pt.Id,
            //                  Code = pt.Code,
            //                  Description = pt.Description
            //              }).Distinct().ToList();



            //return result;


        }
        public List<RawMaterialGrade> GetGroupByBuyer(Masters.Dto.GetA3DashboardDataInput input)
        {
            var query = (from p in _Part.GetAll()
             .Include(e => e.SupplierFk)
             .Include(e => e.BuyerFk)
             .WhereIf(!string.IsNullOrWhiteSpace(input.Supplier), e => e.SupplierFk != null && e.SupplierFk.Id.ToString() == input.Supplier)
             .WhereIf(!string.IsNullOrWhiteSpace(input.Buyer), e => e.BuyerFk != null && e.BuyerFk.Id.ToString() == input.Buyer)
                         join r in _lookup_rmGroupRepository.GetAll() on p.RMGroupId equals r.Id
                         join rw in _lookup_rmGroupRepository.GetAll() on r.RawMaterialGradeId equals rw.Id
                         where (string.IsNullOrWhiteSpace(input.Buyer) || p.BuyerId == int.Parse(input.Buyer)) &&
                               (string.IsNullOrWhiteSpace(input.Supplier) || p.SupplierId == int.Parse(input.Supplier))
                         select new RawMaterialGrade()
                         {
                             Id = (int)r.RawMaterialGradeId,
                             Name = rw.Name
                         })
             .Distinct()
             .ToList();

            return query;
        }
        public List<Supplier> getSupplierforFilter()
        {
            var parts = _Part.GetAll()
                        .Include(e => e.SupplierFk);
            var result = from o in parts
                         orderby(o.SupplierFk.Name)
                         select new Supplier()
                         {
                             Id = o.SupplierFk.Id,
                             Name = o.SupplierFk.Name,
                             Code=o.SupplierFk.Code
                         };
            return result.Distinct().OrderBy(r=>r.Name).ToList();
        }
        public List<SupplierBuyerMap> getSupplierfromDept(string department)
        {
            var query = from p in _partRepository.GetAll()
                        join b in _buyerrepository.GetAll() on p.BuyerId equals b.Id
                        join sp in _supplierBuyerMap.GetAll() on  p.SupplierId  equals  sp.SupplierId 
                        where (department == null || b.Department == department)
                        group new { p, sp } by new { p.BuyerId, p.SupplierId, sp.SupplierName, sp.SupplierCode } into grp
                        select new SupplierBuyerMap 
                        {
                            SupplierId = (int)grp.Key.SupplierId,
                            SupplierName = grp.Key.SupplierName,
                            SupplierCode = grp.Key.SupplierCode
                        };
            return query.ToList();
        }

        public List<Buyer> getBuyer()
        {
            //User user = _userManager.GetUser(AbpSession.ToUserIdentifier());
            List<Buyer> buyers;
            var logedinUser = AbpSession.GetUserId();

            if (logedinUser == 2 || logedinUser == 1 || logedinUser == 108 || logedinUser == 109)
            {
                buyers = _buyerrepository.GetAll().ToList();
            }
            else
            {
                buyers = _buyerrepository.GetAll().ToList();

                List<Buyer> selectedbuyers = new List<Buyer>();
                foreach (var buyer in buyers)
                {
                    if (buyer.UserId == logedinUser)
                        selectedbuyers.Add(buyer);
                    
                }
                return selectedbuyers;
            }
            return buyers;
        }
        public List<RawMaterialGrade> getGrade(Masters.Dto.GetA3DashboardDataInput input)
        {
            //List<RawMaterialGrade> groups;
            //var logedinUser = AbpSession.GetUserId();

            //groups = _rmGroupRepository.GetAll().ToList();

            var parts = _Part.GetAll()
                        .Include(e => e.SupplierFk)
                        .Include(e => e.BuyerFk)
                        .Include(e => e.RMGradeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Supplier), e => e.SupplierFk != null && e.SupplierFk.Id.ToString() == input.Supplier)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Buyer), e => e.BuyerFk != null && e.BuyerFk.Id.ToString() == input.Buyer);
            //var subparts = _subPart.GetAll()
            //            .Include(e => e.SupplierFk)
            //            .Include(e => e.BuyerFk)
            //            .Include(e => e.RMGradeFk)
            //            .WhereIf(!string.IsNullOrWhiteSpace(input.Supplier), e => e.SupplierFk != null && e.SupplierFk.Id.ToString() == input.Supplier)
            //            .WhereIf(!string.IsNullOrWhiteSpace(input.Buyer), e => e.BuyerFk != null && e.BuyerFk.Id.ToString() == input.Buyer);

           
            var result = from o in parts.Where(w => w.RMGroupId != null )
                         select new RawMaterialGrade()
                         {
                             Id = o.RMGroupFk.Id,
                             Name = o.RMGroupFk.Name
                         };

            //if (result == null)
            //{
            //    result = from o in subparts
            //             select new RawMaterialGrade()
            //             {
            //                 Id = o.RMGroupFk.Id,
            //                 Name = o.RMGroupFk.Name
            //             };


            //}
            //else 
            //{
            //    result = result.Concat(from o in subparts
            //                           select new RawMaterialGrade()
            //                           {
            //                               Id = o.RMGroupFk.Id,
            //                               Name = o.RMGroupFk.Name
            //                           });
            //}
               

            

            return result.Distinct().ToList();
        }

        
        public List<RawMaterialGrade> getSpec(Masters.Dto.GetA3DashboardDataInput input)   
        {
            
            //List<RawMaterialGrade> groups;
            //var logedinUser = AbpSession.GetUserId();

            //groups = _rmGroupRepository.GetAll().ToList();


            // in partmaster and subpartmaster grade=group,spec=grade
            var parts = _Part.GetAll()
                        .Include(e => e.SupplierFk)
                        .Include(e => e.BuyerFk)
                        .Include(e => e.RMGroupFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Supplier), e => e.SupplierFk != null && e.SupplierFk.Id.ToString() == input.Supplier)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Buyer), e => e.BuyerFk != null && e.BuyerFk.Id.ToString() == input.Buyer)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Grade), e => e.RMGroupFk != null && e.RMGroupFk.Id.ToString() == input.Grade);
            
            //var subParts = _subPart.GetAll()
            //            .Include(e => e.SupplierFk)
            //            .Include(e => e.BuyerFk)
            //            .Include(e => e.RMGroupFk)
            //            .WhereIf(!string.IsNullOrWhiteSpace(input.Supplier), e => e.SupplierFk != null && e.SupplierFk.Id.ToString() == input.Supplier)
            //            .WhereIf(!string.IsNullOrWhiteSpace(input.Buyer), e => e.BuyerFk != null && e.BuyerFk.Id.ToString() == input.Buyer)
            //            .WhereIf(!string.IsNullOrWhiteSpace(input.Grade), e => e.RMGroupFk != null && e.RMGroupFk.Id.ToString() == input.Grade);


            var result = from o in parts.Where(w=>w.RMGroupId != null)
                         select new RawMaterialGrade()
                         {
                             Id = o.RMGradeFk.Id,
                             Name = o.RMGradeFk.Name
                         };


            //if (result == null)
            //{
            //    result = from o in subParts
            //             select new RawMaterialGrade()
            //             {
            //                 Id = o.RMGradeFk.Id,
            //                 Name = o.RMGradeFk.Name
            //             };


            //}
            //else
            //{
            //    result = result.Concat(from o in subParts
            //                           select new RawMaterialGrade()
            //                           {
            //                               Id = o.RMGradeFk.Id,
            //                               Name = o.RMGradeFk.Name
            //                           });

            //}
                


            return result.Distinct().ToList();
        }

        public List<Plant> getPlant(Masters.Dto.GetA3DashboardDataInput input)
        {
            
            List<Plant> plants;
            var logedinUser = AbpSession.GetUserId();

            //plants = _plantrepository.GetAll().ToList();

            //return plants.Distinct().ToList();


            var result = (from p in _Part.GetAll()
                          join gd in _globusdataRepository.GetAll() on p.PartNo equals gd.PartNo
                          join pt in _plantrepository.GetAll() on gd.PlantCode equals pt.Code
                          where p.BuyerId == Convert.ToInt32(input.Buyer) && p.SupplierId == Convert.ToInt32(input.Supplier)
                          select new Plant
                          {
                              Id = pt.Id,
                              Code = pt.Code,
                              Description = pt.Description
                          }).Distinct().ToList();

            

            return result;

            
        }

        public List<SupplierBuyerMap> getSupplier(int buyerid)
        {
            var suppliers = _supplierBuyerMap.GetAll().Where(w => w.BuyerId == buyerid).ToList();
            return suppliers;
        }

        public DateToStringOutput SendAndGetDate(DateTime? date)
        {
            return new DateToStringOutput
            {
                DateString = date?.ToString("d")
            };
        }

        public async Task UpdateDateRangeSettings(string input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                AppSettings.DashboardCustomization.Configuration + "." + "DateRange",
                input
            );
        }

        public async Task UpdateSupplierSettings(string input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                AppSettings.DashboardCustomization.Configuration + "." + "Supplier",
                input
            );
        }
        public async Task UpdateGradeSettings(string input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                AppSettings.DashboardCustomization.Configuration + "." + "Grade",
                input
            );
        }
        public async Task UpdateGroupSettings(string input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                AppSettings.DashboardCustomization.Configuration + "." + "Group",
                input
            );
        }

        public async Task UpdateBuyerSettings(string input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                AppSettings.DashboardCustomization.Configuration + "." + "Buyer",
                input
            );
        }

        public async Task UpdatePlantSettings(string input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                AppSettings.DashboardCustomization.Configuration + "." + "Plant",
                input
            );
        }

        public async Task<string> GetDateRangeSettingsAsync()
        {
            return await SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "DateRange");
        }

        public async Task<string> GetSupplierSettingsAsync()
        {
            return await SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "Supplier");
        }

        public async Task<string> GetBuyerSettingsAsync()
        {
            return await SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "Buyer");
        }

        public async Task<string> GetGradeSettingsAsync()
        {
            return await SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "Grade");
        }

        public async Task<string> GetGroupSettingsAsync()
        {
            return await SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "Group");
        }

        public async Task<string> GetPlantSettingsAsync()
        {
            return await SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "Plant");
        }

        public List<LeadModelDto> GetA3LeadModel(Masters.Dto.GetA3DashboardDataInput input)
        {
            var A3LMHeader = _a3LeadModelRepository.GetAll().Where(w => w.DocId == input.A3Id).OrderBy(o => o.Id);
			
			List<LeadModelDto> leadModelDtos = new List<LeadModelDto>();

			foreach (var item in A3LMHeader)
            {
                leadModelDtos.Add(new LeadModelDto
                {
                             DocId= item.DocId,
                             Name=item.Name,
                             Description=item.Description,
                });

			}
            return leadModelDtos;

		}
		public List<LeadModelDto> GetLeadModel(Masters.Dto.GetA3DashboardDataInput input)


		{

			var LMHeader = _leadModelRepository.GetAll().OrderBy(o => o.Id);
			List<LeadModelDto> leadModelDtos = new List<LeadModelDto>();

			foreach (var item in LMHeader)
			{
				leadModelDtos.Add(new LeadModelDto
				{
					
					Name = item.Name,
					Description = item.Description,
				});

			}

            return leadModelDtos;

		}

        public async Task<FileDto> GetA3ToExcel(Masters.Dto.GetA3DashboardDataInput input)
        {
            var path = _appConfiguration.GetTemplatePath();

            var priceTrends = GetSupplierRMPriceTrend(input);
            var priceImpacts = GetSupplierRMPriceImpact(input);
            decimal RmImpactTotal = 0;
            decimal processimpactTotal = 0;
            decimal rmImpacttTotal = 0;
            foreach (var item in priceImpacts.PriceImpacts)
            {
                if ( item.SubPart==false && item.SubMixture == false)
                {
                    
                    RmImpactTotal = RmImpactTotal + item.RMImpact;
                    processimpactTotal = processimpactTotal + item.ProcessImpact;
                    rmImpacttTotal = rmImpacttTotal + item.RMImpactt;
                }
                
            }


                var LMHeader = GetLeadModel(input);

            var A3Doc = _a3DocumentRepository.GetAll().Where(w => w.Id == input.A3Id).FirstOrDefault();

            var query = from LeadModel in _leadModelRepository.GetAll()
                        join PartModelMatrix in _partModelMatrixRepository.GetAll() on LeadModel.Id equals PartModelMatrix.LeadModelId
                        join PartNUmber in _Part.GetAll().Where(w => w.BuyerId.ToString() == input.Buyer && w.SupplierId.ToString() == input.Supplier) on PartModelMatrix.PartNumber equals PartNUmber.PartNo

                        select new PartLeadModelMatrixDto()
                        {
                            PartNo = PartNUmber.PartNo,
                            PartDespn = PartNUmber.Description,
                            LeadModelId = LeadModel.Id,
                            Quantity = PartModelMatrix.Quantity,


                        };
            
                 List<PartLeadModelMatrixDto> PartModel = new List<PartLeadModelMatrixDto>(query);

            var BuyerPartNumbersCount = PartModel.Where(x => x.PartNo != null).GroupBy(g=> g.PartNo).Count();

            var A3PriceImp = from A3 in priceImpacts.PriceImpacts
							 group A3 by new { A3.PartNo, A3.CurrentExwPrice, A3.RevisedExwPrice } into A3Imp
							 select new
							 {
								 PartNo = A3Imp.Key.PartNo,
								 CurrentP = A3Imp.Key.CurrentExwPrice,
								 RevisedP = A3Imp.Key.RevisedExwPrice
							 };

           

			List<LeadModelGraphDto> LeadModGraph = new List<LeadModelGraphDto>();

            var partMod = from O in A3PriceImp 
                          join PM in _partModelMatrixRepository.GetAll() on O.PartNo equals PM.PartNumber
							  join  lM in _leadModelRepository.GetAll() on PM.LeadModelId equals lM.Id


							  select new LeadModelGraphDto
							  {
								  LeadModelName = lM.Name,
								  CurrentRM = O.CurrentP * PM.Quantity,
								  RevisedRM = O.RevisedP * PM.Quantity

							  };

            var newProdMod = from a in partMod
                             group a by a.LeadModelName into a1

                             select new LeadModelGraphDto
                             {
                                 LeadModelName = a1.Key,
                                 CurrentRM = a1.Sum(s => s.CurrentRM),
                                 RevisedRM = a1.Sum(s => s.RevisedRM)
                             };

            LeadModGraph = new List<LeadModelGraphDto>(newProdMod);



            return _A3ExcelExporter.ExportToFile(priceTrends.PriceTrends, priceImpacts.PriceImpacts, LMHeader, PartModel.OrderBy(o => (o.PartNo, o.LeadModelId)).ToList(), LeadModGraph,BuyerPartNumbersCount, input.SupplierName, input.BuyerName, path + input.TemplatePath, A3Doc, RmImpactTotal, processimpactTotal, rmImpacttTotal);
        }

        public async Task<FileDto> GetPartBucketToExcel(Masters.Dto.GetA3DashboardDataInput input)
        {
            var path = _appConfiguration.GetTemplatePath();
            string RmonthName = "";
            string RyearName = "";
            string SmonthName = "";
            string SyearName = "";


            var priceTrends = GetSupplierRMPriceTrend(input);
            List<GetRMPriceTrend> rmPrice = new List<GetRMPriceTrend>();
          


            var monthNumbers = new Dictionary<string, int>
                                  {
                                      { "January", 1 },
                                      { "February", 2 },
                                      { "March", 3 },
                                      { "April", 4 },
                                      { "May", 5 },
                                      { "June", 6 },
                                      { "July", 7 },
                                      { "August", 8 },
                                      { "September", 9 },
                                      { "October", 10 },
                                      { "November", 11 },
                                      { "December", 12 },
                                   };


            int slno = 1;
            int subpartslno = 1;
            var slnocom = "1";
            int submix = 1;

            foreach (var item in priceTrends.PriceTrends)
            {

                string SettledDate = item.SetteledMY;
                if (item.SetteledMY == null || item.SetteledMY == "")
                {
                    string datetimeString = DateTime.Now.ToString();
                    DateTime datetimeValue = DateTime.Parse(datetimeString);
                    DateTime dateOnly = datetimeValue.Date;
                    string dateString = dateOnly.ToString("dd-MM-yyyy"); 


                    SettledDate = dateString;
                }

                DateTime Sdate = DateTime.ParseExact(SettledDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                SmonthName = Sdate.ToString("MMMM");
                SyearName = Sdate.ToString("yyyy");

                var Sbucketdetails = _partBucketRepository.GetAll()
                .AsEnumerable()
               .Where(w => w.RMSpec == item.RMGrade &&
               w.BuyerId == Int32.Parse(input.Buyer) &&
               w.SupplierId == null || w.SupplierId == 0 || w.SupplierId == Int32.Parse(input.Supplier)
               &&
               (new DateTime(int.Parse(w.Year), monthNumbers[w.Month], 1) <= new DateTime(int.Parse(SyearName), monthNumbers[SmonthName], 1))
			   ).OrderByDescending(w => w.Id)
                .Take(9);

                if (Sbucketdetails != null)
                {
                    foreach (var pbs in Sbucketdetails)
                    {
                        if (pbs.Buckets == "Base Rm Rate")
                        {
                            item.BaseRMRate = pbs.Value;
                        }
                        if (pbs.Buckets == "RM Surcharge(Grade Diff)")
                        {
                            item.RMSurchargeGradeDiff = pbs.Value;
                        }
                        if (pbs.Buckets == "Secondary Processing")
                        {
                            item.SecondaryProcessing = pbs.Value;
                        }
                        if (pbs.Buckets == "Surface Protection")
                        {
                            item.SurfaceProtection = pbs.Value;
                        }
                        if (pbs.Buckets == "Thickness")
                        {
                            item.Thickness = pbs.Value;
                        }
                        if (pbs.Buckets == "Cutting Cost")
                        {
                            item.CuttingCost = pbs.Value;
                        }
                        if (pbs.Buckets == "MOQ (Volume)")
                        {
                            item.MOQVolume = pbs.Value;
                        }
                        if (pbs.Buckets == "Transport")
                        {
                            item.Transport = pbs.Value;
                        }
                        if (pbs.Buckets == "Others")
                        {
                            item.Others = pbs.Value;
                        }
                    }

                }
                                                
                rmPrice.Add(ObjectMapper.Map<GetRMPriceTrend>(item));
            }

            return _IPartBucketsExcelExporter.ExportToFile(rmPrice, input.SupplierName, input.BuyerName, SmonthName, SyearName, RmonthName, RyearName, path + input.TemplatePath);
        }

        public async Task<FileDto> GetA3ToExcelFromDoc(Masters.Dto.GetA3DashboardDataInput input)
        {
            var path = _appConfiguration.GetTemplatePath();
            var priceTrends = GetA3RMPriceTrend(input);
            var priceImpacts = GetA3RMPriceImpact(input);
			var A3LMHeader = GetLeadModel(input);

            decimal RmImpactTotal = 0;
            decimal processimpactTotal = 0;
            decimal rmImpacttTotal = 0;
            foreach (var item in priceImpacts.PriceImpacts)
            {
                if (item.SubPart == false && item.SubMixture == false)
                {

                    RmImpactTotal = RmImpactTotal + item.RMImpact;
                    processimpactTotal = processimpactTotal + item.ProcessImpact;
                    rmImpacttTotal = rmImpacttTotal + item.RMImpactt;
                }

            }


            var A3PriceImp = from A3 in _a3PriceImpact.GetAll().Where(w => w.DocId == input.A3Id)
                             group A3 by new { A3.PartNo, A3.CurrentExwPrice, A3.RevisedExwPrice } into A3Imp
                             select new
                             {
                                 PartNo = A3Imp.Key.PartNo,
                                 CurrentP = A3Imp.Key.CurrentExwPrice,
                                 RevisedP = A3Imp.Key.RevisedExwPrice
							 };

            List<LeadModelGraphDto> LeadModGraph = new List<LeadModelGraphDto>();
                    
                var partMod = from O in A3PriceImp
							 join A3PM in _a3PartModelMatrixRepository.GetAll().Where(w=>w.DocId==input.A3Id) on O.PartNo equals A3PM.PartNumber
							 join A3lM in _a3LeadModelRepository.GetAll().Where(w => w.DocId == input.A3Id) on A3PM.LeadModelId equals A3lM.LeadModelFk.Id


                              select new LeadModelGraphDto
                              {
                                  LeadModelName = A3lM.Name,
                                  CurrentRM = (Decimal)O.CurrentP * A3PM.Quantity,
                                  RevisedRM = (Decimal)O.RevisedP * A3PM.Quantity

                              };

            var newProdMod = from a in partMod
                                 group a by a.LeadModelName into a1
                                 
                                 select new LeadModelGraphDto
                                 {
                                     LeadModelName = a1.Key,
                                     CurrentRM = a1.Sum(s => s.CurrentRM),
                                     RevisedRM = a1.Sum(s => s.RevisedRM)
                                 };

				LeadModGraph = new List<LeadModelGraphDto>(newProdMod);


			var query = from PartModelMatrix in _a3PartModelMatrixRepository.GetAll().Where(w => w.DocId == input.A3Id) 


                        select new PartLeadModelMatrixDto()
                        {
                            PartNo = PartModelMatrix.PartNumber,
                            PartDespn = PartModelMatrix.Description,
                            LeadModelId = PartModelMatrix.Id,
                            Quantity = PartModelMatrix.Quantity,


                        };


            List<PartLeadModelMatrixDto> PartModel = new List<PartLeadModelMatrixDto>(query);

                var BuyerPartNumbersCount = PartModel.GroupBy(g => g.PartNo).Count();


            var A3Doc = _a3DocumentRepository.GetAll().Where(w => w.Id == input.A3Id).FirstOrDefault();

            return _A3ExcelExporter.ExportToFile(priceTrends.PriceTrends, priceImpacts.PriceImpacts, A3LMHeader, PartModel.OrderBy(o=> (o.PartNo,o.LeadModelId)).ToList(), LeadModGraph, BuyerPartNumbersCount,input.Supplier, input.Buyer, path + input.TemplatePath, A3Doc, RmImpactTotal, processimpactTotal, rmImpacttTotal);
        }




        public async Task RMsheetRetriggerEmail(SendTestEmailInput iput)
        {
            Masters.Dto.GetA3DashboardDataInput input = new Masters.Dto.GetA3DashboardDataInput();


            input.A3Id = iput.A3Id;
            input.Buyer = iput.Buyer;
            input.BuyerName = iput.BuyerName;
            input.Supplier = iput.Supplier;
            input.SupplierName = iput.SupplierName;
            input.TemplatePath = iput.TemplatePath;
            input.Plant = iput.Plant;
            input.Period = iput.Period;


            FileDto excelFile = await GetA3ToExcelFromDoc(input);


            var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync((int)input.A3Id);

            var user = await UserManager.GetUserByIdAsync((long)_abpSession.UserId);


            string L4emaiaddress = user.L4EmailAddress;
            string Cpemailaddress = user.CpEmailAddress;
            string Finemailaddress = user.FinEmailAddress;
            string CommadityExpertEmailAddress = user.CommadityExpertEmailAddress;

            string L4Approvalapppath = GenerateAppPath(iput, a3Document.L4Token,user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string L4Rejectionapppath = GenerateAppPath(iput, a3Document.RL4Token, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string CpApprovalapppath = GenerateAppPath(iput, a3Document.CpToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string CpRejectionapppath = GenerateAppPath(iput, a3Document.RCpToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string FinApprovalapppath = GenerateAppPath(iput, a3Document.FinToken, user.SequenceCheckBox,(int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string FinRejectionapppath = GenerateAppPath(iput, a3Document.RFinToken,user.SequenceCheckBox,(int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string ComExpApprovalapppath = GenerateAppPath(iput, a3Document.CommadityExpertToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string ComExpRejectionapppath = GenerateAppPath(iput, a3Document.RCommadityExpertToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);


            if (a3Document.L4Status == "Awaiting For Approval")
            {
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, L4emaiaddress, "Request Approval for RM Amendment - " + excelFile.FileName, L4Approvalapppath, L4Rejectionapppath, excelFile, a3Document.Id, (int)user.TenantId,user.EmailAddress,"SectionHead");

            }
            if (a3Document.CpStatus == "Awaiting For Approval")
            {
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, Cpemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, CpApprovalapppath, CpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId,user.EmailAddress,"CE");

            }

            if (a3Document.FinStatus == "Awaiting For Approval")
            {
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, Finemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, FinApprovalapppath, FinRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId,user.EmailAddress,"Finance");
            }

            if (a3Document.CommadityExpertStatus == "Awaiting For Approval")
            {

                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, CommadityExpertEmailAddress, "Request Approval for RM Amendment - " + excelFile.FileName, ComExpApprovalapppath, ComExpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId,user.EmailAddress,"Commodity");


            }
        }

        public async Task RMsheetResendEmail(SendTestEmailInput iput)
        {

            Masters.Dto.GetA3DashboardDataInput input = new Masters.Dto.GetA3DashboardDataInput();


            input.A3Id = iput.A3Id;
            input.Buyer = iput.Buyer;
            input.BuyerName = iput.BuyerName;
            input.Supplier = iput.Supplier;
            input.SupplierName = iput.SupplierName;
            input.TemplatePath = iput.TemplatePath;
            input.Plant = iput.Plant;
            input.Period = iput.Period;


            FileDto excelFile = await GetA3ToExcelFromDoc(input);


            var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync((int)input.A3Id);

            var user = await UserManager.GetUserByIdAsync((long)_abpSession.UserId);





            string L4emaiaddress = user.L4EmailAddress;
            string Cpemailaddress = user.CpEmailAddress;
            string Finemailaddress = user.FinEmailAddress;
            string CommadityExpertEmailAddress = user.CommadityExpertEmailAddress;

            string L4Approvalapppath = GenerateAppPath(iput, a3Document.L4Token,user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string L4Rejectionapppath = GenerateAppPath(iput, a3Document.RL4Token, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string CpApprovalapppath = GenerateAppPath(iput, a3Document.CpToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string CpRejectionapppath = GenerateAppPath(iput, a3Document.RCpToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string FinApprovalapppath = GenerateAppPath(iput, a3Document.FinToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string FinRejectionapppath = GenerateAppPath(iput, a3Document.RFinToken , user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string ComExpApprovalapppath = GenerateAppPath(iput, a3Document.CommadityExpertToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string ComExpRejectionapppath = GenerateAppPath(iput, a3Document.RCommadityExpertToken, user.SequenceCheckBox, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);


            if (user.SequenceCheckBox == true)
            {
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, L4emaiaddress, "Request Approval for RM Amendment - " + excelFile.FileName, L4Approvalapppath, L4Rejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress, "SectionHead");
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, Cpemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, CpApprovalapppath, CpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress, "CE");
                //await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, Finemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, FinApprovalapppath, FinRejectionapppath, excelFile, a3Document.Id);
                await SendEmail(iput.BuyerName, iput.SupplierName, a3Document.Version, a3Document.Remarks, CommadityExpertEmailAddress, "Request Approval for RM Amendment - " + excelFile.FileName, ComExpApprovalapppath, ComExpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress, "Commodity");



            }
            if (user.SequenceCheckBox == false)
            {
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, L4emaiaddress, "Request Approval for RM Amendment - " + excelFile.FileName, L4Approvalapppath, L4Rejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress,"SectionHead");
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, Cpemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, CpApprovalapppath, CpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId,user.EmailAddress,"CE");
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, Finemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, FinApprovalapppath, FinRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId,user.EmailAddress,"Finance");
                await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, CommadityExpertEmailAddress, "Request Approval for RM Amendment - " + excelFile.FileName, ComExpApprovalapppath, ComExpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId,user.EmailAddress,"Commodity");



            }
        }

       


        public async Task SendRMsheetConfirmationEmail(SendTestEmailInput iput)
        {
            var user = await UserManager.GetUserByIdAsync((long)_abpSession.UserId);

            if (user.SequenceCheckBox == true)
            {
                await SequenceRMsheetConfirmationEmail(iput);

            }
            if (user.SequenceCheckBox == false)
            
            {
               await WithotSequenceRMsheetConfirmationEmail(iput);
            }

        }

        public async Task SequenceRMsheetConfirmationEmail(SendTestEmailInput iput)
        {

            Masters.Dto.GetA3DashboardDataInput input = new Masters.Dto.GetA3DashboardDataInput();


            input.A3Id = iput.A3Id;
            input.Buyer = iput.Buyer;
            input.BuyerName = iput.BuyerName;
            input.Supplier = iput.Supplier;
            input.SupplierName = iput.SupplierName;
            input.TemplatePath = iput.TemplatePath;
            l3templatepath= iput.TemplatePath;
            input.Plant = iput.Plant;
            input.Period = iput.Period;


            FileDto excelFile = await GetA3ToExcelFromDoc(input);


            var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync((int)input.A3Id);

            var user = await UserManager.GetUserByIdAsync((long)_abpSession.UserId);


            string L4emaiaddress = user.L4EmailAddress;
            string Cpemailaddress = user.CpEmailAddress;
            string Finemailaddress = user.FinEmailAddress;
            string CommadityExpertEmailAddress = user.CommadityExpertEmailAddress;

            // Generate unique tokens
            string L4ApprovalToken = Guid.NewGuid().ToString();
            string CpApprovalToken = Guid.NewGuid().ToString();
            string FnApprovalToken = Guid.NewGuid().ToString();
            string CommadityExpertApprovalToken = Guid.NewGuid().ToString();

            string L4RejectionToken = Guid.NewGuid().ToString();
            string CpRejectionToken = Guid.NewGuid().ToString();
            string FnRejectionToken = Guid.NewGuid().ToString();
            string CommadityExpertRejectionToken = Guid.NewGuid().ToString();

            a3Document.L4Token = L4ApprovalToken;
            a3Document.CpToken = CpApprovalToken;
            a3Document.FinToken= FnApprovalToken;
            a3Document.CommadityExpertToken= CommadityExpertApprovalToken;


            a3Document.RL4Token = L4RejectionToken;
            a3Document.RCpToken = CpRejectionToken;
            a3Document.RFinToken= FnRejectionToken;
            a3Document.RCommadityExpertToken= CommadityExpertRejectionToken;

            a3Document.L4Status = "Awaiting For Approval";
            a3Document.CpStatus = "Awaiting For Approval";
            //a3Document.FinStatus = "Awaiting For Approval";
            a3Document.CommadityExpertStatus = "Awaiting For Approval";

            await _a3DocumentRepository.UpdateAsync(a3Document);

            // Generate approval and rejection URLs
            string L4Approvalapppath = GenerateAppPath(iput, L4ApprovalToken,true, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string L4Rejectionapppath = GenerateAppPath(iput, L4RejectionToken,true, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string CpApprovalapppath = GenerateAppPath(iput, CpApprovalToken,true, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string CpRejectionapppath = GenerateAppPath(iput, CpRejectionToken, true, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
           // string FinApprovalapppath = GenerateAppPath(iput, FnApprovalToken, true, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            //string FinRejectionapppath = GenerateAppPath(iput, FnRejectionToken, true, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string ComExpApprovalapppath = GenerateAppPath(iput, CommadityExpertApprovalToken, true, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string ComExpRejectionapppath = GenerateAppPath(iput, CommadityExpertRejectionToken, true   , (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);

            // Send emails to different recipients with respective email bodies
            await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, L4emaiaddress, "Request Approval for RM Amendment - " + excelFile.FileName, L4Approvalapppath, L4Rejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress,"SectionHead");
            await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, Cpemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, CpApprovalapppath, CpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress,"CE");
            await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, CommadityExpertEmailAddress, "Request Approval for RM Amendment - " + excelFile.FileName, ComExpApprovalapppath, ComExpRejectionapppath, excelFile,a3Document.Id, (int)user.TenantId, user.EmailAddress,"Commodity");
        }

          

        public async Task WithotSequenceRMsheetConfirmationEmail(SendTestEmailInput iput)
        {

            Masters.Dto.GetA3DashboardDataInput input = new Masters.Dto.GetA3DashboardDataInput();


            input.A3Id = iput.A3Id;
            input.Buyer = iput.Buyer;
            input.BuyerName = iput.BuyerName;
            input.Supplier = iput.Supplier;
            input.SupplierName = iput.SupplierName;
            input.TemplatePath = iput.TemplatePath;
            l3templatepath = iput.TemplatePath;
            input.Plant = iput.Plant;
            input.Period = iput.Period;


            FileDto excelFile = await GetA3ToExcelFromDoc(input);


            var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync((int)input.A3Id);

            var user = await UserManager.GetUserByIdAsync((long)_abpSession.UserId);


            string L4emaiaddress = user.L4EmailAddress;
            string Cpemailaddress = user.CpEmailAddress;
            string Finemailaddress = user.FinEmailAddress;
            string CommadityExpertEmailAddress = user.CommadityExpertEmailAddress;

            // Generate unique tokens
            string L4ApprovalToken = Guid.NewGuid().ToString();
            string CpApprovalToken = Guid.NewGuid().ToString();
            string FnApprovalToken = Guid.NewGuid().ToString();
            string CommadityExpertApprovalToken = Guid.NewGuid().ToString();

            string L4RejectionToken = Guid.NewGuid().ToString();
            string CpRejectionToken = Guid.NewGuid().ToString();
            string FnRejectionToken = Guid.NewGuid().ToString();
            string CommadityExpertRejectionToken = Guid.NewGuid().ToString();

            a3Document.L4Token = L4ApprovalToken;
            a3Document.CpToken = CpApprovalToken;
            a3Document.FinToken = FnApprovalToken;
            a3Document.CommadityExpertToken = CommadityExpertApprovalToken;


            a3Document.RL4Token = L4RejectionToken;
            a3Document.RCpToken = CpRejectionToken;
            a3Document.RFinToken = FnRejectionToken;
            a3Document.RCommadityExpertToken = CommadityExpertRejectionToken;

            a3Document.L4Status = "Awaiting For Approval";
            a3Document.CpStatus = "Awaiting For Approval";
            a3Document.FinStatus = "Awaiting For Approval";
            a3Document.CommadityExpertStatus = "Awaiting For Approval";

            await _a3DocumentRepository.UpdateAsync(a3Document);

           



            // Generate approval and rejection URLs
            string L4Approvalapppath = GenerateAppPath(iput, L4ApprovalToken,false, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string L4Rejectionapppath = GenerateAppPath(iput, L4RejectionToken, false, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string CpApprovalapppath = GenerateAppPath(iput, CpApprovalToken, false, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string CpRejectionapppath = GenerateAppPath(iput, CpRejectionToken, false, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string FinApprovalapppath = GenerateAppPath(iput, FnApprovalToken, false  , (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string FinRejectionapppath = GenerateAppPath(iput, FnRejectionToken, false, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);
            string ComExpApprovalapppath = GenerateAppPath(iput, CommadityExpertApprovalToken, false, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version)   ;
            string ComExpRejectionapppath = GenerateAppPath(iput, CommadityExpertRejectionToken, false, (int)_abpSession.UserId, a3Document.Remarks, a3Document.Version);

            // Send emails to different recipients with respective email bodies
            await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, L4emaiaddress, "Request Approval for RM Amendment - " + excelFile.FileName, L4Approvalapppath, L4Rejectionapppath, excelFile,a3Document.Id, (int)user.TenantId,user.EmailAddress,"SectionHead");
            await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks,Cpemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, CpApprovalapppath, CpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress,"CE");
            await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, Finemailaddress, "Request Approval for RM Amendment - " + excelFile.FileName, FinApprovalapppath, FinRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress,"Finance");
            await SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, CommadityExpertEmailAddress, "Request Approval for RM Amendment - " + excelFile.FileName, ComExpApprovalapppath, ComExpRejectionapppath, excelFile, a3Document.Id, (int)user.TenantId, user.EmailAddress,"Commodity");
        }


        public async  Task<FileDto> FinRMsheetConfirmationEmail(SendTestEmailInput iput, string comexpapprovalToken, string comexprejToken,int uid)
        {
            var user = await UserManager.GetUserByIdAsync(uid);
            Masters.Dto.GetA3DashboardDataInput input = new Masters.Dto.GetA3DashboardDataInput();


            input.A3Id = iput.A3Id;
            input.Buyer = iput.BuyerName;
            input.BuyerName = iput.BuyerName;
            input.Supplier = iput.SupplierName;
            input.SupplierName = iput.SupplierName;
            input.TemplatePath = iput.TemplatePath;

            input.Plant = iput.Plant;
            input.Period = iput.Period;


            FileDto excelFile = await GetA3ToExcelFromDoc(input);

            return excelFile;

        }



        public string GenerateAppPath(SendTestEmailInput iput, string token,bool issequnce,int uid,string buyerRemarks,string version)
        {
            string appPath = _webUrlService.GetServerRootAddress().EnsureEndsWith('/')+"Account/EmailApproval?" +
                      $"A3Id={iput.A3Id}&Buyername={iput.BuyerName}&Suppliername={iput.SupplierName}&Token={token}&Issequnce={issequnce}&uid={uid}&buyerRemarks={buyerRemarks}&version={version}";
            return appPath;
        }

        public async Task SendEmail(string buyerName, string supplierName, string version,string BuyerRemarks,string recipient, string subject, string approvalAppPath, string rejectionAppPath, FileDto excelFile,int a3Id,int userTenantId,string  useremailaddress,string RecipientDepartment)
        {
            //var user = await UserManager.GetUserByIdAsync(uid);

            var sversion = version.Split('.')[0];

            var supportattachments = _supportAttachmentsRepository.GetAll().Where(w => w.Buyer == buyerName && w.Supplier==supplierName && w.Version== sversion);

			var emailTemplate = GetTitleAndSubTitle(userTenantId, L("RMsheetEmail_Title"), L("RMsheetEmail_SubTitle"));
			var mailMessage = new StringBuilder();

			

			mailMessage.AppendLine("<b style=\"text-align: left;\">" + L("Buyer") + "</b>: " + buyerName +  "<br />");
			mailMessage.AppendLine("<b style=\"text-align: left;\">" + L("Supplier") + "</b>: " + supplierName +"<br />");
			mailMessage.AppendLine("<b style=\"text-align: left;\">" + L("Version") + "</b>: " + version + "<br />");
			mailMessage.AppendLine("<b style=\"text-align: left;\">" + L("BuyerRemarks") + "</b>: " + BuyerRemarks + "<br />");
			mailMessage.AppendLine("<br />");

           

           




            mailMessage.AppendLine("<p>RMA3 Amendment Sheet Attached for your approval</P>");
			mailMessage.AppendLine("<br />");
			mailMessage.AppendLine("<a style=\"" + _emailapproveButtonStyle + "\" href=\"" + approvalAppPath + "&confirmation=Approve" + "\">" + L("Approve") + "</a>");
			mailMessage.AppendLine("<br />");
			mailMessage.AppendLine("<br />");
			mailMessage.AppendLine("<a style=\"" + _emailrejectButtonStyle +  "\" href=\"" + rejectionAppPath + "&confirmation=Reject" + "\">" + L("Reject") + "</a>");
			emailTemplate.Replace("{EMAIL_BODY}", mailMessage.ToString());


			MailMessage message = new MailMessage();
            message.From = new MailAddress(useremailaddress);
            message.To.Add(recipient);
            message.Subject = subject;



            message.IsBodyHtml = true;

            string htmlBody = $@"
            <html>
            <body>
                <h3>Buyer:{buyerName}</h3>
                <h3>Supplier:{supplierName}</h3>
                <h3>Version:{version}</h3>
                <h3>BuyerRemarks:{BuyerRemarks}</h3>
                
                <br>
                <br>
                <p>RMA3 Amendment Sheet Attached for your approval</P>
                <p>
                    <a href=""{approvalAppPath}+&confirmation=Approve""><button style=""background-color: green; color: white;"">Click here to approve the document (APPROVE)</button></a>
                    <br><br>
                    <a href=""{rejectionAppPath}+&confirmation=Reject""><button style=""background-color: red; color: white;"">Click here to reject the document (REJECT)</button></a>
                </p>
         
            <body>


            </html>";



            message.Body = emailTemplate.ToString();






            if (supportattachments != null)
            {
                foreach (var supportattachment in supportattachments)
                {

                    try
                    {
                        byte[] fileBytes = supportattachment.Filebyte;

                        string tempFilePath = Path.GetTempFileName();
                        string tempFile = Path.ChangeExtension(tempFilePath, ".xlsx");

                        System.IO.File.WriteAllBytes(tempFile, supportattachment.Filebyte);


                        Attachment attachment = new Attachment(tempFile);
                        attachment.Name = supportattachment.FileName;
                        message.Attachments.Add(attachment);





                    }
                    catch (Exception ex)
                    {


                    }


                }

            }
           

            var smtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host);
            var smtpPort = await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Port);
            var smtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName);
            var smtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password);
            var smtpEnableSsl = await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.EnableSsl);



            using (WebClient webClient = new WebClient())
            {
                byte[] fileContent = _tempFileCacheManager.GetFile(excelFile.FileToken);

                using (MemoryStream stream = new MemoryStream(fileContent))
                {
                    stream.Position = 0;
                    Attachment attachment = new Attachment(stream, new ContentType(excelFile.FileType));
                    attachment.Name = "SytemGenerated_" + excelFile.FileName;
                    message.Attachments.Add(attachment);

                    using (SmtpClient smtpClient = new SmtpClient(smtpHost, int.Parse(smtpPort)))
                    {
                        smtpClient.EnableSsl = bool.Parse(smtpEnableSsl);
                        smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);

                        await smtpClient.SendMailAsync(message);
                    }

                    //using (SmtpClient smtpClient = new SmtpClient("mail.teamsybergate.com", 587))
                    //{
                    //    smtpClient.EnableSsl = true;
                    //    smtpClient.Credentials = new NetworkCredential("dhivakar.p@teamsybergate.com", "sgtpl@12345");

                    //    await smtpClient.SendMailAsync(message);
                    //}
                }
            }

            if (RecipientDepartment != "Approval-Fin")
            {
				using (var uow = _unitOfWorkManager.Begin())
				{
					using (CurrentUnitOfWork.SetTenantId((int?)_abpSession.UserId))
					{
						AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
							AbpSession.ToUserIdentifier(),
							new LocalizableString("Mail Sent to " + recipient + " Successfully", RMACTConsts.LocalizationSourceName),
							null,
							Abp.Notifications.NotificationSeverity.Success));
					}
					uow.Complete();
				}
			}
            
        }


        public async Task RejectionEmailToBuyer (int a3document,string recipient,string subject, int uid,string l4approver,string cpApprover,string finapprover,string comexpapprover,string l4status,string cpstatus,string finstatus,string comexpstatus,string buyer,string supplier,string version,string remarks, string l4remarks, string cpremarks, string finremarks, string comexpremarks)
        {
           // var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync((int)a3document);

            var emailTemplate = GetTitleAndSubTitle(uid, "", "");
            var mailMessage = new StringBuilder();

            DateTime currentTime = DateTime.Now;
            var emailContent = $@"
<table class=""v1MsoNormalTable"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""border-collapse: collapse;"">
    <tbody>
        <tr>
            <td style=""border: solid #666666 1.0pt; background: #DEDEDE; padding: 6.0pt 6.0pt 6.0pt 6.0pt;"" align=""center"">
                <p class=""v1MsoNormal"" style=""color: black;"">Name</p>
            </td>
            <td style=""border: solid #666666 1.0pt; border-left: none; background: #DEDEDE; padding: 6.0pt 6.0pt 6.0pt 6.0pt;"" align=""center"">
                <p class=""v1MsoNormal"" style=""color: black;"">Department</p>
            </td>
<td style=""border: solid #666666 1.0pt; border-left: none; background: #DEDEDE; padding: 6.0pt 6.0pt 6.0pt 6.0pt;"" align=""center"">
                <p class=""v1MsoNormal"" style=""color: black;"">Status</p>
            </td>
            <td style=""border: solid #666666 1.0pt; border-left: none; background: #DEDEDE; padding: 6.0pt 6.0pt 6.0pt 6.0pt;"" align=""center"">
                <p class=""v1MsoNormal"" style=""color: black;"">Remarks</p>

            </td>
            
        </tr>
        <tr>
            <td style=""border: solid black 1.0pt; border-top: none; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{l4approver}</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">Section-Head</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{l4status}</p>
            </td>
<td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{l4remarks}</p>
            </td>

        </tr>
        <tr>
            <td style=""border: solid black 1.0pt; border-top: none; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{cpApprover}</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">CE</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{cpstatus}</p>
            </td>
<td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{cpremarks}</p>
            </td>

            <!-- Add other data cells here -->
        </tr>
        <tr>
            <td style=""border: solid black 1.0pt; border-top: none; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{comexpapprover}</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">Commodity Expert</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{comexpstatus}</p>
            </td>
<td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{comexpremarks}</p>
            </td>

            <!-- Add other data cells here -->
        </tr>
<tr>
            <td style=""border: solid black 1.0pt; border-top: none; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{finapprover}</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">F&C</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{finstatus}</p>
            </td>
<td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{finremarks}</p>
            </td>

            
        </tr>
        
    </tbody>
</table>";

            string htmlBody = $@"
            <html>
            <body>
                <br><br>
                <p style=\""text-align: left;\"">Dear    {buyer},</p>
                 <p style=\""text-align: left;\""> Title: {supplier} - {remarks}</p>
                <p style=\""text-align: left;\""> Description: {supplier}- {remarks}</p>


                <b style=\""text-align: left;\""> Buyer:</b>{buyer}<br />
                <b style=\""text-align: left;\""> Supplier:</b>{supplier}<br />
                <b style=\""text-align: left;\""> Version:</b>{version}<br />
                <b style=\""text-align: left;\"">Buyer Remarks:</b>{remarks}<br /><br /><br />
                 {emailContent}
                <p style=\""text-align: left;\""> Thanks</p>
            <body>


            </html>";






            

            MailMessage message = new MailMessage();
            message.From = new MailAddress(recipient);
            message.To.Add(recipient);
            //message.To.Add("dhivakar.p@teamsybergate.com");
            //message.CC.Add("kirubakaran.t@sybergate.co.in");
            message.Subject = "Request Approval for RM Amendment -"+supplier + "-" + remarks;
            message.IsBodyHtml = true;
            message.Body = htmlBody;



            var smtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host);
            var smtpPort = await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Port);
            var smtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName);
            var smtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password);
            var smtpEnableSsl = await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.EnableSsl);



            using (SmtpClient smtpClient = new SmtpClient(smtpHost, int.Parse(smtpPort)))
            {
                smtpClient.EnableSsl = bool.Parse(smtpEnableSsl);
                smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);

                await smtpClient.SendMailAsync(message);
            }


        }

        public async Task ApprovalEmailToBuyer(int a3document, string recipient, string subject, int uid, string l4approver, string cpApprover, string finapprover, string comexpapprover, string l4status, string cpstatus, string finstatus, string comexpstatus, string buyer, string supplier, string version, string remarks,string l4remarks,string cpremarks,string finremarks,string comexpremarks)
        {
            var emailTemplate = GetTitleAndSubTitle(uid, "", "");
            var mailMessage = new StringBuilder();

            DateTime currentTime = DateTime.Now;
            var emailContent = $@"
<table class=""v1MsoNormalTable"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""border-collapse: collapse;"">
    <tbody>
        <tr>
            <td style=""border: solid #666666 1.0pt; background: #DEDEDE; padding: 6.0pt 6.0pt 6.0pt 6.0pt;"" align=""center"">
                <p class=""v1MsoNormal"" style=""color: black;"">Name</p>
            </td>
            <td style=""border: solid #666666 1.0pt; border-left: none; background: #DEDEDE; padding: 6.0pt 6.0pt 6.0pt 6.0pt;"" align=""center"">
                <p class=""v1MsoNormal"" style=""color: black;"">Department</p>
            </td>
<td style=""border: solid #666666 1.0pt; border-left: none; background: #DEDEDE; padding: 6.0pt 6.0pt 6.0pt 6.0pt;"" align=""center"">
                <p class=""v1MsoNormal"" style=""color: black;"">Status</p>
            </td>
            <td style=""border: solid #666666 1.0pt; border-left: none; background: #DEDEDE; padding: 6.0pt 6.0pt 6.0pt 6.0pt;"" align=""center"">
                <p class=""v1MsoNormal"" style=""color: black;"">Remarks</p>

            </td>
            
        </tr>
        <tr>
            <td style=""border: solid black 1.0pt; border-top: none; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{l4approver}</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">Section-Head</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{l4status}</p>
            </td>
<td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{l4remarks}</p>
            </td>

        </tr>
        <tr>
            <td style=""border: solid black 1.0pt; border-top: none; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{cpApprover}</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">CE</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{cpstatus}</p>
            </td>
<td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{cpremarks}</p>
            </td>

            <!-- Add other data cells here -->
        </tr>
        <tr>
            <td style=""border: solid black 1.0pt; border-top: none; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{comexpapprover}</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">Commodity Expert</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{comexpstatus}</p>
            </td>
<td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{comexpremarks}</p>
            </td>

            <!-- Add other data cells here -->
        </tr>
<tr>
            <td style=""border: solid black 1.0pt; border-top: none; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{finapprover}</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">F&C</p>
            </td>
            <td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{finstatus}</p>
            </td>
<td style=""border-top: none; border-left: none; border-bottom: solid black 1.0pt; border-right: solid black 1.0pt; padding: .75pt .75pt .75pt .75pt;"" align=""left"">
                <p class=""v1MsoNormal"">{finremarks}</p>
            </td>

            
        </tr>
        
    </tbody>
</table>";

            string htmlBody = $@"
            <html>
            <body>
                <br><br>

                <p style=\""text-align: left;\"">Dear    {buyer},</p>
                 <p style=\""text-align: left;\""> Title: {supplier} - {remarks}</p>
                <p style=\""text-align: left;\""> Description: {supplier}- {remarks}</p>


                <b style=\""text-align: left;\""> Buyer:</b>{buyer}<br />
                <b style=\""text-align: left;\""> Supplier:</b>{supplier}<br />
                <b style=\""text-align: left;\""> Version:</b>{version}<br />
                <b style=\""text-align: left;\"">Buyer Remarks:</b>{remarks}<br /><br /><br />
                
                <p style=\""text-align: left;\""> The decision for topic is approved by all member's<br /><br /><br />
                 {emailContent}
            <p style=\""text-align: left;\""> Thanks</p>
            <body>


            </html>";








            MailMessage message = new MailMessage();
            message.From = new MailAddress(recipient);
            message.To.Add(recipient);
            //message.To.Add("dhivakar.p@teamsybergate.com");
            //message.CC.Add("kirubakaran.t@sybergate.co.in");
            message.Subject = "Request Approval for RM Amendment -" + supplier + "-" + remarks;
            message.IsBodyHtml = true;
            message.Body = htmlBody;

            var smtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host);
            var smtpPort = await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Port);
            var smtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName);
            var smtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password);
            var smtpEnableSsl = await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.EnableSsl);



            using (SmtpClient smtpClient = new SmtpClient(smtpHost, int.Parse(smtpPort)))
            {
                smtpClient.EnableSsl = bool.Parse(smtpEnableSsl);
                smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);

                await smtpClient.SendMailAsync(message);
            }


        }
        private StringBuilder GetTitleAndSubTitle(int? tenantId, string title, string subTitle)
		{
			var emailTemplate = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(tenantId));
			emailTemplate.Replace("{EMAIL_TITLE}", title);
			emailTemplate.Replace("{EMAIL_SUB_TITLE}", subTitle);

			return emailTemplate;
		}


		public async Task configupdate(int id, string l4email,string l4username,string l4Dept,
                                                string cpmail, string cpuername,string cpdept,
                                                string finemail,string finusername,string findept,
                                                string comexpemail,string comexpusername,string comexpdept,bool comexpchecbox)
        {
           
            
            var user = await UserManager.GetUserByIdAsync(id);
            user.LastModificationTime = DateTime.Now;
            user.L4EmailAddress = l4email;
            user.L4UserName = l4username;
            user.L4Department = l4Dept;
            user.CpEmailAddress = cpmail;
            user.CpUserName = cpuername;
            user.CpDepartment = cpdept;
            user.FinEmailAddress = finemail;
            user.FinUserName = finusername;
            user.FinDepartment = findept;
            user.CommadityExpertEmailAddress = comexpemail;
            user.CommadityExpertUserName = comexpusername;
            user.CommadityExpertDepartment = comexpdept;
            user.SequenceCheckBox= comexpchecbox;
            user.LastModifierUserId = null;


            await UserManager.UpdateAsync(user);


        }


        public List<string> GetApprovalUserDepartments()
        {

            var approvalUser = _approvalUserRepository.GetAll().Select(s => s.Department).Distinct().ToList();
            

            return approvalUser;


        }

        public List<ApprovalUser> GetApprovalUserNames(string input)
        { 
        
            var approvalUser= _approvalUserRepository.GetAll().Where(x => x.Department == input).Distinct().ToList();

            
                return approvalUser; 
        
        
        }

        public string GetApprovalUserEmailAddress(string dept,string username)
        {

            var approvalUser = _approvalUserRepository.GetAll().Where(x => x.Department == dept && x.UserName == username);
            var result="";

            foreach (var item in approvalUser)
            {
                result=item.Email;

            }

            return result;
        }

        public async Task<UserEditDto> GetUserDetails()

        {
            
            var user = await UserManager.GetUserByIdAsync((long)_abpSession.UserId);

            var userDto = ObjectMapper.Map<UserEditDto>(user);

            return userDto;

            
        }


        

       
        


    }
}