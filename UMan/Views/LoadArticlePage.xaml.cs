using UMan.ViewModels;
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