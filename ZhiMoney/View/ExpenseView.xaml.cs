using System.Windows.Controls;

namespace ZhiMoney.View
{
    /// <summary>
    /// Логика взаимодействия для ExpenseView.xaml
    /// </summary>
    public partial class ExpenseView : UserControl
    {
        public ExpenseView()
        {
            InitializeComponent();
            DataContext = new ViewModel.ExpenseViewModel();
        }
    }
}
