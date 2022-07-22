using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UMan.Models;
using UMan.Views;
using Xamarin.Essentials;


namespace UMan.ViewModels
{

    public class AppShellVM : BaseVM
    {
        //Путь.Определяется FilePickerom
        private string path;
        //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"SampleArticle.xml");
        //$@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Articles.SampleArticle.xml";

        /// <summary>
        /// Статьи
        /// </summary>
        private List<Article> _articles = new List<Article>();
        /// <summary>
        /// Список страниц для статей
        /// </summary>
        private List<Chapter> _chapters = new List<Chapter>();
        //Считанный JSON
        private string readyJson;

        private AppShell appShell;

        public string ReadyJson
        {
            get => readyJson;
            private set => readyJson = value;
        }
        public AppShellVM(AppShell appshell)
        {
            _articles = new List<Article>();


            appShell = appshell;

            PickAndLoad();


        }
        /// <summary>
        /// Открывает FilePicker и загружает в приложение статьи
        /// </summary>
        /// <returns><code>void</code></returns>
        private async Task PickAndLoad()
        {
            try
            {
                var file = await FilePicker.PickAsync();

                path = file.FullPath;

                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader sw = new StreamReader(stream))
                    {
                        readyJson = JsonConvert.SerializeObject(_articles);

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
                await appShell.DisplayAlert("Проблемы", "Что-то пошло не так, возможно был выбран не .json \n или же JSON с не правильной структурой", "Ок");
            }
        }

    }




}

