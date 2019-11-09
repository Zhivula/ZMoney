using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZhiMoney.DataBase;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class JournalViewModel
    {
        public List<Income> ItemsIncome { get; set; }
        public List<Expense> ItemseExpense { get; set; }

        public JournalViewModel()
        {
            var journalmodel = new JournalModel();
            ItemsIncome = journalmodel.ItemsIncomes;
            ItemseExpense = journalmodel.ItemsExpense;
        }
    }
}
