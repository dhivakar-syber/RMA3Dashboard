using Xamarin.Forms.Internals;

namespace SyberGate.RMACT.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}