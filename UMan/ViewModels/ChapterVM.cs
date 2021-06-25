using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UMan.Models;
using UMan.Services;
using UMan.Views;
using Xamarin.Forms;

namespace UMan.ViewModels
{
    public class ChapterVM : BaseVM
    {
        private ObservableCollection<Article> articles;

        private Article article = new Article() {Id = default, Content = default,Title = default };

     

        public Article Article
        {
            get => article;

            set
            {
                article = value;
                OnPropertyChanged(nameof(Article));
            }
        }

        public int Id
        {
            get => Article.Id;

            set
            {
                Article.Id = value;

                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get => Article.Title;

            set
            {
                Article.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }


        public string Content
        {
            get => Article.Content;

            set
            {
                Article.Content = value;
                OnPropertyChanged(nameof(Content));
            }

        }

        


        public ObservableCollection<Article> Articles
        {
            get => articles;

            set
            {
                articles = value;
                OnPropertyChanged(nameof(Articles));
            }
        }

        public ChapterVM()
        {
            
            Title = Title;
         
            //Articles = new ObservableCollection<Article>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //ItemTapped = new Command<Item>(OnItemSelected);

  
        }





    }
}
