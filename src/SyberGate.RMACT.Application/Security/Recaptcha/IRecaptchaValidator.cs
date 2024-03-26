using System.Threading.Tasks;

namespace SyberGate.RMACT.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}