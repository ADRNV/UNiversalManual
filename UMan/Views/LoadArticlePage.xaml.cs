using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMan.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace UMan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadArticlePage : ContentPage
    {
        private LoadArticlePageVM LoadArticlePageVM;

      
        public LoadArticlePage()
        {
           
            
            InitializeComponent();

            BindingContext = new LoadArticlePageVM();


        }

      
    }
}