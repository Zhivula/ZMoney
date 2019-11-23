using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiMoney.Data;

namespace ZhiMoney.ViewModel
{
    class PriceChangeUnitViewModel : INotifyPropertyChanged
    {
        private float currentSumma;
        private float weeklyPriceChange;
        private float monthlyPriceChange;
        private float oneYearPriceChange;
        private PackIconKind weekKind;
        private PackIconKind monthKind;
        private PackIconKind yearKind;

        public string Title { get; set; }

        public float CurrentSumma
        {
            get => currentSumma;
            set
            {
                currentSumma = value;
                OnPropertyChanged(nameof(CurrentSumma));
            }
        }
        public float WeeklyPriceChange
        {
            get => weeklyPriceChange;
            set
            {
                weeklyPriceChange = value;
                OnPropertyChanged(nameof(WeeklyPriceChange));
            }
        }
        public float MonthlyPriceChange
        {
            get => monthlyPriceChange;
            set
            {
                monthlyPriceChange = value;
                OnPropertyChanged(nameof(MonthlyPriceChange));
            }
        }
        public float OneYearPriceChange
        {
            get => oneYearPriceChange;
            set
            {
                oneYearPriceChange = value;
                OnPropertyChanged(nameof(OneYearPriceChange));
            }
        }

        public PackIconKind WeekKind
        {
            get => weekKind;
            set
            {
                weekKind = value;
                OnPropertyChanged(nameof(WeekKind));
            }
        }
        public PackIconKind MonthKind
        {
            get => monthKind;
            set
            {
                monthKind = value;
                OnPropertyChanged(nameof(MonthKind));
            }
        }
        public PackIconKind YearKind
        {
            get => yearKind;
            set
            {
                yearKind = value;
                OnPropertyChanged(nameof(YearKind));

            }
        }

        public PriceChangeUnitViewModel(string title, IInputDataModel model)
        {
            Title = title;

            model.AlgorthmSort(out DateTime[] date, out float[] summa, 7);
            WeeklyPriceChange = summa.Sum();
            model.AlgorthmSort(out date,out summa, 30);
            MonthlyPriceChange = summa.Sum();
            model.AlgorthmSort(out date, out summa, 360);
            OneYearPriceChange = summa.Sum();
            CurrentSumma = model.GetCurrentSumma();

            //if(model is IncomeViewModel)
            //{
            //    WeekKind = MonthKind = YearKind = PackIconKind.TrendingUp;              
            //}
            //if (model is ExpenseViewModel)
            //{
            //    WeekKind = MonthKind = YearKind = PackIconKind.TrendingDown;
            //}
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
