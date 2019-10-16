using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ZhiMoney.Model;
using ZhiMoney.Properties;

namespace ZhiMoney.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public MainWindowViewModel()
        {
            OpenIncomeCommand = new DelegateCommand(o => OpenIncome());
            OpenExpenseCommand = new DelegateCommand(o => OpenExpense());
            OpenSettingsCommand = new DelegateCommand(o => OpenSettings());

            PathUserPhoto = Settings.Default["PathPhotoUser"].ToString();
        }
        public string PathUserPhoto { get; set; }
        #region Command
        public DelegateCommand OpenIncomeCommand { get; set; }
        public DelegateCommand OpenExpenseCommand { get; set; }
        public DelegateCommand OpenSettingsCommand { get; set; }
        #endregion
        #region Command implementation
        private void OpenSettings()
        {
            window.ChangingGrid.Children.Clear();
            window.ChangingGrid.Children.Add(new View.SettingsView());
        }
        private void OpenIncome()
        {
            window.ChangingGrid.Children.Clear();
            window.ChangingGrid.Children.Add(new View.IncomeView());
        }
        private void OpenExpense()
        {
            window.ChangingGrid.Children.Clear();
            window.ChangingGrid.Children.Add(new View.ExpenseView());
        }
        #endregion
        private string nameUser;
        private string surnameUser;

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string NameUser
        {
            get => nameUser;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    nameUser = value;
                    OnPropertyChanged(nameof(NameUser));
                }
                else
                {
                    MessageBox.Show("Было введено пустое значение");
                }
            }
        }
        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string SurnameUser
        {
            get => surnameUser;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    surnameUser = value;
                    OnPropertyChanged(nameof(SurnameUser));
                }
                else
                {
                    MessageBox.Show("Было введено пустое значение");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
