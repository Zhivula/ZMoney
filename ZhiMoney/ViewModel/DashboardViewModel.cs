﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ZhiMoney.Data;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class DashboardViewModel : INotifyPropertyChanged
    {
        public string MainTitle { get; set; }
        public string LeftTitle { get; set; }
        public string RightTitle { get; set; }

        private Tuple<string, string>[] items;
        public Tuple<string, string>[] ItemsSource
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(ItemsSource));                
            }
        }

        private Dashboard dashboard;

        public DashboardViewModel(Dashboard dashboard)
        {
            MainTitle = dashboard.mainTitle;
            LeftTitle = dashboard.leftTitle;
            RightTitle = dashboard.rightTitle;
            this.dashboard = dashboard;
            if (dashboard.Names.Count() >= 5)
            {
                ItemsSource = new Tuple<string, string>[5];
                for (int i = 0; i < 5; i++)
                {
                    ItemsSource[i] = new Tuple<string, string>(dashboard.Names[i], dashboard.Values[i].ToString("#.##"));
                }
            }
            else if(dashboard.Names.Count() >= 1)
            {
                ItemsSource = new Tuple<string, string>[dashboard.Names.Count()];
                for (int i = 0; i < dashboard.Names.Count(); i++)
                {
                    ItemsSource[i] = new Tuple<string, string>(dashboard.Names[i], dashboard.Values[i].ToString("#.##"));
                }
            }
            else ItemsSource[0] = new Tuple<string, string>("-", "0");
        }
        public ICommand ShowEverything => new DelegateCommand(o =>
        {
            var welcomewindow = new View.FullListView(dashboard);
            welcomewindow.Show();
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
