using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZhiMoney.Properties;
using ZhiMoney.View;

namespace ZhiMoney
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            if (Settings.Default["NameUser"].ToString() == string.Empty || Settings.Default["SurnameUser"].ToString() == string.Empty)
            {
                var welwindow = new WelcomeWindowView();
                welwindow.Show();
            }
            else
            {
                var mainwindow = new MainWindow();
                mainwindow.Show();
            }
        }
    }
}
