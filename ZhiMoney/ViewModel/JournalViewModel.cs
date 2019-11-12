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
        public List<IInputData> Items { get; set; }

        public JournalViewModel()
        {
            var journalmodel = new JournalModel();
            Items = journalmodel.ItemsIncomes;
            Items.AddRange(journalmodel.ItemsExpense);
            Items.Sort();
        }
    }
}
