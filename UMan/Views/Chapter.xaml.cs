using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMan.Models;
using UMan.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UMan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chapter : ContentPage
    {
        

        private ChapterVM chapterViewModel;
        public ChapterVM chapterVM
        {
            get => chapterViewModel;

            set
            {
                chapterViewModel = value;
                
            }
        }
        public Chapter(Article article)
        {
            InitializeComponent();

            chapterViewModel = new ChapterVM();

            chapterVM.Article = article;

            this.BindingContext = chapterViewModel;
            
        }

        public Chapter()
        {
            InitializeComponent();
        }

        
    }
}