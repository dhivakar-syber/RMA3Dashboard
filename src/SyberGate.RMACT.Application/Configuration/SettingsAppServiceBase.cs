using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Abp.Net.Mail;
using Microsoft.Extensions.Configuration;
using SyberGate.RMACT.Configuration.Dto;
using SyberGate.RMACT.Configuration.Host.Dto;
using SyberGate.RMACT.Tenants.Dashboard;
using SyberGate.RMACT.Masters.Dtos;
using Twilio.TwiML.Messaging;
using NPOI.XWPF.UserModel;
using Abp.Extensions;
using SyberGate.RMACT.Masters.Dto;
using System.IO;

namespace SyberGate.RMACT.Configuration
{
    public abstract class SettingsAppServiceBase : RMACTAppServiceBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IAppConfigurationAccessor _configurationAccessor;
        private readonly ITenantDashboardAppService _tenantDashboard;
        private IEmailSender emailSender;
        private IAppConfigurationAccessor configurationAccessor;

        protected SettingsAppServiceBase(
            IEmailSender emailSender, 
            IAppConfigurationAccessor configurationAccessor,
            ITenantDashboardAppService tenantDashboard)
        {
            _emailSender = emailSender;
            _configurationAccessor = configurationAccessor;
            _tenantDashboard = tenantDashboard;
        }

        protected SettingsAppServiceBase(IEmailSender emailSender, IAppConfigurationAccessor configurationAccessor)
        {
            this.emailSender = emailSender;
            this.configurationAccessor = configurationAccessor;
        }

        #region Send Test Email

        //public async Task SendTestEmail(SendTestEmailInput input)
        //{
        //    await _emailSender.SendAsync(
        //        input.EmailAddress,
        //        L("TestEmail_Subject"),
        //        L("TestEmail_Body")
        //    );
        //}


        public async Task SendTestEmail(SendTestEmailInput input)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("dhivakar.p@teamsybergate.com");
            message.To.Add(input.EmailAddress);
            message.Subject = L("TestEmail_Subject");
            //message.Body = L("TestEmail_Body");
            message.IsBodyHtml = true;

            string htmlBody = $@"
            <html>
            <body>
                <h1>Test-Mail</h1>
                <p>Body</p>
                <p>
                    <a href=""{GetApproveUrl()}""><button style=""background-color: green; color: white;"">Approve</button></a>
                    <a href=""{GetRejectUrl()}""><button style=""background-color: red; color: white;"">Reject</button></a>
                </p>
            </body>
            </html>";

           
          
            message.Body = htmlBody;



            


            //string csvFilePath = "D:\\excel\\thisanth-PartsUploadTemplate (20).xlsx";
            //Attachment attachment = new Attachment(csvFilePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //ccccfvgmessage.Attachments.Add(attachment);





           


                using (SmtpClient smtpClient = new SmtpClient("mail.teamsybergate.com", 587))
                {
                    // Configure SMTP client settings
                    smtpClient.EnableSsl = true; // Use SSL/TLS encryption
                    smtpClient.Credentials = new NetworkCredential("dhivakar.p@teamsybergate.com", "sgtpl@12345");

                    await smtpClient.SendMailAsync(message);
                }

        }

       

        private string GetApproveUrl()
        {
            // Logic to generate the dynamic ChatGPT URL for the approve action
            string chatGptUrl = "https://chat.openai.com/";
            // Additional logic if needed
            return chatGptUrl;
        }

        private string GetRejectUrl()
        {
            // Logic to generate the dynamic ChatGPT URL for the reject action
            string chatGptUrl = "https://chat.openai.com/";
            // Additional logic if needed
            return chatGptUrl;
        }

        public ExternalLoginSettingsDto GetEnabledSocialLoginSettings()
        {
            var dto = new ExternalLoginSettingsDto();
            if (!bool.Parse(_configurationAccessor.Configuration["Authentication:AllowSocialLoginSettingsPerTenant"]))
            {
                return dto;
            }

            if (IsSocialLoginEnabled("Facebook"))
            {
                dto.EnabledSocialLoginSettings.Add("Facebook");
            }

            if (IsSocialLoginEnabled("Google"))
            {
                dto.EnabledSocialLoginSettings.Add("Google");
            }

            if (IsSocialLoginEnabled("Twitter"))
            {
                dto.EnabledSocialLoginSettings.Add("Twitter");
            }

            if (IsSocialLoginEnabled("Microsoft"))
            {
                dto.EnabledSocialLoginSettings.Add("Microsoft");
            }
            
            if (IsSocialLoginEnabled("WsFederation"))
            {
                dto.EnabledSocialLoginSettings.Add("WsFederation");
            }
            
            if (IsSocialLoginEnabled("OpenId"))
            {
                dto.EnabledSocialLoginSettings.Add("OpenId");
            }

            return dto;
        }

        private bool IsSocialLoginEnabled(string name)
        {
            return _configurationAccessor.Configuration.GetSection("Authentication:" + name).Exists() &&
                   bool.Parse(_configurationAccessor.Configuration["Authentication:" + name + ":IsEnabled"]);
        }

        #endregion
    }
}
