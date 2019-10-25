using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZhiMoney.Model;
using ZhiMoney.Properties;
using ZhiMoney.View;

namespace ZhiMoney.ViewModel
{
    class WelcomeWindowViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public Visibility SuccessfullyPanel { get; set; }

        public DelegateCommand CloseWindow { get; set; }
        public DelegateCommand Next { get; set; }
        public DelegateCommand PathToPhotoUser { get; set; }

        private string name;
        private string surname;
        private string pathPhoto;

        public string NameUser
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(NameUser));
            }
        }
        public string SurnameUser
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged(nameof(SurnameUser));
            }
        }
        public string PathPhoto
        {
            get => pathPhoto;
            set
            {
                pathPhoto = value;
                OnPropertyChanged(nameof(PathPhoto));
            }
        }

        public WelcomeWindowViewModel()
        {
            SuccessfullyPanel = Visibility.Hidden;
            Next = new DelegateCommand(o => NextPage());
            CloseWindow = new DelegateCommand(o => Close());
            PathToPhotoUser = new DelegateCommand(o => SearchPathToPhotoUser());
        }
        /// <summary>
        /// Переход к основному окну приложения.
        /// </summary>
        private void NextPage()
        {
            Settings.Default["SurnameUser"] = SurnameUser;
            Settings.Default["NameUser"] = NameUser;
            Settings.Default["PathPhotoUser"] = NameUser;

            Settings.Default.Save();

            MainWindow mainwindow = new MainWindow();

            mainwindow.Show();

            
        }
        /// <summary>
        /// Открытие диалогового окна для указания пути к фото пользователя.
        /// </summary>
        private void SearchPathToPhotoUser()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = string.Empty;
            dlg.DefaultExt = ".jpg";

            dlg.Filter = "Файлы фотографий|*.jpg;*.png;*.ico";

            bool? result = dlg.ShowDialog();

            if (result == true)
            { 
                PathPhoto = dlg.FileName;
                SuccessfullyPanel = Visibility.Visible;

                MainWindowViewModel mainWindowVM = new MainWindowViewModel();
                //mainWindowVM.PathUserPhoto = dlg.FileName;
            }
        }
        private void Close()
        {
            WelcomeWindowView window = Application.Current.Windows.OfType<WelcomeWindowView>().FirstOrDefault();
            window.Close();
        }

    }
}
