using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMan.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UMan.Views;

namespace UMan.ViewModels
{
    public class LoadArticlePageVM : BaseViewModel
    {
            public Command AddArticleCommand;

            private JsonSerializer jsonSerializer; 

            private List<Article> _articles;

            private List<Chapter> _chapters;

            private string readyjson;    

            private string path;
            private void AddArticle()
            {
            async Task PickAndShow()
            {
                try
                {
                    var file = await FilePicker.PickAsync();

                    path = file.FullPath;



                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader sw = new StreamReader(stream))
                    {
                        readyjson = JsonConvert.SerializeObject(_articles);

                        _articles = JsonConvert.DeserializeObject<List<Article>>(sw.ReadToEnd());

                    }
                }


                foreach (Article i in _articles)
                {
                    _chapters.Add(new Chapter(i));
                }
                foreach (Chapter i in _chapters)
                {
                    AppShell.Current.Items.Add(i);
                }


            }
                catch (Exception)
                {
                    
                }


            }

            }

            public LoadArticlePageVM()
            {
                Title = "Загрузить статью (скоро будет)";
                
                AddArticleCommand = new Command(() =>  AddArticle());

                 App.Current.MainPage.DisplayAlert("Это в разработке", "Скоро добавим", "Ладно");
        }
        }
    }

