using System.Threading.Tasks;

namespace SyberGate.RMACT.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}