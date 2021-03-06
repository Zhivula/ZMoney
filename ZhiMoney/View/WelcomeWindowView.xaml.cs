﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZhiMoney.ViewModel;

namespace ZhiMoney.View
{
    /// <summary>
    /// Логика взаимодействия для WelcomeWindowView.xaml
    /// </summary>
    public partial class WelcomeWindowView : Window
    {
        public WelcomeWindowView()
        {
            InitializeComponent();
            DataContext = new WelcomeWindowViewModel();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
