using System;
using System.ComponentModel;

namespace UMan.Models
{
   /// <summary>
   /// Модель статьи
   /// </summary>
    public class Article : INotifyPropertyChanged
    {
        

        #region Поля
        private int id;//Пока бесполезно

        private string title;

        private string content;
        #endregion

        /// <summary>
        /// Пока бесполезно
        /// </summary>
        public int Id
        {
            get => id;

            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        /// <summary>
        /// Заголовок статьи
        /// </summary>
        public string Title
        {
            get => title;

            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// Содержание статьи
        /// </summary>
        public string Content 
        { 
            get => content; 
            
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
                
        }
        #region Реализация INtotifyPropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = " ")
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
