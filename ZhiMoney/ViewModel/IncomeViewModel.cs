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
        private string summa;
        private string selecteditem;
        private Chart chartChild;
        private WindowsFormsHost windowsFormsHost;
        private InputData inputData;
        private Visibility hintVisibility;
        private string hintName;
        private PrefixTree<int> prefixTree;
        private IncomeModel incomeModel;
        private ExpenseModel expenseModel;
        


        public ObservableCollection<string> Combobox { get; set; }

        public Visibility HintVisibility
        {
            get => hintVisibility;
            set
            {
                hintVisibility = value;
                OnPropertyChanged(nameof(HintVisibility));
            }
        }

        public string HintName {
            get => hintName;
            set
            {
                hintName = value;
                OnPropertyChanged(nameof(HintName));
            }
        }

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
                 if (!string.IsNullOrWhiteSpace(value))
                 {
                     HintVisibility = Visibility.Visible;
                     
                    if (prefixTree.TrySearch(value,out int count))
                    {
                        HintName = value;
                    }
                 }
                 else
                 {
                     HintVisibility = Visibility.Hidden;
                 }
            }
        }
        public string Summa
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
            incomeModel = new IncomeModel();
            expenseModel = new ExpenseModel();
            Combobox = incomeModel.Combobox;
            SelectedItem = Combobox.First();
            HintVisibility = Visibility.Hidden;
            inputData = new InputData();
            prefixTree = new PrefixTree<int>();
            FillPrefixTree(prefixTree);
            UpDateChart();
        }
        /// <summary>
        /// Добавление записи о доходе в базу данных 
        /// </summary>
        public ICommand AddRecord => new DelegateCommand(o =>
        {
            var summa = inputData.ConvertStringToFloat(Summa);
            if (!string.IsNullOrWhiteSpace(Name) && summa > 0)
            {
                incomeModel.AddRecord(Name, summa);
                UpDateChart();
                Name = Summa = string.Empty;
            }
            else MessageBox.Show("Некорректные данные.");
        });

        public Chart FilledChart(Chart chart, int days = 30)
        {
            incomeModel.AlgorthmSort(out DateTime[] dateIncome, out float[] summaIncome, days);

            expenseModel.AlgorthmSort(out DateTime[] dateExpense, out float[] summaExpense, days);

            if (SelectedItem.Equals(Combobox[0]))
            {
                for (int i = 0; i < days; i++)
                {
                    chart.Series[0].Points.AddXY(dateIncome[i], summaIncome[i]);
                }
            }
            if (SelectedItem.Equals(Combobox[1]))
            {
                for (int i = 0; i < days; i++)
                {
                    chart.Series[1].Points.AddXY(dateExpense[i], summaExpense[i]);
                }
            }
            if (SelectedItem.Equals(Combobox[2]))
            {
                for (int i = 0; i < days; i++)
                {
                    chart.Series[2].Points.AddXY(dateIncome[i], summaIncome[i] - summaExpense[i]);
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

        private PrefixTree<int> FillPrefixTree(PrefixTree<int> prefixTree)
        {
            using (var context = new MyDbContext())
            {
                List<string> itemsIncome = context.Incomes.Select(x => x.Name).ToList();
                foreach(string item in itemsIncome)  prefixTree.Add(item,1);
                return prefixTree;
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