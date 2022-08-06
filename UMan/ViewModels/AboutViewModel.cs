using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UMan.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "О приложении";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/Lamer0"));
        }

        public ICommand OpenWebCommand { get; }
    }
}