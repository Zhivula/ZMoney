using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiMoney.DataBase;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class JournalViewModel
    {
        public IList Items;
        private JournalModel journalmodel;
        public JournalViewModel()
        {
            journalmodel = new JournalModel();
            //foreach (Income i in journalmodel.ItemsIncomes)
        }
    }
}
