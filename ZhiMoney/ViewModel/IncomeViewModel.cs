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
using System.Windows.Input;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class IncomeViewModel : INotifyPropertyChanged
    {
        private string name;
        private float summa;
        private bool canNameRecord;
        private bool canSummaRecord;

        public string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                    canNameRecord = true;
                }
                else
                {
                    canNameRecord = false;
                }
                OnPropertyChanged(nameof(Name));
            }
        }
        public float Summa
        {
            get => summa;
            set
            {
                incomemodel.ParseSummaToSingle(value.ToString(),out summa,out canSummaRecord);
                OnPropertyChanged(nameof(Summa));
            }
        }
        public Chart WinhostChild { get; set; }
        IncomeModel incomemodel;
        public string SelectedItem { get; set; }
        public ObservableCollection<string> Combobox { get; set; }
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
            if (canNameRecord && canSummaRecord) incomemodel.AddRecord(Name, Summa);
            else MessageBox.Show("Некорректные данные.");
        });
        public Chart Chart(Chart chart)
        {
            int days = 90;
            int[] a = new int[days];
            string[] b = new string[days];
            for (int i = 0; i < days; i++) { a[i] = i; b[i] = DateTime.Now.AddDays(-days + 2 + i).ToShortDateString(); }
            for(int i=0;  i < days;i++) if (SelectedItem.Equals(Combobox.FirstOrDefault())) chart.Series[2].Points.AddXY(b[i], a[i]);
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