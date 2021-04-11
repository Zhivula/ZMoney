using System.Windows;
using System.Windows.Input;
using ZhiMoney.Data;
using ZhiMoney.ViewModel;

namespace ZhiMoney.View
{
    /// <summary>
    /// Логика взаимодействия для FullListView.xaml
    /// </summary>
    public partial class FullListView : Window
    {
        public FullListView(Dashboard dashboard)
        {
            InitializeComponent();
            DataContext = new FullListViewModel(dashboard);
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
