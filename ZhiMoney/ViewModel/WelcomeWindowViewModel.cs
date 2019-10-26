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
        WelcomeWindowView window;
        public DelegateCommand CloseWindow { get; set; }
        public DelegateCommand Next { get; set; }
        public DelegateCommand PathToPhotoUser { get; set; }
        public DelegateCommand MouseDown { get; set; }

        private string name;
        private string surname;
        private string pathPhoto;
        private Visibility successfullyPanel;

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
                //TODO: здесь нужна нормальная проверка, чтобы pathPhoto не было null
                pathPhoto = value;
                OnPropertyChanged(nameof(PathPhoto));
            }
        }
        public Visibility SuccessfullyPanel
        {
            get => successfullyPanel;
            set
            {
                successfullyPanel = value;
                OnPropertyChanged(nameof(SuccessfullyPanel));
            }
        }

        public WelcomeWindowViewModel()
        {
            window = Application.Current.Windows.OfType<WelcomeWindowView>().FirstOrDefault();
            SuccessfullyPanel = Visibility.Hidden;
            Next = new DelegateCommand(o => NextPage(),c=> pathPhoto != null);
            CloseWindow = new DelegateCommand(o => Close());
            PathToPhotoUser = new DelegateCommand(o => SearchPathToPhotoUser());
            MouseDown = new DelegateCommand(o => Move());
        }
        /// <summary>
        /// Переход к основному окну приложения.
        /// </summary>
        private void NextPage()
        {
            Settings.Default["SurnameUser"] = SurnameUser;
            Settings.Default["NameUser"] = NameUser;
            Settings.Default["PathPhotoUser"] = PathPhoto;

            Settings.Default.Save();

            MainWindow mainwindow = new MainWindow();

            mainwindow.Show();
            Close();
        }
        /// <summary>
        /// Открытие диалогового окна для указания пути к фото пользователя.
        /// </summary>
        private void SearchPathToPhotoUser()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = string.Empty,
                DefaultExt = ".jpg",

                Filter = "Файлы фотографий|*.jpg;*.png;*.ico"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                PathPhoto = dlg.FileName;
                SuccessfullyPanel = Visibility.Visible;
            }
            else SuccessfullyPanel = Visibility.Hidden;
        }
        private void Close()
        {
            window.Close();
        }
        private void Move()
        {
            window.DragMove();
        }

    }
}
