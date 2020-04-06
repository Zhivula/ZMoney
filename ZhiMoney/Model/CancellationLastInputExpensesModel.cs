using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZhiMoney.Data;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class CancellationLastInputExpensesModel : ICancellation
    {
        public CancellationLastInputExpensesModel()
        {

        }
        public void DeleteLastElement()
        {
            using (var context = new MyDbContext())
            {
                var last = context.Expenses.OrderByDescending(x => x.Id).First();
                context.Expenses.Remove(last);
                context.SaveChanges();
            }
            MessageBox.Show("Последний элемент был удален!");
        }
        public IInputData GetLastElement()
        {
            using (var context = new MyDbContext())
            {
                if (context.Expenses.Count() != 0) return context.Expenses.OrderByDescending(x => x.Id).First();
                else return null;
            }
        }
    }
}
