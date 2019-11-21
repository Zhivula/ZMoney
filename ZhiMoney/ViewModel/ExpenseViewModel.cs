using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using ZhiMoney.Data;
using ZhiMoney.DataBase;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class ExpenseViewModel: INotifyPropertyChanged
    {
        private string name;
        private string summa;
        private string selecteditem;
        private Chart chartChild;
        private WindowsFormsHost windowsFormsHost;
        private InputData inputData;
        private Visibility hintVisibility;
        private string hintName;
        private PrefixTree prefixTree;
        IncomeModel incomeModel;
        ExpenseModel expenseModel;

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

        public Visibility HintVisibility
        {
            get => hintVisibility;
            set
            {
                hintVisibility = value;
                OnPropertyChanged(nameof(HintVisibility));
            }
        }

        public string HintName
        {
            get => hintName;
            set
            {
                hintName = value;
                OnPropertyChanged(nameof(HintName));
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
                UpDateChart();
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ExpenseViewModel()
        {
            incomeModel = new IncomeModel();
            expenseModel = new ExpenseModel();

            inputData = new InputData();

            prefixTree = new PrefixTree();

            Combobox = expenseModel.Combobox;

            SelectedItem = Combobox[1];

            HintVisibility = Visibility.Hidden;

            expenseModel.FillPrefixTree(ref prefixTree);

            UpDateChart();
        }
        /// <summary>
        /// Добавление записи о расходе в базу данных.
        /// </summary>
        public ICommand AddRecord => new DelegateCommand(o =>
        {
            var summa = inputData.ConvertStringToFloat(Summa);
            if (!string.IsNullOrWhiteSpace(Name) && summa > 0)
            {
                expenseModel.AddRecord(Name, summa);

                UpDateChart();
                prefixTree.Add(Name);
                Name = Summa = string.Empty;
            }
            else MessageBox.Show("Некорректные данные.");
        });
        /// <summary>
        /// Заполняет график данными в зависимости от выбранного элемента ComboBox.
        /// индекс [0] - график дохода
        /// индекс [1] - график расхода
        /// индекс [2] - график общий
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="days">количество дней, которое будет отображаться на графике</param>
        /// <returns>Заполненный график</returns>
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
        /// <summary>
        /// Обновляет график.
        /// </summary>
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
