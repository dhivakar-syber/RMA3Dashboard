

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using SyberGate.RMACT.MultiTenancy;
using SyberGate.RMACT.Tenants.Dashboard;
using SyberGate.RMACT.Configuration.Host.Dto;
using Abp.Runtime.Session;
using Abp.Threading;
using System.IO;
using System.Net.Mail;
using System.Net;
using SyberGate.RMACT.Storage;
using Abp.Localization;
using SyberGate.RMACT.Notifications;
using Microsoft.Extensions.Configuration;
using System.Net.Mime;
using SyberGate.RMACT.Configuration;
using Abp.BackgroundJobs;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Masters.Importing;
using Org.BouncyCastle.Asn1.Ocsp;
using Abp.IO.Extensions;
using Microsoft.AspNetCore.Http;
using Abp.Net.Mail;
using Abp.Configuration;
using SyberGate.RMACT.Authorization.Users;

namespace SyberGate.RMACT.Masters
{
	//[AbpAuthorize(AppPermissions.Pages_Administration_A3Documents)]
    public class A3DocumentsAppService : RMACTAppServiceBase, IA3DocumentsAppService
    {
		private readonly IRepository<A3Document> _a3DocumentRepository;
		private readonly IRepository<A3PriceTrend> _a3PriceTrendRepository;
		private readonly IRepository<A3PriceImpact> _a3PriceImpactRepository;
		private readonly IRepository<A3SubPartImpact> _a3SubPartImpactRepository;
		private readonly IPartRepository _partRepository;
		private readonly IUnitOfWorkManager _unitOfWorkManager;
		private readonly IRepository<BaseRMRate> _baseRMRateRepository;
		private readonly IRepository<SupportAttachments> _supportAttachmentsRepository;
        private readonly IRepository<ApprovalUser> _approvalUser;
        private readonly TenantAppService _tenantAppServicetenantAppService;
		private readonly TenantDashboardAppService _tenantDashboardAppService;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IConfigurationRoot _approotConfiguration;
        private readonly ISettingManager _settingManager;




        public A3DocumentsAppService(IRepository<A3Document> a3DocumentRepository, IPartRepository partRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<A3PriceTrend> a3PriceTrendRepository, IRepository<A3PriceImpact> a3PriceImpactRepository,
			IRepository<A3SubPartImpact> a3SubPartImpactRepository, IRepository<BaseRMRate> baseRMRateRepository, IRepository<SupportAttachments> SupportAttachmentsRepository,
            TenantAppService TenantAppServicetenantAppService,
            TenantDashboardAppService tenantDashboardAppService, ITempFileCacheManager tempFileCacheManager, IAppNotifier AppNotifier,
            IAppConfigurationAccessor configurationAccessor, ISettingManager settingManager, IRepository<ApprovalUser> Approvaluser
            ) 
		  {
			_a3DocumentRepository = a3DocumentRepository;
			_partRepository = partRepository;
			_unitOfWorkManager = unitOfWorkManager;
			_a3PriceTrendRepository = a3PriceTrendRepository;
			_a3PriceImpactRepository = a3PriceImpactRepository;
			_a3SubPartImpactRepository = a3SubPartImpactRepository;
			_baseRMRateRepository = baseRMRateRepository;
			_supportAttachmentsRepository = SupportAttachmentsRepository;
			_tenantAppServicetenantAppService = TenantAppServicetenantAppService;
            _tenantDashboardAppService = tenantDashboardAppService;
            _tempFileCacheManager = tempFileCacheManager;
            _appNotifier = AppNotifier;
            _approotConfiguration = configurationAccessor.Configuration;
            _settingManager = settingManager;
            _approvalUser = Approvaluser;

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_A3Documents)]
        public async Task<PagedResultDto<GetA3DocumentForViewDto>> GetAll(GetAllA3DocumentsInput input)
         {
			
			var filteredA3Documents = _a3DocumentRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Buyer.Contains(input.Filter) || e.Supplier.Contains(input.Filter) || e.Month.Contains(input.Filter) || e.Year.Contains(input.Filter))
						.WhereIf(input.IsConfirmedFilter, e=> e.IsApproved == input.IsConfirmedFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerFilter),  e => e.Buyer == input.BuyerFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierFilter),  e => e.Supplier == input.SupplierFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MonthFilter),  e => e.Month == input.MonthFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.YearFilter),  e => e.Year == input.YearFilter);

			var pagedAndFilteredA3Documents = filteredA3Documents
                .OrderBy(input.Sorting ?? "id desc")
                .PageBy(input);

			var a3Documents = from o in pagedAndFilteredA3Documents
                         select new GetA3DocumentForViewDto() {
							A3Document = new A3DocumentDto
							{
                                Buyer = o.Buyer,
                                Supplier = o.Supplier,
                                Month = o.Month,
                                Year = o.Year,
                                Id = o.Id,
								Version = o.Version,
								IsApproved = o.IsApproved,
								Remarks = o.Remarks,
								L4Approval= o.L4Approval,
								L4remarks= o.L4remarks,
								L4Status=o.L4Status,
								L4Token=o.L4Token,
								RL4Token=o.RL4Token,
								CpApproval= o.CpApproval,
								Cpremarks=o.Cpremarks,
								CpStatus=o.CpStatus,
								CpToken=o.CpToken,
								RCpToken=o.RCpToken,
								FinApproval= o.FinApproval,
								Finremarks= o.Finremarks,
								FinStatus=o.FinStatus,
								FinToken=o.FinToken,
								RFinToken=o.RFinToken,
								CommadityExpertApproval=o.CommadityExpertApproval,
								CommadityExpertremarks=o.CommadityExpertremarks,
								CommadityStatus=o.CommadityExpertStatus,
								CommadityExpertToken=o.CommadityExpertToken,
								RCommadityExpertToken=o.RCommadityExpertToken
								
							}
						};


            var totalCount = await filteredA3Documents.CountAsync();

            return new PagedResultDto<GetA3DocumentForViewDto>(
                totalCount,
                await a3Documents.ToListAsync()
            );
         }

        public async Task<PagedResultDto<GetSupportAttachmentsForViewDto>> GetAllSupportAttachments(GetAllSupportAttchmentsInput input)
        {

			var filteredSupportAttachments = _supportAttachmentsRepository.GetAll().Where(w => w.Buyer==input.BuyerFilter && w.Supplier==input.SupplierFilter &&
            w.Version == input.VersionFilter);
                        
                        

            var pagedAndFilteredA3Documents = filteredSupportAttachments
                .OrderBy(input.Sorting ?? "id desc")
                .PageBy(input);

            var supportattachments = from o in pagedAndFilteredA3Documents
                              select new GetSupportAttachmentsForViewDto()
                              {
                                  supportAttachment = new SupportAttachmentsDto
                                  {
                                     Id= o.Id,
                                      A3Id = o.A3Id,
                                      SupportAttachmentPath = o.SupportAttachmentPath,
                                      Buyer=o.Buyer,
                                      Supplier=o.Supplier,
                                      Version=o.Version,
                                      FileName=o.FileName
                                  }
                              };


            var totalCount = await filteredSupportAttachments.CountAsync();

            return new PagedResultDto<GetSupportAttachmentsForViewDto>(
                totalCount,
                await supportattachments.ToListAsync()
            );
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_A3Documents, AppPermissions.Pages_Administration_A3Documents_Edit)]
      
		 public async Task<GetA3DocumentForEditOutput> GetA3DocumentForEdit(EntityDto input)
         {
            var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetA3DocumentForEditOutput {A3Document = ObjectMapper.Map<CreateOrEditA3DocumentDto>(a3Document)};
			
            return output;
         }
        [AbpAuthorize(AppPermissions.Pages_Administration_A3Documents)]
        public async Task CreateOrEdit(CreateOrEditA3DocumentDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }
        [AbpAuthorize(AppPermissions.Pages_Administration_A3Documents, AppPermissions.Pages_Administration_A3Documents_Create)]
        
		 protected virtual async Task Create(CreateOrEditA3DocumentDto input)
         {
			using (var uow = _unitOfWorkManager.Begin())
			{
				using (CurrentUnitOfWork.SetTenantId(AbpSession.TenantId))
				{
					try
					{
						var a3Document = ObjectMapper.Map<A3Document>(input);
						var version = _a3DocumentRepository.GetAll().Where(w => w.Buyer == input.Buyer && w.Supplier == input.Supplier && w.Month == input.Month && w.Year == input.Year)
							.Count();
						a3Document.Version = input.Year + input.Month + ".V" + (version + 1).ToString();
						var id = await _a3DocumentRepository.InsertAndGetIdAsync(a3Document);

						var param = new Masters.Dto.GetA3DashboardDataInput();
						param.Buyer = input.BuyerId.ToString();
						param.Supplier =  input.SupplierId.ToString();
						param.Period = input.Period;
						param.IsGenerateA3 = true;
						param.A3Id = id;
						param.Group = input.GroupId.ToString();
						param.Grade= input.GradeId.ToString();

						await _partRepository.GetAllRMTrend(param);

						var items = await _partRepository.GetAllRMPriceImpact(param);

						//foreach(var item in items)
      //                  {
						//	if (item.IsParent) {
						//		param.Partno = item.PartNo;
						//		param.Plant = item.PlantCode;
						//		await _partRepository.GetAllSubPartRMPriceImpact(param);
						//	}
      //                  }
					}
					catch (Exception ex)
					{
						throw new Exception(ex.Message);
					}
					finally
					{
						uow.Complete();
					}
				}
			}
		}

		 [AbpAuthorize(AppPermissions.Pages_Administration_A3Documents,AppPermissions.Pages_Administration_A3Documents_Edit)]
		 protected virtual async Task Update(CreateOrEditA3DocumentDto input)
         {
            var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, a3Document);
         }

		[AbpAuthorize(AppPermissions.Pages_Administration_A3Documents,AppPermissions.Pages_Administration_A3Documents_Edit)]
		public async Task Approve(EntityDto input)
		{
			var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync((int)input.Id);
			a3Document.IsApproved = true;
			await _a3DocumentRepository.UpdateAsync(a3Document);

			var trends = _a3PriceTrendRepository.GetAll().Where(x => x.DocId == input.Id);

			foreach (var trend in trends)
			{
				if (trend != null)
				{
					if (trend.RevId != null && trend.RevId != 0)
					{
						var baseRM = await _baseRMRateRepository.FirstOrDefaultAsync((int)trend.RevId);
						baseRM.IsApproved = true;
						await _baseRMRateRepository.UpdateAsync(baseRM);
					}
					if (trend.SetId != null && trend.SetId != 0)
					{
						var reviseRM = await _baseRMRateRepository.FirstOrDefaultAsync((int)trend.SetId);
						reviseRM.IsApproved = true;
						await _baseRMRateRepository.UpdateAsync(reviseRM);
					}
				}
			}

		}
		
        public async Task sEmailApprove(int input, string token, bool issequence, string remarks)
        {
            token =token.Trim();
            var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync(input);

            


            if (token == a3Document.L4Token)
			{
				a3Document.L4Status = "Approved";
				a3Document.L4Approval = true;
				a3Document.L4remarks = remarks;
            }

            if (token == a3Document.CpToken)
            {
                a3Document.CpStatus = "Approved";
                a3Document.CpApproval = true;
                a3Document.Cpremarks = remarks;

            }

            if (token == a3Document.FinToken)
            {
                a3Document.FinStatus = "Approved";
                a3Document.FinApproval = true;
                a3Document.Finremarks = remarks;

            }

            if (token == a3Document.CommadityExpertToken)
            {
                a3Document.CommadityExpertStatus = "Approved";
                a3Document.CommadityExpertApproval = true;
                a3Document.CommadityExpertremarks = remarks;

            }

            if (token == a3Document.RL4Token)
			{
				a3Document.L4Status = "Rejected";
				a3Document.L4Approval = false;
                a3Document.L4remarks = remarks;


            }
            if (token == a3Document.RCpToken)
            {
                a3Document.CpStatus = "Rejected";
                a3Document.CpApproval = false;
                a3Document.Cpremarks = remarks;


            }
            if (token == a3Document.RFinToken)
            {
                a3Document.FinStatus = "Rejected";
                a3Document.FinApproval = false;
                a3Document.Finremarks = remarks;


            }

            if (token == a3Document.RCommadityExpertToken)
            {
                a3Document.CommadityExpertStatus = "Rejected";
                a3Document.CommadityExpertApproval = false;
                a3Document.CommadityExpertremarks = remarks;


            }
			if (a3Document.L4Approval == true && a3Document.CpApproval == true && a3Document.FinApproval == true && a3Document.CommadityExpertApproval == false && issequence==true)
			{
                


            }


                if (a3Document.L4Approval == true && a3Document.CpApproval == true && a3Document.FinApproval == true && a3Document.CommadityExpertApproval == false)
			{

				a3Document.IsApproved = true;
				await _a3DocumentRepository.UpdateAsync(a3Document);

				var trends = _a3PriceTrendRepository.GetAll().Where(x => x.DocId == input);

				foreach (var trend in trends)
				{
					if (trend != null)
					{
						if (trend.RevId != null && trend.RevId != 0)
						{
							var baseRM = await _baseRMRateRepository.FirstOrDefaultAsync((int)trend.RevId);
							baseRM.IsApproved = true;
							await _baseRMRateRepository.UpdateAsync(baseRM);
						}
						if (trend.SetId != null && trend.SetId != 0)
						{
							var reviseRM = await _baseRMRateRepository.FirstOrDefaultAsync((int)trend.SetId);
							reviseRM.IsApproved = true;
							await _baseRMRateRepository.UpdateAsync(reviseRM);
						}
					}
				}
			}

			else
			{
                await _a3DocumentRepository.UpdateAsync(a3Document);
            }

            

        }


        public async Task EmailApprove(int input, string token, string issequence, string remarks, int uid)
        {
            token = token.Trim();
            var a3Document = await _a3DocumentRepository.FirstOrDefaultAsync(input);
            var user = await UserManager.GetUserByIdAsync(uid);
            var L4approvaluser = _approvalUser.GetAll().Where(w => w.Email == user.L4EmailAddress).FirstOrDefault();
            var Cpapprovaluser = _approvalUser.GetAll().Where(w => w.Email == user.CpEmailAddress).FirstOrDefault();
            var finapprovaluser = _approvalUser.GetAll().Where(w => w.Email == user.FinEmailAddress).FirstOrDefault();
            var Comexpapprovaluser = _approvalUser.GetAll().Where(w => w.Email == user.CommadityExpertEmailAddress).FirstOrDefault();

            if (token == a3Document.L4Token)
            {
                a3Document.L4Status = "Approved";
                a3Document.L4Approval = true;
                a3Document.L4remarks = remarks;
            }

            if (token == a3Document.CpToken)
            {
                a3Document.CpStatus = "Approved";
                a3Document.CpApproval = true;
                a3Document.Cpremarks = remarks;

            }

            if (token == a3Document.FinToken)
            {
                a3Document.FinStatus = "Approved";
                a3Document.FinApproval = true;
                a3Document.Finremarks = remarks;

            }

            if (token == a3Document.CommadityExpertToken)
            {
                a3Document.CommadityExpertStatus = "Approved";
                a3Document.CommadityExpertApproval = true;
                a3Document.CommadityExpertremarks = remarks;

            }

            if (token == a3Document.RL4Token)
            {
                a3Document.L4Status = "Rejected";
                a3Document.L4Approval = false;
                a3Document.L4remarks = remarks;


            }
            if (token == a3Document.RCpToken)
            {
                a3Document.CpStatus = "Rejected";
                a3Document.CpApproval = false;
                a3Document.Cpremarks = remarks;


            }
            if (token == a3Document.RFinToken)
            {
                a3Document.FinStatus = "Rejected";
                a3Document.FinApproval = false;
                a3Document.Finremarks = remarks;


            }

            if (token == a3Document.RCommadityExpertToken)
            {
                a3Document.CommadityExpertStatus = "Rejected";
                a3Document.CommadityExpertApproval = false;
                a3Document.CommadityExpertremarks = remarks;


            }

            if (a3Document.L4Status == "Rejected" || a3Document.CpStatus == "Rejected" || a3Document.CommadityExpertStatus == "Rejected" || a3Document.FinStatus == "Rejected")
            {
                //var user = await UserManager.GetUserByIdAsync(uid);

                await _tenantDashboardAppService.RejectionEmailToBuyer(a3Document.Id,user.EmailAddress,"", (int)user.TenantId, L4approvaluser.UserName, Cpapprovaluser.UserName, finapprovaluser.UserName, Comexpapprovaluser.UserName,a3Document.L4Status, a3Document.CpStatus, a3Document.FinStatus,a3Document.CommadityExpertStatus, a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, a3Document.L4remarks, a3Document.Cpremarks, a3Document.Finremarks, a3Document.CommadityExpertremarks);
            }

                if (a3Document.L4Approval == true && a3Document.CpApproval == true && a3Document.CommadityExpertApproval == true && a3Document.FinApproval == false && issequence == "true")
            {
                

                SendTestEmailInput iput = new SendTestEmailInput();
                iput.A3Id = a3Document.Id;
                //iput.Buyer=a3Document.Buyer;
                iput.BuyerName = a3Document.Buyer;
                iput.SupplierName = a3Document.Supplier;
                iput.TemplatePath = "/assets/SampleFiles/A3sheet7New.xlsx";


                FileDto excelFile = await _tenantDashboardAppService.FinRMsheetConfirmationEmail(iput, a3Document.FinToken, a3Document.RFinToken, uid);


                a3Document.FinStatus = "Awaiting For Approval";

                //await _a3DocumentRepository.UpdateAsync(a3Doc);

                string FinApprovalapppath =  _tenantDashboardAppService.GenerateAppPath(iput, a3Document.FinToken, true, uid, a3Document.Remarks, a3Document.Version);
				 string FinRejectionapppath = _tenantDashboardAppService.GenerateAppPath(iput, a3Document.RFinToken, true, uid, a3Document.Remarks, a3Document.Version);

                await _tenantDashboardAppService.SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks,user.FinEmailAddress, "Request Approval for RM Amendment - " + excelFile.FileName, FinApprovalapppath, FinRejectionapppath, excelFile, a3Document.Id, uid,user.EmailAddress,"Approval-Fin");
				//await _tenantDashboardAppService.SendEmail(a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, user.FinEmailAddress, "Request Approval for RM Amendment - " + excelFile.FileName, FinApprovalapppath, FinRejectionapppath, excelFile, uid, user.EmailAddress);


			}



            if (a3Document.L4Approval == true && a3Document.CpApproval == true && a3Document.CommadityExpertApproval == true && a3Document.FinApproval == true  )
            {

                a3Document.IsApproved = true;

                await _tenantDashboardAppService.ApprovalEmailToBuyer(a3Document.Id, user.EmailAddress, "", (int)user.TenantId, L4approvaluser.UserName, Cpapprovaluser.UserName, finapprovaluser.UserName, Comexpapprovaluser.UserName, a3Document.L4Status, a3Document.CpStatus, a3Document.FinStatus, a3Document.CommadityExpertStatus, a3Document.Buyer, a3Document.Supplier, a3Document.Version, a3Document.Remarks, a3Document.L4remarks, a3Document.Cpremarks, a3Document.Finremarks, a3Document.CommadityExpertremarks);


                //await _a3DocumentRepository.UpdateAsync(a3Document);

                var trends = _a3PriceTrendRepository.GetAll().Where(x => x.DocId == input);

                try 
                {
                    foreach (var trend in trends)
                    {
                        if (trend != null)
                        {
                            if (trend.RevId != null && trend.RevId != 0)
                            {
                                var baseRM = await _baseRMRateRepository.FirstOrDefaultAsync((int)trend.RevId);
                                baseRM.IsApproved = true;
                                await _baseRMRateRepository.UpdateAsync(baseRM);
                            }
                            if (trend.SetId != null && trend.SetId != 0)
                            {
                                var reviseRM = await _baseRMRateRepository.FirstOrDefaultAsync((int)trend.SetId);
                                reviseRM.IsApproved = true;
                                await _baseRMRateRepository.UpdateAsync(reviseRM);
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                
                }
                
            }


            await _a3DocumentRepository.UpdateAsync(a3Document);




        }

        [AbpAuthorize(AppPermissions.Pages_Administration_A3Documents,AppPermissions.Pages_Administration_A3Documents_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _a3DocumentRepository.DeleteAsync(input.Id);

			await _a3PriceTrendRepository.DeleteAsync(x => x.DocId == input.Id);
			await _a3PriceImpactRepository.DeleteAsync(x => x.DocId == input.Id);
			await _a3SubPartImpactRepository.DeleteAsync(x => x.DocId == input.Id);
         }


        private string GenerateAppPath(SendTestEmailInput iput, string token, bool issequnce, int uid, string buyerRemarks, string version)
        {
            string appPath = $"{_approotConfiguration.GetValue<string>("IdentityServer:Authority")}Account/EmailApproval?" +
                      $"A3Id={iput.A3Id}&Buyername={iput.BuyerName}&Suppliername={iput.SupplierName}&Token={token}&Issequnce={issequnce}&uid={uid}&buyerRemarks={buyerRemarks}&version={version}";
            return appPath;
        }

        private async Task SendEmail(string buyerName, string supplierName, string version,string BuyerRemarks,string recipient, string subject, string approvalAppPath, string rejectionAppPath, FileDto excelFile, int uid,string useremailaddress)
        {
            //var user = await UserManager.GetUserByIdAsync((long)uid);

            version = version.Split('.')[0];

            var supportattachments = _supportAttachmentsRepository.GetAll().Where(w => w.Buyer == buyerName && w.Supplier == supplierName && w.Version == version);

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



            message.Body = htmlBody;
            var g = "";

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
                    attachment.Name = excelFile.FileName;
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


           

        }




        public async Task uploadexcel(SupportAttachmentsDto input)

        {
            var insertdocument = ObjectMapper.Map<SupportAttachments>(input);

            await _supportAttachmentsRepository.InsertAsync(insertdocument);

        }


        public async Task DeleteSupportAttachments(int id)

        {
            await _supportAttachmentsRepository.DeleteAsync(id); 

        }






    }
}