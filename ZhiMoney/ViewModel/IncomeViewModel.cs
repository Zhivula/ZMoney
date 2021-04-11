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
using ZhiMoney.View;
using SWC = System.Windows.Controls;

namespace ZhiMoney.ViewModel
{
    class IncomeViewModel : INotifyPropertyChanged
    {
        private string name;
        private string summa;
        private string selecteditem;
        private int selecteditemDate;
        private Chart chartChild;
        private WindowsFormsHost windowsFormsHost;
        private InputData inputData;
        private Visibility hintVisibility;
        private string hintName;
        private PrefixTree prefixTree;
        private IncomeModel incomeModel;
        private ExpenseModel expenseModel;
        private PriceChangeUnitView priceChangeUnit;
        private СancellationLastInputView cancellation;
        private SWC.Grid mostFrequentIncomesDashboard;
        private SWC.Grid mostIncomesDashboard;

        public SWC.Grid MostIncomesDashboard
        {
            get => mostIncomesDashboard;
            set
            {
                mostIncomesDashboard = value;
                OnPropertyChanged(nameof(MostIncomesDashboard));
            }
        }
        public SWC.Grid MostFrequentIncomesDashboard
        {
            get => mostFrequentIncomesDashboard;
            set
            {
                mostFrequentIncomesDashboard = value;
                OnPropertyChanged(nameof(MostFrequentIncomesDashboard));
            }
        }

        public ObservableCollection<string> Combobox { get; set; }
        public ObservableCollection<int> ComboboxDate { get; set; }

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

        public PriceChangeUnitView PriceChangeUnit
        {
            get => priceChangeUnit;
            set
            {
                priceChangeUnit = value;
                OnPropertyChanged(nameof(PriceChangeUnit));
            }
        }
        public СancellationLastInputView Cancellation
        {
            get => cancellation;
            set
            {
                cancellation = value;
                OnPropertyChanged(nameof(Cancellation));
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
                     string hintName = string.Empty;                                      
                     if (prefixTree.TrySearchWord(value, ref hintName))
                     {
                         HintVisibility = Visibility.Visible;
                         HintName = hintName;
                     }
                 }
                 else
                 {
                     HintVisibility = Visibility.Hidden;
                     HintName = string.Empty;
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
                UpDateChart(SelectedItemDate);
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public int SelectedItemDate
        {
            get => selecteditemDate;
            set
            {
                selecteditemDate = value;
                UpDateChart(value);
                OnPropertyChanged(nameof(SelectedItemDate));
            }
        }

        public IncomeViewModel()
        {
            incomeModel = new IncomeModel();
            expenseModel = new ExpenseModel();

            inputData = new InputData();

            prefixTree = new PrefixTree();

            MostIncomesDashboard = new SWC.Grid();
            MostFrequentIncomesDashboard = new SWC.Grid();

            Combobox = incomeModel.Combobox;
            ComboboxDate = incomeModel.ComboboxDate;

            SelectedItem = Combobox.First();
            SelectedItemDate = ComboboxDate.First();

            HintVisibility = Visibility.Hidden;

            incomeModel.FillPrefixTree(ref prefixTree);

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
                 prefixTree.Add(Name);
                 Name = Summa = string.Empty;                               
            }
            else MessageBox.Show("Некорректные данные.");
        });
        /// <summary>
        /// Заполняет график данными.
        /// </summary>
        /// <param name="days">Количество дней (необязательный параметр)</param>
        /// <returns></returns>
        public Chart FilledChart (int days = 30)
        {
            var chart = new MyChart().Chart;

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
        /// <summary>
        /// Обновление графика.
        /// Обычно происходит при внесении пользователем новых данных, или при изменении SelectedItem в Combobox
        /// </summary>
        public void UpDateChart(int days = 30)
        {
            //TO DO: наверное, лучше сделать этот метод async
            ChartChild = FilledChart(days);
            WinFormsHost = new WindowsFormsHost
            {
                Child = ChartChild
            };
            PriceChangeUnit = new PriceChangeUnitView("Свободный бюджет", incomeModel);
            Cancellation = new СancellationLastInputView(new Income());
            MostIncomesDashboard.Children.Clear();
            MostFrequentIncomesDashboard.Children.Clear();
            MostIncomesDashboard.Children.Add(new DashboardView(new MostIncomesDashboardModel()));
            MostFrequentIncomesDashboard.Children.Add(new DashboardView(new MostFrequentIncomesDashboardModel()));
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