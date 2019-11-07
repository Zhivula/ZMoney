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
        public List<Income> Items { get; set; }
        private JournalModel journalmodel;
        public JournalViewModel()
        {
            journalmodel = new JournalModel();
            Items = journalmodel.ItemsIncomes;
        }
    }
}
