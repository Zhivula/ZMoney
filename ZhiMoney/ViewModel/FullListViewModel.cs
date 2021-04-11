using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZhiMoney.Data;
using ZhiMoney.Model;
using ZhiMoney.View;

namespace ZhiMoney.ViewModel
{
    class FullListViewModel : INotifyPropertyChanged
    {
        FullListView window;
        public DelegateCommand CloseWindow { get; set; }
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
        public FullListViewModel(Dashboard dashboard)
        {
            MainTitle = dashboard.mainTitle;
            LeftTitle = dashboard.leftTitle;
            RightTitle = dashboard.rightTitle;
            window = Application.Current.Windows.OfType<FullListView>().FirstOrDefault();

            if (dashboard.Names.Count() >= 1)
            {
                ItemsSource = new Tuple<string, string>[dashboard.Names.Count()];
                for (int i = 0; i < dashboard.Names.Count(); i++)
                {
                    ItemsSource[i] = new Tuple<string, string>(dashboard.Names[i], dashboard.Values[i].ToString("##.##"));
                }
            }

            CloseWindow = new DelegateCommand(o => window.Close());
        }
        public ICommand Move => new DelegateCommand(o =>
        {
            window.DragMove();
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
