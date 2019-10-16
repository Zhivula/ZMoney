using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class IncomeModel
    {
        /// <summary>
        /// Добавление записи в базу данных, в таблицу Income 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="summa"></param>
        public void AddRecord(string name, float summa)
        {
            using (var context = new MyDbContext())
            {
                var income = new Income()
                {
                    Name = name,
                    Summa = summa,
                    Date = DateTime.Now
                };
                context.Incomes.Add(income);
                context.SaveChanges();
            }
            MessageBox.Show("Запись успешно внесена!");
        }
    }
}
