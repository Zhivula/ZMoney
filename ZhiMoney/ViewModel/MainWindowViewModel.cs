﻿using System;
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