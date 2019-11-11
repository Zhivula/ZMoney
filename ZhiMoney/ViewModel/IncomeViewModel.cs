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
using ZhiMoney.Data;
using ZhiMoney.DataBase;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class IncomeViewModel : INotifyPropertyChanged
    {
        private string name;
        private float summa;
        private string selecteditem;
        private Chart chartChild;
        private WindowsFormsHost windowsFormsHost;
        IncomeModel incomemodel;
        


        public ObservableCollection<string> Combobox { get; set; }

        public Chart ChartChild
        {
            get => chartChild;
            set
            {
                chartChild = value;
                OnPropertyChanged(nameof(ChartChild));
            }
        }

        public WindowsFormsHost WinFormsHost
        {
            get => windowsFormsHost;
            set
            {
                windowsFormsHost = value;
                OnPropertyChanged(nameof(WinFormsHost));
            }
        }

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

        public string SelectedItem
        {
            get => selecteditem;
            set
            {
                selecteditem = value;
                UpDateChart();
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public IncomeViewModel()
        {
            incomemodel = new IncomeModel();
            var chart = new MyChart();
            Combobox = incomemodel.Combobox;
            SelectedItem = Combobox.First();
            ChartChild = FilledChart(chart.Chart);
            WinFormsHost = new WindowsFormsHost
            {
                Child = ChartChild
            };
        }
        /// <summary>
        /// Добавление записи о доходе в базу данных 
        /// </summary>
        public ICommand AddRecord => new DelegateCommand(o =>
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Summa.ToString()))
            {
                incomemodel.AddRecord(Name, Summa);
                UpDateChart();
            }
            else MessageBox.Show("Некорректные данные.");
        });

        public Chart FilledChart(Chart chart, int days = 30)
        {
            incomemodel.AlgorthmSort(out DateTime[] date, out float[] summa, days);

            if (SelectedItem.Equals(Combobox[0]))
            {
                for (int i = 0; i < days; i++)
                {
                    chart.Series[0].Points.AddXY(date[i], summa[i]);
                }
            }
            if (SelectedItem.Equals(Combobox[1]))
            {
                for (int i = 0; i < days; i++)
                {
                    chart.Series[1].Points.AddXY(date[i], summa[i]);
                }
            }
            return chart;
        }
        public void UpDateChart()
        {
            ChartChild = FilledChart(new MyChart().Chart);
            WinFormsHost = new WindowsFormsHost
            {
                Child = ChartChild
            };
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