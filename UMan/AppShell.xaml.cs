using System;
using UMan.ViewModels;
using UMan.Views;
using Xamarin.Forms;
using UMan.Models;
using Xamarin.Essentials;

namespace UMan
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private AppShellVM AppShellVM;
        public AppShell()
        {
            AppShellVM = new AppShellVM(this);

            InitializeComponent();
         
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(Chapter),typeof(Chapter));
            Routing.RegisterRoute(nameof(LoadArticlePage), typeof(LoadArticlePage));


          
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Chapter");

        }
    }
}
