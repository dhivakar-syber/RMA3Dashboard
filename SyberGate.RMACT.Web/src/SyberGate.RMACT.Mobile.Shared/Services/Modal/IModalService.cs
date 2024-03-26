using System.Threading.Tasks;
using SyberGate.RMACT.Views;
using Xamarin.Forms;

namespace SyberGate.RMACT.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
