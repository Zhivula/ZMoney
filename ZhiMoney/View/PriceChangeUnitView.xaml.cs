using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZhiMoney.Data;

namespace ZhiMoney.View
{
    /// <summary>
    /// Логика взаимодействия для PriceChangeUnitView.xaml
    /// </summary>
    public partial class PriceChangeUnitView : UserControl
    {
        public PriceChangeUnitView(string title, IInputDataModel model)
        {
            InitializeComponent();
            DataContext = new ViewModel.PriceChangeUnitViewModel(title, model);
        }
    }
}
