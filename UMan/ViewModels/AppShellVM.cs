using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using UMan.Models;
using UMan.Views;
using System.Xml.Serialization;
using System.IO;
using Android.Content.Res;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace UMan.ViewModels
{

    public class AppShellVM
    {
        private XmlSerializer Serializer;


        private JsonSerializer JsonSerializer;

        private string path = $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SampleArticle.xml")}";
        //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"SampleArticle.xml");

        //$@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Articles.SampleArticle.xml";

        private List<Article> _articles = new List<Article>() { new Article { Content = "Нормально", Title = "Нормально" } };

        private List<Chapter> _chapters = new List<Chapter>();

        private string readyjson;

        private AppShell appShell;
        public AppShellVM(AppShell appshell)
        {
            JsonSerializer = new JsonSerializer();

            appShell = appshell;

            PickAndShow();


        }

        private async Task PickAndShow()
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
                appShell.Items.Add(i);
            }


            }
            catch (Exception)
            {
                await appShell.DisplayAlert("Проблемы","Что-то пошло не так","Ок");
            }
        }

    }




}

