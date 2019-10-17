using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class ExpenseModel
    {
        /// <summary>
        /// Добавление записи в базу данных, в таблицу Expense 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="summa"></param>
        public void AddRecord(string name, float summa)
        {
            using (var context = new MyDbContext())
            {
                var expense = new Expense()
                {
                    Name = name,
                    Summa = summa,
                    Date = DateTime.Now
                };
                context.Expenses.Add(expense);
                context.SaveChanges();
            }
            MessageBox.Show("Запись успешно внесена!");
        }
    }
}
