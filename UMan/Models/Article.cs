using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UMan.Models
{
   
    public class Article : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;

        private string title;

        private string content;
        public int Id
        {
            get => id;

            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get => title;

            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }


        public string Content 
        { 
            get => content; 
            
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
                
        }
        public void OnPropertyChanged(string propertyName = " ")
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
