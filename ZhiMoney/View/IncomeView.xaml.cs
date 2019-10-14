using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ZhiMoney.View
{
    /// <summary>
    /// Логика взаимодействия для IncomeView.xaml
    /// </summary>
    public partial class IncomeView : UserControl
    {
        public IncomeView()
        {
            InitializeComponent();
            DataContext = new ViewModel.IncomeViewModel();
        }
    }
}
