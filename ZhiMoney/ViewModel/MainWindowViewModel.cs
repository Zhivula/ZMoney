using System;
using System.ComponentModel;

namespace ZhiMoney.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string nameUser;
        private string surnameUser;

        public string NameUser
        {
            get => nameUser;
            set
            {
                nameUser = value;
                OnPropertyChanged(nameof(NameUser));
            }
        }
        public string SurnameUser
        {
            get => surnameUser;
            set
            {
                surnameUser = value;
                OnPropertyChanged(nameof(SurnameUser));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
