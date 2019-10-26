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
        MainWindow window;
        public MainWindowViewModel()
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            OpenIncomeCommand = new DelegateCommand(o => OpenIncome());
            OpenExpenseCommand = new DelegateCommand(o => OpenExpense());
            OpenSettingsCommand = new DelegateCommand(o => OpenSettings());
            OpenWelcomeWindowCommand = new DelegateCommand(o=> OpenWelcomeWindow());

            PathUserPhoto = Settings.Default["PathPhotoUser"].ToString();
            NameUser = Settings.Default["NameUser"].ToString();
            SurnameUser = Settings.Default["SurnameUser"].ToString();
        }
        /// <summary>
        /// Путь к фотографии пользователя.
        /// </summary>
        public string PathUserPhoto { get; set; }
        #region Command
        public DelegateCommand OpenIncomeCommand { get; set; }
        public DelegateCommand OpenExpenseCommand { get; set; }
        public DelegateCommand OpenSettingsCommand { get; set; }
        public DelegateCommand OpenWelcomeWindowCommand { get; set; }
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
        private void OpenWelcomeWindow()
        {
            var welcomewindow = new View.WelcomeWindowView();
            welcomewindow.Show();
            window.Close();
        }
        #endregion

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string NameUser { get; set; }
        
        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string SurnameUser { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
