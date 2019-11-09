using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class JournalModel
    {
        public List<Income> ItemsIncomes { get; set; }
        public List<Expense> ItemsExpense { get; set; }

        public JournalModel()
        {
            ItemsIncomes = new List<Income>();
            ItemsExpense = new List<Expense>();

            ///Подключение к базе данных 
            ///Добавление в ItemsItemsIncomes данных Income
            ///Добавление в ItemsExpense данных Expense
            using (var context = new MyDbContext())
            {
                ItemsIncomes.AddRange(context.Incomes);//Оптимизированное добавление, было так => (foreach (Income i in context.Incomes) ItemsIncome.Add(i);)
                ItemsExpense.AddRange(context.Expenses);
            }
        }
    }
}
