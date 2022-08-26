using System.Collections.Generic;
using System.ComponentModel;

namespace UMan.Models
{
    public class Manual : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;

        private string title;

        private List<string> chapter = new List<string>();

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


        public List<string> Chapter
        {
            get => chapter;

            set
            {
                chapter = value;
                OnPropertyChanged(nameof(Chapter));
            }

        }
        public void OnPropertyChanged(string propertyName = " ")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
