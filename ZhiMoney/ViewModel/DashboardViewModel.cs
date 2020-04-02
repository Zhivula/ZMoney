using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ZhiMoney.Abstraction;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class DashboardViewModel : INotifyPropertyChanged
    {
        public string MainTitle { get; set; }
        public string LeftTitle { get; set; }
        public string RightTitle { get; set; }

        public Tuple<string, float>[] ItemsSource { get; set; }

        public DashboardViewModel(Dashboard dashboard)
        {
            ItemsSource = new Tuple<string, float>[5];

            MainTitle = dashboard.mainTitle;
            LeftTitle = dashboard.leftTitle;
            RightTitle = dashboard.rightTitle;

            for (int i = 0; i < 5; i++)
            {
                ItemsSource[i] = new Tuple<string, float>(dashboard.Names[i],dashboard.Values[i]);
            }
            
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
