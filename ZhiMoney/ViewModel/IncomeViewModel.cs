using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using ZhiMoney.DataBase;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class IncomeViewModel : INotifyPropertyChanged
    {
        private string name;
        private float summa;
        private string selecteditem;
        IncomeModel incomemodel;

        public ObservableCollection<string> Combobox { get; set; }

        public Chart WinhostChild { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public float Summa
        {
            get => summa;
            set
            {
                summa = value;
                OnPropertyChanged(nameof(Summa));
            }
        }
        public WindowsFormsHost Winhost
        {
            get => new WindowsFormsHost
            {
                Child = WinhostChild
            };
        }

        public string SelectedItem
        {
            get => selecteditem;
            set
            {
                selecteditem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public IncomeViewModel()
        {
            incomemodel = new IncomeModel();
            var chart = new Data.MyChart();
            Combobox = incomemodel.Combobox;
            SelectedItem = Combobox.FirstOrDefault();
            WinhostChild = Chart(chart.Chart);
        }
        /// <summary>
        /// Добавление записи о доходе в базу данных 
        /// </summary>
        public ICommand AddRecord => new DelegateCommand(o =>
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Summa.ToString())) incomemodel.AddRecord(Name, Summa);
            else MessageBox.Show("Некорректные данные.");
        });

        public Chart Chart(Chart chart,int days = 30)
        {
            var Date = incomemodel.GetDateTimes();
            var Summa = incomemodel.GetSumma();
            DateTime[] date = new DateTime[days];
            var summa = new float[days];

            for (int i = 0; i < days; i++)
            {
                date[i] = DateTime.Now.AddDays(i - days + 1);
            }

            for (int i = 0; i < days; i++)
            {
                summa[i] = 5;
                for (int j = 0; j < Date.Count(); j++)
                {
                    if (date[i].DayOfYear == Date[j].DayOfYear)
                    {
                        summa[i] += Summa[j];
                    }
                }
            }

            for(int i = 0;  i < days;i++) if (SelectedItem.Equals(Combobox.FirstOrDefault())) chart.Series[2].Points.AddXY(date[i], summa[i]);
            return chart;
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