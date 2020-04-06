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
    class СancellationLastInputIncomesModel : ICancellation
    {
        public СancellationLastInputIncomesModel()
        {

        }
        public void DeleteLastElement()
        {
            using (var context = new MyDbContext())
            {
                var last = context.Incomes.OrderByDescending(x => x.Id).First();
                context.Incomes.Remove(last);
                context.SaveChanges();
            }
            MessageBox.Show("Последний элемент был удален!");
        }
        public IInputData GetLastElement()
        {
            using (var context = new MyDbContext())
            {
                if (context.Incomes.Count() != 0) return context.Incomes.OrderByDescending(x => x.Id).First();
                else return null;
            }
        }
    }
}
