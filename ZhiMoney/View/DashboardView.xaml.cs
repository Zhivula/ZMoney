using System.Windows.Controls;
using ZhiMoney.Data;

namespace ZhiMoney.View
{
    /// <summary>
    /// Логика взаимодействия для DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView(Dashboard dashboard)
        {
            InitializeComponent();
            DataContext = new ViewModel.DashboardViewModel(dashboard);
        }
    }
}
