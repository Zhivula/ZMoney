﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ZhiMoney.Data;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class PriceChangeUnitViewModel : INotifyPropertyChanged
    {
        private float currentSumma;
        private float weeklyPriceChange;
        private float monthlyPriceChange;
        private float oneYearPriceChange;
        private PackIcon weekPackIcon;
        private PackIcon monthPackIcon;
        private PackIcon yearPackIcon;

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

        public PackIcon WeekPackIcon
        {
            get => weekPackIcon;
            set
            {
                weekPackIcon = value;
                OnPropertyChanged(nameof(WeekPackIcon));
            }
        }
        public PackIcon MonthPackIcon
        {
            get => monthPackIcon;
            set
            {
                monthPackIcon = value;
                OnPropertyChanged(nameof(MonthPackIcon));
            }
        }
        public PackIcon YearPackIcon
        {
            get => yearPackIcon;
            set
            {
                yearPackIcon = value;
                OnPropertyChanged(nameof(YearPackIcon));

            }
        }

        public PriceChangeUnitViewModel(string title, IInputDataModel model)
        {
            Title = title;

            WeekPackIcon = new PackIcon();
            MonthPackIcon = new PackIcon();
            YearPackIcon = new PackIcon();

            model.AlgorthmSort(out DateTime[] date, out float[] summa, 7);
            WeeklyPriceChange = summa.Sum();
            model.AlgorthmSort(out date,out summa, 30);
            MonthlyPriceChange = summa.Sum();
            model.AlgorthmSort(out date, out summa, 360);
            OneYearPriceChange = summa.Sum();
            CurrentSumma = model.GetCurrentSumma();

            if (model is IncomeModel)
            {
                WeekPackIcon.Kind = MonthPackIcon.Kind = YearPackIcon.Kind = PackIconKind.TrendingUp;
                WeekPackIcon.Foreground = MonthPackIcon.Foreground = YearPackIcon.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            if (model is ExpenseModel)
            {
                WeekPackIcon.Kind = MonthPackIcon.Kind = YearPackIcon.Kind = PackIconKind.TrendingDown;
                WeekPackIcon.Foreground = MonthPackIcon.Foreground = YearPackIcon.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
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
