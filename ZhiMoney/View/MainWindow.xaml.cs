using System;
using System.Windows;
using ZhiMoney.DataBase;
using System.Linq;

namespace ZhiMoney
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.MainWindowViewModel();
        }
    }
}
